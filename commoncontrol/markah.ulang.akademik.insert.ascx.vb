Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class markah_ulang_akademik_insert
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
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

                kpmkv_semester_list()

                kpmkv_kodkursus_list()

                kpmkv_kelas_list()

                lblMsg.Text = ""
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej  WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
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

            '--ALL
            ddlTahun.Items.Add(New ListItem("-Pilih-", "0"))

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
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
        strSQL += " FROM kpmkv_kelas_kursus INNER JOIN kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID INNER JOIN"
        strSQL += " kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "' "
        strSQL += " AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' GROUP BY kpmkv_kursus.KodKursus,kpmkv_kursus.KursusID"
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
            ' ddlKodKursus.Items.Add(New ListItem("PILIH", "PILIH"))

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

            ddlKelas.DataSource = ds
            ddlKelas.DataTextField = "NamaKelas"
            ddlKelas.DataValueField = "KelasID"
            ddlKelas.DataBind()

            '--ALL
            'ddlNamaKelas.Items.Add(New ListItem("PILIH", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub
    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblMsg.Text = ""

        If (e.CommandName = "Batal") Then
            Dim strPelajarUID = Int32.Parse(e.CommandArgument.ToString())
            Try


                strSQL = "DELETE FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & strPelajarUID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
                If strRet = "0" Then
                    divMsg.Attributes("class") = "info"
                    lblMsg.Text = "Pelajar berjaya dipadamkan"
                Else
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Pelajar tidak berjaya dipadam"

                End If
            Catch ex As Exception
                divMsg.Attributes("class") = "error"
            End Try
        End If
        strRet = BindData(datRespondent)
    End Sub
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar_ulang.PelajarUlangID,kpmkv_pelajar_ulang.PelajarID, kpmkv_pelajar_ulang.Tahun, kpmkv_pelajar_ulang.Semester, kpmkv_pelajar_ulang.Sesi, kpmkv_pelajar_ulang.NamaMataPelajaran, kpmkv_pelajar_ulang.MarkahPB, kpmkv_pelajar_ulang.MarkahPA, kpmkv_pelajar_ulang.Gred, "
        tmpSQL += " kpmkv_pelajar.Tahun, kpmkv_pelajar.Nama, kpmkv_pelajar.Mykad, kpmkv_pelajar.AngkaGiliran, kpmkv_kursus.KodKursus, kpmkv_kelas.NamaKelas FROM  kpmkv_pelajar_ulang LEFT OUTER JOIN"
        tmpSQL += " kpmkv_pelajar ON kpmkv_pelajar_ulang.PelajarID = kpmkv_pelajar.PelajarID LEFT OUTER JOIN"
        tmpSQL += " kpmkv_kursus ON kpmkv_pelajar_ulang.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN"
        tmpSQL += " kpmkv_kelas ON kpmkv_pelajar_ulang.KelasID = kpmkv_kelas.KelasID "
        strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar_ulang.KolejRecordID='" & ddlKolej.Text & "'"

        '--tahun
        If Not ddlTahun.Text = "0" Then
            strWhere += " AND kpmkv_pelajar_ulang.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--semester
        If Not ddlSemester.Text = "0" Then
            strWhere += " AND kpmkv_pelajar_ulang.Semester ='" & ddlSemester.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar_ulang.Sesi ='" & chkSesi.Text & "'"
        End If
        '--kursus
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND kpmkv_pelajar_ulang.KursusID ='" & ddlKodKursus.SelectedValue & "'"
        End If
        '--jantina
        If Not ddlKelas.Text = "" Then
            strWhere += " AND kpmkv_pelajar_ulang.KelasID ='" & ddlKelas.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)


        Return getSQL

    End Function
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        lblMsg.Text = ""
        lblMsgTop.Text = ""
        Dim strPelajarUID As Integer = datRespondent.DataKeys(e.RowIndex).Values("PelajarUlangID")
        Try
            If Not strPelajarUID = Session("strPelajarUID") Then
                strSQL = "Select Gred FROM kpmkv_pelajar_ulang WHERE PelajarUlangID='" & strPelajarUID & "'"
                Dim strGred As String = oCommon.getFieldValue(strSQL)

                If strGred = "NULL" Or strGred = "" Then
                    strSQL = "DELETE FROM kpmkv_pelajar_ulang WHERE PelajarUlangID='" & strPelajarUID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If strRet = "0" Then
                        divTop.Attributes("class") = "info"
                        lblMsgTop.Text = "Calon berjaya dipadamkan"
                        Session("strPelajarUID") = ""
                    Else
                        divTop.Attributes("class") = "error"
                        lblMsgTop.Text = "Calon tidak berjaya dipadam"
                        Session("strPelajarUID") = ""
                    End If
                Else
                    divTop.Attributes("class") = "error"
                    lblMsgTop.Text = "Calon tidak berjaya dipadam. Gred Ulang telah dijana"
                    Session("strPelajarUID") = ""
                End If
            Else
                Session("strPelajarUID") = ""
            End If
        Catch ex As Exception
            divTop.Attributes("class") = "error"
        End Try

        strRet = BindData(datRespondent)

    End Sub
    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        'Response.Redirect("pelajar.ulang.akademik.view.aspx?PelajarID=" & strKeyID)

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
    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""
        strRet = BindData(datRespondent)
        hiddencolumn()

    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
    End Sub

    Private Sub hiddencolumn()

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strPB As TextBox = CType(row.FindControl("MarkahPB"), TextBox)
            Dim strPA As TextBox = CType(row.FindControl("MarkahPA"), TextBox)

            strSQL = "SELECT PB FROM kpmkv_pelajar_ulang WHERE PelajarUlangID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim strPBHide As String = oCommon.getFieldValue(strSQL)

            If strPBHide = "1" Then
                strPB.Enabled = True
            Else
                strPB.Enabled = False
            End If

            strSQL = "SELECT PA FROM kpmkv_pelajar_ulang WHERE PelajarUlangID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim strPAHide As String = oCommon.getFieldValue(strSQL)

            If strPAHide = "1" Then
                strPA.Enabled = True
            Else
                strPA.Enabled = False
            End If
        Next
    End Sub
    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        If ValidateForm() = False Then
            lblMsg.Text = "Sila masukkan NOMBOR 0-100 SAHAJA"
            Exit Sub
        End If

        Try
            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim row As GridViewRow = datRespondent.Rows(i)
                Dim strPBA As TextBox = datRespondent.Rows(i).FindControl("MarkahPB")
                Dim strPAA As TextBox = datRespondent.Rows(i).FindControl("MarkahPA")


                'assign value to integer
                Dim strPB As Integer = strPBA.Text
                Dim strPA As Integer = strPAA.Text

                strSQL = "UPDATE kpmkv_pelajar_ulang SET MarkahPB='" & strPB & "', MarkahPA='" & strPA & "' WHERE PelajarUlangID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
                If strRet = "0" Then
                    divMsg.Attributes("class") = "info"
                    lblMsg.Text = "Berjaya!.Kemaskini markah Ulang  Akademik."
                Else
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Tidak Berjaya!.Kemaskini markah Ulang  Akademik."
                End If
            Next

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try
        Akademik_markah()
        strRet = BindData(datRespondent)
        hiddencolumn()
    End Sub
    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strPB As TextBox = CType(row.FindControl("MarkahPB"), TextBox)
            Dim strPA As TextBox = CType(row.FindControl("MarkahPA"), TextBox)

            If Not strPB.Text.Length = 0 Then
                If oCommon.IsCurrency(strPB.Text) = False Then
                    Return False
                End If
                If CInt(strPB.Text) > 100 Then
                    Return False
                End If
            Else
                strPB.Text = "0"
            End If

            If Not strPA.Text.Length = 0 Then
                If oCommon.IsCurrency(strPA.Text) = False Then
                    Return False
                End If
                If CInt(strPA.Text) > 100 Then
                    Return False
                End If
            Else
                strPA.Text = "0"
            End If
        Next

        Return True
    End Function
    Private Sub Akademik_markah()

        Dim strKodMP As Integer

        Select Case ddlSemester.Text
            Case "1"
                strKodMP = "100"
            Case "2"
                strKodMP = "200"
            Case "3"
                strKodMP = "300"
            Case "4"
                strKodMP = "400"
        End Select

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strPB As TextBox = CType(row.FindControl("MarkahPB"), TextBox)
            Dim strPA As TextBox = CType(row.FindControl("MarkahPA"), TextBox)
            Dim strNamaMataPelajaran As Label = CType(row.FindControl("NamaMataPelajaran"), Label)
            'assign value to integer
            Dim PB1 As Integer = strPB.Text
            Dim PA2 As Integer = strPA.Text
            Dim strGredNama As String = ""
            Dim strpointerNama As String = ""
            Dim strGredMarkah As String = ""
            Dim strMP As String = strNamaMataPelajaran.Text

            Dim strPelajarID As Integer
            Dim PB As String
            Dim PA As String
            Dim PBMarkah As Integer
            Dim PAMarkah As Integer
            Dim Pointer As Integer

            Select Case strNamaMataPelajaran.Text
                Case "BAHASA MELAYU"
                    strGredNama = "BM"
                    strpointerNama = "BahasaMelayu"
                Case "BAHASA INGGERIS"
                    strGredNama = "BI"
                    strpointerNama = "BahasaInggeris"
                Case "SCIENCE"
                    strGredNama = "SC"
                    strpointerNama = "Science"
                Case "SEJARAH"
                    strGredNama = "SJ"
                    strpointerNama = "Sejarah"
                Case "PENDIDIKAN ISLAM"
                    strGredNama = "PI"
                    strpointerNama = "PendidikanIslam"
                Case "PENDIDIKAN MORAL"
                    strGredNama = "PM"
                    strpointerNama = "PendidikanMoral"
                Case "MATHEMATIC"
                    strGredNama = "MT"
                    strpointerNama = "Mathematics"
            End Select

            'get pelajarid from kpmkv_pelajar_ulang
            strSQL = "SELECT PelajarID FROM kpmkv_pelajar_ulang Where  PelajarUlangID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND NamaMataPelajaran='" & strMP & "'"
            strPelajarID = oCommon.getFieldValue(strSQL)

            'get gred from kpmkv_pelajar_markah
            If Not String.IsNullOrEmpty(strGredNama) Then
                strSQL = "SELECT  Gred" & strGredNama & "  FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strGredMarkah = oCommon.getFieldValue(strSQL)
            End If

            strSQL = "SELECT PB FROM kpmkv_matapelajaran WHERE NamaMataPelajaran='" & strMP & "' AND SUBSTRING(KodMataPelajaran,4,3)='" & strKodMP & "' AND Tahun='" & ddlTahun.Text & "'"
            PB = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_matapelajaran WHERE NamaMataPelajaran='" & strMP & "' AND SUBSTRING(KodMataPelajaran,4,3)='" & strKodMP & "' AND Tahun='" & ddlTahun.Text & "'"
            PA = oCommon.getFieldValue(strSQL)

            If Not String.IsNullOrEmpty(PB1) Then
                PBMarkah = oCommon.DoConvertN((PB1 / 100) * CInt(PB), 2)
            End If
            If Not String.IsNullOrEmpty(PA2) Then
                PAMarkah = oCommon.DoConvertN((PA2 / 100) * CInt(PA), 2)
            End If

            Pointer = PBMarkah + PAMarkah

            'untuk tidak hadir sahaja
            If PB1 = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_ulang SET Gred='T' Where PelajarUlangID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND NamaMataPelajaran='" & strMP & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred" & strGredNama & "='" & strGredMarkah & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            ElseIf PA2 = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_ulang SET Gred='T' Where PelajarUlangID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND NamaMataPelajaran='" & strMP & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred" & strGredNama & "='" & strGredMarkah & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Pointer & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK_ULANG'"
                Dim Gred As String = oCommon.getFieldValue(strSQL)
                If Not String.IsNullOrEmpty(Pointer) Then
                    strSQL = "UPDATE kpmkv_pelajar_ulang SET Gred='" & Gred & "' Where PelajarUlangID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND NamaMataPelajaran='" & strMP & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
                If Gred = strGredMarkah Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET Gred" & strGredNama & "='" & strGredMarkah & "'," & strpointerNama & "='" & Pointer & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf Gred = "C" Then
                    'update bm
                    strSQL = "UPDATE kpmkv_pelajar_markah SET Gred" & strGredNama & "='C'," & strpointerNama & "='" & Pointer & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    strSQL = "UPDATE kpmkv_pelajar_markah SET Gred" & strGredNama & "='" & strGredMarkah & "'," & strpointerNama & "='" & Pointer & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If
            '------BM SETARA ULANG 09112016
            If ddlSemester.Text = "4" And strpointerNama = "BahasaMelayu" Then
                Dim BerterusanBM As Integer
                Dim AkhiranBM As Integer
                Dim PB4 As Integer
                Dim PABmSetara As Integer
                Dim PAPB4 As Integer

                Dim B_BahasaMelayuSem1 As Integer
                Dim B_BahasaMelayuSem2 As Integer
                Dim B_BahasaMelayuSem3 As Integer
                Dim B_BahasaMelayuSem4 As Integer
                Dim A_BahasaMelayuSem4 As Integer
                Dim PointerBMSetara As Integer

                strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A04'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                BerterusanBM = oCommon.getFieldValue(strSQL)

                strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A04'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                AkhiranBM = oCommon.getFieldValue(strSQL)

                'get mykad
                strSQL = " SELECT Mykad FROM kpmkv_pelajar"
                strSQL += " WHERE PelajarID='" & strPelajarID & "'"
                Dim strMYKAD1 As String = oCommon.getFieldValue(strSQL)

                'get pelajarid
                strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                strSQL += " WHERE Tahun='" & ddlTahun.Text & "' AND Semester='1'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strSQL += " AND Mykad='" & strMYKAD1 & "'"
                Dim strPelajarID1 As String = oCommon.getFieldValue(strSQL)
                'get bm sem 1
                strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID1 & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='1'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_BahasaMelayuSem1 = oCommon.getFieldValue(strSQL)
                'round up
                B_BahasaMelayuSem1 = Math.Ceiling(B_BahasaMelayuSem1)
                '----------------------------------------------------------------------------

                'get pelajarid
                strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                strSQL += " WHERE Tahun='" & ddlTahun.Text & "' AND Semester='2'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strSQL += " AND Mykad='" & strMYKAD1 & "'"
                Dim strPelajarID2 As String = oCommon.getFieldValue(strSQL)
                'get Bm sem 2
                strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID2 & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='2'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_BahasaMelayuSem2 = oCommon.getFieldValue(strSQL)
                'round up
                B_BahasaMelayuSem2 = Math.Ceiling(B_BahasaMelayuSem2)


                'get pelajarid
                strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                strSQL += " WHERE Tahun='" & ddlTahun.Text & "' AND Semester='3'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strSQL += " AND Mykad='" & strMYKAD1 & "'"
                Dim strPelajarID3 As String = oCommon.getFieldValue(strSQL)
                'get bm sem 3
                strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID3 & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='3'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_BahasaMelayuSem3 = oCommon.getFieldValue(strSQL)
                'round up
                B_BahasaMelayuSem3 = Math.Ceiling(B_BahasaMelayuSem3)

                'get bm sem 4 PB
                strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                B_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

                'get bm sem 4 PA
                strSQL = "Select A_BahasaMelayu3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                A_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

                Dim Kertas1 As Integer = 0
                Dim Kertas2 As Integer = 0

                strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.getFieldValueEx(strSQL)

                ''--get user info
                Dim ar_Kertas As Array
                ar_Kertas = strRet.Split("|")

                If (String.IsNullOrEmpty(ar_Kertas(0).ToString())) Then
                    Kertas1 = 0
                Else
                    Kertas1 = ar_Kertas(0)
                End If

                If (String.IsNullOrEmpty(ar_Kertas(1).ToString())) Then
                    Kertas2 = 0
                Else
                    Kertas2 = ar_Kertas(1)
                End If

                If Not (B_BahasaMelayuSem4) = "-1" Then
                    PB4 = Math.Ceiling((B_BahasaMelayuSem4 / 100) * BerterusanBM)
                    PABmSetara = Math.Ceiling((A_BahasaMelayuSem4 / 100) * 40)
                    PAPB4 = Math.Ceiling(((Kertas1 + Kertas2 + PABmSetara) / 280) * AkhiranBM)
                    'PAPB4 = Math.Ceiling(PAPB * AkhiranBM)

                    'gred sem 4 
                    Dim PointSem4 As Integer = Math.Ceiling(PB4 + PAPB4)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointSem4 & "' Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    PointerBMSetara = Math.Ceiling((((B_BahasaMelayuSem1 / 100) * 10) + ((B_BahasaMelayuSem2 / 100) * 10) + ((B_BahasaMelayuSem3 / 100) * 10) + ((PointSem4 / 100) * 70)))

                    strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='" & PointerBMSetara & "' Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                ElseIf (B_BahasaMelayuSem4) = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If

        Next
    End Sub
    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()

    End Sub
End Class
