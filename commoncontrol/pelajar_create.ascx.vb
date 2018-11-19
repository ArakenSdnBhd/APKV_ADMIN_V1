Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class pelajar_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

         lblMsg.Text = ""
        Try
            If Not IsPostBack Then

                kpmkv_negeri_list()
                ddlNegeri.Text = "0"

                kpmkv_jenis_list()
                ddlJenis.Text = "0"

                kpmkv_kolej_list()
                ddlKolej.Text = "0"


                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_semester_list()

                kpmkv_kaum_list()

                kpmkv_kluster_list()
                ddlKluster.Text = "0"

                kpmkv_kodkursus_list()
                ddlKodKursus.Text = "0"

                kpmkv_kelas_list()
                ddlNamaKelas.Text = "0"
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    '-----list----'

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
            ddlNegeri.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_jenis_list()
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
            ddlJenis.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej  ORDER BY Nama ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKolej.DataSource = ds
            ddlKolej.DataTextField = "Nama"
            ddlKolej.DataValueField = "RecordID"
            ddlKolej.DataBind()




            '--ALL
            ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

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
        strSQL = "SELECT Semester FROM kpmkv_semester  WHERE Semester='1'"
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
    Private Sub kpmkv_kaum_list()
        strSQL = "SELECT Kaum FROM kpmkv_kaum  ORDER BY Kaum"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKaum.DataSource = ds
            ddlKaum.DataTextField = "Kaum"
            ddlKaum.DataValueField = "Kaum"
            ddlKaum.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kluster_list()
        strSQL = "SELECT kpmkv_kluster.NamaKluster, kpmkv_kluster.KlusterID FROM  kpmkv_kursus_kolej"
        strSQL += " LEFT OUTER JOIN kpmkv_kursus ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN"
        strSQL += " kpmkv_kluster ON kpmkv_kursus.KlusterID = kpmkv_kluster.KlusterID"
        strSQL += " WHERE kpmkv_kursus.Tahun ='" & ddlTahun.SelectedValue & "' AND kpmkv_kursus.Sesi='" & chkSesi.Text & "' GROUP BY kpmkv_kluster.NamaKluster,  kpmkv_kluster.KlusterID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKluster.DataSource = ds
            ddlKluster.DataTextField = "NamaKluster"
            ddlKluster.DataValueField = "KlusterID"
            ddlKluster.DataBind()

            '--ALL
            ddlKluster.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID FROM kpmkv_kursus_kolej LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kursus_kolej.KolejRecordID='" & ddlKolej.Text & "' AND kpmkv_kursus.Tahun='" & ddlTahun.SelectedValue & "' AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' AND KlusterID='" & ddlKluster.SelectedValue & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKursus.DataSource = ds
            ddlKodKursus.DataTextField = "KodKursus"
            ddlKodKursus.DataValueField = "KursusID"
            ddlKodKursus.DataBind()

            '--ALL
            ddlKodKursus.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kelas_list()
        strSQL = " SELECT kpmkv_kelas.NamaKelas, kpmkv_kelas.KelasID FROM kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.Text & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' AND kpmkv_kursus.Tahun= '" & ddlTahun.SelectedValue & "' ORDER BY  kpmkv_kelas.NamaKelas"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        'Response.Write(strSQL)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNamaKelas.DataSource = ds
            ddlNamaKelas.DataTextField = "NamaKelas"
            ddlNamaKelas.DataValueField = "KelasID"
            ddlNamaKelas.DataBind()

            '--ALL
            ddlNamaKelas.Items.Add(New ListItem("-Pilih-", "0"))

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
            If kpmkv_pelajar_create() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Calon baru berjaya didaftarkan"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Calon baru tidak berjaya didaftarkan"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean


        '--ddlsesi
        If chkSesi.Text = "" Then
            lblMsg.Text = "Sila pilih Sesi!"
            chkSesi.Focus()
            Return False
        End If

        '--txtKluster
        If ddlKluster.Text.Length = 0 Then
            lblMsg.Text = "Sila Pilih Kluster!"
            ddlKluster.Focus()
            Return False
        End If

        '--txtNama
        If txtNama.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Calon!"
            txtNama.Focus()
            Return False
        End If

        '--txtMYKAD
        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan MYKAD Calon!"
            txtMYKAD.Focus()
            Return False
        ElseIf oCommon.isNumeric(txtMYKAD.Text) = False Then
            lblMsg.Text = "Huruf tidak dibenarkan .Sila masukkan no MYKAD [############]"
            txtMYKAD.Focus()
            Return False
        End If

        strSQL = "SELECT Mykad FROM kpmkv_pelajar Where Mykad='" & txtMYKAD.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "MYKAD telah digunakan. Pendaftaran Pelajar tidak berjaya"
            Return False
        End If

        '--ddlAgama
        If chkAgama.Text = "" Then
            lblMsg.Text = "Sila pilih jenis Agama!"
            chkAgama.Focus()
            Return False
        End If

        '--ddlJantina
        If chkJantina.Text = "" Then
            lblMsg.Text = "Sila pilih jenis Jantina!"
            chkJantina.Focus()
            Return False
        End If

        '--txtEmail
        If txtEmail.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Email Calon!"
        ElseIf oCommon.isEmail(txtEmail.Text) = False Then
            lblMsg.Text = "Emel Calon tidak sah. (Contoh: Emel@contoh.com)"
            Return False
        End If

        '--ddlkelas
        If ddlNamaKelas.Text = "" Then
            lblMsg.Text = "Sila pilih kelas!"
            ddlNamaKelas.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function kpmkv_pelajar_create() As Boolean
        Dim strRecordID As String = oCommon.getGUID
        'create with isApproved='Y'
        strSQL = "INSERT INTO kpmkv_pelajar (KolejRecordID,Pengajian,KursusID,KelasID,Tahun,Semester,Sesi,Nama,MYKAD,Jantina,Kaum,Agama,Email,Catatan,StatusID,JenisCalonID,IsDeleted)"
        strSQL += "VALUES ('" & ddlKolej.Text & "','PRA DIPLOMA','" & ddlKodKursus.SelectedValue & "','" & ddlNamaKelas.SelectedValue & "','" & ddlTahun.Text & "','" & ddlSemester.Text & "','" & chkSesi.SelectedValue & "','" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtMYKAD.Text.ToUpper) & "','" & chkJantina.SelectedValue & "','" & ddlKaum.Text & "','" & chkAgama.SelectedValue & "','" & oCommon.FixSingleQuotes(txtEmail.Text) & "','" & oCommon.FixSingleQuotes(txtCatatan.Text) & "','2','2','N')"
        strRet = oCommon.ExecuteSQL(strSQL)
        'Response.Write(strSQL)
        If strRet = "0" Then

            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE Mykad='" & txtMYKAD.Text & "'"
            Dim strPelajarID As Integer = oCommon.getFieldValue(strSQL)
            strSQL = "INSERT INTO kpmkv_pelajar_markah (PelajarID,KolejRecordID,KursusID,Tahun,Semester,Sesi)"
            strSQL += " VALUES ('" & strPelajarID & "','" & ddlKolej.SelectedValue & "','" & ddlKodKursus.SelectedValue & "','" & ddlTahun.SelectedValue & "','" & ddlSemester.SelectedValue & "','" & chkSesi.Text & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function

    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged

        If ddlNegeri.SelectedItem.Value <> "ALL" Then
            strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
        Else
            strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej ORDER BY Nama ASC"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKolej.DataSource = ds
            ddlKolej.DataTextField = "Nama"
            ddlKolej.DataValueField = "RecordID"
            ddlKolej.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kluster_list()
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub
    Protected Sub ddlKluster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKluster.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub
    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
    End Sub

    
End Class