Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Public Class BS_jadual_waktu
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""
        Try
            If Not IsPostBack Then
                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_kohort_list()
                ddlKohort.Text = Now.Year

                kpmkv_semester_list()
                strRet = BindData(datRespondent)
                'divImport.Visible = False
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY TahunID"
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

            '--ALL
            ' ddlTahun.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kohort_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY TahunID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKohort.DataSource = ds
            ddlKohort.DataTextField = "Tahun"
            ddlKohort.DataValueField = "Tahun"
            ddlKohort.DataBind()

            '--ALL
            ' ddlKohort.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester  ORDER BY Semester"
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
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Tahun,Kohort ASC"


        '--not deleted
        tmpSQL = "Select * from kpmkv_jadual_waktu"
        strWhere = " WHERE Tahun='" & Now.Year & "'"

       
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ' Response.Write(getSQL)

        Return getSQL

    End Function
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
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Sub
        End If

        Try
            If FlUploadpdf.PostedFile IsNot Nothing Then
                Dim fileName As String = Path.GetFileName(FlUploadpdf.PostedFile.FileName)
                Dim fileExtension As String = Path.GetExtension(FlUploadpdf.PostedFile.FileName)

                'first check if "uploads" folder exist or not, if not create it
                Dim fileSavePath As String = Server.MapPath("BankSoalan")
                If Not Directory.Exists(fileSavePath) Then
                    Directory.CreateDirectory(fileSavePath)
                End If

                'after checking or creating folder it's time to save the file
                fileSavePath = fileSavePath & "//" & fileName
                FlUploadpdf.PostedFile.SaveAs(fileSavePath)
                Dim fileInfo As New FileInfo(fileSavePath)
                Using sqlConn As New SqlConnection(strConn)
                    Using sqlCmd As New SqlCommand()
                        sqlCmd.CommandText = "INSERT INTO kpmkv_jadual_waktu" & ControlChars.CrLf & _
                        "(FileName,FileSize,FileExtension,FilePath,Title,Description,Tahun,Kohort,Sesi,Semester,DateCreated,DateStart,DateEnd) " & ControlChars.CrLf & _
                        "VALUES (@FileName,@FileSize,@FileExtension,@FilePath,@Title,@Description,@Tahun,@Kohort,@Sesi,@Semester,@DateCreated,@DateStart,@DateEnd);"
                        sqlCmd.Parameters.AddWithValue("@FileName", fileName)
                        sqlCmd.Parameters.AddWithValue("@FileSize", fileInfo.Length.ToString())
                        sqlCmd.Parameters.AddWithValue("@FileExtension", fileExtension)
                        sqlCmd.Parameters.AddWithValue("@FilePath", fileSavePath)
                        sqlCmd.Parameters.AddWithValue("@Title", txttitle.Text)
                        sqlCmd.Parameters.AddWithValue("@Description", txtDesc.Text)
                        sqlCmd.Parameters.AddWithValue("@Tahun", ddlTahun.Text)
                        sqlCmd.Parameters.AddWithValue("@Kohort", ddlKohort.Text)
                        sqlCmd.Parameters.AddWithValue("@Sesi", chkSesi.Text)
                        sqlCmd.Parameters.AddWithValue("@Semester", ddlSemester.Text)
                        sqlCmd.Parameters.AddWithValue("@DateCreated", Date.Now)
                        sqlCmd.Parameters.AddWithValue("@DateStart", txtDateStart.Text)
                        sqlCmd.Parameters.AddWithValue("@DateEnd", txtDateEnd.Text)
                        sqlCmd.Connection = sqlConn
                        sqlConn.Open()
                        sqlCmd.ExecuteNonQuery()
                        sqlConn.Close()
                        strRet = BindData(datRespondent)
                    End Using
                End Using

                lblMsg.Text = "File Uploaded Successfully!"
                lblMsg.ForeColor = System.Drawing.Color.Green
            Else
                lblMsg.Text = "Error: Please select a file to upload!"
            End If
        Catch
            lblMsg.Text = "Error: Error while uploading file!"
        End Try
    End Sub
    Private Function ValidatePage() As Boolean

        '--chksesi
        If chkSesi.Text = "" Then
            lblMsg.Text = "Sila pilih Sesi!"
            chkSesi.Focus()
            Return False
        End If

        '--title
        If txttitle.Text = "" Then
            lblMsg.Text = "Sila masukkan Tajuk!"
            txttitle.Focus()
            Return False
        End If

        '--txtdesc
        If txtDesc.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Catatan!"
            txtDesc.Focus()
            Return False
        End If

        '--txtdateStart
        If txtDateStart.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Tarikh Mula!"
            txtDateStart.Focus()
            Return False
        End If

        '--txtDateEnd
        If txtDateEnd.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Tarikh Akhir!"
            txtDateEnd.Focus()
            Return False
        End If

        If txtDateEnd.Text < txtDateStart.Text Then
            lblMsg.Text = "Tarikh Akhir tidak boleh kurang dari Tarikh Mula!"
            txtDateEnd.Focus()
            Return False
        End If

        Return True
    End Function
    'Protected Sub lnkDownloadMe_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
    '        Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
    '        Dim fileId As Integer =
    '        Convert.ToInt32(datRespondent.DataKeys(gvrow.RowIndex).Value.ToString())
    '        Using sqlConn As New SqlConnection(strConn)
    '            Using sqlCmd As New SqlCommand()
    '                sqlCmd.CommandText = "SELECT * FROM kpmkv_jadual_waktu WHERE JWID=@JWID"
    '                sqlCmd.Parameters.AddWithValue("@FileId", fileId)
    '                sqlCmd.Connection = sqlConn
    '                sqlConn.Open()
    '                Dim dr As SqlDataReader = sqlCmd.ExecuteReader()
    '                If dr.Read() Then
    '                    Dim fileName As String = dr("FileName").ToString()
    '                    Dim fileLength As String = dr("FileSize").ToString()
    '                    Dim filePath As String = dr("FilePath").ToString()
    '                    Dim Title As String = dr("@Title").ToString()
    '                    Dim Description As String = dr("@Description").ToString()
    '                    Dim Tahun As String = dr("@Tahun").ToString()
    '                    Dim Kohort As String = dr("@Kohort").ToString()
    '                    Dim DateCreated As String = dr("@DateCreated").ToString()
    '                    Dim DateStart As String = dr("@DateStart").ToString()
    '                    Dim DateEnd As String = dr("@DateEnd").ToString()
    '                    If File.Exists(filePath) Then
    '                        Response.Clear()
    '                        Response.BufferOutput = False
    '                        Response.ContentType = "application/octet-stream"
    '                        Response.AddHeader("Content-Length", fileLength)
    '                        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
    '                        Response.TransmitFile(filePath)
    '                        Response.Flush()
    '                    Else
    '                        lblMsg.Text = "Error: File not found!"
    '                    End If
    '                End If
    '            End Using
    '        End Using
    '    Catch
    '        lblMsg.Text = "Error: Error while downloading file!"
    '    End Try
    'End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        lblMsg.Text = ""

        Dim strJWID As Integer = datRespondent.DataKeys(e.RowIndex).Values("JWID")
        Try
            If Not strJWID = Session("strJWID") Then
                strSQL = "DELETE FROM kpmkv_jadual_waktu WHERE JWID='" & strJWID & "'"
                If strRet = oCommon.ExecuteSQL(strSQL) = 0 Then

                    divMsg.Attributes("class") = "info"
                    lblMsg.Text = "Jadual Waktu berjaya dipadamkan"
                    Session("strJWID") = ""
                Else
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Jadual Waktu tidak berjaya dipadamkan"
                    Session("strJWID") = ""
                End If
            Else
                Session("strJWID") = ""
            End If
        Catch ex As Exception
            divMsg.Attributes("class") = "error"
        End Try

        strRet = BindData(datRespondent)
    End Sub

End Class