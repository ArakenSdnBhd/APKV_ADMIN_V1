Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Data.Common

Partial Public Class admin_accessright
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim nPageno As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'user type dropdown
            ctrl_usergroup_list()
            ddlGroup.Text = "0"

            menu_list()
            ddlmenu.Text = "0"

            submenu_list()
            ddlsubmenu.Text = "0"

            strRet = BindData(datRespondent)

            divdetail.Visible = False

        End If
    End Sub
    Private Sub ctrl_usergroup_list()

        strSQL = "SELECT * FROM tbl_ctrl_usergroup WHERE UserGroupCode !='A00' ORDER BY UserGroup"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
           
            ddlGroup.DataSource = ds
            ddlGroup.DataTextField = "UserGroup"
            ddlGroup.DataValueField = "UserGroupCode"
            ddlGroup.DataBind()
            ' UserGroupCode.SelectedIndex = 0

            ''--add blank row
            ddlgroup.Items.Insert(0, New ListItem("-Pilih-", "0"))


        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub menu_list()

        strSQL = "SELECT * FROM tbl_menuheader ORDER BY HeaderCode"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)

            ddlmenu.DataSource = ds
            ddlmenu.DataTextField = "HeaderText"
            ddlmenu.DataValueField = "HeaderCode"
            ddlmenu.DataBind()

            ''--add blank row
            ddlmenu.Items.Insert(0, New ListItem("SEMUA", "0"))


        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub submenu_list()

            If Not ddlsubmenu.Text = "0" Then
                strSQL = "SELECT UPPER(MenuText) AS MenuText, MenuCode FROM tbl_menu WHERE MenuCode !='M55' AND MenuCode !='M56'   ORDER BY HeaderCode"
            Else
            strSQL = "SELECT UPPER(MenuText) AS MenuText, MenuCode FROM tbl_menu WHERE HeaderCode ='" & ddlmenu.SelectedValue & "' "
            strSQL += " AND MenuCode !='M55' AND MenuCode !='M56'  ORDER BY HeaderCode "
            End If


        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)

            ddlsubmenu.DataSource = ds
            ddlsubmenu.DataTextField = "MenuText"
            ddlsubmenu.DataValueField = "MenuCode"
            ddlsubmenu.DataBind()

            ''--add blank row
            ddlsubmenu.Items.Insert(0, New ListItem("SEMUA", "0"))


        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    '--1st table--'
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
        Dim strOrder As String = " ORDER BY ParentMenuCode ASC"

        tmpSQL = "SELECT * FROM tbl_submenu"
       
        'headermenu
        If Not ddlmenu.Text = "0" Then
            strWhere += " WHERE HeaderCode='" & ddlmenu.Text & "' AND ParentMenuCode !='M55' AND ParentMenuCode !='M56'"
        End If
        If Not ddlsubmenu.Text = "0" Then
            strWhere += "AND ParentMenuCode='" & ddlsubmenu.Text & "' "
        End If
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_PageIndexChanged(sender As Object, e As EventArgs) Handles datRespondent.PageIndexChanged
        datRespondent.DataBind()
    End Sub

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


    '--btn cari--'
    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""
        strRet = BindData(datRespondent)



    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
           
            If chkAll.Checked = True Then
                ''delete all usergroupcode
                strSQL = "DELETE FROM tbl_usergroupmenu WHERE UserGroupCode='" & ddlgroup.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "INSERT INTO tbl_usergroupmenu(MenuCode) Select MenuCode  FROM tbl_menu WHERE MenuCode !='M55' AND MenuCode !='M56'ORDER BY MenuCode ASC"
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "UPDATE tbl_usergroupmenu SET UserGroupCode='" & ddlgroup.Text & "' WHERE UserGroupCode IS NULL "
                strRet = oCommon.ExecuteSQL(strSQL)

                lblMsgTop.Text = "Penetapan Akses Pengguna Berjaya"
                lblMsg.Text = "Penetapan Akses Pengguna Berjaya"
                Exit Sub

            End If
           
                ''--re-assign access right
                For i As Integer = 0 To datRespondent.Rows.Count - 1
                    Dim row As GridViewRow = datRespondent.Rows(i)
                    Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

                If isChecked Then
                    strSQL = "SELECT * FROM tbl_usergroupmenu WHERE UserGroupCode = '" & ddlgroup.Text & "' AND MenuCode='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.isExist(strSQL)

                    If strRet = True Then
                        strSQL = " DELETE FROM tbl_usergroupmenu WHERE UserGroupCode = '" & ddlgroup.Text & "' AND MenuCode='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        strSQL = "INSERT INTO tbl_usergroupmenu(UserGroupCode,MenuCode) VALUES('" & ddlgroup.Text & "','" & datRespondent.DataKeys(i).Value.ToString & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        strSQL = "INSERT INTO tbl_usergroupmenu(UserGroupCode,MenuCode) VALUES('" & ddlgroup.Text & "','" & datRespondent.DataKeys(i).Value.ToString & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                End If
                Next

            lblMsgTop.Text = "Penetapan Akses Pengguna Berjaya"
            lblMsg.Text = "Penetapan Akses Pengguna Berjaya"

        Catch ex As Exception
            lblMsgTop.Text = ex.Message
            lblMsg.Text = ex.Message

        End Try
        divdetail.Visible = True
        strRet = BindDatalist(datRespondent2)

    End Sub

    Private Sub ddlmenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlmenu.SelectedIndexChanged
        submenu_list()
    End Sub


    '--2nd table--'
    Private Function BindDatalist(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL2, strConn)
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
    Private Function getSQL2() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        tmpSQL = "SELECT tbl_submenu.SubMenuText,"
        tmpSQL += " tbl_menu.ParentMenuCode,"
        tmpSQL += " tbl_menuheader.HeaderText, tbl_usergroupmenu.UserGroupCode"
        tmpSQL += " FROM tbl_usergroupmenu"
        tmpSQL += " inner join tbl_submenu on tbl_usergroupmenu.MenuCode =tbl_submenu .ParentMenuCode "
        tmpSQL += " left join tbl_menu on tbl_submenu.ParentMenuCode=tbl_menu.ParentMenuCode "
        tmpSQL += " inner  join tbl_menuheader on tbl_menu.HeaderCode=tbl_menuheader.HeaderCode "
        tmpSQL += " WHERE tbl_usergroupmenu.UserGroupCode='" & ddlgroup.SelectedValue & "' "
        tmpSQL += " ORDER BY tbl_submenu.SubMenuCode"
        getSQL2 = tmpSQL
        ''--debug
        'Response.Write(getSQL)

        Return getSQL2

    End Function


    

    Private Sub datRespondent2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent2.PageIndexChanging
        datRespondent2.PageIndex = e.NewPageIndex
        strRet = BindDatalist(datRespondent2)
    End Sub


    'Private Sub ddlgroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlgroup.SelectedIndexChanged
    '    divdetail.Visible = True
    '    strRet = BindDatalist(datRespondent2)

    '    lblgroup.Text = ddlgroup.SelectedValue
    'End Sub

    Private Sub datRespondent2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent2.RowCommand

        lblMsg.Text = ""
        lblMsgTop.Text = ""

        If (e.CommandName = "Batal") Then

            Dim strParentCode As String = e.CommandArgument.ToString()

            'delete penetapan pensyarah -kelas
            strSQL = "DELETE FROM tbl_usergroupmenu WHERE MenuCode='" & strParentCode & "' AND UserGroupCode='" & ddlgroup.SelectedValue & "'"
            If strRet = oCommon.ExecuteSQL(strSQL) = 0 Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Akses berjaya dipadamkan"
                lblMsgTop.Text = "Akses berjaya dipadamkan"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Akses tidak berjaya dipadamkan"
                lblMsgTop.Text = "Akses tidak berjaya dipadamkan"
            End If
        End If
        strRet = BindDatalist(datRespondent2)
    End Sub
End Class















