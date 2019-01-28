Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class transkrip_sijil_vok1
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

                kpmkv_layak_svm()

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


    Private Sub kpmkv_layak_svm()

        LayakSVM.Items.Add("LAYAK")
        LayakSVM.Items.Add("TIDAK LAYAK")

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


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        status_cetakan()
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
        tmpSQL += " kpmkv_kluster.NamaKluster, kpmkv_kursus.NamaKursus, kpmkv_pelajar.Kaum, kpmkv_pelajar.Jantina, kpmkv_pelajar.Agama, kpmkv_status.Status, kpmkv_kelas.NamaKelas"

        tmpSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN kpmkv_kluster ON kpmkv_kursus.KlusterID=kpmkv_kluster.KlusterID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_status ON kpmkv_pelajar.StatusID = kpmkv_status.StatusID LEFT OUTER JOIN kpmkv_kelas ON kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_SVM ON kpmkv_pelajar.PelajarID = kpmkv_SVM.PelajarID"
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

        If LayakSVM.Text = "LAYAK" Then
            strWhere += " AND kpmkv_SVM.LayakSVM = '1'"
        End If

        If LayakSVM.Text = "TIDAK LAYAK" Then
            strWhere += " AND kpmkv_SVM.LayakSVM = '0'"
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

    Protected Sub btnPrintSlip_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrintSlip.Click
        Dim myDocument As New Document(PageSize.A4)

        Try
            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=TranskripSijilVokMalaysia.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As Image = Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            '1'--start here
            strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & ddlKolej.SelectedValue & "'"
            Dim strKolejnama As String = oCommon.getFieldValue(strSQL)

            'kolejnegeri
            strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE Nama='" & strKolejnama & "'"
            Dim strKolejnegeri As String = oCommon.getFieldValue(strSQL)


            strSQL = "SELECT kpmkv_pelajar.PelajarID,kpmkv_pelajar.MYKAD,kpmkv_pelajar.Nama, kpmkv_pelajar.AngkaGiliran, "
            strSQL += " kpmkv_kursus.KodKursus,kpmkv_kluster.NamaKluster, kpmkv_kursus.NamaKursus,kpmkv_pelajar.isBMTahun,"
            strSQL += " kpmkv_pelajar.agama,kpmkv_kursus.JenisKursus"
            strSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID "
            strSQL += " LEFT OUTER JOIN kpmkv_kluster On kpmkv_kursus.KlusterID=kpmkv_kluster.KlusterID"
            strSQL += " LEFT OUTER JOIN kpmkv_status On kpmkv_pelajar.StatusID = kpmkv_status.StatusID"
            strSQL += " LEFT OUTER JOIN kpmkv_kelas On kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
            strSQL += " LEFT OUTER JOIN kpmkv_SVM ON kpmkv_pelajar.PelajarID = kpmkv_SVM.PelajarID"
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

            If LayakSVM.Text = "LAYAK" Then
                strSQL += " AND kpmkv_SVM.LayakSVM = '1'"
            End If

            If LayakSVM.Text = "TIDAK LAYAK" Then
                strSQL += " AND kpmkv_SVM.LayakSVM = '0'"
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

                        strSQL = "UPDATE kpmkv_status_cetak SET Transkrip = '1' WHERE statusID = '" & IDExist & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                    Dim strmykad As String = ds.Tables(0).Rows(i).Item(1).ToString
                    Dim strname As String = ds.Tables(0).Rows(i).Item(2).ToString
                    Dim strag As String = ds.Tables(0).Rows(i).Item(3).ToString
                    Dim strkodKursus As String = ds.Tables(0).Rows(i).Item(4).ToString
                    Dim strbidang As String = ds.Tables(0).Rows(i).Item(5).ToString
                    Dim strprogram As String = ds.Tables(0).Rows(i).Item(6).ToString
                    Dim strTahun As String = ds.Tables(0).Rows(i).Item(7).ToString
                    Dim strAgama As String = ds.Tables(0).Rows(i).Item(8).ToString
                    Dim strJenisKursus As String = ds.Tables(0).Rows(i).Item(9).ToString
                    ''getting data end

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    'Transkrip Sijil Vokasional Malaysia
                    Dim fontPath As String = String.Concat(Server.MapPath("~/font/"))
                    Dim bf_TNR As BaseFont = BaseFont.CreateFont(fontPath & "timesbd.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)

                    Dim TNR_BOLD_Font As iTextSharp.text.Font = New iTextSharp.text.Font(bf_TNR, 13)

                    Dim table As New PdfPTable(1)
                    Dim myPara001 As New Paragraph()
                    table.WidthPercentage = 103
                    table.SetWidths({100})
                    table.DefaultCell.Border = 0

                    Dim cell = New PdfPCell()
                    Dim cetak = "Transkrip Sijil Vokasional Malaysia"
                    cell.AddElement(New Paragraph(cetak, TNR_BOLD_Font))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)


                    ''PROFILE STARTS HERE

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    'table = New PdfPTable(2)

                    'table.WidthPercentage = 103
                    'table.SetWidths({30, 70})


                    'Dim cell = New PdfPcell()
                    'Dim cetak = Environment.NewLine & "NAMA"
                    'cetak += Environment.NewLine & "NO.KAD PENGENALAN"
                    'cetak += Environment.NewLine & "ANGKA GILIRAN"
                    'cetak += Environment.NewLine & "INSTITUSI"
                    'cetak += Environment.NewLine & "KLUSTER"
                    'cetak += Environment.NewLine & "KURSUS"
                    'cetak += Environment.NewLine & ""

                    'cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    'cell.Border = 0
                    'table.Addcell(cell)

                    'cell = New PdfPcell()
                    'cetak = Environment.NewLine & ":  " & strname
                    'cetak += Environment.NewLine & ":  " & strmykad
                    'cetak += Environment.NewLine & ":  " & strag
                    'cetak += Environment.NewLine & ":  " & strKolejnama
                    'cetak += Environment.NewLine & ":  " & strbidang
                    'cetak += Environment.NewLine & ":  " & strprogram & " (" & strkodKursus & ")"
                    'cetak += Environment.NewLine & " "

                    'cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    'cell.Border = 0
                    'table.Addcell(cell)
                    'Debug.WriteLine(cetak)

                    'myDocument.Add(table)

                    ''NAMA
                    table = New PdfPTable(2)
                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "NAMA"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strname

                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)

                    ''NO. KAD PENGENALAN
                    table = New PdfPTable(2)

                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "NO.KAD PENGENALAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strmykad
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)

                    ''ANGKA GILIRAN
                    table = New PdfPTable(2)

                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "ANGKA GILIRAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strag
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)

                    ''INSTITUSI
                    table = New PdfPTable(2)

                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "INSTITUSI"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strKolejnama & ", " & strKolejnegeri
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)

                    ''KLUSTER
                    table = New PdfPTable(2)

                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "KLUSTER"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strbidang
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)

                    ''KURSUS
                    table = New PdfPTable(2)

                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "KURSUS"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strprogram

                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)


                    myDocument.Add(imgSpacing)

                    ''profile ends here
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    Dim fontSize65 As Single = 6.5
                    Dim fontSize7 As Single = 7

                    'get data from sem 1 
                    strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE Mykad = '" & strmykad & "' AND Semester='1'"
                    strSQL += " AND StatusID='2' AND JenisCalonID='2' AND KelasID IS NOT NULL"
                    Dim strPelajarID1 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT KursusID FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID1 & "' "
                    Dim strkursusid1 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT Tahun FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID1 & "'"
                    Dim strtahun1 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TahunSem FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID1 & "'"
                    Dim strtahunSem1 As String = oCommon.getFieldValue(strSQL)

                    'get data from sem 2
                    strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE Mykad = '" & strmykad & "' AND Semester='2'"
                    strSQL += " AND StatusID='2' AND JenisCalonID='2' AND KelasID IS NOT NULL"
                    Dim strPelajarID2 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT KursusID FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID2 & "' "
                    Dim strkursusid2 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT Tahun FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID2 & "'"
                    Dim strtahun2 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TahunSem FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID2 & "'"
                    Dim strtahunSem2 As String = oCommon.getFieldValue(strSQL)

                    ''''énd get data''''''''''''''''''

                    '''''header sem 1 & 2''''''''''''''''''''''''''''''''''''''''''
                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({35, 11, 7, 7, 35, 11, 7, 6})

                    cell = New PdfPCell()
                    cetak = "SEMESTER 1 TAHUN " & strtahunSem1
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "SEMESTER 2 TAHUN " & strtahunSem2
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({11, 35, 7, 7, 11, 35, 7, 6})

                    cell = New PdfPCell()
                    cetak = "KOD"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "MATA PELAJARAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "JAM KREDIT"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "GRED"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "KOD"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "MATA PELAJARAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "JAM KREDIT"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "GRED"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''matapelajaran sem 1 & 2
                    table = New PdfPTable(8)
                    table.WidthPercentage = 103
                    table.SetWidths({11, 35, 7, 7, 11, 35, 7, 6})
                    table.DefaultCell.Border = 0

                    ''matapelajaran semester 1

                    cell = New PdfPCell()
                    ''subject akademik
                    strSQL = "SELECT KodMataPelajaran, NamaMataPelajaran, JamKredit,PelajarMarkahGred FROM kpmkv_matapelajaran"
                    strSQL += " WHERE Tahun ='" & strtahun1 & "'"
                    strSQL += " AND Semester= '1'"
                    If strAgama = "ISLAM" Then
                        strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    Else
                        strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    End If

                    strSQL += " ORDER BY KodMataPelajaran"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    Dim sqlDA2 As New SqlDataAdapter(strSQL, objConn)
                    Dim ds2 As DataSet = New DataSet
                    sqlDA2.Fill(ds2, "AnyTable")


                    Dim cetakCode As String = ""
                    Dim cetakJam As String = ""
                    Dim cetakSub As String = ""
                    Dim cetakGred As String = ""
                    Dim subj As Integer = ds2.Tables(0).Rows.Count - 1

                    For iloop As Integer = 0 To subj
                        ''get gred from pelajar_markah
                        Dim ColGred As String = (ds2.Tables(0).Rows(iloop).Item(3).ToString())

                        strSQL = "SELECT " & ColGred & " FROM kpmkv_pelajar_markah"
                        strSQL += " WHERE PelajarID='" & strPelajarID1 & "'"
                        Dim strGred As String = oCommon.getFieldValue(strSQL)


                        Dim subjcode As String = (ds2.Tables(0).Rows(iloop).Item(0).ToString())
                        subjcode = subjcode.Replace(vbCr, "").Replace(vbLf, "")


                        cetakCode += subjcode & Environment.NewLine
                        cetakSub += ds2.Tables(0).Rows(iloop).Item(1).ToString & Environment.NewLine
                        cetakJam += ds2.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine
                        cetakGred += "     " & strGred & Environment.NewLine

                    Next

                    ''Modul
                    strSQL = "SELECT KodModul, NamaModul, JamKredit,PelajarMarkahGred from kpmkv_Modul where Tahun ='" & strtahun1 & "'"
                    strSQL += " AND Semester= '1' AND KursusID='" & strkursusid1 & "'"

                    strSQL += " ORDER BY KodModul"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    Dim sqlDA3 As New SqlDataAdapter(strSQL, objConn)
                    Dim ds3 As DataSet = New DataSet
                    sqlDA3.Fill(ds3, "AnyTable")


                    Dim subj2 As Integer = ds3.Tables(0).Rows.Count - 1

                    For iloop As Integer = 0 To subj2

                        ''get gredv from pelajar_markah
                        Dim ColGredV As String = (ds3.Tables(0).Rows(iloop).Item(3).ToString())

                        strSQL = "SELECT " & ColGredV & " FROM kpmkv_pelajar_markah"
                        strSQL += " WHERE PelajarID='" & strPelajarID1 & "'"
                        Dim strGredV1 As String = oCommon.getFieldValue(strSQL)

                        Dim subjcode2 As String = (ds3.Tables(0).Rows(iloop).Item(0).ToString())
                        subjcode2 = subjcode2.Replace(vbCr, "").Replace(vbLf, "")

                        If ds3.Tables(0).Rows(iloop).Item(1).ToString.Length < 41 Then
                            cetakCode += subjcode2 & Environment.NewLine
                        Else
                            cetakCode += subjcode2 & Environment.NewLine & Environment.NewLine
                        End If


                        cetakSub += ds3.Tables(0).Rows(iloop).Item(1).ToString & Environment.NewLine


                        If ds3.Tables(0).Rows(iloop).Item(1).ToString.Length < 41 Then
                            cetakJam += ds3.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine
                            cetakGred += "     " & strGredV1 & Environment.NewLine
                        Else
                            cetakJam += ds3.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine & Environment.NewLine
                            cetakGred += "     " & strGredV1 & Environment.NewLine & Environment.NewLine
                        End If




                    Next

                    cell = New PdfPCell()


                    Debug.WriteLine(cetakCode)
                    myPara001 = New Paragraph(cetakCode, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakSub)

                    myPara001 = New Paragraph(cetakSub, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakJam)
                    myPara001 = New Paragraph(cetakJam, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    'cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakGred)

                    myPara001 = New Paragraph(cetakGred, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0

                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)



                    ''''''''máta pelajaran sem 2''''''''''''''''''

                    ''subject akademik
                    strSQL = "SELECT KodMataPelajaran, NamaMataPelajaran, JamKredit,PelajarMarkahGred FROM kpmkv_matapelajaran"
                    strSQL += " WHERE Tahun ='" & strtahun2 & "'"
                    strSQL += " AND Semester= '2'"
                    If strAgama = "ISLAM" Then
                        strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    Else
                        strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    End If

                    strSQL += " ORDER BY KodMataPelajaran"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    Dim sqlDA4 As New SqlDataAdapter(strSQL, objConn)
                    Dim ds4 As DataSet = New DataSet
                    sqlDA4.Fill(ds4, "AnyTable")


                    Dim cetakCode2 As String = ""
                    Dim cetakJam2 As String = ""
                    Dim cetakSub2 As String = ""
                    Dim cetakGred2 As String = ""
                    Dim subj4 As Integer = ds4.Tables(0).Rows.Count - 1

                    For iloop As Integer = 0 To subj4
                        ''get gred from pelajar_markah
                        Dim ColGred2 As String = (ds4.Tables(0).Rows(iloop).Item(3).ToString())

                        strSQL = "SELECT " & ColGred2 & " FROM kpmkv_pelajar_markah"
                        strSQL += " WHERE PelajarID='" & strPelajarID2 & "'"
                        Dim strGred2 As String = oCommon.getFieldValue(strSQL)


                        Dim subjcode2 As String = (ds4.Tables(0).Rows(iloop).Item(0).ToString())
                        subjcode2 = subjcode2.Replace(vbCr, "").Replace(vbLf, "")


                        cetakCode2 += subjcode2 & Environment.NewLine
                        cetakSub2 += ds4.Tables(0).Rows(iloop).Item(1).ToString & Environment.NewLine
                        cetakJam2 += ds4.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine
                        cetakGred2 += "    " & strGred2 & Environment.NewLine

                    Next

                    ''Modul
                    strSQL = "SELECT KodModul, NamaModul, JamKredit,PelajarMarkahGred from kpmkv_Modul where Tahun ='" & strtahun2 & "'"
                    strSQL += " AND Semester= '2' AND KursusID='" & strkursusid2 & "'"

                    strSQL += " ORDER BY KodModul"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    Dim sqlDA5 As New SqlDataAdapter(strSQL, objConn)
                    Dim ds5 As DataSet = New DataSet
                    sqlDA5.Fill(ds5, "AnyTable")


                    Dim subj5 As Integer = ds5.Tables(0).Rows.Count - 1

                    For iloop As Integer = 0 To subj5
                        ''get gred from pelajar_markah
                        Dim ColGredV2 As String = (ds5.Tables(0).Rows(iloop).Item(3).ToString())

                        strSQL = "SELECT " & ColGredV2 & " FROM kpmkv_pelajar_markah"
                        strSQL += " WHERE PelajarID='" & strPelajarID2 & "'"
                        Dim strGredV2 As String = oCommon.getFieldValue(strSQL)

                        Dim subjcode3 As String = (ds5.Tables(0).Rows(iloop).Item(0).ToString())
                        subjcode3 = subjcode3.Replace(vbCr, "").Replace(vbLf, "")

                        If ds5.Tables(0).Rows(iloop).Item(1).ToString.Length < 41 Then
                            cetakCode2 += subjcode3 & Environment.NewLine
                        Else
                            cetakCode2 += subjcode3 & Environment.NewLine & Environment.NewLine
                        End If


                        cetakSub2 += ds5.Tables(0).Rows(iloop).Item(1).ToString & Environment.NewLine


                        If ds5.Tables(0).Rows(iloop).Item(1).ToString.Length < 41 Then
                            cetakJam2 += ds5.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine
                            cetakGred2 += "    " & strGredV2 & Environment.NewLine
                        Else
                            cetakJam2 += ds5.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine & Environment.NewLine
                            cetakGred2 += "    " & strGredV2 & Environment.NewLine & Environment.NewLine
                        End If




                    Next

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakCode2)
                    myPara001 = New Paragraph(cetakCode2, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakSub2)

                    myPara001 = New Paragraph(cetakSub2, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakJam2)
                    myPara001 = New Paragraph(cetakJam2, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    'cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakGred2)

                    myPara001 = New Paragraph(cetakGred2, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0

                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)


                    myDocument.Add(table)

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    'get data from sem 3
                    strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE Mykad = '" & strmykad & "' AND Semester='3'"
                    strSQL += " AND StatusID='2' AND JenisCalonID='2' AND KelasID IS NOT NULL"
                    Dim strPelajarID3 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT KursusID FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID3 & "' "
                    Dim strkursusid3 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT Tahun FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID3 & "'"
                    Dim strtahun3 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TahunSem FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID3 & "'"
                    Dim strtahunSem3 As String = oCommon.getFieldValue(strSQL)

                    'get data from sem 4
                    strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE Mykad = '" & strmykad & "' AND Semester='4'"
                    strSQL += " AND StatusID='2' AND JenisCalonID='2' AND KelasID IS NOT NULL"
                    Dim strPelajarID4 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT KursusID FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID4 & "' "
                    Dim strkursusid4 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT Tahun FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID4 & "'"
                    Dim strtahun4 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TahunSem FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID4 & "'"
                    Dim strtahunSem4 As String = oCommon.getFieldValue(strSQL)

                    ''''énd get data''''''''''''''''''


                    '''''header sem 3 & 4''''''''''''''''''''''''''''''''''''''''''
                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({35, 11, 7, 7, 35, 11, 7, 6})

                    cell = New PdfPCell()
                    cetak = "SEMESTER 3 TAHUN " & strtahunSem3
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "SEMESTER 4 TAHUN " & strtahunSem4
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({11, 35, 7, 7, 11, 35, 7, 6})

                    cell = New PdfPCell()
                    cetak = "KOD"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "MATA PELAJARAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "JAM KREDIT"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "GRED"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "KOD"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "MATA PELAJARAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "JAM KREDIT"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "GRED"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''matapelajaran sem 3 & 4
                    table = New PdfPTable(8)
                    table.WidthPercentage = 103
                    table.SetWidths({11, 35, 7, 7, 11, 35, 7, 6})
                    table.DefaultCell.Border = 0

                    ''matapelajaran semester 3

                    cell = New PdfPCell()
                    ''subject akademik
                    strSQL = "SELECT KodMataPelajaran, NamaMataPelajaran, JamKredit,PelajarMarkahGred FROM kpmkv_matapelajaran"
                    strSQL += " WHERE Tahun ='" & strtahun3 & "'"
                    strSQL += " AND Semester= '3'"
                    If strAgama = "ISLAM" Then
                        strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    Else
                        strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    End If
                    If strJenisKursus = "SOCIAL" Then
                        strSQL += " AND (Jenis ='SOCIAL' OR Jenis IS NULL OR Jenis ='')"
                    ElseIf strJenisKursus = "TEKNOLOGI" Then
                        strSQL += " AND (Jenis = 'TEKNOLOGI' OR Jenis IS NULL OR Jenis ='')"

                    End If

                    strSQL += " ORDER BY Idx"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    Dim sqlDA6 As New SqlDataAdapter(strSQL, objConn)
                    Dim ds6 As DataSet = New DataSet
                    sqlDA6.Fill(ds6, "AnyTable")


                    Dim cetakCode3 As String = ""
                    Dim cetakJam3 As String = ""
                    Dim cetakSub3 As String = ""
                    Dim cetakGred3 As String = ""
                    Dim subj3 As Integer = ds6.Tables(0).Rows.Count - 1

                    For iloop As Integer = 0 To subj3
                        ''get gred from pelajar_markah
                        Dim ColGred3 As String = (ds6.Tables(0).Rows(iloop).Item(3).ToString())

                        strSQL = "SELECT " & ColGred3 & " FROM kpmkv_pelajar_markah"
                        strSQL += " WHERE PelajarID='" & strPelajarID3 & "'"
                        Dim strGred3 As String = oCommon.getFieldValue(strSQL)


                        Dim subjcode3 As String = (ds6.Tables(0).Rows(iloop).Item(0).ToString())
                        subjcode3 = subjcode3.Replace(vbCr, "").Replace(vbLf, "")


                        cetakCode3 += subjcode3 & Environment.NewLine
                        cetakSub3 += ds6.Tables(0).Rows(iloop).Item(1).ToString & Environment.NewLine
                        cetakJam3 += ds6.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine
                        cetakGred3 += "    " & strGred3 & Environment.NewLine

                    Next

                    ''Modul
                    strSQL = "SELECT KodModul, NamaModul, JamKredit,PelajarMarkahGred from kpmkv_Modul where Tahun ='" & strtahun3 & "'"
                    strSQL += " AND Semester= '3' AND KursusID='" & strkursusid3 & "'"

                    strSQL += " ORDER BY KodModul"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    Dim sqlDA7 As New SqlDataAdapter(strSQL, objConn)
                    Dim ds7 As DataSet = New DataSet
                    sqlDA7.Fill(ds7, "AnyTable")


                    Dim subj6 As Integer = ds7.Tables(0).Rows.Count - 1

                    For iloop As Integer = 0 To subj6
                        ''get gred from pelajar_markah
                        Dim ColGredV3 As String = (ds7.Tables(0).Rows(iloop).Item(3).ToString())

                        strSQL = "SELECT " & ColGredV3 & " FROM kpmkv_pelajar_markah"
                        strSQL += " WHERE PelajarID='" & strPelajarID3 & "'"
                        Dim strGredV3 As String = oCommon.getFieldValue(strSQL)

                        Dim subjcode7 As String = (ds7.Tables(0).Rows(iloop).Item(0).ToString())
                        subjcode7 = subjcode7.Replace(vbCr, "").Replace(vbLf, "")

                        If ds7.Tables(0).Rows(iloop).Item(1).ToString.Length < 41 Then
                            cetakCode3 += subjcode7 & Environment.NewLine
                        Else
                            cetakCode3 += subjcode7 & Environment.NewLine & Environment.NewLine
                        End If


                        cetakSub3 += ds7.Tables(0).Rows(iloop).Item(1).ToString & Environment.NewLine


                        If ds7.Tables(0).Rows(iloop).Item(1).ToString.Length < 41 Then
                            cetakJam3 += ds7.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine
                            cetakGred3 += "    " & strGredV3 & Environment.NewLine
                        Else
                            cetakJam3 += ds7.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine & Environment.NewLine
                            cetakGred3 += "    " & strGredV3 & Environment.NewLine & Environment.NewLine
                        End If




                    Next

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakCode3)
                    myPara001 = New Paragraph(cetakCode3, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakSub3)

                    myPara001 = New Paragraph(cetakSub3, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakJam3)
                    myPara001 = New Paragraph(cetakJam3, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    ''cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakGred3)

                    myPara001 = New Paragraph(cetakGred3, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0

                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)



                    ''''''''máta pelajaran sem 4''''''''''''''''''

                    ''subject akademik
                    strSQL = "SELECT KodMataPelajaran, NamaMataPelajaran, JamKredit,PelajarMarkahGred FROM kpmkv_matapelajaran"
                    strSQL += " WHERE Tahun ='" & strtahun4 & "'"
                    strSQL += " AND Semester= '4'"
                    If strAgama = "ISLAM" Then
                        strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    Else
                        strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    End If

                    If strJenisKursus = "SOCIAL" Then
                        strSQL += " AND (Jenis ='SOCIAL' OR Jenis IS NULL OR Jenis ='')"
                    ElseIf strJenisKursus = "TEKNOLOGI" Then
                        strSQL += " AND (Jenis = 'TEKNOLOGI' OR Jenis IS NULL OR Jenis ='')"

                    End If

                    strSQL += " ORDER BY Idx"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    Dim sqlDA8 As New SqlDataAdapter(strSQL, objConn)
                    Dim ds8 As DataSet = New DataSet
                    sqlDA8.Fill(ds8, "AnyTable")


                    Dim cetakCode4 As String = ""
                    Dim cetakJam4 As String = ""
                    Dim cetakSub4 As String = ""
                    Dim cetakGred4 As String = ""
                    Dim subj8 As Integer = ds8.Tables(0).Rows.Count - 1

                    For iloop As Integer = 0 To subj8
                        ''get gred from pelajar_markah
                        Dim ColGred4 As String = (ds8.Tables(0).Rows(iloop).Item(3).ToString())

                        strSQL = "SELECT " & ColGred4 & " FROM kpmkv_pelajar_markah"
                        strSQL += " WHERE PelajarID='" & strPelajarID4 & "'"
                        Dim strGred4 As String = oCommon.getFieldValue(strSQL)


                        Dim subjcode4 As String = (ds8.Tables(0).Rows(iloop).Item(0).ToString())
                        subjcode4 = subjcode4.Replace(vbCr, "").Replace(vbLf, "")


                        cetakCode4 += subjcode4 & Environment.NewLine
                        cetakSub4 += ds8.Tables(0).Rows(iloop).Item(1).ToString & Environment.NewLine
                        cetakJam4 += ds8.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine
                        cetakGred4 += "    " & strGred4 & Environment.NewLine

                    Next

                    ''Modul
                    strSQL = "SELECT KodModul, NamaModul, JamKredit,PelajarMarkahGred from kpmkv_Modul where Tahun ='" & strtahun4 & "'"
                    strSQL += " AND Semester= '4' AND KursusID='" & strkursusid4 & "'"

                    strSQL += " ORDER BY KodModul"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    Dim sqlDA9 As New SqlDataAdapter(strSQL, objConn)
                    Dim ds9 As DataSet = New DataSet
                    sqlDA9.Fill(ds9, "AnyTable")


                    Dim subj9 As Integer = ds9.Tables(0).Rows.Count - 1

                    For iloop As Integer = 0 To subj9
                        ''get gred from pelajar_markah
                        Dim ColGredV4 As String = (ds9.Tables(0).Rows(iloop).Item(3).ToString())

                        strSQL = "SELECT " & ColGredV4 & " FROM kpmkv_pelajar_markah"
                        strSQL += " WHERE PelajarID='" & strPelajarID4 & "'"
                        Dim strGredV4 As String = oCommon.getFieldValue(strSQL)


                        Dim subjcode9 As String = (ds9.Tables(0).Rows(iloop).Item(0).ToString())
                        subjcode9 = subjcode9.Replace(vbCr, "").Replace(vbLf, "")

                        If ds9.Tables(0).Rows(iloop).Item(1).ToString.Length < 41 Then
                            cetakCode4 += subjcode9 & Environment.NewLine
                        Else
                            cetakCode4 += subjcode9 & Environment.NewLine & Environment.NewLine
                        End If


                        cetakSub4 += ds9.Tables(0).Rows(iloop).Item(1).ToString & Environment.NewLine


                        If ds9.Tables(0).Rows(iloop).Item(1).ToString.Length < 41 Then
                            cetakJam4 += ds9.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine
                            cetakGred4 += "    " & strGredV4 & Environment.NewLine 'cell.VerticalAlignment = Element.ALIGN_MIDDLE
                        Else
                            cetakJam4 += ds9.Tables(0).Rows(iloop).Item(2).ToString & Environment.NewLine & Environment.NewLine
                            cetakGred4 += "    " & strGredV4 & Environment.NewLine & Environment.NewLine
                        End If




                    Next

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakCode4)
                    myPara001 = New Paragraph(cetakCode4, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakSub4)

                    myPara001 = New Paragraph(cetakSub4, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakJam4)
                    myPara001 = New Paragraph(cetakJam4, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    'cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakGred4)

                    myPara001 = New Paragraph(cetakGred4, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    'cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)


                    myDocument.Add(table)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    ''line image
                    table = New PdfPTable(1)
                    table.WidthPercentage = 103
                    table.SetWidths({100})

                    cell = New PdfPCell()
                    cetak = "___________________________________________________________________________________________________________________________________________________"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize65)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)


                    ''--draw spacing
                    Dim imgdrawLine As String = Server.MapPath("~/img/spacing2.png")
                    Dim imgDSpacing As Image = Image.GetInstance(imgdrawLine)
                    imgDSpacing.Alignment = Image.ALIGN_LEFT  'ALIGN_LEFT
                    imgDSpacing.Border = 0

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    '' Get data from kpmkv_svm sem 4

                    strSQL = "SELECT PNGKA FROM kpmkv_SVM WHERE Mykad='" & strmykad & "'"
                    strSQL += " AND  Tahun='" & ddlTahun.SelectedValue & "' AND Semester='4' AND KodKursus='" & strkodKursus & "'"
                    strSQL += " AND Sesi ='" & chkSesi.Text & "'"

                    Dim strPNGKA As String = oCommon.getFieldValue(strSQL)



                    ''PNGK akademik & jumlah jam kredit akadmik

                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({20, 5, 10, 10, 30, 5, 10, 10})

                    cell = New PdfPCell()
                    cetak = "PNGK AKADEMIK"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = FormatNumber(CDbl(strPNGKA), 2)
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    ''get jumlah jam kredit akademik
                    ''sem1

                    'strSQL = "SELECT SUM(JamKredit) as JK1 FROM kpmkv_matapelajaran"
                    'strSQL += " WHERE Tahun ='" & strtahun1 & "'"
                    'strSQL += " AND Semester= '1'"
                    'If strAgama = "ISLAM" Then
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    'Else
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    'End If
                    'Dim JK1 As Integer = oCommon.getFieldValue(strSQL)

                    '''''sem2
                    'strSQL = "SELECT SUM(JamKredit) as JK2 FROM kpmkv_matapelajaran"
                    'strSQL += " WHERE Tahun ='" & strtahun2 & "'"
                    'strSQL += " AND Semester= '2'"
                    'If strAgama = "ISLAM" Then
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    'Else
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    'End If
                    'Dim JK2 As Integer = oCommon.getFieldValue(strSQL)

                    '''''sem3
                    'strSQL = "SELECT SUM(JamKredit) as JK3 FROM kpmkv_matapelajaran"
                    'strSQL += " WHERE Tahun ='" & strtahun3 & "'"
                    'strSQL += " AND Semester= '3'"
                    'If strAgama = "ISLAM" Then
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    'Else
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    'End If
                    'Dim JK3 As Integer = oCommon.getFieldValue(strSQL)

                    '''''sem4
                    'strSQL = "SELECT SUM(JamKredit) as JK4 FROM kpmkv_matapelajaran"
                    'strSQL += " WHERE Tahun ='" & strtahun4 & "'"
                    'strSQL += " AND Semester= '4'"
                    'If strAgama = "ISLAM" Then
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    'Else
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    'End If
                    'Dim JK4 As Integer = oCommon.getFieldValue(strSQL)

                    'Dim TotalJK As Integer = JK1 + JK2 + JK3 + JK4

                    strSQL = " SELECT Jum_JamKredit_Akademik FROM kpmkv_pelajar_markah"
                    strSQL += " WHERE PelajarID = '" & strkey & "'"
                    strSQL += " AND Semester = '4'"

                    Dim TotalJK As Integer = oCommon.getFieldValue(strSQL)

                    cell = New PdfPCell()
                    cetak = "JUMLAH JAM KREDIT AKADEMIK"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = TotalJK
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    ''PNGK vok & jumlah jam kredit vok

                    strSQL = "SELECT PNGKV FROM kpmkv_SVM WHERE Mykad='" & strmykad & "'"
                    strSQL += " AND  Tahun='" & ddlTahun.SelectedValue & "' AND Semester='4' AND KodKursus='" & strkodKursus & "'"
                    strSQL += " AND Sesi ='" & chkSesi.Text & "'"

                    Dim strPNGKV As String = oCommon.getFieldValue(strSQL)

                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({20, 5, 10, 10, 30, 5, 10, 10})

                    cell = New PdfPCell()
                    cetak = "PNGK VOKASIONAL"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = FormatNumber(CDbl(strPNGKV), 2)
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    ''Modul1
                    strSQL = "SELECT SUM(JamKredit) as JKV1 from kpmkv_Modul where Tahun ='" & strtahun1 & "'"
                    strSQL += " AND Semester= '1' AND KursusID='" & strkursusid1 & "'"

                    Dim JV1 As Integer = oCommon.getFieldValue(strSQL)

                    ''Modul2
                    strSQL = "SELECT SUM(JamKredit) as JKV2 from kpmkv_Modul where Tahun ='" & strtahun2 & "'"
                    strSQL += " AND Semester= '2' AND KursusID='" & strkursusid2 & "'"

                    Dim JV2 As Integer = oCommon.getFieldValue(strSQL)

                    ''Modul3
                    strSQL = "SELECT SUM(JamKredit) as JKV3 from kpmkv_Modul where Tahun ='" & strtahun3 & "'"
                    strSQL += " AND Semester= '3' AND KursusID='" & strkursusid3 & "'"

                    Dim JV3 As Integer = oCommon.getFieldValue(strSQL)

                    ''Modul4
                    strSQL = "SELECT SUM(JamKredit) as JKV4 from kpmkv_Modul where Tahun ='" & strtahun4 & "'"
                    strSQL += " AND Semester= '4' AND KursusID='" & strkursusid4 & "'"

                    Dim JV4 As Integer = oCommon.getFieldValue(strSQL)

                    Dim totalJV As Integer = JV1 + JV2 + JV3 + JV4

                    cell = New PdfPCell()
                    cetak = "JUMLAH JAM KREDIT VOKASIONAL"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = totalJV
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    ''PNGK keseluruhan

                    strSQL = "SELECT PNGKK FROM kpmkv_SVM WHERE Mykad='" & strmykad & "'"
                    strSQL += " AND  Tahun='" & ddlTahun.SelectedValue & "' AND Semester='4' AND KodKursus='" & strkodKursus & "'"
                    strSQL += " AND Sesi ='" & chkSesi.Text & "'"

                    Dim strPNGKK As String = oCommon.getFieldValue(strSQL)

                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({20, 5, 10, 10, 30, 5, 10, 10})

                    cell = New PdfPCell()
                    cetak = "PNGK KESELURUHAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = FormatNumber(CDbl(strPNGKK), 2)
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)



                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    myDocument.Add(imgSpacing)

                    strSQL = " Select FileLocation FROM kpmkv_config_pengarahPeperiksaan WHERE ID='" & ddlSign.SelectedValue & "'"
                    Dim FullFileName As String = oCommon.getFieldValue(strSQL)

                    Dim imageHeader As String = String.Concat(Server.MapPath("~/signature/" + FullFileName))

                    'Dim imageHeader As String = Server.MapPath(fileSavePath)
                    Dim imgHeader As Image = Image.GetInstance(imageHeader)
                    imgHeader.ScalePercent(19)
                    imgHeader.SetAbsolutePosition(410, 30)

                    myDocument.Add(imgHeader)

                    myDocument.NewPage()
                    ''--content end


                    myDocument.NewPage()

                End If


            Next

            myDocument.Close()

            HttpContext.Current.Response.Write(myDocument)
            HttpContext.Current.Response.End()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_jenis_list()
        kpmkv_kolej_list()
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()

    End Sub

    Private Sub status_cetakan()

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

            strSQL = "SELECT statusID FROM kpmkv_status_cetak WHERE PelajarID = '" & strkey & "'"
            Dim IDExist As String = oCommon.getFieldValue(strSQL)

            Dim lblStatusCetak As Label = datRespondent.Rows(i).FindControl("lblStatusCetak")

            If Not IDExist = "" Then

                strSQL = "SELECT Transkrip FROM kpmkv_status_cetak WHERE statusID = '" & IDExist & "'"
                Dim Transkrip As String = oCommon.getFieldValue(strSQL)

                If Transkrip = "1" Then

                    lblStatusCetak.Text = "Telah Dicetak"

                End If

            End If

        Next

    End Sub

End Class