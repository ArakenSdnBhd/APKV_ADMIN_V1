Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class markah_bmsetara
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""
        lblMsgResult.Text = ""
        Try
            If Not IsPostBack Then

                strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
                lblUserId.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT UserType FROM kpmkv_users WHERE LoginID='" & Server.HtmlEncode(Request.Cookies("kpmkv_loginid").Value) & "'"
                lblJenisPemeriksa.Text = oCommon.getFieldValue(strSQL)

                kpmkv_kolej_list()

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
            lblMsgResult.Text = ex.Message
        End Try
    End Sub
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod pemeriksa."

                lblMsgResult.Text = "Tiada rekod pemeriksa."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            lblMsgResult.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function
    Private Sub kpmkv_kolej_list()
        If LblJenisPemeriksa.Text = "ADMIN" Then
            strSQL = " SELECT KodKolej FROM kpmkv_pemeriksa WHERE Tahun='" & Now.Year & "' ORDER By KodKolej"
        Else
            strSQL = " SELECT KodKolej FROM kpmkv_pemeriksa WHERE kpmkv_pemeriksa.UserID='" & lblUserId.Text & "' AND Tahun='" & Now.Year & "'"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodPusat.DataSource = ds
            ddlKodPusat.DataTextField = "KodKolej"
            ddlKodPusat.DataValueField = "KodKolej"
            ddlKodPusat.DataBind()

            '--ALL
            'ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            lblMsgResult.Text = "System Error:" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pemeriksa.Tahun, kpmkv_pemeriksa.Semester, kpmkv_pemeriksa.Sesi ASC"

        If LblJenisPemeriksa.Text = "ADMIN" Then
            tmpSQL = " SELECT PemeriksaID,Tahun,Semester,Sesi,KodKolej,KertasNo FROM kpmkv_pemeriksa"
            strWhere = " WHERE kpmkv_pemeriksa.KodKolej='" & ddlKodPusat.Text & "'"
            strWhere += " AND Sesi='" & chkSesi.Text & "' AND Tahun='" & Now.Year & "'"
            ''--debug
            ''Response.Write(getSQL)
        Else
            tmpSQL = " SELECT PemeriksaID,Tahun,Semester,Sesi,KodKolej,KertasNo FROM  kpmkv_pemeriksa "
            strWhere = " WHERE kpmkv_pemeriksa.KodKolej='" & ddlKodPusat.Text & "' AND kpmkv_pemeriksa.UserID='" & lblUserId.Text & "'"
            strWhere += " AND Sesi='" & chkSesi.Text & "' AND Tahun='" & Now.Year & "'"
            ''--debug
            ''Response.Write(getSQL)

        End If
        getSQL = tmpSQL & strWhere & strOrder
        Return getSQL

    End Function

    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblMsg.Text = ""
        lblMsgResult.Text = ""
        Dim strKertas As String = ""

        If (e.CommandName = "Pilih") = True Then

            Dim strkeyID = Int32.Parse(e.CommandArgument.ToString())
            strSQL = "SELECT KertasNo from kpmkv_pemeriksa WHERE PemeriksaID='" & strkeyID & "'"
            strKertas = oCommon.getFieldValue(strSQL)

            If strKertas = "1" Then
                Response.Redirect("admin.bmsetara.kertas1.aspx?PemeriksaID=" & strkeyID)
            ElseIf strKertas = "2" Then
                Response.Redirect("admin.bmsetara.kertas2.aspx?PemeriksaID=" & strkeyID)
            Else
                divMsgResult.Attributes("class") = "error"
                lblMsgResult.Text = "Tiada Tetapan pada kertas no!"
            End If

        End If
            strRet = BindData(datRespondent)

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
    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        lblMsgResult.Text = ""
        strRet = BindData(datRespondent)
    End Sub
End Class

