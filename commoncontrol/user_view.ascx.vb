Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class user_view
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                lblPensyarahID.Text = Request.QueryString("PensyarahID")
                '-get pensyarah ic-'
                strSQL = "SELECT MYKAD FROM kpmkv_pensyarah WHERE PensyarahID='" & Request.QueryString("PensyarahID") & "'"
                Dim strMykad As String = oCommon.getFieldValue(strSQL)
                '-get kumpulan pengguna pensyrah-'
                strSQL = "SELECT UserType FROM kpmkv_users WHERE LoginID='" & oCommon.FixSingleQuotes(strMykad) & "'"
                Dim strUserType As String = oCommon.getFieldValue(strSQL)


                kpmkv_Kumpulan_Pengguna()
                If Not strUserType = "" Then
                    ddlKumpulan.SelectedValue = strUserType
                Else
                    ddlKumpulan.Text = "0"
                End If



                LoadPage()
                strRet = BindData(datRespondent)
                strRet = BindData2(datRespondent2)

            End If

        Catch ex As Exception
            'lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_Kumpulan_Pengguna()
        strSQL = "SELECT UserGroup FROM tbl_ctrl_usergroup ORDER BY UserGroup ASC"
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
            lblMsgTop.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub LoadPage()
        strSQL = "SELECT kpmkv_pensyarah.*,kpmkv_status.Status FROM kpmkv_pensyarah "
        strSQL += " LEFT OUTER JOIN kpmkv_status ON kpmkv_pensyarah.StatusID=kpmkv_status.StatusID"
        strSQL += " WHERE kpmkv_pensyarah.PensyarahID='" & Request.QueryString("PensyarahID") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Nama")) Then
                    lblNama.Text = ds.Tables(0).Rows(0).Item("Nama")
                Else
                    lblNama.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Jawatan")) Then
                    lblJawatan.Text = ds.Tables(0).Rows(0).Item("Jawatan")
                Else
                    lblJawatan.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Gred")) Then
                    lblGred.Text = ds.Tables(0).Rows(0).Item("Gred")
                Else
                    lblGred.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MYKAD")) Then
                    lblMYKAD.Text = ds.Tables(0).Rows(0).Item("MYKAD")
                Else
                    lblMYKAD.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tel")) Then
                    lblTel.Text = ds.Tables(0).Rows(0).Item("Tel")
                Else
                    lblTel.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Email")) Then
                    lblEmail.Text = ds.Tables(0).Rows(0).Item("Email")
                Else
                    lblEmail.Text = ""
                End If
                '--
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Jantina")) Then
                    lblJantina.Text = ds.Tables(0).Rows(0).Item("Jantina")
                Else
                    lblJantina.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kaum")) Then
                    lblKaum.Text = ds.Tables(0).Rows(0).Item("Kaum")
                Else
                    lblKaum.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Agama")) Then
                    lblAgama.Text = ds.Tables(0).Rows(0).Item("Agama")
                Else
                    lblAgama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Status")) Then
                    lblStatus2.Text = ds.Tables(0).Rows(0).Item("Status")
                Else
                    lblStatus2.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KolejRecordID")) Then
                    lblKolejID.Text = ds.Tables(0).Rows(0).Item("KolejRecordID")
                Else
                    lblKolejID.Text = ""
                End If

            End If

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
                'lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                'lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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
    Private Function BindData2(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL2, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"

            Else
                divMsg.Attributes("class") = "info"
                'lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            'lblMsg.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_kursus.Tahun, kpmkv_kursus.Sesi, kpmkv_modul.Semester, kpmkv_kluster.NamaKluster ASC"

        tmpSQL = "SELECT kpmkv_kelas.NamaKelas,kpmkv_pensyarah_modul.PensyarahModulID, kpmkv_kursus.Tahun, kpmkv_kursus.Sesi, kpmkv_modul.Semester, kpmkv_kluster.NamaKluster, "
        tmpSQL += " kpmkv_kursus.KodKursus, kpmkv_kursus.NamaKursus, kpmkv_modul.KodModul, kpmkv_modul.NamaModul, kpmkv_modul.JamKredit"
        tmpSQL += " FROM  kpmkv_pensyarah_modul LEFT OUTER JOIN kpmkv_kelas ON kpmkv_pensyarah_modul.KelasID = kpmkv_kelas.KelasID LEFT OUTER  JOIN"
        tmpSQL += " kpmkv_kursus ON kpmkv_pensyarah_modul.KursusID = kpmkv_kursus.KursusID LEFT OUTER  JOIN kpmkv_kluster ON kpmkv_kursus.KlusterID = kpmkv_kluster.KlusterID LEFT OUTER  JOIN"
        tmpSQL += " kpmkv_modul ON kpmkv_pensyarah_modul.ModulID = kpmkv_modul.ModulID LEFT OUTER  JOIN kpmkv_pensyarah ON kpmkv_pensyarah_modul.PensyarahID = kpmkv_pensyarah.PensyarahID"
        strWhere += "  WHERE kpmkv_pensyarah.PensyarahID='" & Request.QueryString("PensyarahID") & "'"


        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function
    Private Function getSQL2() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Tahun ASC"

        tmpSQL = "SELECT kpmkv_pensyarah_matapelajaran.PensyarahMataPelajaranID, kpmkv_pensyarah_matapelajaran.Tahun, kpmkv_pensyarah_matapelajaran.Semester, kpmkv_pensyarah_matapelajaran.Sesi, "
        tmpSQL += " kpmkv_matapelajaran.KodMataPelajaran, kpmkv_matapelajaran.NamaMataPelajaran, kpmkv_kelas.NamaKelas FROM kpmkv_pensyarah_matapelajaran "
        tmpSQL += " LEFT OUTER JOIN kpmkv_pensyarah ON kpmkv_pensyarah_matapelajaran.PensyarahID = kpmkv_pensyarah.PensyarahID LEFT OUTER JOIN"
        tmpSQL += " kpmkv_kelas ON kpmkv_pensyarah_matapelajaran.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN kpmkv_matapelajaran ON kpmkv_pensyarah_matapelajaran.MataPelajaranID= kpmkv_matapelajaran.MataPelajaranId"
        strWhere += "  WHERE kpmkv_pensyarah.PensyarahID='" & Request.QueryString("PensyarahID") & "'"


        getSQL2 = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL2)

        Return getSQL2

    End Function
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub
    Private Sub datRespondent2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent2.PageIndexChanging
        datRespondent2.PageIndex = e.NewPageIndex
        strRet = BindData2(datRespondent2)

    End Sub
    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        Response.Redirect("user.update.aspx?PensyarahID=" & lblPensyarahID.Text)
    End Sub
    '------ kemaskini konfigurasi sistem-------------'

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_pensyarah_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini konfigurasi sistem pensyarah"
                lblMsgTop.Text = "Berjaya mengemaskini konfigurasi sistem pensyarah"
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        '--ddlkumpulan
        If ddlKumpulan.Text = "0" Then
            lblMsg.Text = "Sila pilih Kumpulan Pengguna!"
            ddlKumpulan.Focus()
            Return False
        End If


        Return True
    End Function

    Private Function kpmkv_pensyarah_update() As Boolean

        '-get pensyarah ic-'
        strSQL = "SELECT MYKAD FROM kpmkv_pensyarah WHERE PensyarahID='" & Request.QueryString("PensyarahID") & "'"
        Dim strMykad As String = oCommon.getFieldValue(strSQL)

        '-1.Check user existance in tbl user-'
        strSQL = "SELECT LoginID FROM kpmkv_users WHERE LoginID='" & oCommon.FixSingleQuotes(strMykad) & "'"
        Dim strLoginID As String = oCommon.isExist(strSQL)

        '-2.get nama Kolej in tbl kolej-'
        strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & oCommon.FixSingleQuotes(lblKolejID.Text) & "'"
        Dim strNamaKolej As String = oCommon.getFieldValue(strSQL)
        '-3.get negeri Kolej in tbl kolej-'
        strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE RecordID='" & oCommon.FixSingleQuotes(lblKolejID.Text) & "'"
        Dim strNegeriKolej As String = oCommon.getFieldValue(strSQL)



        If strLoginID = True Then
            If txtKatalaluan.Text = "" Then

                strSQL = "UPDATE kpmkv_users SET UserType= '" & oCommon.FixSingleQuotes(ddlKumpulan.SelectedValue) & "'"
                strSQL += " WHERE LoginID='" & oCommon.FixSingleQuotes(strMykad) & "'"
            Else

                strSQL = "UPDATE kpmkv_users SET UserType= '" & oCommon.FixSingleQuotes(ddlKumpulan.SelectedValue) & "',"
                strSQL += " Pwd='" & oCommon.FixSingleQuotes(txtKatalaluan.Text) & "'"
                strSQL += " WHERE LoginID='" & oCommon.FixSingleQuotes(strMykad) & "'"
            End If

        ElseIf strLoginID = False Then

            If txtKatalaluan.Text = "" Then
                '-- create if user not exist in tbl user-'
                strSQL = " INSERT INTO kpmkv_users(RecordID,LoginID,Pwd,UserType,Nama,Negeri)"
                strSQL += " VALUES('" & oCommon.FixSingleQuotes(lblKolejID.Text) & "','" & oCommon.FixSingleQuotes(lblMYKAD.Text) & "','pwd',"
                strSQL += " '" & oCommon.FixSingleQuotes(ddlKumpulan.SelectedValue) & "','" & oCommon.FixSingleQuotes(strNamaKolej) & "',"
                strSQL += " '" & oCommon.FixSingleQuotes(strNegeriKolej) & "')"
            Else

                strSQL = " INSERT INTO kpmkv_users(RecordID,LoginID,Pwd,UserType,Nama,Negeri)"
                strSQL += " VALUES('" & oCommon.FixSingleQuotes(lblKolejID.Text) & "','" & oCommon.FixSingleQuotes(lblMYKAD.Text) & "','" & oCommon.FixSingleQuotes(txtKatalaluan.Text) & "',"
                strSQL += " '" & oCommon.FixSingleQuotes(ddlKumpulan.SelectedValue) & "','" & oCommon.FixSingleQuotes(strNamaKolej) & "',"
                strSQL += " '" & oCommon.FixSingleQuotes(strNegeriKolej) & "')"

            End If


        End If
        'Response.Write(strSQL)
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            lblMsgTop.Text = "System Error:" & strRet
            Return False
        End If
        Return True
    End Function
End Class