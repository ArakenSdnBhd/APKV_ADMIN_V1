Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class angkagiliran_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsgResult.Text = ""
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

                kpmkv_kodkursus_list()
                ddlKodKursus.Text = "0"

                kpmkv_kelas_list()
                ddlNamaKelas.Text = "0"

                strRet = BindData(datRespondent)
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
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej ORDER BY Jenis"
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
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
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

            '--ALL
            ddlTahun.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
        strSQL += " FROM kpmkv_kelas_kursus INNER JOIN kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID INNER JOIN"
        strSQL += " kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "' AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "'  GROUP BY kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
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
        strSQL = " SELECT kpmkv_kelas.NamaKelas, kpmkv_kelas.KelasID"
        strSQL += " FROM  kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' ORDER BY KelasID"
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
            ddlNamaKelas.Items.Add(New ListItem("PILIH", "0"))

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
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi, kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, "
        tmpSQL += " kpmkv_kursus.KodKursus, kpmkv_kelas.NamaKelas"
        tmpSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN"
        tmpSQL += " kpmkv_status ON kpmkv_pelajar.StatusID = kpmkv_status.StatusID LEFT OUTER JOIN kpmkv_kelas ON kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
        strWhere = " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_pelajar.IsDeleted='N'"

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If
        '--Kod
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KursusID='" & ddlKodKursus.SelectedValue & "'"
        End If
        '--sesi
        If Not ddlNamaKelas.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlNamaKelas.SelectedValue & "'"
        End If


        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ' 'Response.Write(getSQL)

        Return getSQL

    End Function
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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub btnAngkagiliran_Click(sender As Object, e As EventArgs) Handles btnAngkagiliran.Click


        Try
            Dim str As String
            strSQL = "SELECT  TOP 1 AngkaGiliran FROM kpmkv_pelajar WHERE Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.Text & "' "
            strSQL += " AND KursusID='" & ddlKodKursus.SelectedValue & "' AND KelasID='" & ddlNamaKelas.SelectedValue & "' AND KolejRecordID='" & ddlKolej.SelectedValue & "' ORDER BY AngkaGiliran DESC"
            strRet = oCommon.getFieldValue(strSQL)

            If Not strRet = "" Then
                divMsgResult.Attributes("class") = "info"
                lblMsgResult.Text = "AngkaGiliran Telah Dijana!"
                Exit Try
            Else
            End If

            'No. Kolej 0	1
            Dim strMedan123 As String = ""
            strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID='" & ddlKolej.SelectedValue & "'"
            strMedan123 = oCommon.getFieldValue(strSQL)

            'Pengambilan
            Dim strMedan4 As String = ""
            If chkSesi.Text = "1" Then
                strMedan4 = "1"
            Else
                strMedan4 = "2"
            End If

            'Tahun
            Dim strMedan5 As String = ""
            strSQL = "SELECT Kod FROM kpmkv_tahun WHERE Tahun='" & ddlTahun.SelectedValue & "'"
            strMedan5 = oCommon.getFieldValue(strSQL)

            'Kursus W	T	P
            Dim strMedan6_7_8 As String = ""
            strMedan6_7_8 = ddlKodKursus.SelectedItem.Text

            'No Siri Pelajar 0	0	1
            Dim strcheckID As String
            Dim strMedan9_10_11 As String
            strSQL = "SELECT  TOP 1 AngkaGiliran FROM kpmkv_pelajar WHERE Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.Text & "'  AND KursusID='" & ddlKodKursus.SelectedValue & "' AND KolejRecordID='" & ddlKolej.SelectedValue & "' ORDER BY AngkaGiliran DESC"
            strRet = oCommon.getFieldValue(strSQL)
            If strRet = "" Then
                strcheckID = ""
            Else
                strcheckID = strRet.Substring(8, 3)
            End If
            'Dim getNum As String = strcheckID.Substring(9, 3)


            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim row As GridViewRow = datRespondent.Rows(0)
                str = datRespondent.DataKeys(i).Value.ToString


                If Not strcheckID = "" Then
                    strMedan9_10_11 = CInt(strcheckID + 1)
                Else
                    strMedan9_10_11 = "001"
                End If

                'set number

                If (strMedan9_10_11.Length = 1) Then
                    strMedan9_10_11 = "00" + strMedan9_10_11.ToString()

                ElseIf (strMedan9_10_11.Length = 2) Then
                    strMedan9_10_11 = "0" + strMedan9_10_11.ToString()
                End If
                strcheckID = strMedan9_10_11

                Dim strAngkaGiliranBaru As String = strMedan123 + strMedan4 + strMedan5 + strMedan6_7_8 + strMedan9_10_11
                'validate
                strSQL = "SELECT AngkaGiliran FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliranBaru & "' AND PelajarID='" & str & "'"
                If oCommon.isExist(strSQL) = False Then
                    strSQL = "UPDATE kpmkv_pelajar SET AngkaGiliran='" & strAngkaGiliranBaru & "' WHERE PelajarID='" & str & "'"

                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        divMsgResult.Attributes("class") = "error"
                        lblMsgResult.Text = "Jana AngkaGiliran Calon Tidak Berjaya!"
                        Exit Sub
                    End If
                Else
                    divMsgResult.Attributes("class") = "error"
                    lblMsgResult.Text = "Tidak Berjaya! AngkaGiliran Calon sudah wujud."
                End If
            Next


        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

        '  divMsgResult.Attributes("class") = "info"
        '   lblMsgResult.Text = "Jana AngkaGiliran Calon Berjaya!"
        strRet = BindData(datRespondent)
    End Sub
    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click

        Dim str As String
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            'Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")
            str = datRespondent.DataKeys(i).Value.ToString
            strSQL = "UPDATE kpmkv_pelajar SET AngkaGiliran='' WHERE Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.Text & "'"
            strSQL += " AND KursusID='" & ddlKodKursus.SelectedValue & "' AND KolejRecordID='" & ddlKolej.SelectedValue & "' AND PelajarID='" & str & "'"
            'strSQL = "UPDATE kpmkv_pelajar SET AngkaGiliran='' WHERE PelajarID='" & str & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        Next
        divMsgResult.Attributes("class") = "info"
        lblMsgResult.Text = "Jana AngkaGiliran Calon berjaya dibatalkan!"
        strRet = BindData(datRespondent)
    End Sub
    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged

        kpmkv_kolej_list()

    End Sub
    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_jenis_list()
        ddlJenis.Text = "0"
        kpmkv_kolej_list()
        ddlKolej.Text = "0"
    End Sub
    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        ddlKodKursus.Text = "0"
    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        chkSesi.Text = ""
        kpmkv_kodkursus_list()
        ddlKodKursus.Text = "0"
        kpmkv_kelas_list()
        ddlNamaKelas.Text = "0"
    End Sub
End Class