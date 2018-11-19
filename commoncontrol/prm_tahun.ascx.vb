Imports System.Data.SqlClient
Imports System.IO
Public Class prm_tahun1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("strTypeid") = ""


                If Not Request.QueryString("edit") = "" Then
                    Load_page()
                End If

                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    '--load for edit--'
    Private Sub Load_page()

        strSQL = " SELECT * FROM kpmkv_tahun"
        strSQL += " WHERE TahunID='" & Request.QueryString("edit") & "'"

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


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    txtTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    txtTahun.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kod")) Then
                    txtCode.Text = ds.Tables(0).Rows(0).Item("Kod")
                Else
                    txtCode.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("RunningNoDigit")) Then
                    txtLastNo.Text = ds.Tables(0).Rows(0).Item("RunningNoDigit")
                Else
                    txtLastNo.Text = "0"
                End If

                'If Not IsDBNull(ds.Tables(0).Rows(0).Item("Status")) Then
                '    ddlStatus.SelectedValue = ds.Tables(0).Rows(0).Item("Status")
                'Else
                '    ddlStatus.SelectedValue = ""
                'End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LastRunningNo")) Then
                    txtLastNo.Text = ds.Tables(0).Rows(0).Item("LastRunningNo")
                Else
                    txtLastNo.Text = "0"
                End If

            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub


    '--list--'
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""



        Dim strOrder As String = " ORDER BY Tahun DESC"

        tmpSQL = "SELECT * FROM kpmkv_tahun"



        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function

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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "No Record Found!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Total Records#:" & myDataSet.Tables(0).Rows.Count
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

    '-----add function-----'
    Private Function Save() As Boolean

        If Not Request.QueryString("edit") = "" Then

            strSQL = "UPDATE kpmkv_tahun SET "

            strSQL += " Tahun='" & oCommon.FixSingleQuotes(txtTahun.Text) & "',"

            strSQL += " Kod ='" & oCommon.FixSingleQuotes(txtCode.Text) & "',"
            strSQL += " RunningNoDigit ='" & oCommon.FixSingleQuotes(txtDigit.Text) & "',"

            'strSQL += " Status='" & oCommon.FixSingleQuotes(ddlStatus.SelectedValue) & "'"

            strSQL += " WHERE TahunID='" & Request.QueryString("edit") & "'"

        Else
            strSQL = "INSERT INTO kpmkv_tahun(Tahun,Kod,RunningNoDigit,Status)"

            strSQL += " VALUES("
            strSQL += " '" & oCommon.FixSingleQuotes(txtTahun.Text) & " ',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtCode.Text) & " ',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtDigit.Text) & " '"
            'strSQL += " '" & oCommon.FixSingleQuotes(ddlStatus.SelectedValue) & "')"

        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet

            Return False
        End If
    End Function

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick


        lblMsg.Text = ""

        Try

            '--execute
            If Save() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Rekod berjaya disimpan"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak berjaya disimpan"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

        strRet = BindData(datRespondent)
    End Sub


    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("prm.tahun.aspx")
    End Sub


End Class