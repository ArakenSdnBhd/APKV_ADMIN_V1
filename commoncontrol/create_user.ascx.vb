Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class create_user
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_Kumpulan_Pengguna()
                ddlKumpulan.Text = "0"

                kpmkv_negeri_list()
                ddlNegeri.Text = "0"
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
     Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri ORDER BY Negeri"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Negeri"
            ddlNegeri.DataValueField = "Negeri"
            ddlNegeri.DataBind()

            '--ALL
            ddlNegeri.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            'lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_Kumpulan_Pengguna()
        strSQL = "SELECT UserGroup FROM tbl_ctrl_usergroup WHERE UserGroup NOT IN ('SU-KOLEJ','PENSYARAH','PELAJAR','SYSADMIN') ORDER BY UserGroup ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKumpulan.DataSource = ds
            ddlKumpulan.DataTextField = "UserGroup"
            ddlKumpulan.DataValueField = "UserGroup"
            ddlKumpulan.DataBind()

            '--ALL
            ddlKumpulan.Items.Add(New ListItem("-Pilih", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message


        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        '--txtNama
        If txtNama.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama!"
            txtNama.Focus()
            Return False
        End If

        '--txtMYKAD
        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan MYKAD!"
            txtMYKAD.Focus()
            Return False
        ElseIf oCommon.isMyKad2(txtMYKAD.Text) = False Then
            lblMsg.Text = "Huruf tidak dibenarkan .Sila masukkan no MYKAD [######06####]"
            txtMYKAD.Focus()
            Return False
        End If

        strSQL = "SELECT MYKAD FROM kpmkv_users WHERE MYKAD='" & txtMYKAD.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "MYKAD telah digunakan. Pendaftaran Pengguna tidak berjaya."
            Return False
        End If


        '--txtJawatan
        If ddlKumpulan.SelectedValue = "0" Then
            lblMsg.Text = "Sila pilih Kumpulan Pengguna!"
            ddlKumpulan.Focus()
            Return False
        End If


        '--txtNegeri
        If ddlKumpulan.SelectedValue = "0" Then
            lblMsg.Text = "Sila pilih Negeri!"
            ddlKumpulan.Focus()
            Return False
        End If


        '--txtEmail
        If txtEmail.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Email!"
        ElseIf oCommon.isEmail(txtEmail.Text) = False Then
            lblMsg.Text = "Emel Pengguna tidak sah. (Contoh: user@mail.com)"
            txtEmail.Focus()
            Return False
        End If

        ''--txtlogin
        'If txtLogin.Text.Length = 0 Then
        '    lblMsg.Text = "Sila masukkan Login ID!"
        '    Return False
        'End If

        ''--txtpwd
        'If txtPwd.Text.Length = 0 Then
        '    Return False
        'End If

        Return True
    End Function

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_user_create() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Pengguna baru berjaya didaftarkan"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Pengguna baru tidak berjaya didaftarkan"

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function kpmkv_user_create() As Boolean

        Dim strRecordID As String = oCommon.getGUID

        strSQL = "INSERT INTO kpmkv_users (RecordID,LoginID,Pwd,UserType,Negeri,Nama,Mykad,Tel,Email,StatusID) "
        strSQL += "VALUES ('" & strRecordID & "','" & oCommon.FixSingleQuotes(txtLogin.Text) & "','" & oCommon.FixSingleQuotes(txtPwd.Text) & "','" & ddlKumpulan.Text & "','" & ddlNegeri.Text & "','" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "','" & oCommon.FixSingleQuotes(txtTel.Text) & "','" & oCommon.FixSingleQuotes(txtEmail.Text) & "','2')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = 0 Then
            divMsg.Attributes("class") = "error"
            Exit Function
        Else
            Return True
        End If

    End Function
End Class