Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class modul_view
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
                    lblKod.Text = ds.Tables(0).Rows(0).Item("KodModul")
                Else
                    lblKod.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaModul")) Then
                    lblNama.Text = ds.Tables(0).Rows(0).Item("NamaModul")
                Else
                    lblNama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("JamKredit")) Then
                    lblJamKredit.Text = ds.Tables(0).Rows(0).Item("JamKredit")
                Else
                    lblJamKredit.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PBAmali")) Then
                    lblPBAmali.Text = ds.Tables(0).Rows(0).Item("PBAmali")
                Else
                    lblPBAmali.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PBTeori")) Then
                    lblPBTeori.Text = ds.Tables(0).Rows(0).Item("PBTeori")
                Else
                    lblPBTeori.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PAAmali")) Then
                    lblPAAmali.Text = ds.Tables(0).Rows(0).Item("PAAmali")
                Else
                    lblPAAmali.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PATeori")) Then
                    lblPATeori.Text = ds.Tables(0).Rows(0).Item("PATeori")
                Else
                    lblPATeori.Text = ""
                End If

            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Protected Sub btnExecute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExecute.Click
        Response.Redirect("modul.update.aspx?ModulID=" & Request.QueryString("ModulID"))

    End Sub
    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function
End Class