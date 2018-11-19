Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class gred_view
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
        strSQL = "SELECT * FROM kpmkv_gred WHERE GredID='" & Request.QueryString("GredID") & "'"
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MarkahFrom")) Then
                    lblMarkahMula.Text = ds.Tables(0).Rows(0).Item("MarkahFrom")
                Else
                    lblMarkahMula.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MarkahTo")) Then
                    lblMarkahAkhir.Text = ds.Tables(0).Rows(0).Item("MarkahTo")
                Else
                    lblMarkahAkhir.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Gred")) Then
                    lblGred.Text = ds.Tables(0).Rows(0).Item("Gred")
                Else
                    lblGred.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pointer")) Then
                    lblPointer.Text = ds.Tables(0).Rows(0).Item("Pointer")
                Else
                    lblPointer.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Status")) Then
                    lblStatus.Text = ds.Tables(0).Rows(0).Item("Status")
                Else
                    lblStatus.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kompentasi")) Then
                    lblKompetensi.Text = ds.Tables(0).Rows(0).Item("Kompentasi")
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
        Response.Redirect("gred.update.aspx?GredID=" & Request.QueryString("GredID"))
    End Sub

End Class