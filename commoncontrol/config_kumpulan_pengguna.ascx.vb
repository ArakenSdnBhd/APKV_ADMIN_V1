Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class config_kumpulan_pengguna1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_group_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat konfigurasi kumpulan pengguna"
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean

        '--txtgroup
        If txtKumpulanPengguna.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan kumpulan pengguna!"
            txtKumpulanPengguna.Focus()
            Return False
        End If

        If Not lblUserGroupCode.Text = txtKod.Text Then   '--changes made to the kumpulan pengguna
            strSQL = "SELECT UserGroupCode FROM tbl_ctrl_usergroup WHERE UserGroupCode='" & oCommon.FixSingleQuotes(txtKod.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Kumpulan Pengguna telah digunakan.Sila masukkan kumpulan pengguna yang baru"
                Return False
            End If
        End If

        Return True
    End Function
    Private Function kpmkv_group_update() As Boolean

        strSQL = "INSERT tbl_ctrl_usergroup(UserGroup,UserGroupCode,Catatan) VALUES ('" & oCommon.FixSingleQuotes(txtKumpulanPengguna.Text) & "','" & oCommon.FixSingleQuotes(txtKod.Text) & "','" & oCommon.FixSingleQuotes(txtcatatan.Text) & "')"
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

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY UserGroupCode ASC"

        tmpSQL = "SELECT * FROM tbl_ctrl_usergroup"
        strWhere = " WHERE UserGroupCode IS NOT NULL"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub



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

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value
        Try
            strSQL = "DELETE FROM tbl_ctrl_usergroup WHERE UserGroupCode='" & strKeyID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya membatalkan parameter"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tidak Berjaya membatalkan parameter"
            End If

            ''debug
            'Response.Write(strSQL)
            strRet = BindData(datRespondent)

        Catch ex As Exception
            divMsg.Attributes("class") = "error"

        End Try
    End Sub
End Class