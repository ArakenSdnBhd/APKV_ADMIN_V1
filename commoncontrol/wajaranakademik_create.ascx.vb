
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class wajaranakademik_create
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
        strSQL = "SELECT Jenis FROM kpmkv_wajaran_a GROUP BY Jenis "
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
            '--Debug
            ''Response.Write(strSQL)
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
            If kpmkv_wajaranakademik_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat wajaran akademik ."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean

        '--ddlTahun
        If ddlTahun.SelectedValue = "00" Then
            lblMsg.Text = "Sila pilih Jenis Tahun!"
            ddlTahun.Focus()
            Return False
        End If

        '--txtSubjek
        If txtSubjek.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Subjek!"
            txtSubjek.Focus()
            Return False
        End If

        If Not lblSubjek.Text = txtSubjek.Text Then       '--changes made to the Subjek
            strSQL = "SELECT Gred FROM kpmkv_wajaran_a WHERE Subjek='" & oCommon.FixSingleQuotes(txtSubjek.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Subjek telah dimasukkan. Sila masukkan subjek yang baru."
                Return False
            End If
        End If

        '--txtBerterusan
        If txtBerterusan.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan nilai berterusan!"
            txtBerterusan.Focus()
            Return False
        End If

        '--txtAkhir
        If txtAkhir.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan nilai akhir!"
            txtAkhir.Focus()
            Return False
        End If

        '--txtjenis
        If txtJenis.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Jenis!"
            txtJenis.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function kpmkv_wajaranakademik_update() As Boolean

        strSQL = "INSERT kpmkv_wajaran_a(Tahun,Subjek,Berterusan,Akhir,Jenis) VALUES ('" & oCommon.FixSingleQuotes(ddlTahun.Text) & "','" & oCommon.FixSingleQuotes(txtSubjek.Text) & "','" & oCommon.FixSingleQuotes(txtBerterusan.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtAkhir.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtJenis.Text.ToUpper) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        '--Debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet

            Return False
        End If

    End Function
End Class