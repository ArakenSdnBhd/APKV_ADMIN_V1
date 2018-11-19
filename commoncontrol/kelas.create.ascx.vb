Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class kelas_create
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

                lblMsg.Text = ""
                
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
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
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                DivTopMsg.Attributes("class") = "error"
                lblMsgTop.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsgTop.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Tahun ASC"

        tmpSQL = "SELECT kpmkv_kolej.Nama AS NamaKolej,kpmkv_kelas.* FROM kpmkv_kelas LEFT OUTER JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_kelas.KolejRecordID"
        strWhere = " WHERE kpmkv_kelas.IsDeleted='N' AND kpmkv_kelas.KolejRecordID='" & ddlKolej.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

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
            If kpmkv_kelas_create() = True Then
                divMsg.Attributes("class") = "info"
                DivTopMsg.Attributes("class") = "info"
                lblMsgTop.Text = "Kelas berjaya didaftarkan"
                lblMsg.Text = "Kelas berjaya didaftarkan"
            Else
                divMsg.Attributes("class") = "error"
                DivTopMsg.Attributes("class") = "error"
                lblMsgTop.Text = "Kelas tidak berjaya didaftarkan"
                lblMsg.Text = "Kelas tidak berjaya didaftarkan"
            End If
            strRet = BindData(datRespondent)

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean

        '--txtNama
        If txtNamaKelas.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Kelas!"
            txtNamaKelas.Focus()
            Return False
        End If

        strSQL = "SELECT * FROM kpmkv_kelas WHERE Tahun='" & ddlTahun.SelectedValue & "' AND NamaKelas='" & txtNamaKelas.Text & "' and KolejRecordID='" & ddlKolej.Text & "' AND IsDeleted='N'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Nama Kelas telah digunakan. Sila masukkan Nama Kelas yang baru."
            Return False
        End If

        Return True
    End Function
    Private Function kpmkv_kelas_create() As Boolean
        'kpmkv_kelas
        strSQL = "INSERT INTO kpmkv_kelas(KolejRecordID,Tahun,NamaKelas,IsDeleted) "
        strSQL += "VALUES ('" & ddlKolej.Text & "','" & ddlTahun.SelectedValue & "','" & oCommon.FixSingleQuotes(txtNamaKelas.Text) & "','N')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        lblMsg.Text = ""
        lblMsgTop.Text = ""
        Dim strKelasID As Integer = datRespondent.DataKeys(e.RowIndex).Values("KelasID")
        Try
            If Not strKelasID = Session("KelasID") Then
                'kpmkv_pelajar
                strSQL = "SELECT KelasID FROM kpmkv_pelajar WHERE KelasID='" & strKelasID & "'"
                If oCommon.isExist(strSQL) = True Then
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada Calon"
                    lblMsgTop.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada Calon"
                    Session("strKelasID") = ""
                    Exit Sub
                End If

                'kpmkv_pelajar_history
                strSQL = "SELECT KelasID FROM kpmkv_pelajar_history WHERE KelasID='" & strKelasID & "'"
                If oCommon.isExist(strSQL) = True Then
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada sejarah calon"
                    lblMsgTop.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada sejarah calon"
                    Session("strKelasID") = ""
                    Exit Sub
                End If

                'kpmkv_pelajar_ulang
                strSQL = "SELECT KelasID FROM kpmkv_pelajar_ulang WHERE KelasID='" & strKelasID & "'"
                If oCommon.isExist(strSQL) = True Then
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada calon ulang"
                    lblMsgTop.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada calon ulang"
                    Session("strKelasID") = ""
                    Exit Sub
                End If

                'kpmkv_pensyarah_matapelajaran
                strSQL = "SELECT KelasID FROM kpmkv_pensyarah_matapelajaran WHERE KelasID='" & strKelasID & "'"
                If oCommon.isExist(strSQL) = True Then
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada Matapelajaran"
                    lblMsgTop.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada matapelajaran"
                    Session("strKelasID") = ""
                    Exit Sub
                End If

                'kpmkv_pensyarah_modul
                strSQL = "SELECT KelasID FROM kpmkv_pensyarah_modul WHERE KelasID='" & strKelasID & "'"
                If oCommon.isExist(strSQL) = True Then
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada modul"
                    lblMsgTop.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada modul"
                    Session("strKelasID") = ""
                    Exit Sub
                End If

                'kpmkv_pensyarah_modul_history()
                strSQL = "SELECT KelasID FROM kpmkv_pensyarah_modul_history WHERE KelasID='" & strKelasID & "'"
                If oCommon.isExist(strSQL) = True Then
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada sejarah modul"
                    lblMsgTop.Text = "Kelas tidak berjaya dipadam. Rekod kelas masih wujud pada sejarah modul"
                    Session("strKelasID") = ""
                    Exit Sub
                End If

                'delete 
                strSQL = "DELETE FROM kpmkv_kelas WHERE KelasID='" & strKelasID & "'"

                If strRet = oCommon.ExecuteSQL(strSQL) = 0 Then
                    divMsg.Attributes("class") = "info"
                    lblMsg.Text = "Kelas berjaya dipadamkan"
                    Session("strKelasID") = ""
                Else
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kelas tidak berjaya dipadamkan"
                    Session("strKelasID") = ""
                End If
            Else
                Session("strKelasID") = ""
            End If
        Catch ex As Exception
            divMsg.Attributes("class") = "error"
        End Try

        strRet = BindData(datRespondent)
    End Sub
    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value
        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub
    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
        ddlKolej.Text = 0
    End Sub
   
End Class