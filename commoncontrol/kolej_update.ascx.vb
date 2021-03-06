Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class kolej_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        Try
            If Not IsPostBack Then
                kpmkv_jeniskolej_list()
                kpmkv_negeri_list()

                strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID='" & Request.QueryString("RecordID") & "'"
                lblKod.Text = oCommon.getFieldValue(strSQL)


                LoadPage()
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try

    End Sub

    Private Sub kpmkv_jeniskolej_list()
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

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
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


    Private Sub LoadPage()
        strSQL = "SELECT * FROM kpmkv_kolej WHERE RecordID='" & Request.QueryString("RecordID") & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Jenis")) Then
                    ddlJenis.Text = ds.Tables(0).Rows(0).Item("Jenis")
                Else
                    ddlJenis.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kod")) Then
                    txtKod.Text = ds.Tables(0).Rows(0).Item("Kod")
                Else
                    txtKod.Text = ""
                End If
                lblKod.Text = txtKod.Text   '--to check duplicate

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Nama")) Then
                    txtNama.Text = ds.Tables(0).Rows(0).Item("Nama")
                Else
                    txtNama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tel")) Then
                    txtTel.Text = ds.Tables(0).Rows(0).Item("Tel")
                Else
                    txtTel.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fax")) Then
                    txtFax.Text = ds.Tables(0).Rows(0).Item("Fax")
                Else
                    txtFax.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Email")) Then
                    txtEmail.Text = ds.Tables(0).Rows(0).Item("Email")
                Else
                    txtEmail.Text = ""
                End If

                '--alamat
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Alamat1")) Then
                    txtAlamat1.Text = ds.Tables(0).Rows(0).Item("Alamat1")
                Else
                    txtAlamat1.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Alamat2")) Then
                    txtAlamat2.Text = ds.Tables(0).Rows(0).Item("Alamat2")
                Else
                    txtAlamat2.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Poskod")) Then
                    txtPoskod.Text = ds.Tables(0).Rows(0).Item("Poskod")
                Else
                    txtPoskod.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Bandar")) Then
                    txtBandar.Text = ds.Tables(0).Rows(0).Item("Bandar")
                Else
                    txtBandar.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Negeri")) Then
                    ddlNegeri.Text = ds.Tables(0).Rows(0).Item("Negeri")
                Else
                    ddlNegeri.Text = ""
                End If

                '--PEngarah
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaPengarah")) Then
                    txtNamaPengarah.Text = ds.Tables(0).Rows(0).Item("NamaPengarah")
                Else
                    txtNamaPengarah.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EmailPengarah")) Then
                    txtEmailPengarah.Text = ds.Tables(0).Rows(0).Item("EmailPengarah")
                Else
                    txtEmailPengarah.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TelPengarah")) Then
                    txtTelPengarah.Text = ds.Tables(0).Rows(0).Item("TelPengarah")
                Else
                    txtTelPengarah.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MobilePengarah")) Then
                    txtMobilePengarah.Text = ds.Tables(0).Rows(0).Item("MobilePengarah")
                Else
                    txtMobilePengarah.Text = ""
                End If

                '--JPP
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaKJPP")) Then
                    txtNamaKJPP.Text = ds.Tables(0).Rows(0).Item("NamaKJPP")
                Else
                    txtNamaKJPP.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EmailKJPP")) Then
                    txtEmailKJPP.Text = ds.Tables(0).Rows(0).Item("EmailKJPP")
                Else
                    txtEmailKJPP.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TelKJPP")) Then
                    txtTelKJPP.Text = ds.Tables(0).Rows(0).Item("TelKJPP")
                Else
                    txtTelKJPP.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MobileKJPP")) Then
                    txtBimbitKJPP.Text = ds.Tables(0).Rows(0).Item("MobileKJPP")
                Else
                    txtBimbitKJPP.Text = ""
                End If

                '--SUP
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaSUP")) Then
                    txtNamaSUP.Text = ds.Tables(0).Rows(0).Item("NamaSUP")
                Else
                    txtNamaSUP.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("JawatanSUP")) Then
                    txtJawatanSUP.Text = ds.Tables(0).Rows(0).Item("JawatanSUP")
                Else
                    txtJawatanSUP.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("GredSUP")) Then
                    txtGredSUP.Text = ds.Tables(0).Rows(0).Item("GredSUP")
                Else
                    txtGredSUP.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EmailSUP")) Then
                    txtEmailSUP.Text = ds.Tables(0).Rows(0).Item("EmailSUP")
                Else
                    txtEmailSUP.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MobileSUP")) Then
                    txtMobileSUP.Text = ds.Tables(0).Rows(0).Item("MobileSUP")
                Else
                    txtMobileSUP.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TelSUP")) Then
                    txtTelSUP.Text = ds.Tables(0).Rows(0).Item("TelSUP")
                Else
                    txtTelSUP.Text = ""
                End If

            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_kolej_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat Kolej."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        '--txtNamaPengarah
        If txtNamaPengarah.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Pengarah!"
            txtNamaPengarah.Focus()
            Return False
        End If
        '--txtJawatanPengarah
        If txtJawatanPengarah.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Jawatan Pengarah!"
            txtJawatanPengarah.Focus()
            Return False
        End If
        '--txtGredPengarah
        If txtGredPengarah.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Gred Pengarah!"
            txtGredPengarah.Focus()
            Return False
        End If
        '--txtEmailPengarah
        If txtEmailPengarah.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Email Pengarah!"
        ElseIf oCommon.isEmail(txtEmailPengarah.Text) = False Then
            lblMsg.Text = "Emel Pengarah tidak sah. (Contoh: Pengarah@contoh.com)"
            Return False
        End If
        ''--txtMobilePengarah
        'If txtMobilePengarah.Text.Length = 0 Then
        '    lblMsg.Text = "Sila masukkan Telefon Bimbit Pengarah!"
        'ElseIf oCommon.ValidatePhone(txtMobilePengarah.Text) = False Then
        '    lblMsg.Text = "Sila masukkan Telefon Bimbit Pengarah [0##-###### ]!"
        '    txtMobilePengarah.Focus()
        '    Return False
        'End If
        ''--txtTelPengarah
        'If txtTelPengarah.Text.Length = 0 Then
        '    lblMsg.Text = "Sila masukkan Telefon Pengarah!"
        'ElseIf oCommon.IsTextValidated(txtTelPengarah.Text) = False Then
        '    lblMsg.Text = "Sila masukkan Telefon Pengarah [0#-###### ]!"
        '    txtTelPengarah.Focus()
        '    Return False
        'End If

        '--txtNamaKJPP
        If txtNamaKJPP.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama KJPP!"
            txtNamaKJPP.Focus()
            Return False
        End If
        '--txtJawatanKJPP
        If txtJawatanKJPP.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Jawatan KJPP!"
            txtJawatanKJPP.Focus()
            Return False
        End If
        '--txtGredKJPP
        If txtGredKJPP.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Gred KJPP!"
            txtGredKJPP.Focus()
            Return False
        End If
        '--txtEmailKJPP
        If txtEmailKJPP.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Email KJPP!"
        ElseIf oCommon.isEmail(txtEmailKJPP.Text) = False Then
            lblMsg.Text = "Emel KJPP tidak sah. (Contoh: KJPP@contoh.com)"
            txtEmailKJPP.Focus()
            Return False
        End If
        ''--txtMobileKJPP
        'If txtBimbitKJPP.Text.Length = 0 Then
        '    lblMsg.Text = "Sila masukkan Telefon Bimbit KJPP!"
        'ElseIf oCommon.ValidatePhone(txtBimbitKJPP.Text) = False Then
        '    lblMsg.Text = "Sila masukkan Telefon Bimbit KJPP [0##-###### ]!"
        '    txtBimbitKJPP.Focus()
        '    Return False
        'End If
        ''--txtTelKJPP
        'If txtTelKJPP.Text.Length = 0 Then
        '    lblMsg.Text = "Sila masukkan Telefon KJPP!"
        'ElseIf oCommon.ValidatePhone(txtTelKJPP.Text) = False Then
        '    lblMsg.Text = "Sila masukkan Telefon KJPP [0#-###### ]!"
        '    txtTelKJPP.Focus()
        '    Return False
        'End If

        '--txtNamaSUP
        If txtNamaSUP.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama SUP!"
            txtNamaSUP.Focus()
            Return False
        End If
        '--txtJawatanSUP
        If txtJawatanSUP.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Jawatan SUP!"
            txtJawatanSUP.Focus()
            Return False
        End If
        '--txtGredSUP
        If txtGredSUP.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Gred SUP!"
            txtGredSUP.Focus()
            Return False
        End If
        '--txtEmailSUP
        If txtEmailSUP.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Email SUP!"
        ElseIf oCommon.isEmail(txtEmailSUP.Text) = False Then
            lblMsg.Text = "Emel SUP tidak sah. (Contoh: SUP@contoh.com)"
            txtEmailSUP.Focus()
            Return False
        End If
        ''--txtMobileSUP
        'If txtMobileSUP.Text.Length = 0 Then
        '    lblMsg.Text = "Sila masukkan Telefon Bimbit SUP!"
        'ElseIf oCommon.ValidatePhone(txtMobileSUP.Text) = False Then
        '    lblMsg.Text = "Sila masukkan Telefon SUP [0##-###### ]!"
        '    txtMobileSUP.Focus()
        '    Return False
        'End If
        ''--txtTelSUP
        'If txtTelSUP.Text.Length = 0 Then
        '    lblMsg.Text = "Sila masukkan Telefon SUP!"
        'ElseIf oCommon.ValidatePhone(txtTelSUP.Text) = False Then
        '    lblMsg.Text = "Sila masukkan Telefon SUP [0#-###### ]!"
        '    txtTelSUP.Focus()
        '    Return False
        'End If

        Return True
    End Function

    Private Function kpmkv_kolej_update() As Boolean
        strSQL = "UPDATE kpmkv_kolej SET Jenis='" & oCommon.FixSingleQuotes(ddlJenis.Text) & "',Kod='" & oCommon.FixSingleQuotes(txtKod.Text.ToUpper) & "',Nama='" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "',Tel='" & oCommon.FixSingleQuotes(txtTel.Text) & "',Fax='" & oCommon.FixSingleQuotes(txtFax.Text) & "',Email='" & oCommon.FixSingleQuotes(txtEmail.Text) & "',Alamat1='" & oCommon.FixSingleQuotes(txtAlamat1.Text.ToUpper) & "',Alamat2='" & oCommon.FixSingleQuotes(txtAlamat2.Text.ToUpper) & "',Poskod='" & oCommon.FixSingleQuotes(txtPoskod.Text.ToUpper) & "',Bandar='" & oCommon.FixSingleQuotes(txtBandar.Text.ToUpper) & "',Negeri='" & oCommon.FixSingleQuotes(ddlNegeri.Text) & "',NamaPengarah='" & txtNamaPengarah.Text.ToUpper & "', EmailPengarah ='" & txtEmailPengarah.Text & "', TelPengarah ='" & txtTelPengarah.Text & "', MobilePengarah ='" & txtMobilePengarah.Text & "', "
        strSQL += "JawatanPengarah='" & txtJawatanPengarah.Text.ToUpper & "', GredPengarah='" & txtGredPengarah.Text.ToUpper & "', NamaKJPP='" & txtNamaKJPP.Text.ToUpper & "', EmailKJPP='" & txtEmailKJPP.Text & "', TelKJPP='" & txtTelKJPP.Text & "', MobileKJPP='" & txtBimbitKJPP.Text & "', "
        strSQL += "JawatanKJPP='" & txtJawatanKJPP.Text.ToUpper & "', GredKJPP='" & txtGredKJPP.Text.ToUpper & "', NamaSUP='" & txtNamaSUP.Text.ToUpper & "', EmailSUP='" & txtEmailSUP.Text & "', TelSUP='" & txtTelSUP.Text & "', MobileSUP='" & txtMobileSUP.Text & "', JawatanSUP='" & txtJawatanSUP.Text.ToUpper & "', GredSUP='" & txtGredSUP.Text.ToUpper & "'"
        strSQL += " WHERE REcordID='" & Request.QueryString("RecordID") & "'"

        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strSQL = "UPDATE kpmkv_kolej SET IsDeleted='Y' WHERE REcordID='" & Request.QueryString("RecordID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            strSQL = "UPDATE kpmkv_users StatusID='1' WHERE REcordID='" & Request.QueryString("RecordID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            lblMsg.Text = "Berjaya meghapuskan rekod Kolej tersebut."
        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub

   
End Class