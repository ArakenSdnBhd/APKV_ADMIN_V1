Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class user_view_master
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_users_view()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub kpmkv_users_view()
        Try
            lblLoginID.Text = Request.Cookies("kpmkv_loginid").Value
            'Code Asal'
            'strSQL = "SELECT Nama FROM kpmkv_users WHERE LoginID='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
            'lblNama.Text = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT MYKAD FROM kpmkv_pensyarah WHERE MYKAD='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
            strRet = oCommon.isExist(strSQL)

            If strRet = True Then
                strSQL = "SELECT Nama FROM kpmkv_pensyarah WHERE MYKAD='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
                lblNama.Text = oCommon.getFieldValue(strSQL)
            Else
                strSQL = "SELECT Nama FROM kpmkv_users WHERE LoginID='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
                lblNama.Text = oCommon.getFieldValue(strSQL)
            End If

        Catch ex As Exception
            Response.Redirect("logout.aspx?msg=You have logout from other browser or window. Please login again.")

        End Try
    End Sub

End Class