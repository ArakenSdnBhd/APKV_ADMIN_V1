Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class gred_bmsetara_create
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblMsg.Text = ""

                kpmkv_tahun_list()

                LoadPage()

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY Tahun DESC"
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
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub LoadPage()
        strSQL = "SELECT Jenis FROM kpmkv_gred_bmsetara GROUP BY Jenis "
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
                    txtJenis.Text = ds.Tables(0).Rows(0).Item("Jenis")
                Else
                    txtJenis.Text = ""
                End If

            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function ValidatePage() As Boolean

        '--ddlTahun
        If ddlTahun.SelectedValue = "00" Then
            lblMsg.Text = "Sila pilih Jenis Tahun!"
            ddlTahun.Focus()
            Return False
        End If
        'sesi
        If chkSesi.Text = "" Then
            lblMsg.Text = "Sila pilih Sesi!"
            chkSesi.Focus()
            Return False
        End If
        '--txtGed
        If txtGred.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Gred!"
            txtGred.Focus()
            Return False
        End If
        If Not lblGred.Text = txtGred.Text Then       '--changes made to the Gred
            strSQL = "SELECT Gred FROM kpmkv_gred_bmsetara WHERE Gred='" & oCommon.FixSingleQuotes(txtGred.Text) & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.Text & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Gred telah digunakan. Sila masukkan gred yang baru."
                Return False
            End If
        End If

        '--txtMarkah
        If txtMarkahFrom.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah!"
            txtMarkahFrom.Focus()
            Return False
        End If

        '--txtMarkahTo
        If txtMarkahTo.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah!"
            txtMarkahTo.Focus()
            Return False
        End If

        If CInt(txtMarkahFrom.Text) > CInt(txtMarkahTo.Text) Then
            lblMsg.Text = "Markah Mula mesti lebih Kecik dari Markah Akhir"
            txtMarkahFrom.Focus()
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
    Private Function Insert() As Boolean

        strSQL = "INSERT kpmkv_gred_bmsetara(Tahun,Sesi,MarkahFrom,MarkahTo,Gred,Status,Kompetensi,Jenis) VALUES ('" & oCommon.FixSingleQuotes(ddlTahun.Text) & "','" & oCommon.FixSingleQuotes(chkSesi.Text) & "','" & oCommon.FixSingleQuotes(txtMarkahFrom.Text) & "','" & oCommon.FixSingleQuotes(txtMarkahTo.Text) & "','" & oCommon.FixSingleQuotes(txtGred.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStatus.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtKompetensi.Text.ToUpper) & "','BMSETARA')"
        strRet = oCommon.ExecuteSQL(strSQL)
        '--Debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya daftar baru Gred BM Setara "
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet

            Return False
        End If

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

    Protected Sub btlist_Click(sender As Object, e As EventArgs) Handles btlist.Click
        Response.Redirect("gred.bmsetara.list.aspx")
    End Sub
End Class