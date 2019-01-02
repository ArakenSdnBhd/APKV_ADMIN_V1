Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Public Class admin_reportFIK
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
                ddlTahun.Text = Now.Year

                kpmkv_negeri_list()
                If Not Session("Negeri") = "" Then
                    ddlNegeri.Text = Session("Negeri")
                Else
                    ddlNegeri.Text = "0"
                End If


            End If

        Catch ex As Exception
            lblMsg.Text = "Error Message:" & ex.Message
        End Try

    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun ORDER BY TahunID"
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

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_negeri_list()
        If Not (String.IsNullOrEmpty(Session("Negeri"))) Then
            strSQL = "SELECT Negeri FROM kpmkv_negeri  Where Negeri='" & Session("Negeri") & "'"
        Else
            strSQL = "SELECT Negeri FROM kpmkv_negeri ORDER BY Negeri"
        End If
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

            ''--ALL
            If Session("Negeri") = "" Then
                ddlNegeri.Items.Add(New ListItem("-Pilih-", "0"))
            End If

        Catch ex As Exception
            'lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " GROUP BY  kpmkv_SVM.KolejRecordID,kpmkv_kolej.Nama,kpmkv_kolej.Negeri,kpmkv_SVM.IsBmTahun,kpmkv_SVM.Sesi  ORDER BY kpmkv_SVM.KolejRecordID,kpmkv_kolej.Nama ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_SVM.KolejRecordID,kpmkv_kolej.Nama,kpmkv_kolej.Negeri,kpmkv_SVM.IsBmTahun,kpmkv_SVM.Sesi,"
        tmpSQL += " (SELECT COUNT(kpmkv_pelajar.IsCalon) FROM kpmkv_pelajar WHERE kpmkv_pelajar.IsCalon='1'"
        tmpSQL += " AND kpmkv_pelajar.IsBMTahun='" & ddlTahun.Text & "'"
        tmpSQL += " AND kpmkv_pelajar.StatusID='2'"
        tmpSQL += " AND kpmkv_pelajar.JenisCalonID='2'"
        tmpSQL += " AND kpmkv_pelajar.KolejRecordID = kpmkv_SVM.KolejRecordID) total,"
        tmpSQL += " SUM(case when kpmkv_SVM.IsLayak = '1' then 1 else 0 end) C_Layak,"
        tmpSQL += " SUM(case when kpmkv_SVM.IsLayak = '1' then 1 else 0 end) C_VOK,"
        tmpSQL += " SUM(case when kpmkv_SVM.IsLayak = '0' then 1 else 0 end) C_XLayak,"
        tmpSQL += " SUM(case when kpmkv_SVM.IsPNGKA = '1' then 1 else 0 end) C_PNGKA,"
        tmpSQL += " SUM(case when kpmkv_SVM.IsPNGKV = '1' then 1 else 0 end) C_PNGKV,"
        tmpSQL += " SUM(case when kpmkv_SVM.IsSETARA = '1' then 1 else 0 end) C_BM "
        tmpSQL += " FROM kpmkv_SVM, kpmkv_kolej"
        tmpSQL += " WHERE kpmkv_kolej.RecordID = kpmkv_SVM.KolejRecordID AND kpmkv_SVM.Semester ='4'"

        '--tahun
        If Not ddlTahun.Text = "" Then
            strWhere += " AND kpmkv_SVM.IsBMTahun ='" & ddlTahun.Text & "'"
        End If

        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_SVM.Sesi ='" & chkSesi.Text & "'"
        End If

        '--Negeri
        If Not ddlNegeri.Text = "0" Then
            strWhere += " AND kpmkv_kolej.Negeri='" & ddlNegeri.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function
    Private Sub CountPNGKA()

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            Dim strKolejID As String = CType(datRespondent.Rows(i).FindControl("KolejRecordID"), Label).Text


            strSQL = "SELECT COUNT(kpmkv_pelajar_markah.PNGKA) AS C_PNGKA FROM kpmkv_pelajar_markah,kpmkv_pelajar"
            strSQL += " WHERE kpmkv_pelajar.PelajarID = kpmkv_pelajar_markah.PelajarID"
            strSQL += " AND kpmkv_pelajar.KolejRecordID=kpmkv_pelajar_markah.KolejRecordID"
            strSQL += " AND kpmkv_pelajar.IsCalon='1' "
            strSQL += " AND kpmkv_pelajar.IsBMTahun='" & ddlTahun.Text & "'"
            strSQL += " AND kpmkv_pelajar.StatusID='2'"
            strSQL += " AND kpmkv_pelajar.JenisCalonID='2'"
            strSQL += " AND kpmkv_pelajar.Semester='4'"
            strSQL += " AND kpmkv_pelajar_markah.PNGKA >='2'"
            strSQL += " AND kpmkv_pelajar_markah.KolejRecordID='" & strKolejID & "'"
            CType(datRespondent.Rows(i).FindControl("C_PNGKA"), Label).Text = oCommon.getFieldValue(strSQL)
        Next

    End Sub
    Private Sub CountPNGKV()

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            Dim strKolejID As String = CType(datRespondent.Rows(i).FindControl("KolejRecordID"), Label).Text


            strSQL = "SELECT COUNT(kpmkv_pelajar_markah.PNGKV) AS C_PNGKA FROM kpmkv_pelajar_markah,kpmkv_pelajar"
            strSQL += " WHERE kpmkv_pelajar.PelajarID = kpmkv_pelajar_markah.PelajarID"
            strSQL += " AND kpmkv_pelajar.KolejRecordID=kpmkv_pelajar_markah.KolejRecordID"
            strSQL += " AND kpmkv_pelajar.IsCalon='1' "
            strSQL += " AND kpmkv_pelajar.IsBMTahun='" & ddlTahun.Text & "'"
            strSQL += " AND kpmkv_pelajar.StatusID='2'"
            strSQL += " AND kpmkv_pelajar.JenisCalonID='2'"
            strSQL += " AND kpmkv_pelajar.Semester='4'"
            strSQL += " AND kpmkv_pelajar_markah.PNGKV >='2.67'"
            strSQL += " AND kpmkv_pelajar_markah.KolejRecordID='" & strKolejID & "'"
            CType(datRespondent.Rows(i).FindControl("C_PNGKV"), Label).Text = oCommon.getFieldValue(strSQL)
        Next

    End Sub
    Private Sub CountBM()

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            Dim strKolejID As String = CType(datRespondent.Rows(i).FindControl("KolejRecordID"), Label).Text


            strSQL = "SELECT COUNT(kpmkv_pelajar_markah.GredBMSetara) AS C_PNGKA FROM kpmkv_pelajar_markah,kpmkv_pelajar"
            strSQL += " WHERE kpmkv_pelajar.PelajarID = kpmkv_pelajar_markah.PelajarID"
            strSQL += " AND kpmkv_pelajar.KolejRecordID=kpmkv_pelajar_markah.KolejRecordID"
            strSQL += " AND kpmkv_pelajar.IsCalon='1' "
            strSQL += " AND kpmkv_pelajar.IsBMTahun='" & ddlTahun.Text & "'"
            strSQL += " AND kpmkv_pelajar.StatusID='2'"
            strSQL += " AND kpmkv_pelajar.JenisCalonID='2'"
            strSQL += " AND kpmkv_pelajar.Semester='4'"
            strSQL += " AND kpmkv_pelajar_markah.GredBMSetara IN ('E','D','C','C+','B','B+','A-','A','A+')"
            strSQL += " AND kpmkv_pelajar_markah.KolejRecordID='" & strKolejID & "'"
            CType(datRespondent.Rows(i).FindControl("C_BM"), Label).Text = oCommon.getFieldValue(strSQL)
        Next

    End Sub
    Private Sub CountLayakSVM()

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            Dim strKolejID As String = CType(datRespondent.Rows(i).FindControl("KolejRecordID"), Label).Text


            strSQL = "SELECT COUNT(LayakID) AS LAYAKSVM FROM kpmkv_SVM"
            strSQL += " WHERE Semester='4' AND IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
            strSQL += " AND LayakSVM = '1' AND KolejRecordID='" & strKolejID & "'"
            CType(datRespondent.Rows(i).FindControl("C_Layak"), Label).Text = oCommon.getFieldValue(strSQL)
        Next

    End Sub
    Private Sub CalX_Layak()
        Dim IntTotal As Integer = 0
        Dim IntTotalLayak As Integer = 0

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)

            CType(datRespondent.Rows(i).FindControl("C_XLayak"), Label).Text = (CType(datRespondent.Rows(i).FindControl("total"), Label).Text) - CType(datRespondent.Rows(i).FindControl("C_Layak"), Label).Text
        Next

    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        CountLayakSVM()
        CalX_Layak()
        CountPNGKA()
        CountPNGKV()
        CountBM()
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
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            lblMsg.Text = ""

            ExportToCSV(getSQL)

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=FileLayakSVM.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

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

End Class