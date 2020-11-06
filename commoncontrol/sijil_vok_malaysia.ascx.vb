Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Security.Cryptography



Public Class sijil_vok_malaysia1
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


                kpmkv_jenis_list()


                kpmkv_kolej_list()


                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_kelas_list()

                kpmkv_kodkursus_list()

                kpmkv_Pengarah_list()

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



        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "' ORDER BY Nama"
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

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID FROM kpmkv_kursus_kolej LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kursus_kolej.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.SelectedValue & "' "
        strSQL += " AND kpmkv_kursus.Sesi='" & chkSesi.Text & "' GROUP BY kpmkv_kursus.KodKursus,kpmkv_kursus.KursusID"
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

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kelas_list()
        strSQL = " SELECT kpmkv_kelas.NamaKelas, kpmkv_kelas.KelasID FROM kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' AND kpmkv_kursus.Tahun= '" & ddlTahun.SelectedValue & "' ORDER BY  kpmkv_kelas.NamaKelas"
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

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_Pengarah_list()
        strSQL = "SELECT ID,NamaPengarah FROM kpmkv_config_pengarahPeperiksaan"
        strSQL += " WHERE Status='AKTIF' ORDER BY NamaPengarah ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSign.DataSource = ds
            ddlSign.DataTextField = "NamaPengarah"
            ddlSign.DataValueField = "ID"
            ddlSign.DataBind()


        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        status_cetakan()
    End Sub

    Private Sub status_cetakan()

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

            strSQL = "SELECT statusID FROM kpmkv_status_cetak WHERE PelajarID = '" & strkey & "'"
            Dim IDExist As String = oCommon.getFieldValue(strSQL)

            Dim lblStatusCetak As Label = datRespondent.Rows(i).FindControl("lblStatusCetak")

            If Not IDExist = "" Then

                strSQL = "SELECT svm FROM kpmkv_status_cetak WHERE statusID = '" & IDExist & "'"
                Dim svm As String = oCommon.getFieldValue(strSQL)

                If svm = "1" Then

                    lblStatusCetak.Text = "Telah Dicetak"

                End If

            End If

        Next

    End Sub

    Private Sub statusCetak()

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

            strSQL = "SELECT TotalPrint FROM kpmkv_sijil_RunningNo WHERE PelajarID = '" & strkey & "'"
            Dim TotalPrint As String = oCommon.getFieldValue(strSQL)

            Dim cetakStatus As Label = datRespondent.Rows(i).FindControl("lblStatus")

            If Not TotalPrint = "" Then

                If TotalPrint > 0 Then

                    cetakStatus.Text = "Telah Dicetak"

                End If

            End If


        Next

    End Sub

    Private Sub btnKemaskini_Click(sender As Object, e As EventArgs) Handles btnKemaskini.Click

        lblMsg.Text = ""

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

            strSQL = " SELECT RunningNo FROM kpmkv_sijil_RunningNo WHERE PelajarID = '" & strkey & "'"
            Dim RunNo As String = oCommon.getFieldValue(strSQL)

            If RunNo = "" Then

                ''isbmtahun
                strSQL = " SELECT isBMTahun FROM kpmkv_pelajar WHERE PelajarID = '" & strkey & "'"
                Dim strIsBMTahun As String = oCommon.getFieldValue(strSQL)

                ''kod
                strSQL = " SELECT Kod FROM kpmkv_tahun WHERE Tahun = '" & strIsBMTahun & "'"
                Dim strKod As String = oCommon.getFieldValue(strSQL)

                ''digit
                strSQL = " SELECT RunningNoDigit FROM kpmkv_tahun WHERE Tahun = '" & strIsBMTahun & "'"
                Dim strRunNoDigit As String = oCommon.getFieldValue(strSQL)

                ''lastrunningno
                strSQL = " SELECT LastRunningNo FROM kpmkv_tahun WHERE Tahun = '" & strIsBMTahun & "'"
                Dim strLastRunNo As Integer = oCommon.getFieldValue(strSQL)
                strLastRunNo = strLastRunNo + 1

                ''INSERT 
                strSQL = " INSERT INTO kpmkv_sijil_RunningNo (PelajarID, RunningNo, TotalPrint) VALUES ('" & strkey & "', '" & strKod & "   " & strLastRunNo.ToString("D" & strRunNoDigit) & "', 0)"
                strRet = oCommon.ExecuteSQL(strSQL)

                ''UPDATE
                strSQL = " UPDATE kpmkv_tahun SET LastRunningNo = '" & strLastRunNo & "' WHERE Tahun = '" & strIsBMTahun & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

        Next

        strRet = BindData(datRespondent)

    End Sub

    Private Sub tbl_menu_check()

        Dim str As String
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

            str = datRespondent.DataKeys(i).Value.ToString
            Dim strMykad As String = CType(datRespondent.Rows(i).FindControl("Mykad"), Label).Text

            strSQL = "SELECT KelasID FROM kpmkv_pelajar Where Mykad='" & strMykad & "' AND IsDeleted='N' AND KelasID IS NOT NULL"
            If oCommon.isExist(strSQL) = False Then
                cb.Checked = True
            End If
        Next

    End Sub
    Protected Sub CheckUncheckAll(sender As Object, e As System.EventArgs)

        Dim chk1 As CheckBox
        chk1 = DirectCast(datRespondent.HeaderRow.Cells(0).FindControl("chkAll"), CheckBox)
        For Each row As GridViewRow In datRespondent.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("chkSelect"), CheckBox)
            chk.Checked = chk1.Checked
        Next

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
                lblMsg.Text = "Jumlah Rekod#: " & myDataSet.Tables(0).Rows.Count
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
        tmpSQL += " kpmkv_kluster.NamaKluster, kpmkv_kursus.NamaKursus, kpmkv_pelajar.Kaum, kpmkv_pelajar.Jantina, kpmkv_pelajar.Agama, kpmkv_status.Status, kpmkv_kelas.NamaKelas,"
        tmpSQL += " kpmkv_svm.PNGKA ,kpmkv_svm.PNGKV,kpmkv_svm.GredBMSetara,"
        tmpSQL += " kpmkv_sijil_RunningNo.RunningNo"
        tmpSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN kpmkv_kluster ON kpmkv_kursus.KlusterID=kpmkv_kluster.KlusterID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_status ON kpmkv_pelajar.StatusID = kpmkv_status.StatusID LEFT OUTER JOIN kpmkv_kelas ON kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_svm ON kpmkv_svm.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_sijil_RunningNo ON kpmkv_sijil_RunningNo.PelajarID = kpmkv_pelajar.PelajarID"
        strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_pelajar.Semester ='4'"

        '--tahun
        If Not ddlTahun.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If

        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If
        '--kodkursus
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
        End If
        '--NamaKelas
        If Not ddlNamaKelas.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlNamaKelas.SelectedValue & "'"
        End If
        '--txtNama
        If Not txtNama.Text.Length = 0 Then
            strWhere += " AND kpmkv_pelajar.Nama LIKE '%" & oCommon.FixSingleQuotes(txtNama.Text) & "%'"
        End If

        '--txtMYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND kpmkv_pelajar.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        '--txtAngkaGiliran
        If Not txtAngkaGiliran.Text.Length = 0 Then
            strWhere += " AND kpmkv_pelajar.AngkaGiliran LIKE '%" & oCommon.FixSingleQuotes(txtAngkaGiliran.Text) & "%'"
        End If
        If Not ddlStatus.SelectedValue = "" Then
            If ddlStatus.SelectedValue = "SETARA" Then
                strWhere += "   AND kpmkv_SVM.LayakSVM = '1' 
                                AND kpmkv_SVM.GredBMSetara IN ('C', 'C+', 'B-', 'B', 'B+', 'A-', 'A', 'A+')"
            ElseIf ddlStatus.SelectedValue = "TIDAK SETARA" Then
                strWhere += "   AND kpmkv_SVM.LayakSVM = '1' 
                                AND kpmkv_SVM.GredBMSetara IN ('D', 'E', 'G')"
            End If
        End If
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

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

    Protected Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
    End Sub

    Private Function Encrypt(qrString As String) As String

        Dim encryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(qrString)

        Using encryptor As Aes = Aes.Create()

            Dim rfc As New Rfc2898DeriveBytes(encryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = rfc.GetBytes(32)
            encryptor.IV = rfc.GetBytes(16)

            Using ms As New MemoryStream()

                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)

                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()

                End Using

                qrString = Convert.ToBase64String(ms.ToArray())

            End Using

        End Using

        Return qrString

    End Function


    Protected Sub btnPrintSlip_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrintSlip.Click

        Dim myDocument As New Document(PageSize.A4)

        HttpContext.Current.Response.ContentType = "application/pdf"
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SijilVokMalaysia.pdf")
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

        myDocument.Open()

        ''--draw spacing
        Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
        Dim imgSpacing As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imgdrawSpacing)
        imgSpacing.Alignment = iTextSharp.text.Image.LEFT_ALIGN  'left
        imgSpacing.Border = 0

        '1'--start here
        strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & ddlKolej.SelectedValue & "'"
        Dim strKolejnama As String = oCommon.getFieldValue(strSQL)

        'kolejnegeri
        strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE Nama='" & strKolejnama & "'"
        Dim strKolejnegeri As String = oCommon.getFieldValue(strSQL)


        strSQL = "SELECT kpmkv_pelajar.PelajarID,kpmkv_pelajar.MYKAD,kpmkv_pelajar.Nama, kpmkv_pelajar.AngkaGiliran, "
        strSQL += " kpmkv_kursus.KodKursus,kpmkv_kluster.NamaKluster, kpmkv_kursus.NamaKursus,kpmkv_pelajar.isBMTahun,"
        strSQL += " kpmkv_svm.PNGKA ,kpmkv_svm.PNGKV"

        strSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID "
        strSQL += " LEFT OUTER JOIN kpmkv_kluster On kpmkv_kursus.KlusterID=kpmkv_kluster.KlusterID"
        strSQL += " LEFT OUTER JOIN kpmkv_status On kpmkv_pelajar.StatusID = kpmkv_status.StatusID"
        strSQL += " LEFT OUTER JOIN kpmkv_kelas On kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
        strSQL += " LEFT OUTER JOIN kpmkv_svm On kpmkv_svm.PelajarID = kpmkv_pelajar.PelajarID"
        strSQL += " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' "
        strSQL += " And kpmkv_pelajar.KolejRecordID ='" & ddlKolej.SelectedValue & "' "
        strSQL += " AND kpmkv_pelajar.Semester ='4'"

        If Not ddlTahun.Text = "" Then
            strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If

        If Not chkSesi.Text = "" Then
            strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If

        If Not ddlKodKursus.Text = "" Then
            strSQL += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
        End If

        If Not ddlNamaKelas.Text = "" Then
            strSQL += " AND kpmkv_pelajar.KelasID ='" & ddlNamaKelas.SelectedValue & "'"
        End If

        If Not txtNama.Text.Length = 0 Then
            strSQL += " AND kpmkv_pelajar.Nama LIKE '%" & oCommon.FixSingleQuotes(txtNama.Text) & "%'"
        End If

        If Not txtMYKAD.Text.Length = 0 Then
            strSQL += " AND kpmkv_pelajar.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        If Not txtAngkaGiliran.Text.Length = 0 Then
            strSQL += " AND kpmkv_pelajar.AngkaGiliran LIKE '%" & oCommon.FixSingleQuotes(txtAngkaGiliran.Text) & "%'"
        End If

        If Not ddlStatus.SelectedValue = "" Then
            If ddlStatus.SelectedValue = "SETARA" Then
                strSQL += " AND kpmkv_SVM.LayakSVM = '1' 
                                AND kpmkv_SVM.GredBMSetara IN ('C', 'C+', 'B-', 'B', 'B+', 'A-', 'A', 'A+')"
            ElseIf ddlStatus.SelectedValue = "TIDAK SETARA" Then
                strSQL += " AND kpmkv_SVM.LayakSVM = '1' 
                                AND kpmkv_SVM.GredBMSetara IN ('D', 'E', 'G')"
            End If
        End If

        strSQL += " ORDER BY kpmkv_pelajar.Nama ASC"

        strRet = oCommon.ExecuteSQL(strSQL)

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

            Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

            If cb.Checked = True Then

                Dim strkey As String = ds.Tables(0).Rows(i).Item(0).ToString

                ''UPDATE kpmkv_status_cetak
                strSQL = "SELECT statusID FROM kpmkv_status_cetak WHERE PelajarID = '" & strkey & "'"
                Dim IDExist As String = oCommon.getFieldValue(strSQL)

                If IDExist = "" Then

                    strSQL = "INSERT INTO kpmkv_status_cetak (PelajarID, svm, slipBM, slipSJ, Transkrip) VALUES ('" & strkey & "', '0', '0', '0', '0')"
                    strRet = oCommon.ExecuteSQL(strSQL)

                Else

                    strSQL = "UPDATE kpmkv_status_cetak SET svm = '1' WHERE statusID = '" & IDExist & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If

                Dim encryptedStrkey As String = HttpUtility.UrlEncode(Encrypt(strkey.Trim()))

                Dim strmykad As String = ds.Tables(0).Rows(i).Item(1).ToString
                Dim strname As String = ds.Tables(0).Rows(i).Item(2).ToString
                Dim strag As String = ds.Tables(0).Rows(i).Item(3).ToString
                Dim strkodKursus As String = ds.Tables(0).Rows(i).Item(4).ToString
                Dim strbidang As String = ds.Tables(0).Rows(i).Item(5).ToString
                Dim strprogram As String = ds.Tables(0).Rows(i).Item(6).ToString
                Dim strTahun As String = ds.Tables(0).Rows(i).Item(7).ToString
                Dim strPNGKA As String = ds.Tables(0).Rows(i).Item(8).ToString
                Dim strPNGKV As String = ds.Tables(0).Rows(i).Item(9).ToString
                Dim strStatus As String = ""
                If ddlStatus.SelectedValue = "SETARA" Then
                    strStatus = "SETARA"
                ElseIf ddlStatus.SelectedValue = "TIDAK SETARA" Then
                    strStatus = "TIDAK SETARA"
                End If
                ''getting data end

                Dim table As New PdfPTable(3)
                table.WidthPercentage = 101
                table.SetWidths({42, 16, 42})
                table.DefaultCell.Border = 0


                myDocument.Add(table)

                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)

                ''PROFILE STARTS HERE

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim cell = New PdfPCell()
                Dim cetak = Environment.NewLine & ""

                ''NAMA
                table = New PdfPTable(4)
                table.WidthPercentage = 101
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "NAMA"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = " : "
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = strname
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)




                ''NO. KAD PENGENALAN
                table = New PdfPTable(4)
                table.WidthPercentage = 101
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "NO.KAD PENGENALAN"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = " : "
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = strmykad
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)




                ''ANGKA GILIRAN
                table = New PdfPTable(4)
                table.WidthPercentage = 101
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "ANGKA GILIRAN"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = " : "
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = strag
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)




                ''INSTITUSI
                table = New PdfPTable(4)
                table.WidthPercentage = 101
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "INSTITUSI"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = " : "
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = strKolejnama & ", " & strKolejnegeri
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)




                ''KLUSTER
                table = New PdfPTable(4)
                table.WidthPercentage = 101
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "KLUSTER"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = " : "
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = strbidang
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)




                ''KURSUS
                table = New PdfPTable(4)
                table.WidthPercentage = 101
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "KURSUS"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = " : "
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = strprogram
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)

                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)

                ''profile ends here
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                strSQL = "select GredBMSetara from kpmkv_pelajar_markah where PelajarID = '" & strkey & "'"
                strSQL += " AND Tahun='" & ddlTahun.SelectedValue & "'"
                strSQL += " AND Sesi='" & chkSesi.SelectedValue & "'"
                Dim strgred As String = oCommon.getFieldValue(strSQL)
                If strgred = "" Then
                    strgred = ""
                End If


                table = New PdfPTable(4)
                table.WidthPercentage = 101
                table.SetWidths({0, 75, 15, 5})
                table.DefaultCell.Border = 0

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "BAHASA MELAYU KOLEJ VOKASIONAL 1104"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "GRED " & strgred
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                Debug.WriteLine(cetak)
                myDocument.Add(table)


                'Kluster---------------------------------------------------------------------------

                table = New PdfPTable(4)
                table.WidthPercentage = 101
                table.SetWidths({0, 75, 15, 5})
                table.DefaultCell.Border = 0

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "KOMPETEN SEMUA MODUL " & strbidang
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                Debug.WriteLine(cetak)
                myDocument.Add(table)


                'pngka---------------------------------------------------------------------------

                table = New PdfPTable(4)
                table.WidthPercentage = 101
                table.SetWidths({0, 75, 15, 5})
                table.DefaultCell.Border = 0

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "PURATA NILAI GRED KUMULATIF AKADEMIK (PNGKA)"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += FormatNumber(CDbl(strPNGKA), 2)
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                Debug.WriteLine(cetak)
                myDocument.Add(table)

                'pngkv----------------------------------------------------------------------------

                table = New PdfPTable(4)
                table.WidthPercentage = 101
                table.SetWidths({0, 75, 15, 5})
                table.DefaultCell.Border = 0

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "PURATA NILAI GRED KUMULATIF VOKASIONAL (PNGKV)"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += FormatNumber(CDbl(strPNGKV), 2)
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                Debug.WriteLine(cetak)
                myDocument.Add(table)

                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)

                'footer------------------------------------------------------------------------------
                If strStatus = "SETARA" Then
                    table = New PdfPTable(3)
                    table.WidthPercentage = 101
                    table.SetWidths({0, 90, 5})
                    table.DefaultCell.Border = 0

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += "Lembaga Peperiksaan memperakukan bahawa calon yang namanya tersebut "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    Debug.WriteLine(cetak)
                    myDocument.Add(table)

                    table = New PdfPTable(3)
                    table.WidthPercentage = 101
                    table.SetWidths({0, 90, 5})
                    table.DefaultCell.Border = 0

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += "di atas ini telah dianugerahkan Sijil Vokasional Malaysia yang setara dengan  "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    Debug.WriteLine(cetak)
                    myDocument.Add(table)

                    table = New PdfPTable(3)
                    table.WidthPercentage = 101
                    table.SetWidths({0, 90, 5})
                    table.DefaultCell.Border = 0

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += "3 kredit Sijil Pelajaran Malaysia."
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    Debug.WriteLine(cetak)
                    myDocument.Add(table)

                End If

                If printQR.SelectedValue = 1 Then

                    Dim hints As IDictionary = New Dictionary(Of qrcode.EncodeHintType, Object)
                    hints.Add(qrcode.EncodeHintType.ERROR_CORRECTION, qrcode.ErrorCorrectionLevel.Q)
                    Dim qr As BarcodeQRCode = New BarcodeQRCode("http://apkv.moe.gov.my/svm.pengesahan.aspx?id=" & encryptedStrkey, 150, 150, hints)
                    'Dim qr As BarcodeQRCode = New BarcodeQRCode("http://localhost:56105/svm.pengesahan.aspx?id=" & encryptedStrkey, 150, 150, hints)
                    Dim qrImage As iTextSharp.text.Image = qr.GetImage()
                    qrImage.SetAbsolutePosition(250, 15)
                    qrImage.ScalePercent(60)
                    myDocument.Add(qrImage)

                End If



                strSQL = " Select FileLocation FROM kpmkv_config_pengarahPeperiksaan WHERE ID='" & ddlSign.SelectedValue & "'"
                Dim FullFileName As String = oCommon.getFieldValue(strSQL)

                Dim imageHeader As String = String.Concat(Server.MapPath("~/signature/" + FullFileName))

                'Dim imageHeader As String = Server.MapPath(fileSavePath)
                Dim imgHeader As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageHeader)
                imgHeader.ScalePercent(22)
                imgHeader.SetAbsolutePosition(410, 32)

                myDocument.Add(imgHeader)

                ''isbmtahun
                strSQL = " SELECT isBMTahun FROM kpmkv_pelajar WHERE PelajarID = '" & strkey & "'"
                Dim strIsBMTahun As String = oCommon.getFieldValue(strSQL)

                ''kod
                strSQL = " SELECT Kod FROM kpmkv_tahun WHERE Tahun = '" & strIsBMTahun & "'"
                Dim strKod As String = oCommon.getFieldValue(strSQL)

                ''runningno
                strSQL = " SELECT RunningNo FROM kpmkv_sijil_RunningNo WHERE PelajarID = '" & strkey & "'"
                Dim strRunningNo As String = oCommon.getFieldValue(strSQL)


                ''agency font
                Dim fontPath As String = String.Concat(Server.MapPath("~/font/"))
                Dim bfAgency As BaseFont = BaseFont.CreateFont(fontPath & "agency.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)

                Dim agencyFont As iTextSharp.text.Font = New iTextSharp.text.Font(bfAgency, 15)
                '' changed fontsize from 13 to 15 21112018

                table = New PdfPTable(3)
                table.WidthPercentage = 101
                table.SetWidths({0, 90, 5})
                table.SetExtendLastRow(True, True)
                table.DefaultCell.Border = 1

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell
                cetak = strRunningNo & Environment.NewLine
                cetak += " "
                cell.AddElement(New Paragraph(cetak, agencyFont))
                cell.VerticalAlignment = Element.ALIGN_BOTTOM
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)

                ''--content end

            End If



        Next

        myDocument.Close()

        HttpContext.Current.Response.Write(myDocument)
        HttpContext.Current.Response.End()
    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_jenis_list()
        kpmkv_kolej_list()
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()

    End Sub


End Class