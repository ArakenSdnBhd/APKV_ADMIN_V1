Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class gred_create
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_jenis_list()

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT DISTINCT Jenis FROM kpmkv_gred ORDER BY Jenis"
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
    Private Function ValidatePage() As Boolean

        '--txtGed
        If txtGred.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Gred!"
            txtGred.Focus()
            Return False
        End If

        If Not lblGred.Text = txtGred.Text Then       '--changes made to the Gred
            strSQL = "SELECT Gred FROM kpmkv_gred WHERE Gred='" & oCommon.FixSingleQuotes(txtGred.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Gred telah digunakan. Sila masukkan gred yang baru."
                Return False
            End If
        End If

        '--txtPointer
        If txtPointer.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Pointer!"
            txtPointer.Focus()
            Return False
        End If

        '--txtMarkah
        If txtMarkahMula.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah Mula!"
            txtMarkahMula.Focus()
            Return False
        End If

        '--txtMarkah
        If txtMarkahAkhir.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah Akhir!"
            txtMarkahAkhir.Focus()
            Return False
        End If

        If CInt(txtMarkahMula.Text) > CInt(txtMarkahAkhir.Text) Then
            lblMsg.Text = "Markah Mula mesti lebih Kecik dari Markah Akhir"
            txtMarkahMula.Focus()
            Return False
        End If

        '--txtStatus
        If txtStatus.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan status gred!"
            txtStatus.Focus()
            Return False
        End If

        '--txtKompetensi
        If txtKompetensi.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Kompetensi!"
            txtKompetensi.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function insert() As Boolean

        strSQL = "INSERT kpmkv_gred(Pointer,MarkahFrom,MarkahTo,Gred,Status,Kompentasi,Jenis) VALUES ('" & oCommon.FixSingleQuotes(txtPointer.Text) & "','" & oCommon.FixSingleQuotes(txtMarkahMula.Text) & "','" & oCommon.FixSingleQuotes(txtMarkahAkhir.Text) & "','" & oCommon.FixSingleQuotes(txtGred.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStatus.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtKompetensi.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(ddlJenis.Text.ToUpper) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        '--Debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya daftar baru Gred"
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet

            Return False
        End If
        Response.Write(strSQL)
    End Function
    Protected Sub btnDaftar_Click(sender As Object, e As EventArgs) Handles btnDaftar.Click
        lblMsg.Text = ""
        If ValidatePage() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        Else
            Insert()
        End If
    End Sub

    Protected Sub btnList_Click(sender As Object, e As EventArgs) Handles btnList.Click
        Response.Redirect("admin.gred.list.aspx")
    End Sub
End Class