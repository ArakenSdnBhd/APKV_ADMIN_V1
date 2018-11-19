Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class kelas_kursus
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

                kpmkv_kluster_list()
                ddlKluster.Text = "0"

                kpmkv_kodkursus_list()
                ddlKodKursus.Text = "0"

                kpmkv_kelas_list()
                ddlNamaKelas.Text = "0"

                lblMsg.Text = ""
                lblMsgTop.Text = ""
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
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
        If ddlNegeri.SelectedItem.Value <> "ALL" Then
            strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej  WHERE Negeri='" & ddlNegeri.Text & "' AND Jenis='" & ddlJenis.Text & "'"
        Else
            strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej  ORDER BY Nama ASC"
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

            '--ALL
            ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY TahunID"
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
    Private Sub kpmkv_kluster_list()
        strSQL = "SELECT KlusterID,NamaKluster FROM kpmkv_kluster  WHERE Tahun = '" & ddlTahun.Text & "' AND IsDeleted='N' ORDER BY NamaKluster"
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

        strSQL = "SELECT kpmkv_kursus.KodKursus,kpmkv_kursus.KursusID FROM kpmkv_kursus_kolej LEFT OUTER JOIN kpmkv_kursus "
        strSQL += " ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN kpmkv_kluster "
        strSQL += " ON kpmkv_kursus.KlusterID = kpmkv_kluster.KlusterID"
        strSQL += " WHERE kpmkv_kursus_kolej.KolejRecordID='" & ddlKolej.Text & "' AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "' AND kpmkv_kursus.Sesi='" & chkSesi.Text & "'  AND kpmkv_kluster.KlusterID='" & ddlKluster.Text & "' GROUP BY kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"

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
        strSQL = " SELECT NamaKelas, KelasID FROM kpmkv_kelas"
        strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "'  AND Tahun= '" & ddlTahun.Text & "' ORDER BY  NamaKelas"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

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
        Dim strOrder As String = " ORDER BY kpmkv_kolej.Nama,kpmkv_kursus.Tahun, kpmkv_kursus.Sesi,kpmkv_kursus.KodKursus ASC"

        tmpSQL = "SELECT kpmkv_kolej.Nama AS NamaKolej,kpmkv_kelas_kursus.KelasKursusID, kpmkv_kursus.Tahun, kpmkv_kursus.Sesi, kpmkv_kursus.KodKursus, kpmkv_kelas.NamaKelas"
        tmpSQL += " FROM  kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        tmpSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strWhere = " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.Text & "'  AND kpmkv_kelas_kursus.IsDeleted='N'"

        getSQL = tmpSQL & strWhere & strOrder

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

    Protected Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""
        Try
            'duplicate
            'validate
            strSQL = "SELECT kpmkv_kelas.KelasID FROM  kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON "
            strSQL += " kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
            strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE KelasID ='" & ddlNamaKelas.Text & "' AND KursusID='" & ddlKodKursus.Text & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsgTop.Text = "Kelas telah didaftarkan.Sila Pilih Kelas baru"
                Exit Sub
            Else
                'insert kpmkv_kursus_txn table
                strSQL = "INSERT INTO kpmkv_kelas_kursus(KursusID,KelasID,IsDeleted)"
                strSQL += "VALUES ('" & ddlKodKursus.Text & "','" & ddlNamaKelas.Text & "','N')"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then
                    divMsg.Attributes("class") = "info"
                    lblMsg.Text = "Penetapan kelas bagi kursus berjaya didaftarkan"
                Else
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Penetapan kelas bagi Kursus tidak berjaya didaftarkan"
                End If
            End If
            
            strRet = BindData(datRespondent)

        Catch ex As Exception
            divMsg.Attributes("class") = "error"

        End Try
    End Sub
    Protected Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value
        Dim strKelasID As String = ""
        Try
            'cheking data
            strSQL = "SELECT KelasID FROM kpmkv_kelas_kursus WHERE KelasKursusID='" & strKeyID & "'"
            strKelasID = oCommon.getFieldValue(strSQL)

            'kpmkv_pelajar
            strSQL = "SELECT KelasID FROM kpmkv_pelajar WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam. Rekod Kelas masih wujud pada Calon"
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Kelas masih wujud pada Calon"
                Exit Sub
            End If

            'kpmkv_pelajar_history
            strSQL = "SELECT KelasID FROM kpmkv_pelajar_history WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Sejarah Calon."
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Sejarah Calon"
                Exit Sub
            End If

            'kpmkv_pelajar_ulang
            strSQL = "SELECT KelasID FROM kpmkv_pelajar_ulang WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Calon Ulang."
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Calon Ulang."
                Exit Sub
            End If

            'kpmkv_pensyarah_matapelajaran
            strSQL = "SELECT KelasID FROM kpmkv_pensyarah_matapelajaran WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Matapelajaran."
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Matapelajaran."
                Exit Sub
            End If

            'kpmkv_pensyarah_modul
            strSQL = "SELECT KelasID FROM kpmkv_pensyarah_modul WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Modul"
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Modul"
                Exit Sub
            End If

            'kpmkv_pensyarah_modul_history()
            strSQL = "SELECT KelasID FROM kpmkv_pensyarah_modul_history WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Sejarah Modul"
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Sejarah Modul"
                Exit Sub
            End If

            strSQL = "DELETE FROM kpmkv_kelas_kursus WHERE KelasKursusID='" & strKeyID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Rekod Kelas Mengikut Kursus berjaya dipadamkan"
                lblMsgTop.Text = "Rekod Kelas Mengikut Kursus berjaya dipadamkan"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod Kelas Mengikut Kursus tidak berjaya dipadamkan"
                lblMsgTop.Text = "Rekod Kelas Mengikut Kursus tidak berjaya dipadamkan"

            End If

            ''debug
            'Response.Write(strSQL)
            strRet = BindData(datRespondent)

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
        End Try
    End Sub
    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblMsg.Text = ""
        lblMsgTop.Text = ""
        If (e.CommandName = "padam") = True Then

            Dim strkeyid = Int32.Parse(e.CommandArgument.ToString())

            Dim strKelasID As String = ""

            'cheking data
            strSQL = "SELECT KelasID FROM kpmkv_kelas_kursus WHERE KelasKursusID='" & strkeyid & "'"
            strKelasID = oCommon.getFieldValue(strSQL)

            'kpmkv_pelajar
            strSQL = "SELECT KelasID FROM kpmkv_pelajar WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam. Rekod Kelas masih wujud pada Calon"
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Kelas masih wujud pada Calon"
                Exit Sub
            End If

            'kpmkv_pelajar_history
            strSQL = "SELECT KelasID FROM kpmkv_pelajar_history WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Sejarah Calon."
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Sejarah Calon"
                Exit Sub
            End If

            'kpmkv_pelajar_ulang
            strSQL = "SELECT KelasID FROM kpmkv_pelajar_ulang WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Calon Ulang."
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Calon Ulang."
                Exit Sub
            End If

            'kpmkv_pensyarah_matapelajaran
            strSQL = "SELECT KelasID FROM kpmkv_pensyarah_matapelajaran WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Matapelajaran."
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Matapelajaran."
                Exit Sub
            End If

            'kpmkv_pensyarah_modul
            strSQL = "SELECT KelasID FROM kpmkv_pensyarah_modul WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Modul"
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Modul"
                Exit Sub
            End If

            'kpmkv_pensyarah_modul_history()
            strSQL = "SELECT KelasID FROM kpmkv_pensyarah_modul_history WHERE KelasID='" & strKelasID & "'"
            If oCommon.isExist(strSQL) = True Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Sejarah Modul"
                lblMsgTop.Text = "Kelas tidak berjaya dipadam.Rekod kelas masih wujud pada Sejarah Modul"
                Exit Sub
            End If

            strSQL = "DELETE FROM kpmkv_kelas_kursus WHERE KelasKursusID='" & strkeyid & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Rekod Kelas Mengikut Kursus berjaya dipadamkan"
                lblMsgTop.Text = "Rekod Kelas Mengikut Kursus berjaya dipadamkan"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod Kelas Mengikut Kursus tidak berjaya dipadamkan"
                lblMsgTop.Text = "Rekod Kelas Mengikut Kursus tidak berjaya dipadamkan"

            End If

        End If
        strRet = BindData(datRespondent)

    End Sub
    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
    End Sub
    Protected Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_kluster_list()
        kpmkv_kodkursus_list()

    End Sub
    Protected Sub ddlKluster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKluster.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub

End Class