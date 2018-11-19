Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class takwim_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblMsg.Text = ""
                '--refresh
                menu_list()

                submenu_list()
                ddlSubMenu.Text = "0"
                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_semester_list()

                txtDate.Text = Format(CDate(Date.Now), "dd-MM-yyyy")
                txtDateTo.Text = Format(CDate(Date.Now), "dd-MM-yyyy")

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        End Try

    End Sub
    Private Sub menu_list()

        strSQL = "SELECT * FROM tbl_menuheader_kolej ORDER BY HeaderCode"
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
            ddlMenu.DataValueField = "HeaderCode"
            ddlmenu.DataBind()

            ''--add blank row
            ddlMenu.Items.Insert(0, New ListItem("-PILIH-", "0"))


        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub submenu_list()

        strSQL = "Select SubMenuText FROM tbl_submenu_kolej WHERE HeaderCode='" & ddlMenu.SelectedValue & "' ORDER BY HeaderCode"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)

            ddlSubMenu.DataSource = ds
            ddlSubMenu.DataTextField = "SubMenuText"
            ddlSubMenu.DataValueField = "SubMenuText"
            ddlSubMenu.DataBind()

            ''--add blank row
            ddlSubMenu.Items.Insert(0, New ListItem("-PILIH-", "0"))


        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY Tahun"
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
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester  ORDER BY SemesterID"
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

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub ClearScreen()
        lblMsg.Text = ""

        txtTitle.Text = ""
        txtCatatan.Text = ""

    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            '-insert into course list
            strSQL = "INSERT INTO kpmkv_takwim(Tahun,Kohort,Sesi,Semester,HeaderCode,SubMenuText,TarikhMula,TarikhAkhir,Title,Catatan,Aktif) "
            strSQL += " VALUES ('" & Now.Year & "','" & ddlTahun.Text & "','" & chkSesi.Text & "','" & ddlSemester.Text & "','" & ddlMenu.SelectedItem.Text & "','" & ddlSubMenu.Text & "','" & Request.Form(txtDate.UniqueID) & "','" & Request.Form(txtDateTo.UniqueID) & "','" & txtTitle.Text & "','" & txtCatatan.Text & "','1')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "Penambahan berjaya!"
            Else
                lblMsg.Text = "Tidak berjaya. " & strRet
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message
        End Try

    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.takwim.list.aspx?tahun=" & ddlTahun.Text)
    End Sub

    Private Sub ddlMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMenu.SelectedIndexChanged
        submenu_list()
        ddlSubMenu.Text = "0"
    End Sub

End Class