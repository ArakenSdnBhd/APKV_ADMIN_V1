Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Data.Common

Partial Public Class markah_import
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnCancel.Attributes.Add("onclick", "return confirm('Pasti ingin membatalkan kemasukan data tersebut? Kolej.Tahun.Semester.IsApproved=N');")

        Try
            If Not IsPostBack Then
                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year    '--default current year

                kpmkv_semester_list()

                lblKolejRecordID.Text = Request.QueryString("RecordID")
            End If

        Catch ex As Exception
            lblMsg.Text = "Error Message:" & ex.Message
        End Try

    End Sub

    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun WITH (NOLOCK) ORDER BY Tahun"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

        Catch ex As Exception
            lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester WITH (NOLOCK) ORDER BY Semester"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester.DataSource = ds
            ddlSemester.DataTextField = "Semester"
            ddlSemester.DataValueField = "Semester"
            ddlSemester.DataBind()

        Catch ex As Exception
            lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        '--upload excel
        lblUploadCode.Text = oCommon.getRandom
        If ImportExcel() = True Then
            divMsg.Attributes("class") = "info"
            '--display rekod if success
            strRet = BindData(datRespondent)
        Else
            divMsg.Attributes("class") = "error"
        End If

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
            lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
            lblMsg.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Nama ASC"

        '-approve and not deleted
        tmpSQL = "SELECT * FROM kpmkv_pelajar a,kpmkv_markah b"
        strWhere = " WHERE a.RecordID=b.PelajarRecordID"
        strWhere += " AND a.KolejRecordID='" & lblKolejRecordID.Text & "'"
        strWhere += " AND b.KolejRecordID='" & lblKolejRecordID.Text & "' AND b.Tahun='" & ddlTahun.Text & "' AND b.Semester='" & ddlSemester.Text & "' AND b.IsApproved='N'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/inbox/"))

        If FlUploadcsv.HasFile Then
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            'Dim excelConnectionString As String = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fullFileName & ";Extended Properties=Excel 8.0;HDR=YES;")

            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [SEMESTER$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet
            Try
                connection.Open()
                da.Fill(ds)
                connection.Close()
                command.Dispose()
            Catch ex As Exception
                lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                lblMsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

            Dim validationMessage As String = ValidateSiteData(ds)
            If validationMessage = "" Then
                'Process file
                Dim importResult As String = ProcessSiteImportFile(ds)
                If importResult = "" Then
                    lblMsgTop.Text = "Fail BERJAYA di muatnaik!! " '& fullFileName
                    'Delete Excel file - this is where you'd do it
                Else
                    lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    lblMsg.Text = "System Error:" & importResult
                    Return False
                End If
            Else
                lblMsg.Text = "Validation Error:<br />" & validationMessage
                Return False
            End If
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Sila pilih fail untuk dimuatnaik!"
            lblMsgTop.Text = lblMsg.Text
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Dim strNama As String = ""
        Dim strMYKAD As String = ""

        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - 1
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Nama")) Then
                    strNama = SiteData.Tables(0).Rows(i).Item("Nama")
                Else
                    strNama = ""
                End If

                '--MYKAD is required!
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MYKAD")) Then
                    strMYKAD = SiteData.Tables(0).Rows(i).Item("MYKAD")
                Else
                    sb.Append("Nama:" & strNama & ". MYKAD# tiada!<br />")
                End If

                strSQL = "SELECT KolejRecordID FROM kpmkv_pelajar WHERE MYKAD='" & strMYKAD & "' AND KolejRecordID='" & lblKolejRecordID.Text & "'"
                If oCommon.isExist(strSQL) = False Then
                    sb.Append(strMYKAD & ": MYKAD# tidak didaftarkan di Kolej tersebut.<br />")
                End If
            Next

            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Protected Function ProcessSiteImportFile(ByVal SiteData As DataSet) As String
        Dim sqlConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connection As SqlConnection = New SqlConnection(sqlConnectionString)

        Dim command As SqlCommand = New SqlCommand("INSERT INTO kpmkv_markah (PelajarRecordID,KolejRecordID,Tahun,Semester,M1_TEORI,M2_TEORI,M3_TEORI,M4_TEORI,M5_TEORI,M6_TEORI,M7_TEORI,M8_TEORI,M1_AMALI,M2_AMALI,M3_AMALI,M4_AMALI,M5_AMALI,M6_AMALI,M7_AMALI,M8_AMALI,UploadCode) " _
            & "VALUES (@PelajarRecordID,@KolejRecordID,@Tahun,@Semester,@M1_TEORI,@M2_TEORI,@M3_TEORI,@M4_TEORI,@M5_TEORI,@M6_TEORI,@M7_TEORI,@M8_TEORI,@M1_AMALI,@M2_AMALI,@M3_AMALI,@M4_AMALI,@M5_AMALI,@M6_AMALI,@M7_AMALI,@M8_AMALI,@UploadCode)", connection)

        Dim strMYKAD As String = ""
        Dim strPelajarRecordID As String = ""
        Dim newSiteID As String = ""
        Try
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - 1
                connection.Open()
                strMYKAD = Left(SiteData.Tables(0).Rows(i).Item("MYKAD").ToString(), 50)
                strPelajarRecordID = getPelajarRecordID(strMYKAD)

                command.Parameters.AddWithValue("@PelajarRecordID", strPelajarRecordID)
                command.Parameters.AddWithValue("@KolejRecordID", lblKolejRecordID.Text)
                command.Parameters.AddWithValue("@Tahun", ddlTahun.Text)
                command.Parameters.AddWithValue("@Semester", ddlSemester.Text)

                command.Parameters.AddWithValue("@M1_TEORI", Left(SiteData.Tables(0).Rows(i).Item("M1_TEORI").ToString(), 5))
                command.Parameters.AddWithValue("@M2_TEORI", Left(SiteData.Tables(0).Rows(i).Item("M2_TEORI").ToString(), 5))
                command.Parameters.AddWithValue("@M3_TEORI", Left(SiteData.Tables(0).Rows(i).Item("M3_TEORI").ToString(), 5))
                command.Parameters.AddWithValue("@M4_TEORI", Left(SiteData.Tables(0).Rows(i).Item("M4_TEORI").ToString(), 5))
                command.Parameters.AddWithValue("@M5_TEORI", Left(SiteData.Tables(0).Rows(i).Item("M5_TEORI").ToString(), 5))
                command.Parameters.AddWithValue("@M6_TEORI", Left(SiteData.Tables(0).Rows(i).Item("M6_TEORI").ToString(), 5))
                command.Parameters.AddWithValue("@M7_TEORI", Left(SiteData.Tables(0).Rows(i).Item("M7_TEORI").ToString(), 5))
                command.Parameters.AddWithValue("@M8_TEORI", Left(SiteData.Tables(0).Rows(i).Item("M8_TEORI").ToString(), 5))
                command.Parameters.AddWithValue("@M1_AMALI", Left(SiteData.Tables(0).Rows(i).Item("M1_AMALI").ToString(), 5))
                command.Parameters.AddWithValue("@M2_AMALI", Left(SiteData.Tables(0).Rows(i).Item("M2_AMALI").ToString(), 5))
                command.Parameters.AddWithValue("@M3_AMALI", Left(SiteData.Tables(0).Rows(i).Item("M3_AMALI").ToString(), 5))
                command.Parameters.AddWithValue("@M4_AMALI", Left(SiteData.Tables(0).Rows(i).Item("M4_AMALI").ToString(), 5))
                command.Parameters.AddWithValue("@M5_AMALI", Left(SiteData.Tables(0).Rows(i).Item("M5_AMALI").ToString(), 5))
                command.Parameters.AddWithValue("@M6_AMALI", Left(SiteData.Tables(0).Rows(i).Item("M6_AMALI").ToString(), 5))
                command.Parameters.AddWithValue("@M7_AMALI", Left(SiteData.Tables(0).Rows(i).Item("M7_AMALI").ToString(), 5))
                command.Parameters.AddWithValue("@M8_AMALI", Left(SiteData.Tables(0).Rows(i).Item("M8_AMALI").ToString(), 5))

                command.Parameters.AddWithValue("@UploadCode", lblUploadCode.Text)

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

    Private Function getPelajarRecordID(ByVal strMYKAD As String) As String
        Dim strValue As String = ""
        strSQL = "SELECT RecordID FROM kpmkv_pelajar WHERE MYKAD='" & strMYKAD & "'"
        strValue = oCommon.getFieldValue(strSQL)
        Return strValue

    End Function


    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        lblMsgTop.Text = ""

        strSQL = "DELETE FROM kpmkv_markah WHERE KolejRecordID='" & lblKolejRecordID.Text & "' AND IsApproved='N' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsgTop.Text = "Pembatalan Gagal. Lihat mesej dibawah."
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & strRet
        Else
            '--refresh screen
            strRet = BindData(datRespondent)

            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya memansuhkan kemasukan data."
        End If

    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Dim strPelajarRecordID As String = ""
        strSQL = "SELECT PelajarRecordID FROM kpmkv_markah WHERE MarkahID=" & strKeyID
        strPelajarRecordID = oCommon.getFieldValue(strSQL)

        Response.Redirect("admin.markah.update.aspx?RecordID=" & strPelajarRecordID & "&MarkahID=" & strKeyID)

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

        strSQL = "UPDATE kpmkv_markah SET IsApproved='Y' WHERE KolejRecordID='" & lblKolejRecordID.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & strRet
        Else
            '--refresh screen
            strRet = BindData(datRespondent)

            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengesahkan kemasukan kemasukan data."
        End If

    End Sub

    Protected Sub lnkList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.pelajar.markah.list.aspx?RecordID=" & Request.QueryString("RecordID"))

    End Sub
End Class