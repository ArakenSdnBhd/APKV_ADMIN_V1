Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Data.Common
Public Class modul_import_create
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("class") = "info"
                '--display rekod if success
                ' strRet = BindData(datRespondent)
            Else
                divMsg.Attributes("class") = "error"
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsgTop.Text = "Rekod belum disahkan. Sila sahkan dengan menekan button [Sahkan] dibawah."
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY NamaModul ASC"

        tmpSQL = "SELECT * FROM kpmkv_modul"
        strWhere = "  WHERE IsDeleted='N'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function
    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/inbox/"))
        Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
        FlUploadcsv.PostedFile.SaveAs(fullFileName)

        Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")

        Dim connExcel As New OleDbConnection(excelConnectionString)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        'Read Data from First Sheet
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        'Bind Data to GridView
        datRespondent.Caption = fullFileName
        datRespondent.DataSource = dt
        datRespondent.DataBind()

        '    If FlUploadcsv.HasFile Then
        '        Dim rand As Random = New Random()
        '        Dim randNum = rand.Next(1000)
        '        Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
        '        FlUploadcsv.PostedFile.SaveAs(fullFileName)

        '        '--required ms access engine
        '        Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
        '        'Dim excelConnectionString As String = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fullFileName & ";Extended Properties=Excel 8.0;HDR=YES;")
        '        'Response.Write("excelConnectionString:" & excelConnectionString)

        '        Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
        '        Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [modul$]", connection)
        '        Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
        '        Dim ds As DataSet = New DataSet

        '        Try
        '            connection.Open()
        '            da.Fill(ds)
        '            connection.Close()

        '            'Bind Data to GridView
        '            'datRespondent.Caption = path.GetFileName(FilePath)
        '            datRespondent.DataSource = ds
        '            datRespondent.DataBind()
        '            command.Dispose()
        '        Catch ex As Exception
        '            lblMsg.Text = "System Error:" & ex.Message
        '            Return False
        '        Finally
        '            If connection.State = ConnectionState.Open Then
        '                connection.Close()
        '            End If
        '        End Try

        '        'Dim validationMessage As String = ValidateSiteData(ds)
        '        'If validationMessage = "" Then
        '        '    'Process file
        '        '    Dim importResult As String = ProcessSiteImportFile(ds)
        '        '    If importResult = "" Then
        '        '        lblMsgTop.Text = "Fail BERJAYA di muatnaik!! " ' & fullFileName
        '        '        'Delete Excel file - this is where you'd do it
        '        '    Else
        '        '        lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
        '        '        lblMsg.Text = "System Error:" & importResult
        '        '        Return False
        '        '    End If
        '        'Else
        '        '    lblMsg.Text = "Validation Error:<br />" & validationMessage
        '        '    Return False
        '        'End If
        '    Else
        '        divMsg.Attributes("class") = "error"
        '        lblMsg.Text = "Please select file to upload!"
        '        Return False
        '    End If

        '    Return True

        'End Function

        'Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        '    Dim strNama As String = ""
        '    Dim strKodModul As String = ""

        '    Try
        '        'Loop through DataSet and validate data
        '        'If data is bad, bail out, otherwise continue on with the bulk copy
        '        Dim sb As StringBuilder = New StringBuilder()
        '        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")
        '            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("NamaModul")) Then
        '                strNama = SiteData.Tables(0).Rows(i).Item("NamaModul")
        '            Else
        '                strNama = ""
        '            End If
        '        Next

        '        Return sb.ToString()
        '    Catch ex As Exception
        '        Return ex.Message
        '    End Try

    End Function

    Protected Function ProcessSiteImportFile(ByVal SiteData As DataSet) As String
        Dim sqlConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connection As SqlConnection = New SqlConnection(sqlConnectionString)


        Dim command As SqlCommand = New SqlCommand("INSERT INTO kpmkv_modul (Tahun,Semester,Sesi,NamaKluster,KodKursus,NamaKursus,KodModul,NamaModul,JamKredit,IsDeleted) " _
            & "VALUES (@Tahun,@Semester,@Sesi,@NamaKluster,@KodKursus,@NamaKursus,@KodModul,@NamaModul,@JamKredit,@IsDeleted)", connection)

        Dim strRecordID As String = ""
        Dim newSiteID As String = ""
        Try
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - 1
                connection.Open()
                strRecordID = oCommon.getGUID
                command.Parameters.AddWithValue("@Tahun", Left(SiteData.Tables(0).Rows(i).Item("Tahun").ToString(), 50))
                command.Parameters.AddWithValue("@Semester", Left(SiteData.Tables(0).Rows(i).Item("Semester").ToString(), 50))
                command.Parameters.AddWithValue("@Sesi", Left(SiteData.Tables(0).Rows(i).Item("Sesi").ToString(), 50))
                command.Parameters.AddWithValue("@NamaKluster", Left(SiteData.Tables(0).Rows(i).Item("NamaKluster").ToString(), 255))
                command.Parameters.AddWithValue("@KodKursus", Left(SiteData.Tables(0).Rows(i).Item("KodKursus").ToString(), 50))
                command.Parameters.AddWithValue("@NamaKursus", Left(SiteData.Tables(0).Rows(i).Item("NamaKursus").ToString(), 255))
                command.Parameters.AddWithValue("@KodModul", Left(SiteData.Tables(0).Rows(i).Item("KodModul").ToString(), 50))
                command.Parameters.AddWithValue("@NamaModul", Left(SiteData.Tables(0).Rows(i).Item("NamaModul").ToString(), 255))
                command.Parameters.AddWithValue("@JamKredit", Left(SiteData.Tables(0).Rows(i).Item("JamKredit").ToString(), 1))
                command.Parameters.AddWithValue("@IsDeleted", Left(SiteData.Tables(0).Rows(i).Item("IsDeleted").ToString(), 1))

                newSiteID = command.ExecuteScalar()
                command.Parameters.Clear()
                connection.Close()
            Next

            command.Dispose()
            Return ""
        Catch ex As Exception
            Return ex.Message + "<br />"    '+ ex.StackTrace
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try

    End Function

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        lblMsgTop.Text = ""


        '--refresh screen
        strRet = BindData(datRespondent)



    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Redirect("modul.update.aspx?ModulID=" & strKeyID)

    End Sub

    Public Function FileIsLocked(ByVal strFullFileName As String) As Boolean
        Dim blnReturn As Boolean = False
        Dim fs As System.IO.FileStream

        Try
            fs = System.IO.File.Open(strFullFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.Read, IO.FileShare.None)
            fs.Close()
        Catch ex As System.IO.IOException
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Error Message FileIsLocked:" & ex.Message
            blnReturn = True
        End Try

        Return blnReturn
    End Function

    Private Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        lblMsgTop.Text = ""
        lblMsg.Text = ""

        '--approved selected record
        kpmkv_pensyarah_IsApproved()

        '--transfer bulk from kpmkv_pensyarah ke kpmkv_pensyarah_kolej
        kpmkv_pensyarah_kolej_insert()

        '--ALL OK. refresh screen
        strRet = BindData(datRespondent)

        If lblMsg.Text.Length = 0 Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengesahkan kemasukan kemasukan data."
        End If

    End Sub

    Private Sub kpmkv_pensyarah_IsApproved()

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    strSQL = "UPDATE kpmkv_modul SET IsDeleted='Y' WHERE ModulID='" & strID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If
                End If
            End If
        Next

    End Sub
    Private Sub kpmkv_pensyarah_kolej_insert()

    End Sub
    Protected Sub lnkList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkList.Click
        Response.Redirect("modul.list.aspx")

    End Sub
End Class