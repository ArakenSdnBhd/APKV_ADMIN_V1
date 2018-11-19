Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class matapelajaran_update
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

                strSQL = "SELECT KodMataPelajaran FROM kpmkv_matapelajaran WHERE MataPelajaranID='" & Request.QueryString("MataPelajaranID") & "'"
                lblKodMataPelajaran.Text = oCommon.getFieldValue(strSQL)

                LoadPage()
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub LoadPage()
        strSQL = "SELECT * FROM kpmkv_matapelajaran WHERE MataPelajaranID='" & Request.QueryString("MataPelajaranID") & "'"
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


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KodMataPelajaran")) Then
                    txtKodMataPelajaran.Text = ds.Tables(0).Rows(0).Item("KodMataPelajaran")
                Else
                    txtKodMataPelajaran.Text = ""
                End If
                lblKodMataPelajaran.Text = txtKodMataPelajaran.Text   '--to check duplicate

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaMataPelajaran")) Then
                    txtNamaMataPelajaran.Text = ds.Tables(0).Rows(0).Item("NamaMataPelajaran")
                Else
                    txtNamaMataPelajaran.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("JamKredit")) Then
                    txtJamKredit.Text = ds.Tables(0).Rows(0).Item("JamKredit")
                Else
                    txtJamKredit.Text = ""
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
        If txtKodMataPelajaran.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Kod MataPelajaran!"
            txtKodMataPelajaran.Focus()
            Return False
        End If

        If Not lblKodMataPelajaran.Text = txtKodMataPelajaran.Text Then       '--changes made to the kod
            strSQL = "SELECT KodMataPelajaran FROM kpmkv_matapelajaran WHERE KodMataPelajaran='" & oCommon.FixSingleQuotes(txtKodMataPelajaran.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Kod MataPelajaran telah digunakan. Sila masukkan kod yang baru."
                Return False
            End If
        End If

        '--txtNama
        If txtNamaMataPelajaran.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama MataPelajaran!"
            txtNamaMataPelajaran.Focus()
            Return False
        End If

        strSQL = "SELECT * FROM kpmkv_matapelajaran WHERE KodMataPelajaran='" & txtKodMataPelajaran.Text & "' and NamaMataPelajaran='" & txtNamaMataPelajaran.Text & "' and JamKredit='" & txtJamKredit.Text & "' and IsDeleted='N'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Kod MataPelajaran telah digunakan. Sila masukkan kod yang baru."
            Return False
        End If
        Return True
    End Function

    Private Function kpmkv_modul_update() As Boolean
        strSQL = "UPDATE kpmkv_matapelajaran WITH (UPDLOCK) SET KodMataPelajaran='" & oCommon.FixSingleQuotes(txtKodMataPelajaran.Text.ToUpper) & "',NamaMataPelajaran='" & oCommon.FixSingleQuotes(txtNamaMataPelajaran.Text.ToUpper) & "',JamKredit='" & txtJamKredit.Text & "' WHERE MataPelajaranID='" & Request.QueryString("MataPelajaranID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strSQL = "UPDATE kpmkv_matapelajaran WITH (UPDLOCK) SET IsDeleted='Y' WHERE MataPelajaranID='" & Request.QueryString("MataPelajaranID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya meghapuskan rekod MataPelajaran tersebut."
        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub

    Private Sub lnkList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkList.Click
        Response.Redirect("matapelajaran.list.aspx")

    End Sub

End Class