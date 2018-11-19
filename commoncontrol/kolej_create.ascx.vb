Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class kolej_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_jeniskolej_list()
                ddlJenis.SelectedValue = "00"  '--Pilih

                kpmkv_negeri_list()
                ddlNegeri.SelectedValue = "00"  '--Pilih

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub kpmkv_jeniskolej_list()
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej  ORDER BY Jenis"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlJenis.DataSource = ds
            ddlJenis.DataTextField = "Jenis"
            ddlJenis.DataValueField = "Jenis"
            ddlJenis.DataBind()

            '--ALL
            ddlJenis.Items.Add(New ListItem("--Pilih--", "00"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri  ORDER BY Negeri"
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
            ddlNegeri.Items.Add(New ListItem("--Pilih--", "00"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_kolej_create() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mendaftarkan Kolej baru."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        '--ddlJenis
        If ddlJenis.SelectedValue = "00" Then
            lblMsg.Text = "Sila pilih Jenis Kolej!"
            ddlJenis.Focus()
            Return False
        End If

        '--txtKod
        If txtKod.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Kod Kolej!"
            txtKod.Focus()
            Return False
        End If

        strSQL = "SELECT Kod FROM kpmkv_kolej WHERE Kod='" & oCommon.FixSingleQuotes(txtKod.Text) & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Kod Kolej telah digunakan. Sila masukkan kod yang baru."
            Return False
        End If

        '--txtNama
        If txtNama.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Kolej!"
            txtNama.Focus()
            Return False
        End If

        '--ddlNegeri
        If ddlNegeri.SelectedValue = "00" Then
            lblMsg.Text = "Sila pilih Negeri!"
            ddlNegeri.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function kpmkv_kolej_create() As Boolean
        Dim strRecordID As String = oCommon.getGUID


        strSQL = "INSERT INTO kpmkv_kolej (RecordID,Jenis,Kod,Nama,Tel,Fax,Email,Alamat1,Negeri,Alamat2,Poskod,Bandar,NamaPengarah,EmailPengarah,MobilePengarah,TelPengarah,JawatanPengarah,GredPengarah,NamaKJPP,EmailKJPP,MobileKJPP,TelKJPP,JawatanKJPP,GredKJPP,NamaSUP,EmailSUP,TelSUP,MobileSUP,JawatanSUP,GredSUP)"
        strSQL += "VALUES ('" & strRecordID & "','" & ddlJenis.Text & "','" & oCommon.FixSingleQuotes(txtKod.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtTel.Text) & "',"
        strSQL += "'" & oCommon.FixSingleQuotes(txtFax.Text) & "','" & oCommon.FixSingleQuotes(txtEmail.Text) & "','" & oCommon.FixSingleQuotes(txtAlamat1.Text.ToUpper) & "','" & ddlNegeri.Text & "','" & txtAlamat2.Text & "','" & txtPoskod.Text & "','" & txtBandar.Text & "',"
        strSQL += "'" & oCommon.FixSingleQuotes(txtNamaPengarah.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtEmailPengarah.Text) & "','" & oCommon.FixSingleQuotes(txtBimbitPengarah.Text) & "','" & oCommon.FixSingleQuotes(txtTelPengarah.Text) & "','" & oCommon.FixSingleQuotes(txtJawatanPengarah.Text) & "','" & oCommon.FixSingleQuotes(txtGredPengarah.Text) & "',"
        strSQL += "'" & oCommon.FixSingleQuotes(txtNamaKJPP.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtEmailKJPP.Text) & "','" & oCommon.FixSingleQuotes(txtBimbitKJPP.Text) & "','" & oCommon.FixSingleQuotes(txtTelKJPP.Text) & "','" & oCommon.FixSingleQuotes(txtJawatanKJPP.Text) & "','" & oCommon.FixSingleQuotes(txtGredKJPP.Text) & "',"
        strSQL += "'" & oCommon.FixSingleQuotes(txtNamaSUP.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtEmailSUP.Text) & "','" & oCommon.FixSingleQuotes(txtMobileSUP.Text) & "','" & oCommon.FixSingleQuotes(txtTelSUP.Text) & "','" & oCommon.FixSingleQuotes(txtJawatanSUP.Text) & "','" & oCommon.FixSingleQuotes(txtGredSUP.Text) & "')"

        strRet = oCommon.ExecuteSQL(strSQL)


        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If
    End Function

End Class