Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class config_submenu1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_menuheader_list()
                ddlmenuutama.Text = ""

                kpmkv_menu_list()
                ddlmenu.Text = ""

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_menuheader_list()
        strSQL = "SELECT * FROM tbl_menuheader "
        If ddlJenis.SelectedValue = "SUKOLEJ" Then
            strSQL += " WHERE Type='SEMUA'"
        Else
        End If
        strSQL += " ORDER BY Idx ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlmenuutama.DataSource = ds
            ddlmenuutama.DataTextField = "HeaderText"
            ddlmenuutama.DataValueField = "HeaderCode"
            ddlmenuutama.DataBind()

            ''--ALL
            ddlmenuutama.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_menu_list()
        strSQL = "SELECT UPPER(MenuText) AS MenuText, MenuCode"
        strSQL += " FROM tbl_menu WHERE HeaderCode='" & ddlmenuutama.SelectedValue & "' ORDER BY Idx ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlmenu.DataSource = ds
            ddlmenu.DataTextField = "MenuText"
            ddlmenu.DataValueField = "MenuCode"
            ddlmenu.DataBind()

            ''--ALL
            ddlmenu.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
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
                lblMsg.Text = "Berjaya mengemaskini maklumat konfigurasi sub menu"
                strRet = BindData(datRespondent)
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean

        '--txtgroup
        If ddlmenuutama.SelectedValue = "" Then
            lblMsg.Text = "Sila pilih menu utama!"
            ddlmenuutama.Focus()
            Return False
        End If
        If ddlmenu.SelectedValue = "" Then
            lblMsg.Text = "Sila pilih menu!"
            ddlmenu.Focus()
            Return False
        End If
        If txtsubmenu.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan submenu!"
            txtsubmenu.Focus()
            Return False
        End If
        If txtlink.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan menulink!"
            txtlink.Focus()
            Return False
        End If

        If Not lblSubMenuCode.Text = txtKod.Text Then   '--changes made to  the menu
            strSQL = "SELECT SubMenuCode FROM tbl_submenu WHERE SubMenuCode='" & oCommon.FixSingleQuotes(txtKod.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "SubMenu telah digunakan.Sila masukkan Kod yang baru"
                Return False
            End If
        End If

        Return True
    End Function
    Private Function kpmkv_group_update() As Boolean

        strSQL = " INSERT tbl_submenu(SubMenuCode,SubMenuText,SubMenuLink,SubMenuimg,ParentMenuCode,HeaderCode,Idx) "
        strSQL += " VALUES ('" & oCommon.FixSingleQuotes(txtKod.Text) & "',"
        strSQL += " '" & oCommon.FixSingleQuotes(txtsubmenu.Text) & "',"
        strSQL += " '" & oCommon.FixSingleQuotes(txtlink.Text) & "',"
        strSQL += " '" & txtmenuimg.Text & "','" & ddlmenu.SelectedValue & "',"
        strSQL += " '" & ddlmenuutama.SelectedValue & "','" & txtIdx.Text & "')"
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
        Dim strOrder As String = " ORDER BY tbl_submenu.Idx ASC"

        tmpSQL = "SELECT * FROM tbl_submenu LEFT JOIN tbl_menuheader "
        tmpSQL += " ON tbl_submenu.HeaderCode=tbl_menuheader.HeaderCode"
        tmpSQL += " LEFT JOIN tbl_menu ON tbl_submenu.ParentMenuCode=tbl_menu.ParentMenuCode"
        strWhere += " WHERE tbl_submenu.SubMenuCode Is Not NULL"
        If Not ddlmenuutama.SelectedValue = "" Then
            strWhere += " AND tbl_submenu.HeaderCode='" & ddlmenuutama.SelectedValue & "' "
        End If
        If Not ddlmenu.SelectedValue = "" Then
            strWhere += " AND tbl_submenu.ParentMenuCode='" & ddlmenu.SelectedValue & "' "
        End If

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
            strSQL = "DELETE FROM tbl_submenu WHERE SubMenuCode='" & strKeyID & "'"
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

    Private Sub ddlmenuutama_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlmenuutama.SelectedIndexChanged
        kpmkv_menu_list()
        ddlmenu.SelectedValue = ""
        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_menuheader_list()
        ddlmenuutama.SelectedValue = ""
        ddlmenu.SelectedValue = ""
    End Sub

    Private Sub ddlmenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlmenu.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub
End Class