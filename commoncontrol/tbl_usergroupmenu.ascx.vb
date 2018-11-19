Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class tbl_usergroupmenu
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            tbl_usergroupmenu_load()
        End If
    End Sub


    Private Sub tbl_usergroupmenu_load()

        Try

            strSQL = "select tbl_menuheader.Idx, tbl_menu .HeaderCode,LOWER(tbl_menuheader.HeaderText) AS HeaderText  from tbl_usergroupmenu "
            strSQL += " inner join tbl_menu on tbl_usergroupmenu .MenuCode = tbl_menu.MenuCode"
            strSQL += " inner join tbl_menuheader on tbl_menuheader .HeaderCode =tbl_menu.HeaderCode "
            strSQL += " WHERE tbl_usergroupmenu.UserGroupCode='" & Session("UserGroupCode") & "'"

            strSQL += " group by tbl_menu .HeaderCode ,tbl_menuheader .HeaderCode ,tbl_menuheader.HeaderText,tbl_menuheader.Idx "
            strSQL += " order by tbl_menuheader.Idx ASC"
            'Response.Write(strSQL)

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            Dim numrows As Integer

            numrows = MyTable.Rows.Count

            Dim strHeader, strMenu As String
            Dim strHeaderText, strMenuLink, strSubMenuText As String
            Dim strMenuImg As String
            Dim strHeaderCode As String
            strHeader = ""
            strMenu = ""
            strMenuImg = ""
            strHeaderCode = ""
            If numrows > 0 Then
                For i = 0 To numrows - 1
                    'Print header for menu
                    strHeaderText = ds.Tables(0).Rows(i).Item("HeaderText")
                    strHeaderCode = ds.Tables(0).Rows(i).Item("HeaderCode")

                    strSQL = "SELECT tbl_submenu.Idx,tbl_submenu.SubMenuImg ,tbl_submenu.SubMenuLink, tbl_submenu.SubMenuText"
                    strSQL += " FROM tbl_submenu INNER JOIN tbl_usergroupmenu on tbl_usergroupmenu.MenuCode=tbl_submenu .ParentMenuCode"
                    strSQL += " WHERE tbl_usergroupmenu.UserGroupCode='" & Session("UserGroupCode") & "' "
                    strSQL += " AND tbl_submenu.HeaderCode ='" & strHeaderCode & "' ORDER BY tbl_submenu.Idx ASC"
                    strRet = oCommon.getFieldValue(strSQL)
                    'Response.Write(strSQL)

                    Dim strConn1 As String = ConfigurationManager.AppSettings("ConnectionString")
                    Dim objConn1 As SqlConnection = New SqlConnection(strConn1)
                    Dim sqlDA1 As New SqlDataAdapter(strSQL, objConn1)

                    Dim ds1 As DataSet = New DataSet
                    sqlDA1.Fill(ds1, "AnyTable")

                    Dim nCount1 As Integer = 1
                    Dim MyTable1 As DataTable = New DataTable
                    MyTable1 = ds1.Tables(0)
                    Dim numrows1 As Integer

                    numrows1 = MyTable1.Rows.Count
                    '--get header
                    strMenu += "<tr><td class='fbnav_header' colspan=2'>" & strHeaderText & "</td></tr>"
                    For k = 0 To numrows1 - 1
                        strMenuImg = ds1.Tables(0).Rows(k).Item("SubMenuImg")
                        strMenuLink = ds1.Tables(0).Rows(k).Item("SubMenuLink")
                        strSubMenuText = ds1.Tables(0).Rows(k).Item("SubMenuText")


                        strMenu += "<tr class='fbnav_items'>"
                        strMenu += "<td class='fbnav_items'><a href='" & strMenuLink & "' rel='nofollow' target='_self'><img style='vertical-align: middle; border: none;' src='" & strMenuImg & "' width='16px' height='16px' alt='::' />" & strSubMenuText & "</a></td>"
                        strMenu += "</tr>"



                    Next
                    strMenu += "<tr><td colspan=2>&nbsp</td></tr>"
                    tblUserGroupMenu.InnerHtml = strMenu



                Next

            End If
        Catch ex As Exception
            objConn.Dispose()
        End Try

    End Sub
End Class