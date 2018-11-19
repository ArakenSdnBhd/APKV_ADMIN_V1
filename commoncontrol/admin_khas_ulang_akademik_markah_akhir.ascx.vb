Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class admin_khas_ulang_akademik_markah_akhir
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""
        lblMsg2.Text = ""
        Try
            If Not IsPostBack Then

                lblPemeriksa.Text = Request.QueryString("PemeriksaID")

                strSQL = " SELECT Tahun, Sesi, KodKolej, Subjek FROM  kpmkv_pemeriksa "
                strSQL += " WHERE PemeriksaID='" & lblPemeriksa.Text & "'"
                strRet = oCommon.getFieldValueEx(strSQL)
                'Response.Write(strSQL)
                ''--get pemeriksa info
                Dim ar_pemeriksa As Array
                ar_pemeriksa = strRet.Split("|")

                lblTahun.Text = ar_pemeriksa(0)
                lblSesi.Text = ar_pemeriksa(1)
                lblKodPusat.Text = ar_pemeriksa(2)
                lblKertas.Text = ar_pemeriksa(3)

                strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Kod='" & lblKodPusat.Text & "'"
                lblKolejRecorID.Text = oCommon.getFieldValue(strSQL)

                strRet = BindData(datRespondent)
                ' ValidateGridView()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
            lblMsg2.Text = ex.Message
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
            lblMsg2.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function
    Private Function getSQL() As String
        Dim tmpSQL As String = ""
        Dim strOrder As String = " ORDER BY AngkaGiliran"

        '--not deleted
        tmpSQL = "SELECT PelajarID,AngkaGiliran,Mykad,Markah,Catatan FROM kpmkv_pelajar_Akademik_Ulang"
        tmpSQL += " WHERE IsDeleted='N' AND IsCalon='1'"
        tmpSQL += " AND KolejRecordID ='" & lblKolejRecorID.Text & "'"
        tmpSQL += " AND IsAKATahun ='" & lblTahun.Text & "'"
        tmpSQL += " AND IsAKASesi ='" & lblSesi.Text & "'"

        getSQL = tmpSQL & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
        ' ValidateGridView()
    End Sub
    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

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

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""
        lblMsg2.Text = ""
        Try
            If ValidateForm() = False Then
                lblMsg.Text = "Sila masukkan NOMBOR SAHAJA. 0-100"
                lblMsg2.Text = "Sila masukkan NOMBOR SAHAJA. 0-100"
                Exit Sub
            End If

            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim row As GridViewRow = datRespondent.Rows(i)
                Dim strPelajarID As Integer = datRespondent.DataKeys(i).Value.ToString
                Dim Markah As TextBox = datRespondent.Rows(i).FindControl("Markah")
                Dim Catatan As TextBox = datRespondent.Rows(i).FindControl("Catatan")

                'assign value to integer
                Dim strMarkah As String = Markah.Text
                Dim strCatatan As String = Catatan.Text


                strSQL = "UPDATE kpmkv_pelajar_Akademik_Ulang SET Markah='" & strMarkah & "', Catatan='" & strCatatan & "' "
                strSQL += " WHERE PelajarID='" & strPelajarID & "'"
                strSQL += " AND IsDeleted='N' AND IsCalon='1'"
                strSQL += " AND KolejRecordID ='" & lblKolejRecorID.Text & "'"
                strSQL += " AND IsAKATahun ='" & lblTahun.Text & "'"
                strSQL += " AND IsAKASesi ='" & lblSesi.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            Next
            divMsg2.Attributes("class") = "info"
            lblMsg2.Text = "Berjaya!.Kemaskini markah Ulang Akademik."

            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya!.Kemaskini markah Ulang Akademik."

            strRet = BindData(datRespondent)

        Catch ex As Exception
            divMsg2.Attributes("class") = "Error:" & ex.Message
            lblMsg2.Text = "Tidak Berjaya!.Kemaskini markah Ulang Akademik."

            divMsg.Attributes("class") = "Error:" & ex.Message
            lblMsg.Text = "Tidak Berjaya!.Kemaskini markah Ulang Akademik."
        End Try


        ' Akademik_markah()
        'Akademik_gred()
    End Sub
    Protected Sub btnEksport_Click(sender As Object, e As EventArgs) Handles btnEksport.Click
        Try

            Dim tableColumn As DataColumnCollection
            Dim tableRows As DataRowCollection

            Dim myDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
            myDataAdapter.Fill(myDataSet, "Akademik Borang Markah")
            myDataAdapter.SelectCommand.CommandTimeout = 80000
            objConn.Close()

            '--transfer to an object
            tableColumn = myDataSet.Tables(0).Columns
            tableRows = myDataSet.Tables(0).Rows

            If txtKodMP.Text = "" Then
                lblMsg.Text = "Masukkan Kod MataPelajaran"
                lblMsg2.Text = "Masukkan Kod MataPelajaran!"
                Exit Sub
            Else
                CreatePDF(tableColumn, tableRows)
            End If

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error." & ex.Message
            lblMsg2.Text = "System Error." & ex.Message

        End Try

    End Sub
    Private Sub CreatePDF(ByVal tableColumns As DataColumnCollection, ByVal tableRows As DataRowCollection)

        Dim msFileName As String
        Dim msFilePath As String
        Dim KodMP As String
        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)
        ' Document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate())
        Try
            ''--msFileName = Guid.NewGuid.ToString & ".pdf"
            KodMP = txtKodMP.Text
            msFileName = "BorangUlangAkademik" & KodMP & ".pdf"
            msFilePath = Server.MapPath("borang\")

            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & msFileName)
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            'Step 3: Open the document now using
            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing2.png")
            Dim imgSpacing As Image = Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = Image.ALIGN_LEFT  'ALIGN_LEFT
            imgSpacing.Border = 0


            Dim strPusat As String = ""
            Dim strBmSetara As String = ""
            Dim strMykad As String = ""
            Dim strAngkaGiliran As String = ""
            Dim strMarkah As String = ""
            Dim strCatatan As String = ""

            strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID='" & lblKodPusat.Text & "' "
            strPusat = oCommon.getFieldValue(strSQL)

            'strSQL = "SELECT KodMataPelajaran FROM kpmkv_matapelajaran WHERE NamaMataPelajaran='BAHASA MELAYU' AND KodMataPelajaran Like '%A014%'   "
            strBmSetara = txtKodMP.Text


            ''--page header

            Dim myPara03 As New Paragraph("LEMBAGA PEPERIKSAAN", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara03.Alignment = Element.ALIGN_CENTER
            myDocument.Add(myPara03)

            Dim myPara04 As New Paragraph("KEMENTERIAN PENDIDIKAN MALAYSIA", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara04.Alignment = Element.ALIGN_CENTER
            myDocument.Add(myPara04)

            Dim myPara05 As New Paragraph("PENTAKSIRAN PUSAT " & "SESI, " & lblSesi.Text & ", " & lblTahun.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara05.Alignment = Element.ALIGN_CENTER
            myDocument.Add(myPara05)

            Dim myPara06 As New Paragraph("BORANG PENGISIAN MARKAH BERTULIS (K15)", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara06.Alignment = Element.ALIGN_CENTER
            myDocument.Add(myPara06)

            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)


            Dim myTableTitle As New PdfPTable(8)
            myTableTitle.WidthPercentage = 100 ' Table size is set to 100% of the page
            myTableTitle.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
            'myTable.HorizontalAlignment = Rectangle.NO_BORDER
            Dim intTblWidthTitle() As Integer = {36, 12, 36, 20, 36, 12, 36, 12}
            myTableTitle.SetWidths(intTblWidthTitle)


            Dim Cell1TTL As New PdfPCell(New Phrase("NOMBOR PUSAT ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell1TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell1TTL)

            Dim Cell2TTL As New PdfPCell(New Phrase("" & strPusat, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell2TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell2TTL)

            Dim Cell3TTL As New PdfPCell(New Phrase("NOMBOR MATA PELAJARAN", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell3TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell3TTL)

            Dim Cell4TTL As New PdfPCell(New Phrase("" & KodMP, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell4TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell4TTL)

            Dim Cell5TTL As New PdfPCell(New Phrase("NOMBOR KETUA PEMERIKSA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell5TTL)

            Dim Cell6TTL As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell6TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell6TTL)

            Dim Cell7TTL As New PdfPCell(New Phrase("NOMBOR PEMERIKSA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell7TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell7TTL)

            Dim Cell8TTL As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell8TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell8TTL)
            myDocument.Add(myTableTitle)

            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)

            Dim myTable As New PdfPTable(5)
            myTable.WidthPercentage = 100 ' Table size is set to 100% of the page
            myTable.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
            'myTable.HorizontalAlignment = Rectangle.NO_BORDER
            Dim intTblWidth() As Integer = {5, 20, 20, 20, 36}
            myTable.SetWidths(intTblWidth)

            Dim Cell1Hdr As New PdfPCell(New Phrase("BIL ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell1Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell1Hdr)

            Dim Cell2Hdr As New PdfPCell(New Phrase("ANGKA GILIRAN", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell2Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell2Hdr)

            Dim Cell3Hdr As New PdfPCell(New Phrase("NO KP", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell3Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell3Hdr)

            Dim Cell4Hdr As New PdfPCell(New Phrase("MARKAH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell4Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell4Hdr)

            Dim Cell5Hdr As New PdfPCell(New Phrase("CATATAN(1,2 ATAU 3)", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell5Hdr)
            myDocument.Add(myTable)

            myDocument.Add(imgSpacing)


            Dim k As Integer = 1
            Dim intRPN As Integer = 1
            'Get the data from database into datatable 
            Dim cmd As New SqlCommand(getSQL)
            Dim dt As DataTable = GetData(cmd)

            'append new line 
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim intCountPelajar As Integer = dt.Rows.Count
                Dim strStudentID As String = ""
                'set RPN


                strSQL = "SELECT Mykad FROM kpmkv_pelajar_Akademik_Ulang WHERE PelajarID='" & dt.Rows(i)("PelajarID").ToString() & "'"
                strMykad = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT AngkaGiliran FROM kpmkv_pelajar_Akademik_Ulang WHERE PelajarID='" & dt.Rows(i)("PelajarID").ToString() & "'"
                strAngkaGiliran = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Markah FROM kpmkv_pelajar_Akademik_Ulang WHERE PelajarID='" & dt.Rows(i)("PelajarID").ToString() & "'"
                strMarkah = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Catatan FROM kpmkv_pelajar_Akademik_Ulang WHERE PelajarID='" & dt.Rows(i)("PelajarID").ToString() & "'"
                strCatatan = oCommon.getFieldValue(strSQL)

                Dim myTableMP8 As New PdfPTable(5)
                myTableMP8.WidthPercentage = 100 ' Table size is set to 100% of the page
                myTableMP8.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                myTableMP8.DefaultCell.BorderWidth = Rectangle.NO_BORDER
                Dim intTblCell8() As Integer = {5, 20, 20, 20, 36}
                myTableMP8.SetWidths(intTblCell8)

                myTableMP8.AddCell(New Phrase(intRPN, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP8.AddCell(New Phrase(strAngkaGiliran, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP8.AddCell(New Phrase(strMykad, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP8.AddCell(New Phrase(strMarkah, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP8.AddCell(New Phrase(strCatatan, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myDocument.Add(myTableMP8)
                myDocument.Add(imgSpacing)

                If Not intRPN = intCountPelajar Then
                    If Not k = 20 Then
                        k = k + 1
                    Else
                        k = 1
                        myDocument.Add(imgSpacing)

                        Dim myTableSubTitle As New PdfPTable(3)
                        myTableSubTitle.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle() As Integer = {36, 36, 36}
                        myTableSubTitle.SetWidths(intTblWidthSubTitle)


                        Dim Cell1Sub As New PdfPCell(New Phrase("PENGESAHAN OLEH ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell1Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle.AddCell(Cell1Sub)

                        Dim Cell2Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell2Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle.AddCell(Cell2Sub)

                        Dim Cell3Sub As New PdfPCell(New Phrase("PETUNJUK :", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell3Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle.AddCell(Cell3Sub)
                        myDocument.Add(myTableSubTitle)

                        Dim myTableSubTitle1 As New PdfPTable(3)
                        myTableSubTitle1.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle1.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle1() As Integer = {36, 36, 36}
                        myTableSubTitle1.SetWidths(intTblWidthSubTitle1)

                        Dim Cell4Sub As New PdfPCell(New Phrase("NAMA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell4Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle1.AddCell(Cell4Sub)

                        Dim Cell5Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell5Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle1.AddCell(Cell5Sub)

                        Dim Cell6Sub As New PdfPCell(New Phrase("HADIR TIDAK MENJAWAB:1", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell6Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle1.AddCell(Cell6Sub)
                        myDocument.Add(myTableSubTitle1)

                        Dim myTableSubTitle2 As New PdfPTable(3)
                        myTableSubTitle2.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle2.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle2() As Integer = {36, 36, 36}
                        myTableSubTitle2.SetWidths(intTblWidthSubTitle2)

                        Dim Cell7Sub As New PdfPCell(New Phrase("TARIKH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell7Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle2.AddCell(Cell7Sub)

                        Dim Cell8Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell8Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle2.AddCell(Cell8Sub)

                        Dim Cell9Sub As New PdfPCell(New Phrase("TIADA SKRIP:2", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell9Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle2.AddCell(Cell9Sub)
                        myDocument.Add(myTableSubTitle2)

                        Dim myTableSubTitle3 As New PdfPTable(3)
                        myTableSubTitle3.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle3.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle3() As Integer = {36, 36, 36}
                        myTableSubTitle3.SetWidths(intTblWidthSubTitle3)

                        Dim Cell10Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell10Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle3.AddCell(Cell10Sub)

                        Dim Cell11Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell11Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle3.AddCell(Cell11Sub)

                        Dim Cell12Sub As New PdfPCell(New Phrase("TIDAK HADIR:3", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell12Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle3.AddCell(Cell12Sub)
                        myDocument.Add(myTableSubTitle3)


                        Dim myTableSubTitle4 As New PdfPTable(3)
                        myTableSubTitle4.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle4.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle4() As Integer = {36, 36, 36}
                        myTableSubTitle4.SetWidths(intTblWidthSubTitle4)

                        Dim Cell13Sub As New PdfPCell(New Phrase("DISEMAK OLEH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell13Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle4.AddCell(Cell13Sub)

                        Dim Cell14Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell14Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle4.AddCell(Cell14Sub)

                        Dim Cell15Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell15Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle4.AddCell(Cell15Sub)
                        myDocument.Add(myTableSubTitle4)

                        Dim myTableSubTitle5 As New PdfPTable(3)
                        myTableSubTitle5.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle5.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle5() As Integer = {36, 36, 36}
                        myTableSubTitle5.SetWidths(intTblWidthSubTitle5)

                        Dim Cell16Sub As New PdfPCell(New Phrase("NAMA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell16Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle5.AddCell(Cell16Sub)

                        Dim Cell17Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell17Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle5.AddCell(Cell17Sub)

                        Dim Cell18Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell18Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle5.AddCell(Cell18Sub)
                        myDocument.Add(myTableSubTitle5)

                        Dim myTableSubTitle6 As New PdfPTable(3)
                        myTableSubTitle6.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle6.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle6() As Integer = {36, 36, 36}
                        myTableSubTitle6.SetWidths(intTblWidthSubTitle6)

                        Dim Cell19Sub As New PdfPCell(New Phrase("TARIKH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell19Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle6.AddCell(Cell19Sub)

                        Dim Cell20Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell20Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle6.AddCell(Cell20Sub)

                        Dim Cell21Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell21Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle6.AddCell(Cell21Sub)
                        myDocument.Add(myTableSubTitle6)

                        myDocument.NewPage()
                        myDocument.Add(myPara03)
                        myDocument.Add(myPara04)
                        myDocument.Add(myPara05)
                        myDocument.Add(myPara06)

                        myDocument.Add(imgSpacing)
                        myDocument.Add(imgSpacing)

                        myDocument.Add(myTableTitle)

                        myDocument.Add(imgSpacing)
                        myDocument.Add(imgSpacing)

                        myDocument.Add(myTable)

                        myDocument.Add(imgSpacing)
                    End If
                Else
                    myDocument.Add(imgSpacing)

                    Dim myTableSubTitle As New PdfPTable(3)
                    myTableSubTitle.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle() As Integer = {36, 36, 36}
                    myTableSubTitle.SetWidths(intTblWidthSubTitle)


                    Dim Cell1Sub As New PdfPCell(New Phrase("PENGESAHAN OLEH ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell1Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle.AddCell(Cell1Sub)

                    Dim Cell2Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell2Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle.AddCell(Cell2Sub)

                    Dim Cell3Sub As New PdfPCell(New Phrase("PETUNJUK :", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell3Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle.AddCell(Cell3Sub)
                    myDocument.Add(myTableSubTitle)

                    Dim myTableSubTitle1 As New PdfPTable(3)
                    myTableSubTitle1.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle1.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle1() As Integer = {36, 36, 36}
                    myTableSubTitle1.SetWidths(intTblWidthSubTitle1)

                    Dim Cell4Sub As New PdfPCell(New Phrase("NAMA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell4Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle1.AddCell(Cell4Sub)

                    Dim Cell5Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell5Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle1.AddCell(Cell5Sub)

                    Dim Cell6Sub As New PdfPCell(New Phrase("HADIR TIDAK MENJAWAB:1", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell6Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle1.AddCell(Cell6Sub)
                    myDocument.Add(myTableSubTitle1)

                    Dim myTableSubTitle2 As New PdfPTable(3)
                    myTableSubTitle2.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle2.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle2() As Integer = {36, 36, 36}
                    myTableSubTitle2.SetWidths(intTblWidthSubTitle2)

                    Dim Cell7Sub As New PdfPCell(New Phrase("TARIKH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell7Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle2.AddCell(Cell7Sub)

                    Dim Cell8Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell8Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle2.AddCell(Cell8Sub)

                    Dim Cell9Sub As New PdfPCell(New Phrase("TIADA SKRIP:2", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell9Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle2.AddCell(Cell9Sub)
                    myDocument.Add(myTableSubTitle2)

                    Dim myTableSubTitle3 As New PdfPTable(3)
                    myTableSubTitle3.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle3.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle3() As Integer = {36, 36, 36}
                    myTableSubTitle3.SetWidths(intTblWidthSubTitle3)

                    Dim Cell10Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell10Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle3.AddCell(Cell10Sub)

                    Dim Cell11Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell11Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle3.AddCell(Cell11Sub)

                    Dim Cell12Sub As New PdfPCell(New Phrase("TIDAK HADIR:3", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell12Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle3.AddCell(Cell12Sub)
                    myDocument.Add(myTableSubTitle3)


                    Dim myTableSubTitle4 As New PdfPTable(3)
                    myTableSubTitle4.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle4.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle4() As Integer = {36, 36, 36}
                    myTableSubTitle4.SetWidths(intTblWidthSubTitle4)

                    Dim Cell13Sub As New PdfPCell(New Phrase("DISEMAK OLEH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell13Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle4.AddCell(Cell13Sub)

                    Dim Cell14Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell14Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle4.AddCell(Cell14Sub)

                    Dim Cell15Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell15Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle4.AddCell(Cell15Sub)
                    myDocument.Add(myTableSubTitle4)

                    Dim myTableSubTitle5 As New PdfPTable(3)
                    myTableSubTitle5.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle5.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle5() As Integer = {36, 36, 36}
                    myTableSubTitle5.SetWidths(intTblWidthSubTitle5)

                    Dim Cell16Sub As New PdfPCell(New Phrase("NAMA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell16Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle5.AddCell(Cell16Sub)

                    Dim Cell17Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell17Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle5.AddCell(Cell17Sub)

                    Dim Cell18Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell18Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle5.AddCell(Cell18Sub)
                    myDocument.Add(myTableSubTitle5)

                    Dim myTableSubTitle6 As New PdfPTable(3)
                    myTableSubTitle6.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle6.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle6() As Integer = {36, 36, 36}
                    myTableSubTitle6.SetWidths(intTblWidthSubTitle6)

                    Dim Cell19Sub As New PdfPCell(New Phrase("TARIKH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell19Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle6.AddCell(Cell19Sub)

                    Dim Cell20Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell20Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle6.AddCell(Cell20Sub)

                    Dim Cell21Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell21Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle6.AddCell(Cell21Sub)
                    myDocument.Add(myTableSubTitle6)

                End If


                intRPN = intRPN + 1

            Next

            lblMsg.Text = "PDF siap dijana."
            lblMsg2.Text = "PDF siap dijana."

        Catch ex As DocumentException
            '--display on screen
            lblMsg.Text = "System Error. Contact system admin. " & ex.Message
            lblMsg2.Text = "System Error. Contact system admin. " & ex.Message

        Catch ioe As IOException
            '--display on screen
            lblMsg.Text = "System Error. Contact system admin. " & ioe.Message.ToString
            lblMsg2.Text = "System Error. Contact system admin. " & ioe.Message.ToString
        Finally
            'Step 5: Remember to close the documnet
            myDocument.Close()

        End Try

    End Sub
    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strAmali1 As TextBox = CType(row.FindControl("Markah"), TextBox)

            '--validate NUMBER and less than 100
            '--amali1
            If Not strAmali1.Text.Length = 0 Then
                'If oCommon.IsCurrency(strAmali1.Text) = False Then
                '    Return False
                'End If
                If CInt(strAmali1.Text) > 100 Then
                    Return False
                End If
            Else
                strAmali1.Text = ""
            End If


        Next

        Return True
    End Function
End Class