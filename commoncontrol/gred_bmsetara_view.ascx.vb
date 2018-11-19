Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class gred_bmsetara_view
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
        strSQL = "SELECT * FROM kpmkv_gred_bmsetara WHERE GredbsID='" & Request.QueryString("GredbsID") & "'"
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sesi")) Then
                    lblSesi.Text = ds.Tables(0).Rows(0).Item("Sesi")
                Else
                    lblSesi.Text = ""
                End If


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MarkahFrom")) Then
                    lblMarkahFrom.Text = ds.Tables(0).Rows(0).Item("MarkahFrom")
                Else
                    lblMarkahFrom.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MarkahTo")) Then
                    lblMarkahTo.Text = ds.Tables(0).Rows(0).Item("MarkahTo")
                Else
                    lblMarkahTo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Gred")) Then
                    lblGred.Text = ds.Tables(0).Rows(0).Item("Gred")
                Else
                    lblGred.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Status")) Then
                    lblStatus.Text = ds.Tables(0).Rows(0).Item("Status")
                Else
                    lblStatus.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kompetensi")) Then
                    lblKompetensi.Text = ds.Tables(0).Rows(0).Item("Kompetensi")
                Else
                    lblKompetensi.Text = ""
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
        Response.Redirect("gred.bmsetara.update.aspx?GredbsID=" & Request.QueryString("GredbsID"))
    End Sub
End Class