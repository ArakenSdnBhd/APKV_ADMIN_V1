Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class wajaranvokasional_view
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                LoadPage()

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub LoadPage()
        strSQL = "SELECT * FROM kpmkv_wajaran_v WHERE WajaranID='" & Request.QueryString("WajaranID") & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    lblTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    lblTahun.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Subjek")) Then
                    lblSubjek.Text = ds.Tables(0).Rows(0).Item("Subjek")
                Else
                    lblSubjek.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Teori")) Then
                    lblTeori.Text = ds.Tables(0).Rows(0).Item("Teori")
                Else
                    lblTeori.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Amali")) Then
                    lblAmali.Text = ds.Tables(0).Rows(0).Item("Amali")
                Else
                    lblAmali.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Jenis")) Then
                    lblJenis.Text = ds.Tables(0).Rows(0).Item("Jenis")
                Else
                    lblJenis.Text = ""
                End If



            End If
            ''--debug
            'Response.Write(strSQL)
        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub
    Protected Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click

        Response.Redirect("wajaranvokasional.update.aspx?WajaranID=" & Request.QueryString("WajaranID"))
    End Sub
End Class