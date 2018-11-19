Imports System.Data.SqlClient
Imports System.IO
Public Class prm_pengarahPeperiksaan1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim fileName As String = ""
    Dim fileExtension As String = ""
    Dim fullFileName As String = ""


    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("strTypeid") = ""


                If Not Request.QueryString("edit") = "" Then
                    Load_page()
                End If

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub


    '--load for edit--'
    Private Sub Load_page()

        strSQL = " SELECT NamaPengarah,Description,Status FROM kpmkv_config_pengarahPeperiksaan"
        strSQL += " WHERE ID='" & Request.QueryString("edit") & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaPengarah")) Then
                    txtNama.Text = ds.Tables(0).Rows(0).Item("NamaPengarah")
                Else
                    txtNama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Status")) Then
                    ddlStatus.SelectedValue = ds.Tables(0).Rows(0).Item("Status")
                Else
                    ddlStatus.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Description")) Then
                    txtDescription.Text = ds.Tables(0).Rows(0).Item("Description")
                Else
                    txtDescription.Text = ""
                End If

            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub


    '--list--'
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""



        Dim strOrder As String = " ORDER BY ID DESC"

        tmpSQL = "SELECT ID,NamaPengarah,FileLocation,Description,Status FROM kpmkv_config_pengarahPeperiksaan"



        getSQL = tmpSQL & strWhere & strOrder

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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "No Record Found!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Total Records#:" & myDataSet.Tables(0).Rows.Count
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

    '-----add function-----'
    Private Function Save() As Boolean




        If FileUpload.PostedFile IsNot Nothing Then
            fileName = Path.GetFileName(FileUpload.PostedFile.FileName)
            fileExtension = Path.GetExtension(FileUpload.PostedFile.FileName)

            'first check if "uploads" folder exist or not, if not create it
            Dim fileSavePath As String = String.Concat(Server.MapPath("~/signature/"))
            If Not Directory.Exists(fileSavePath) Then
                Directory.CreateDirectory(fileSavePath)
            End If



            'after checking or creating folder it's time to save the file
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            fullFileName = oCommon.getRandom + "-" + fileName
            fileSavePath = fileSavePath + fullFileName
            FileUpload.PostedFile.SaveAs(fileSavePath)
            Dim fileInfo As New FileInfo(fileSavePath)
        End If

        If Not Request.QueryString("edit") = "" Then

            strSQL = "UPDATE kpmkv_config_pengarahPeperiksaan SET "

            strSQL += " NamaPengarah='" & oCommon.FixSingleQuotes(txtNama.Text) & "',"
            If Not fileName = "" Then
                strSQL += " FileLocation ='" & oCommon.FixSingleQuotes(fullFileName) & "',"
            End If
            strSQL += " Status='" & oCommon.FixSingleQuotes(ddlStatus.SelectedValue) & "',"
            strSQL += " Description='" & oCommon.FixSingleQuotes(txtDescription.Text) & "'"

            strSQL += " WHERE ID='" & Request.QueryString("edit") & "'"

        Else
            strSQL = "INSERT INTO kpmkv_config_pengarahPeperiksaan(NamaPengarah,Status,FileLocation,Description)"

            strSQL += " VALUES("
            strSQL += " '" & oCommon.FixSingleQuotes(txtNama.Text) & " ',"
            strSQL += " '" & oCommon.FixSingleQuotes(ddlStatus.SelectedValue) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(fullFileName) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtDescription.Text) & "')"

        End If

        strRet = oCommon.ExecuteSQL(strSQL)
        '--Debug
        'Response.Write(strSQL)

        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet

            Return False
        End If
    End Function

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick


        lblMsg.Text = ""

        If FileUpload.PostedFile IsNot Nothing Then
            fileName = Path.GetFileName(FileUpload.PostedFile.FileName)
            fileExtension = Path.GetExtension(FileUpload.PostedFile.FileName)

            If Not fileName = "" Then
                'validate file
                If Not (fileExtension = ".PNG" Or fileExtension = ".png" Or fileExtension = ".JPEG" Or fileExtension = ".jpeg" Or fileExtension = ".JPG" Or fileExtension = ".jpg") Then
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Hanya format fail .PNG dan .JPEG dibenarkan!"

                    Exit Sub
                End If
            End If
        End If

            Try

            '--execute
            If Save() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Rekod berjaya disimpan"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak berjaya disimpan"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

        strRet = BindData(datRespondent)
    End Sub


    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("prm.pengarahPeperiksaan.aspx")
    End Sub


    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strTid = datRespondent.DataKeys(e.RowIndex).Values("ID").ToString

        'chk session to prevent postback
        If Not strTid = Session("strTypeid") Then
            strSQL = "DELETE FROM kpmkv_config_pengarahPeperiksaan WHERE ID='" & oCommon.FixSingleQuotes(strTid) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Session("strDid") = ""
        End If
        strRet = BindData(datRespondent)
    End Sub



End Class