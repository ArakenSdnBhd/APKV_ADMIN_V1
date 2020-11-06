Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class admin_khas_ulang_akademik_markah_khas
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""

        Try
            If Not IsPostBack Then

                strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
                lblID.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT UserType FROM kpmkv_users WHERE LoginID='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
                lblType.Text = oCommon.getFieldValue(strSQL)

                If lblType.Text = "ADMIN" Then
                Else
                    strSQL = " SELECT Distinct Subjek FROM kpmkv_pemeriksa "
                    strSQL += " WHERE UserID='" & lblID.Text & "' AND Tahun='" & Now.Year & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)
                    'Response.Write(strSQL)
                    ''--get pemeriksa info
                    Dim ar_pemeriksa As Array
                    ar_pemeriksa = strRet.Split("|")
                    lblMatapelajaran.Text = ar_pemeriksa(0)
                End If

                loadStores()
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Protected Sub loadStores()
        objConn.Open()
        If lblType.Text = "ADMIN" Then
            strSQL = "SELECT * from kpmkv_markah_khas_akademik WHERE Tahun='" & Now.Year & "'"
        Else
            strSQL = "SELECT * from kpmkv_markah_khas_akademik WHERE PemeriksaID='" & lblID.Text & "' AND Tahun='" & Now.Year & "'"
        End If

        Dim cmd As New SqlCommand(strSQL, objConn)

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        da.Fill(ds)
        Dim count As Integer = ds.Tables(0).Rows.Count
        objConn.Close()
        If ds.Tables(0).Rows.Count > 0 Then
            gridView.DataSource = ds
            gridView.DataBind()
        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            gridView.DataSource = ds
            gridView.DataBind()
            Dim columncount As Integer = gridView.Rows(0).Cells.Count
            lblMsg.Text = " Tiada Rekod !!!"
        End If
    End Sub
    Protected Sub gridView_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gridView.EditIndex = e.NewEditIndex
        loadStores()
    End Sub
    Protected Sub gridView_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim KhasID As String = gridView.DataKeys(e.RowIndex).Values("KhasID").ToString()
        objConn.Open()
        Dim cmd As New SqlCommand("DELETE FROM kpmkv_markah_khas_akademik WHERE KhasID=" + KhasID, objConn)
        Dim result As Integer = cmd.ExecuteNonQuery()
        objConn.Close()
        If result = 1 Then
            loadStores()
            lblMsg.Text = "      Berjaya Padam.......    "
        End If
    End Sub
    Protected Sub gridView_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim KhasID As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "KhasID"))
            Dim lnkbtnresult As Button = DirectCast(e.Row.FindControl("ButtonDelete"), Button)
            If lnkbtnresult IsNot Nothing Then
                lnkbtnresult.Attributes.Add("onclick", (Convert.ToString("javascript:return deleteConfirm('") & KhasID) + "')")
            End If
        End If
    End Sub
    Protected Sub gridView_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName.Equals("AddNew") Then
            Dim inMykad As TextBox = gridView.FooterRow.FindControl("inMykad")
            Dim inAngkaGiliran As TextBox = gridView.FooterRow.FindControl("inAngkaGiliran")
            Dim inKodKursus As TextBox = gridView.FooterRow.FindControl("inKodKursus")
            Dim inMataPelajaran As TextBox = gridView.FooterRow.FindControl("inMataPelajaran")
            Dim inMarkah As TextBox = gridView.FooterRow.FindControl("inMarkah")
            Dim inCatatan As TextBox = gridView.FooterRow.FindControl("inCatatan")

            'assign value to integer
            Dim strMykad As String = inMykad.Text
            Dim strAngkaGiliran As String = inAngkaGiliran.Text
            Dim strKodKursus As String = inKodKursus.Text
            Dim strMataPelajaran As String = inMataPelajaran.Text
            Dim strMarkah As String = inMarkah.Text
            Dim strCatatan As String = inCatatan.Text

            objConn.Open()
            If lblType.Text = "ADMIN" Then
                strSQL = "INSERT into kpmkv_markah_khas_akademik(Mykad,AngkaGiliran,KodKursus,MataPelajaran,Markah,Catatan,PemeriksaID,Tahun) values(@Mykad, @AngkaGiliran, @KodKursus, @MataPelajaran, @Markah, @Catatan, @PemeriksaID, @Tahun)"
            Else
                strSQL = "INSERT into kpmkv_markah_khas_akademik(Mykad,AngkaGiliran,KodKursus,MataPelajaran,Markah,Catatan,PemeriksaID,Tahun) values('" & strMykad & "','" & strAngkaGiliran & "','" & strKodKursus & "','" & strMataPelajaran & "','" & strMarkah & "','" & strCatatan & "','" & lblID.Text & "','" & Now.Year & "')"
            End If

            Using cmd As New SqlCommand(strSQL)
                Using sda As New SqlDataAdapter()
                    cmd.Connection = objConn
                    cmd.Parameters.AddWithValue("@Mykad", strMykad)
                    cmd.Parameters.AddWithValue("@AngkaGiliran", strAngkaGiliran)
                    cmd.Parameters.AddWithValue("@KodKursus", strKodKursus)
                    cmd.Parameters.AddWithValue("@MataPelajaran", strMataPelajaran)
                    cmd.Parameters.AddWithValue("@Markah", strMarkah)
                    cmd.Parameters.AddWithValue("@Catatan", strCatatan)
                    cmd.Parameters.AddWithValue("@PemeriksaID", lblID.Text)
                    cmd.Parameters.AddWithValue("@Tahun", Now.Year)
                    sda.SelectCommand = cmd

                    Dim result As Integer = cmd.ExecuteNonQuery()
                    objConn.Close()
                    If result = 1 Then
                        loadStores()

                        lblMsg.Text = inMykad.Text + "      Berjaya......    "
                    Else

                        lblMsg.Text = inMykad.Text + " Tidak Berjaya....."
                    End If

                End Using
            End Using

            'Dim cmd As New SqlCommand(strSQL, objConn)

            'Dim result As Integer = cmd.ExecuteNonQuery()
            'objConn.Close()
            'If result = 1 Then
            '    loadStores()

            '    lblMsg.Text = inMykad.Text + "      Berjaya......    "
            'Else

            '    lblMsg.Text = inMykad.Text + " Tidak Berjaya....."
            'End If

        End If
    End Sub

    Protected Sub btnCetak_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCetak.Click
        Dim myDocument As New Document(PageSize.A4)

        Try
            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=BorangMarkahKhas.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As Image = Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            Dim table As New PdfPTable(1)
            table.WidthPercentage = 100
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            Dim cell = New PdfPCell()
            Dim cetak As String = ""

            cetak = "BORANG MARKAH KHAS" & Environment.NewLine
            cetak += Environment.NewLine
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.UNDERLINE)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            table = New PdfPTable(6)
            table.WidthPercentage = 100
            table.SetWidths({10, 11, 12, 28, 12, 28})
            table.DefaultCell.Border = 0

            ''Mykad
            cell = New PdfPCell()
            cetak = ""

            cetak = "Mykad"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            ''AngkaGiliran
            cell = New PdfPCell()
            cetak = ""

            cetak = "AngkaGiliran"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            ''Markah Kertas 1
            cell = New PdfPCell()
            cetak = ""

            cetak = "Kod Kursus"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            ''Catatan
            cell = New PdfPCell()
            cetak = ""

            cetak = "Matapelajaran"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            ''Markah Kertas 2
            cell = New PdfPCell()
            cetak = ""

            cetak = "Markah"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            ''Catatan
            cell = New PdfPCell()
            cetak = ""

            cetak = "Catatan"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            Dim i As Integer

            For i = 0 To gridView.Rows.Count - 1

                Dim strMykad As Label = CType(gridView.Rows(i).FindControl("lblMykad"), Label)
                Dim strAngkaGiliran As Label = CType(gridView.Rows(i).FindControl("lblAngkaGiliran"), Label)
                Dim strKodKursus As Label = CType(gridView.Rows(i).FindControl("lblKodKursus"), Label)
                Dim strMatapelajaran As Label = CType(gridView.Rows(i).FindControl("lblMatapelajaran"), Label)
                Dim strMarkah As Label = CType(gridView.Rows(i).FindControl("lblMarkah"), Label)
                Dim strCatatan As Label = CType(gridView.Rows(i).FindControl("lblCatatan"), Label)

                table = New PdfPTable(6)
                table.WidthPercentage = 100
                table.SetWidths({10, 11, 16, 26, 11, 26})
                table.DefaultCell.Border = 0

                ''Mykad
                cell = New PdfPCell()
                cetak = ""

                cetak = strMykad.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''AngkaGiliran
                cell = New PdfPCell()
                cetak = ""

                cetak = strAngkaGiliran.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''Markah Kertas 1
                cell = New PdfPCell()
                cetak = ""

                cetak = strKodKursus.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''Catatan
                cell = New PdfPCell()
                cetak = ""

                cetak = strMatapelajaran.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''Markah Kertas 2
                cell = New PdfPCell()
                cetak = ""

                cetak = strMarkah.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''Catatan
                cell = New PdfPCell()
                cetak = ""

                cetak = strCatatan.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)

            Next

            myDocument.Close()

            HttpContext.Current.Response.Write(myDocument)
            HttpContext.Current.Response.End()

        Catch ex As Exception

        End Try
    End Sub

End Class