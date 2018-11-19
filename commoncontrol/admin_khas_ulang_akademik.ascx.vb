Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class admin_khas_ulang_akademik
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strBil As String = ""
    Dim IntPelajarID As Integer = 0
    Dim strKolejRecordID As String = ""
    Dim strNama As String = ""
    Dim strMykad As String = ""
    Dim strAngkaGiliran As String = ""
    Dim strTahun As String = ""
    Dim strSesi As String = ""
    Dim strKodKursus As String = ""
    Dim strMataPelajaran As String = ""
    Dim strIsAKATahun As String = ""
    Dim strIsAKASesi As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblMsg.Text = ""

            kpmkv_tahun_list()
            ddlTahun.Text = Now.Year

            kpmkv_kod_list()
            'ddlKodMataPelajaran.Text = "0"

            btnFile.Enabled = True
            btnUpload.Enabled = True
        End If
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun ORDER BY TahunID"
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

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kod_list()
        strSQL = "SELECT Distinct NamaMataPelajaran FROM kpmkv_matapelajaran ORDER BY NamaMataPelajaran"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlAkademik.DataSource = ds
            ddlAkademik.DataTextField = "NamaMataPelajaran"
            ddlAkademik.DataValueField = "NamaMataPelajaran"
            ddlAkademik.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
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
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY ImportDated Desc"

        tmpSQL = "SELECT * FROM kpmkv_pelajar_Akademik_Ulang"
        strWhere = " WHERE IsDeleted='N' AND IsAkaTahun ='" & ddlTahun.Text & "' AND IsAkaSesi ='" & chkSesi.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ' Response.Write(getSQL)

        Return getSQL

    End Function
    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function
    Private Sub btnFile_Click(sender As Object, e As EventArgs) Handles btnFile.Click
        lblMsg.Text = ""
        Response.ContentType = "Application/xlsx"
        Response.AppendHeader("Content-Disposition", "attachment; filename=PELAJARUlang.xlsx")
        Response.TransmitFile(Server.MapPath("~/sample_data/PELAJARUlang.xlsx"))
        Response.End()

    End Sub
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=markah.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub
    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function
    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblMsg.Text = ""
        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("class") = "info"
            Else
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try

    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/inbox/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [ulang$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then
                    SaveSiteData(ds)

                Else
                    'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kesalahan Kemasukkan Maklumat Calon:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblMsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function
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

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")

                refreshVar()
                strMsg = ""

                'bil
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("BIL")) Then
                    strBil = SiteData.Tables(0).Rows(i).Item("BIL")
                End If

                'PelajarID
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("PelajarID")) Then
                    IntPelajarID = SiteData.Tables(0).Rows(i).Item("PelajarID")
                Else
                    strMsg += "Sila isi PelajarID|"
                End If

                'KolejID
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KolejRecordID")) Then
                    strKolejRecordID = SiteData.Tables(0).Rows(i).Item("KolejRecordID")
                Else
                    strMsg += "Sila isi KolejRecordID|"
                End If

                'Nama
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Nama")) Then
                    strNama = SiteData.Tables(0).Rows(i).Item("Nama")
                Else
                    strMsg += "Sila isi Nama|"
                End If

                'Angka
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("AngkaGiliran")) Then
                    strAngkaGiliran = SiteData.Tables(0).Rows(i).Item("AngkaGiliran")
                Else
                    strMsg += "Sila isi AngkaGiliran|"
                End If

                'mykad
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mykad")) Then
                    strMykad = SiteData.Tables(0).Rows(i).Item("MYKAD")
                Else
                    strMsg += "Sila isi Mykad|"
                End If

                '--MYKAD is required!
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mykad")) Then
                    strMykad = SiteData.Tables(0).Rows(i).Item("Mykad")
                Else
                    strMsg += "Sila isi Mykad|"
                End If

                'mykad length
                If oCommon.isMyKad2(strMykad) = False Then
                    strMsg += "Huruf tidak dibenarkan .Sila masukkan no Mykad[######06####]"
                Else
                End If

                'Tahun
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Kohort")) Then
                    strTahun = SiteData.Tables(0).Rows(i).Item("Kohort")
                Else
                    strMsg += "Sila isi Kohort|"
                End If

                'Sesi
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Sesi")) Then
                    strSesi = SiteData.Tables(0).Rows(i).Item("Sesi")
                Else
                    strMsg += "Sila isi Sesi|"
                End If

                'kodkursus
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KodKursus")) Then
                    strKodKursus = SiteData.Tables(0).Rows(i).Item("KodKursus")
                Else
                    strMsg += "Sila isi KodKursus|"
                End If

                'MataPelajaran
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MataPelajaran")) Then
                    strMataPelajaran = SiteData.Tables(0).Rows(i).Item("MataPelajaran")
                Else
                    strMsg += "Sila isi MataPelajaran|"
                End If

                'IsAKATahun
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("IsAKATahun")) Then
                    strIsAKATahun = SiteData.Tables(0).Rows(i).Item("IsAKATahun")
                Else
                    strMsg += "Sila isi IsAKATahun|"
                End If

                'IsAKASesi
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("IsAKASesi")) Then
                    strIsAKASesi = SiteData.Tables(0).Rows(i).Item("IsAKASesi")
                Else
                    strMsg += "Sila isi IsAKASesi|"
                End If

                If strMsg.Length = 0 Then
                    'strMsg = "Record#:" & i.ToString & "OK"
                    'strMsg += "<br/>"
                Else
                    strMsg = "Bil#:" & strBil & ":" & strMsg
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)
                'disp bil

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""

        'Dim str As String
        Try

            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")
                IntPelajarID = SiteData.Tables(0).Rows(i).Item("PelajarID")
                strKolejRecordID = SiteData.Tables(0).Rows(i).Item("KolejRecordID")
                strNama = SiteData.Tables(0).Rows(i).Item("Nama")
                strMykad = SiteData.Tables(0).Rows(i).Item("Mykad")
                strAngkaGiliran = SiteData.Tables(0).Rows(i).Item("AngkaGiliran")
                strTahun = SiteData.Tables(0).Rows(i).Item("Kohort")
                strSesi = SiteData.Tables(0).Rows(i).Item("Sesi")
                strKodKursus = SiteData.Tables(0).Rows(i).Item("KodKursus")
                strMataPelajaran = SiteData.Tables(0).Rows(i).Item("MataPelajaran")

                strSQL = "INSERT INTO kpmkv_pelajar_Akademik_Ulang (PelajarID,KolejRecordID,Nama,Mykad,AngkaGiliran,Kohort,Sesi,KodKursus,MataPelajaran,IsAKATahun,IsAKASesi,ImportDated)"
                strSQL += " VALUES ('" & IntPelajarID & "','" & strKolejRecordID & "'," & strNama & "'," & strMykad & "','" & strAngkaGiliran & "',"
                strSQL += "'" & strTahun & "','" & strSesi & "','" & strKodKursus & "','" & strMataPelajaran & "',"
                strSQL += "'" & strIsAKATahun & "','" & strIsAKASesi & "','" & Now.Date & "')"
                strRet = oCommon.ExecuteSQL(strSQL)
                'Response.Write(strSQL)
            Next

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Calon tidak berjaya didaftarkan"
            Return False
        End Try

        Return True

    End Function
    Private Sub refreshVar()

        strBil = 0
        strKolejRecordID = ""
        IntPelajarID = 0
        strNama = ""
        strMykad = ""
        strAngkaGiliran = ""
        strTahun = ""
        strSesi = ""
        strKodKursus = ""
        strMataPelajaran = ""
        strIsAKASesi = ""
        strIsAKATahun = ""
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        strRet = BindData(datRespondent)
    End Sub

End Class