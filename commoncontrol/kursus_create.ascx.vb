Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class kursus_create
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim lblDebug As String = ConfigurationManager.AppSettings("DebugMode")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       

        Try
            If Not IsPostBack Then

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_namakluster_list()

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_namakluster_list()

        strSQL = "SELECT NamaKluster, KlusterID FROM kpmkv_kluster WHERE IsDeleted='N' AND Tahun='" & ddlTahun.Text & "' ORDER BY NamaKluster"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNamaKluster.DataSource = ds
            ddlNamaKluster.DataTextField = "NamaKluster"
            ddlNamaKluster.DataValueField = "KlusterID"
            ddlNamaKluster.DataBind()



        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

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
            If kpmkv_kursus_create() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mendaftarkan Kursus baru."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean


        '--ddlNamaKluster
        If ddlNamaKluster.Text = "" Then
            lblMsg.Text = "Sila masukkan Nama Kluster!"
            ddlNamaKluster.Text = ""
            Return False
        End If

        '--chksesi
        If chkSesi.Text = "" Then
            lblMsg.Text = "Sila pilih Sesi!"
            chkSesi.Focus()
            Return False
        End If

        '--chkjenis
        If chkJenisProgram.Text = "" Then
            lblMsg.Text = "Sila pilih Jenis Program!"
            chkJenisProgram.Focus()
            Return False
        End If

        '--txtNama
        If txtNama.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Kursus!"
            txtNama.Focus()
            Return False
        End If

        '--txtKod
        If txtKod.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Kod Kursus!"
            txtKod.Focus()
            Return False
        End If

        '-- check Availability
        strSQL = "SELECT KodKursus FROM kpmkv_kursus WHERE KlusterID='" & ddlNamaKluster.Text & "' and KodKursus='" & txtKod.Text & "' and Tahun='" & ddlTahun.Text & "' and Sesi='" & chkSesi.Text & "' and IsDeleted='N'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Kod Kursus telah digunakan.Sila daftarkan kod kursus yang baru"
            Return False
        End If

        Return True
    End Function
    Private Function kpmkv_kursus_create() As Boolean
        Dim strKlusterRecordID As String = oCommon.getGUID

        'kpmkv_kursus
        strSQL = "INSERT INTO kpmkv_kursus(Tahun,KlusterID,KodKursus,NamaKursus,Sesi,JenisKursus,IsDeleted) "
        strSQL += "VALUES ('" & ddlTahun.Text & "','" & ddlNamaKluster.SelectedValue & "','" & oCommon.FixSingleQuotes(txtKod.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "','" & chkSesi.Text & "','" & chkJenisProgram.Text & "','N')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function

   
    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_namakluster_list()
    End Sub
End Class