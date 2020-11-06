Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class slip_SJ1251

    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""


    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_tahun_peperiksaan()


                kpmkv_kolej_list()
                ddlKolej.Text = "0"

                kpmkv_Pengarah_list()

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
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

    Private Sub kpmkv_tahun_peperiksaan()
        strSQL = "SELECT DISTINCT isAKATahun FROM kpmkv_pelajar_Akademik_Ulang ORDER BY IsAKATahun"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahunPeperiksaan.DataSource = ds
            ddlTahunPeperiksaan.DataTextField = "isAKATahun"
            ddlTahunPeperiksaan.DataValueField = "isAKATahun"
            ddlTahunPeperiksaan.DataBind()


        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub kpmkv_kolej_list()

        strSQL = "SELECT RecordID, Nama FROM kpmkv_kolej ORDER BY Nama"
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

            ddlKolej.Items.Insert(0, "-PILIH-")

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
            lblMsg.Text = "System Error:" & ex.Message
        Finally
            objConn.Dispose()
        End Try

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
        Dim strOrder As String = " ORDER BY kpmkv_kolej.Nama, kpmkv_pelajar_Akademik_Ulang.Nama ASC"

        '--not deleted
        tmpSQL = "  SELECT DISTINCT
                    kpmkv_pelajar_Akademik_Ulang.Tahun, kpmkv_pelajar_Akademik_Ulang.Sesi, kpmkv_pelajar_Akademik_Ulang.Nama, kpmkv_pelajar_Akademik_Ulang.MYKAD, kpmkv_pelajar_Akademik_Ulang.AngkaGiliran, 
                    kpmkv_kolej.Nama AS namaKolej
                    FROM
                    kpmkv_pelajar_Akademik_Ulang
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar_Akademik_Ulang.KolejRecordID
                    LEFT JOIN kpmkv_kursus ON kpmkv_kursus.KodKursus = kpmkv_pelajar_Akademik_Ulang.KodKursus
                    LEFT JOIN kpmkv_kluster ON kpmkv_kluster.KlusterID = kpmkv_kursus.KlusterID"

        strWhere = "    WHERE
                        kpmkv_pelajar_Akademik_Ulang.Tahun = '" & ddlTahun.Text & "'
                        AND kpmkv_pelajar_Akademik_Ulang.Sesi = '" & chkSesi.Text & "'
                        AND kpmkv_pelajar_Akademik_Ulang.IsAKATahun = '" & ddlTahunPeperiksaan.Text & "'"

        If ddlStatus.Text = "LULUS" Then
            strWhere += "   AND kpmkv_pelajar_Akademik_Ulang.Kompetensi = '" & ddlStatus.Text & "'"
        Else
            strWhere += "   AND kpmkv_pelajar_Akademik_Ulang.Kompetensi = '" & ddlStatus.Text & "'"
        End If

        If Not ddlKolej.Text = "-PILIH-" Then
            strWhere += "   AND kpmkv_pelajar_Akademik_Ulang.KolejRecordID = '" & ddlKolej.SelectedValue & "'"
        End If


        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        Debug.WriteLine(getSQL)

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

    'Protected Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
    '    kpmkv_kodkursus_list()
    '    kpmkv_kelas_list()
    'End Sub

    'Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
    '    kpmkv_kelas_list()
    'End Sub

    Protected Sub btnPrintSlip_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrintSlip.Click
        Dim myDocument As New Document(PageSize.A4)

        Try
            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SlipSJSetara1251.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As Image = Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            '1'--start here

            strSQL = "  SELECT DISTINCT
                        kpmkv_pelajar_Akademik_Ulang.Tahun, kpmkv_pelajar_Akademik_Ulang.Sesi, kpmkv_pelajar_Akademik_Ulang.Nama, kpmkv_pelajar_Akademik_Ulang.MYKAD, kpmkv_pelajar_Akademik_Ulang.AngkaGiliran, 
                        kpmkv_kolej.Nama AS namaKolej
                        FROM
                        kpmkv_pelajar_Akademik_Ulang
                        LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar_Akademik_Ulang.KolejRecordID
                        LEFT JOIN kpmkv_kursus ON kpmkv_kursus.KodKursus = kpmkv_pelajar_Akademik_Ulang.KodKursus
                        LEFT JOIN kpmkv_kluster ON kpmkv_kluster.KlusterID = kpmkv_kursus.KlusterID"

            strSQL += "    WHERE
                        kpmkv_pelajar_Akademik_Ulang.Tahun = '" & ddlTahun.Text & "'
                        AND kpmkv_pelajar_Akademik_Ulang.Sesi = '" & chkSesi.Text & "'
                        AND kpmkv_pelajar_Akademik_Ulang.IsAKATahun = '" & ddlTahunPeperiksaan.Text & "'"

            If ddlStatus.Text = "LULUS" Then
                strSQL += "   AND kpmkv_pelajar_Akademik_Ulang.Kompetensi = '" & ddlStatus.Text & "'"
            Else
                strSQL += "   AND kpmkv_pelajar_Akademik_Ulang.Kompetensi = '" & ddlStatus.Text & "'"
            End If

            If Not ddlKolej.Text = "-PILIH-" Then
                strSQL += "   AND kpmkv_pelajar_Akademik_Ulang.KolejRecordID = '" & ddlKolej.SelectedValue & "'"
            End If

            strSQL += " ORDER BY kpmkv_kolej.Nama, kpmkv_pelajar_Akademik_Ulang.Nama ASC"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

                If cb.Checked = True Then

                    Dim strTahun As String = ds.Tables(0).Rows(i).Item(0).ToString
                    Dim strSesi As String = ds.Tables(0).Rows(i).Item(1).ToString
                    Dim strName As String = ds.Tables(0).Rows(i).Item(2).ToString
                    Dim strMykad As String = ds.Tables(0).Rows(i).Item(3).ToString
                    Dim strAG As String = ds.Tables(0).Rows(i).Item(4).ToString

                    ''get kolejrecordid
                    strSQL = "SELECT KolejRecordID FROM kpmkv_pelajar_Akademik_Ulang WHERE AngkaGiliran = '" & strAG & "'"
                    Dim strKolejRecordID As String = oCommon.getFieldValue(strSQL)

                    'get namakolej
                    strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & strKolejRecordID & "'"
                    Dim strKolejnama As String = oCommon.getFieldValue(strSQL)

                    'kolejnegeri
                    strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE Nama='" & strKolejnama & "'"
                    Dim strKolejnegeri As String = oCommon.getFieldValue(strSQL)

                    ''get kodkursus
                    strSQL = "SELECT KodKursus FROM kpmkv_pelajar_Akademik_Ulang WHERE AngkaGiliran = '" & strAG & "'"
                    Dim strKodKursus = oCommon.getFieldValue(strSQL)

                    ''get namakursus
                    strSQL = "SELECT DISTINCT NamaKursus FROM kpmkv_kursus WHERE KodKursus = '" & strKodKursus & "'"
                    Dim strNamaKursus = oCommon.getFieldValue(strSQL)

                    ''get klusterid
                    strSQL = "SELECT KlusterID FROM kpmkv_kursus WHERE KodKursus = '" & strKodKursus & "' AND Tahun = '" & strTahun & "'"
                    Dim strKlusterID = oCommon.getFieldValue(strSQL)

                    ''get klustername
                    strSQL = "SELECT NamaKluster FROM kpmkv_kluster WHERE KlusterID = '" & strKlusterID & "'"
                    Dim strNamaKluster = oCommon.getFieldValue(strSQL)



                    ''UPDATE kpmkv_status_cetak
                    strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran = '" & strAG & "'"
                    Dim strkey = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT statusID FROM kpmkv_status_cetak WHERE PelajarID = '" & strkey & "'"
                    Dim IDExist As String = oCommon.getFieldValue(strSQL)

                    If IDExist = "" Then

                        strSQL = "INSERT INTO kpmkv_status_cetak (PelajarID, svm, slipBM, slipSJ, Transkrip) VALUES ('" & strkey & "', '0', '0', '0', '0')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    Else

                        strSQL = "UPDATE kpmkv_status_cetak SET slipSJ = '1' WHERE statusID = '" & IDExist & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                    ''getting data end

                    Dim table As New PdfPTable(3)
                    table.WidthPercentage = 100
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

                    Dim cell = New PdfPCell()
                    Dim cetak = Environment.NewLine & ""

                    table = New PdfPTable(1)
                    table.WidthPercentage = 100
                    table.SetWidths({100})

                    ''timesbd font
                    Dim fontPath As String = String.Concat(Server.MapPath("~/font/"))
                    Dim bfTimesbd As BaseFont = BaseFont.CreateFont(fontPath & "timesbd.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)

                    Dim timesbdFont As iTextSharp.text.Font = New iTextSharp.text.Font(bfTimesbd, 11)

                    cell = New PdfPCell()
                    cetak = "Calon yang tersebut namanya di bawah telah mengambil peperiksaan Sejarah 1251 dan menunjukkan" & Environment.NewLine
                    cetak += "prestasi seperti yang tercatat di bawah."
                    cell.AddElement(New Paragraph(cetak, timesbdFont))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    ''PROFILE STARTS HERE

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    ''NAMA
                    table = New PdfPTable(3)
                    table.WidthPercentage = 100
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "NAMA"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strName
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)




                    ''NO. KAD PENGENALAN
                    table = New PdfPTable(3)
                    table.WidthPercentage = 100
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "NO.KAD PENGENALAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strMykad
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)




                    ''ANGKA GILIRAN
                    table = New PdfPTable(3)
                    table.WidthPercentage = 100
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "ANGKA GILIRAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strAG
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)




                    ''INSTITUSI
                    table = New PdfPTable(3)
                    table.WidthPercentage = 100
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "INSTITUSI"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strKolejnama & ", " & strKolejnegeri
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    ''BIDANG
                    table = New PdfPTable(3)
                    table.WidthPercentage = 100
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "BIDANG"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strNamaKluster
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)




                    ''PROGRAM
                    table = New PdfPTable(3)
                    table.WidthPercentage = 100
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "PROGRAM"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strNamaKursus
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)






                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    ''profile ends here
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    table = New PdfPTable(5)
                    table.WidthPercentage = 100
                    table.SetWidths({30, 4, 50, 18, 1})

                    cell = New PdfPCell()
                    cetak = "KOD"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "MATA PELAJARAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "GRED"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    strSQL = "SELECT Kompetensi from kpmkv_pelajar_Akademik_Ulang where AngkaGiliran = '" & strAG & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strSQL += " AND IsAKATahun='" & ddlTahunPeperiksaan.Text & "'"
                    Dim strKompetensi As String = oCommon.getFieldValue(strSQL)
                    If strKompetensi = "" Then
                        strKompetensi = ""
                    End If


                    table = New PdfPTable(5)
                    table.WidthPercentage = 100
                    table.SetWidths({30, 4, 50, 18, 1})

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += "1251"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += "SEJARAH"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += strKompetensi
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                    cell.Border = 0
                    table.AddCell(cell)

                    Debug.WriteLine(cetak)
                    myDocument.Add(table)

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    'myDocument.Add(imgSpacing) 08102019 2
                    'myDocument.Add(imgSpacing)
                    'myDocument.Add(imgSpacing)
                    'myDocument.Add(imgSpacing) 08102019
                    'myDocument.Add(imgSpacing)
                    'myDocument.Add(imgSpacing)
                    'myDocument.Add(imgSpacing)

                    ''TT
                    strSQL = " Select FileLocation FROM kpmkv_config_pengarahPeperiksaan WHERE ID='" & ddlSign.SelectedValue & "'"
                    Dim FullFileName As String = oCommon.getFieldValue(strSQL)

                    Dim imageHeader As String = String.Concat(Server.MapPath("~/signature/" + FullFileName))

                    'Dim imageHeader As String = Server.MapPath(fileSavePath)
                    Dim imgHeader As Image = Image.GetInstance(imageHeader)
                    imgHeader.ScalePercent(23)
                    imgHeader.SetAbsolutePosition(355, 75)

                    myDocument.Add(imgHeader)

                    ''Pernyataan ini dijeluarkan....

                    Dim ayatPenyataSJ As String = "slipSJ_ayatbawah_V1.png"

                    imageHeader = String.Concat(Server.MapPath("~/signature/" + ayatPenyataSJ))

                    'Dim imageHeader As String = Server.MapPath(fileSavePath)
                    imgHeader = Image.GetInstance(imageHeader)
                    imgHeader.ScalePercent(37)
                    imgHeader.SetAbsolutePosition(35, 10)

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

    Private Sub status_cetakan()

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim strAG As String = datRespondent.DataKeys(i).Value.ToString

            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran = '" & strAG & "'"
            Dim strkey = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT statusID FROM kpmkv_status_cetak WHERE PelajarID = '" & strkey & "'"
            Dim IDExist As String = oCommon.getFieldValue(strSQL)

            Dim lblStatusCetak As Label = datRespondent.Rows(i).FindControl("lblStatusCetak")

            If Not IDExist = "" Then

                strSQL = "SELECT slipSJ FROM kpmkv_status_cetak WHERE statusID = '" & IDExist & "'"
                Dim slipSJ As String = oCommon.getFieldValue(strSQL)

                If slipSJ = "1" Then

                    lblStatusCetak.Text = "Telah Dicetak"

                End If

            End If

        Next

    End Sub

End Class