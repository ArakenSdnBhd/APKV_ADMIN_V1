Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class modul_update
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

                strSQL = "SELECT KodModul FROM kpmkv_modul WHERE ModulID='" & Request.QueryString("ModulID") & "'"
                lblKod.Text = oCommon.getFieldValue(strSQL)

                LoadPage()
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub LoadPage()
        strSQL = "SELECT * FROM kpmkv_modul WHERE ModulID='" & Request.QueryString("ModulID") & "'"
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

               
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KodModul")) Then
                    txtKod.Text = ds.Tables(0).Rows(0).Item("KodModul")
                Else
                    txtKod.Text = ""
                End If
                lblKod.Text = txtKod.Text   '--to check duplicate

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaModul")) Then
                    txtNama.Text = ds.Tables(0).Rows(0).Item("NamaModul")
                Else
                    txtNama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("JamKredit")) Then
                    txtJamKredit.Text = ds.Tables(0).Rows(0).Item("JamKredit")
                Else
                    txtJamKredit.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PBAmali")) Then
                    txtPBAmali.Text = ds.Tables(0).Rows(0).Item("PBAmali")
                Else
                    txtPBAmali.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PBTeori")) Then
                    txtPBTeori.Text = ds.Tables(0).Rows(0).Item("PBTeori")
                Else
                    txtPBTeori.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PAAmali")) Then
                    txtPAAmali.Text = ds.Tables(0).Rows(0).Item("PAAmali")
                Else
                    txtPAAmali.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PATeori")) Then
                    txtPATeori.Text = ds.Tables(0).Rows(0).Item("PATeori")
                Else
                    txtPATeori.Text = ""
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
            If kpmkv_modul_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat Modul."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        '--txtKod
        If txtKod.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Kod Modul!"
            txtKod.Focus()
            Return False
        End If

        If Not lblKod.Text = txtKod.Text Then       '--changes made to the kod
            strSQL = "SELECT KodModul FROM kpmkv_modul WHERE KodModul='" & oCommon.FixSingleQuotes(txtKod.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Kod Modul telah digunakan. Sila masukkan kod yang baru."
                Return False
            End If
        End If

        '--txtNama
        If txtNama.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Modul!"
            txtNama.Focus()
            Return False
        End If

        strSQL = "SELECT * FROM kpmkv_modul WHERE Kod='" & txtKod.Text & "' and Nama='" & txtNama.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Kod Modul telah digunakan. Sila masukkan kod yang baru."
            Return False
        End If
        Return True
    End Function

    Private Function kpmkv_modul_update() As Boolean
        ' Dim strsemester As String = txtKod.Text.Substring(3, 1)

        strSQL = "UPDATE kpmkv_modul SET KodModul='" & oCommon.FixSingleQuotes(txtKod.Text.ToUpper) & "',NamaModul='" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "',JamKredit='" & txtJamKredit.Text & "',"
        strSQL += " PBAmali='" & txtPBAmali.Text & "',PBTeori='" & txtPBTeori.Text & "',PAAmali='" & txtPAAmali.Text & "',PATeori='" & txtPATeori.Text & "'"
        strSQL += " WHERE ModulID='" & Request.QueryString("ModulID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strSQL = "UPDATE kpmkv_modul WITH (UPDLOCK) SET IsDeleted='Y' WHERE ModulID='" & Request.QueryString("ModulID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya meghapuskan rekod Modul tersebut."
        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub

    Private Sub lnkList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkList.Click
        Response.Redirect("modul.list.aspx")

    End Sub


End Class