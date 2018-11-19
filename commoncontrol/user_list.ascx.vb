Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class user_list
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

                kpmkv_status_list()
                ddlStatus.SelectedValue = "2"

                kpmkv_negeri_list()
                ddlNegeri.Text = "0"

                kpmkv_jenis_list()
                ddlJenis.Text = "0"

                kpmkv_kolej_list()
                ddlKolej.Text = "0"

                kpmkv_Kumpulan_Pengguna()
                ddlKumpulan.Text = "0"

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri ORDER BY Negeri"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Negeri"
            ddlNegeri.DataValueField = "Negeri"
            ddlNegeri.DataBind()

            '--ALL
            ddlNegeri.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej ORDER BY Jenis"
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

            '--ALL
            ddlJenis.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKolej.DataSource = ds
            ddlKolej.DataTextField = "Nama"
            ddlKolej.DataValueField = "RecordID"
            ddlKolej.DataBind()

            '--ALL
            ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_Kumpulan_Pengguna()
        strSQL = "SELECT UserGroup FROM tbl_ctrl_usergroup ORDER BY UserGroup ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKumpulan.DataSource = ds
            ddlKumpulan.DataTextField = "UserGroup"
            ddlKumpulan.DataValueField = "UserGroup"
            ddlKumpulan.DataBind()

            '--ALL
            ddlKumpulan.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message


        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_status_list()
        strSQL = "SELECT Status,StatusID FROM kpmkv_status  ORDER BY Kod"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStatus.DataSource = ds
            ddlStatus.DataTextField = "Status"
            ddlStatus.DataValueField = "StatusID"
            ddlStatus.DataBind()

            '--ALL
            'ddlStatus.Items.Add(New ListItem("PILIH", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
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
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_users.UserID,kpmkv_users.Nama ASC"

        tmpSQL = "SELECT kpmkv_users.UserID,kpmkv_users.Nama as Kolej, kpmkv_users.LoginID, kpmkv_users.Pwd, kpmkv_users.UserType, kpmkv_users.Nama,"
        tmpSQL += "  kpmkv_users.Mykad,kpmkv_users.Negeri, kpmkv_status.Status, kpmkv_users.Email, kpmkv_users.Tel"
        tmpSQL += " FROM  kpmkv_users LEFT OUTER JOIN kpmkv_kolej ON kpmkv_users.RecordID=kpmkv_kolej.RecordID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_status ON kpmkv_users.StatusID=kpmkv_status.StatusID"
        strWhere = " WHERE kpmkv_users.UserType<>'SYSADMIN'"

        If Not ddlNegeri.SelectedValue = "0" Then
            strWhere += "AND kpmkv_users.Negeri='" & ddlNegeri.SelectedValue & "'"
        End If
        'kolej
        If Not ddlKolej.SelectedValue = "0" Then
            strWhere += "AND kpmkv_users.RecordID='" & ddlKolej.SelectedValue & "'"
        End If
        '--txtNama
        If Not txtNama.Text.Length = 0 Then
            strWhere += " AND kpmkv_users.Nama LIKE '%" & oCommon.FixSingleQuotes(txtNama.Text) & "%'"
        End If

        '--txtMYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND kpmkv_users.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        '--Status
        If Not ddlStatus.Text = "" Then
            strWhere += " AND kpmkv_users.StatusID='" & ddlStatus.SelectedValue & "'"
        End If
        '--Status
        If Not ddlKumpulan.SelectedValue = "0" Then
            strWhere += " AND kpmkv_users.UserType='" & ddlKumpulan.SelectedValue & "'"
        End If
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub
    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblMsg.Text = ""
        If (e.CommandName = "padam") = True Then

            Dim UserID = Int32.Parse(e.CommandArgument.ToString())

            strSQL = "UPDATE kpmkv_users SET StatusID='1' WHERE UserID='" & UserID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Pengguna berjaya dipadamkan"
                Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Pengguna tidak berjaya dipadamkn"
                End If

        strRet = BindData(datRespondent)

    End Sub
    'Private Sub ExportToCSV(ByVal strQuery As String)
    '    'Get the data from database into datatable 
    '    Dim cmd As New SqlCommand(strQuery)
    '    Dim dt As DataTable = GetData(cmd)

    '    Response.Clear()
    '    Response.Buffer = True
    '    Response.AddHeader("content-disposition", "attachment;filename=FileExport.csv")
    '    Response.Charset = ""
    '    Response.ContentType = "application/text"


    '    Dim sb As New StringBuilder()
    '    For k As Integer = 0 To dt.Columns.Count - 1
    '        'add separator 
    '        sb.Append(dt.Columns(k).ColumnName + ","c)
    '    Next

    '    'append new line 
    '    sb.Append(vbCr & vbLf)
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        For k As Integer = 0 To dt.Columns.Count - 1
    '            '--add separator 
    '            'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

    '            'cleanup here
    '            If k <> 0 Then
    '                sb.Append(",")
    '            End If

    '            Dim columnValue As Object = dt.Rows(i)(k).ToString()
    '            If columnValue Is Nothing Then
    '                sb.Append("")
    '            Else
    '                Dim columnStringValue As String = columnValue.ToString()

    '                Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

    '                If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
    '                    ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
    '                    cleanedColumnValue = "=" & cleanedColumnValue
    '                End If
    '                sb.Append(cleanedColumnValue)
    '            End If

    '        Next
    '        'append new line 
    '        sb.Append(vbCr & vbLf)
    '    Next
    '    Response.Output.Write(sb.ToString())
    '    Response.Flush()
    '    Response.End()

    'End Sub

    'Protected Function CleanCSVString(ByVal input As String) As String
    '    Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
    '    Return output

    'End Function

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

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        strSQL = "SELECT RecordID FROM kpmkv_users WHERE UserID='" & strKeyID & "'"
        Dim strUserKolej As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT UserType FROM kpmkv_users WHERE UserID='" & strKeyID & "'"
        Dim strUserType As String = oCommon.getFieldValue(strSQL)
        If strUserType = "SU-KOLEJ" Then
           
            Response.Redirect("kolej.view.aspx?RecordID=" & strUserKolej)

        ElseIf strUserType = "PENSYARAH" Then

            strSQL = "SELECT MyKad FROM kpmkv_users WHERE UserID='" & strKeyID & "'"
            Dim strMykadID As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PensyarahID FROM kpmkv_pensyarah WHERE MyKad='" & strMykadID & "'"
            Dim strPensyarahID As Integer = oCommon.getFieldValueInt(strSQL)

            Response.Redirect("pensyarah.view.aspx?PensyarahID=" & strPensyarahID)

        Else
            Response.Redirect("user.view.aspx?UserID=" & strKeyID)
        End If
    End Sub
    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
    End Sub
End Class