Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class admin_gred
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

                kpmkv_semester_list()


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
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester ORDER BY SemesterID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester.DataSource = ds
            ddlSemester.DataTextField = "Semester"
            ddlSemester.DataValueField = "Semester"
            ddlSemester.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL1, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function
    Private Function BindData2(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL2, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function
    Private Function getSQL1() As String
        Dim tmpSQL As String

        tmpSQL = "SELECT t1.KolejRecordID,t1.Nama,t1.KursusID, t1.KodKursus, t1.NamaKursus, t1.Tahun,t1.Semester,t1.Sesi,t1.[nPB], "
        tmpSQL += "(SELECT Count(*) from kpmkv_pelajar WHERE  kpmkv_pelajar.KolejRecordID=t1.KolejRecordID AND kpmkv_pelajar.KursusID=t1.KursusID"
        tmpSQL += " AND kelasid is not null AND StatusID ='2' AND Semester='" & ddlSemester.Text & "' And sesi='" & chkSesi.Text & "' And Tahun='" & ddlTahun.Text & "') as nTotal "
        tmpSQL += " FROM "
        tmpSQL += " (SELECT kpmkv_pelajar_markah.KolejRecordID,kpmkv_kolej.Nama,kpmkv_pelajar_markah.KursusID,kpmkv_kursus.KodKursus,kpmkv_kursus.NamaKursus, kpmkv_pelajar_markah.Tahun,kpmkv_pelajar_markah.Semester,kpmkv_pelajar_markah.Sesi,"
        tmpSQL += " COUNT(*) AS 'nPB' FROM kpmkv_pelajar_markah,kpmkv_kolej,kpmkv_pelajar,kpmkv_kursus WHERE kpmkv_pelajar_markah.KolejRecordID = kpmkv_kolej.RecordID "
        tmpSQL += " AND kpmkv_pelajar_markah.pelajarid = kpmkv_pelajar.pelajarid  "
        tmpSQL += " AND kpmkv_kursus.KursusID=kpmkv_pelajar_markah.KursusID AND kpmkv_pelajar.kelasid is not null AND kpmkv_pelajar.StatusID ='2'"
        tmpSQL += " AND kpmkv_pelajar_markah.Semester='" & ddlSemester.Text & "' And kpmkv_pelajar_markah.sesi='" & chkSesi.Text & "' And kpmkv_pelajar_markah.Tahun='" & ddlTahun.Text & "'"
        tmpSQL += " AND  A_Amali1 IS NULL AND  A_Teori1 IS NULL AND  A_Amali2 IS NULL AND  A_Teori2 IS NULL AND  A_Amali3 IS NULL"
        tmpSQL += " AND  A_Teori3 IS NULL AND  A_Amali4 IS NULL AND  A_Teori4 IS NULL AND  A_Amali5 IS NULL AND  A_Teori5 IS NULL"
        tmpSQL += " AND  A_Amali6 IS NULL AND  A_Teori6 IS NULL AND  A_Amali7 IS NULL AND  A_Teori7 IS NULL AND  A_Amali8 IS NULL AND  A_Teori8 IS NULL"
        tmpSQL += " GROUP BY kpmkv_pelajar_markah.KolejRecordID,kpmkv_kolej.Nama,kpmkv_pelajar_markah.KursusID,kpmkv_kursus.KodKursus,kpmkv_kursus.NamaKursus, kpmkv_pelajar_markah.Tahun,kpmkv_pelajar_markah.Semester,kpmkv_pelajar_markah.Sesi) t1"

        getSQL1 = tmpSQL
        ''--debug
        'Response.Write(getSQL)

        Return getSQL1

    End Function
    Private Function getSQL2() As String
        Dim tmpSQL As String

        tmpSQL = "SELECT t1.KolejRecordID,t1.Nama,t1.KursusID, t1.KodKursus, t1.NamaKursus, t1.Tahun,t1.Semester,t1.Sesi,coalesce(t1.[nPA], 0) as [nPA], "
        tmpSQL += "(SELECT Count(*) from kpmkv_pelajar WHERE  kpmkv_pelajar.KolejRecordID=t1.KolejRecordID AND kpmkv_pelajar.KursusID=t1.KursusID"
        tmpSQL += " AND kelasid is not null AND StatusID ='2' AND Semester='" & ddlSemester.Text & "' And sesi='" & chkSesi.Text & "' And Tahun='" & ddlTahun.Text & "') as nTotal "
        tmpSQL += " FROM "
        tmpSQL += " (SELECT kpmkv_pelajar_markah.KolejRecordID,kpmkv_kolej.Nama, kpmkv_pelajar_markah.KursusID, kpmkv_kursus.KodKursus,kpmkv_kursus.NamaKursus, kpmkv_pelajar_markah.Tahun,kpmkv_pelajar_markah.Semester,kpmkv_pelajar_markah.Sesi,"
        tmpSQL += " COUNT(*) AS 'nPA' FROM kpmkv_pelajar_markah,kpmkv_kolej,kpmkv_pelajar,kpmkv_kursus WHERE kpmkv_pelajar_markah.KolejRecordID = kpmkv_kolej.RecordID "
        tmpSQL += " AND kpmkv_pelajar_markah.pelajarid = kpmkv_pelajar.pelajarid  "
        tmpSQL += " AND kpmkv_kursus.KursusID=kpmkv_pelajar_markah.KursusID AND kpmkv_pelajar.kelasid is not null AND kpmkv_pelajar.StatusID ='2'"
        tmpSQL += " AND kpmkv_pelajar_markah.Semester='" & ddlSemester.Text & "' And kpmkv_pelajar_markah.sesi='" & chkSesi.Text & "' And kpmkv_pelajar_markah.Tahun='" & ddlTahun.Text & "'"
        tmpSQL += " AND  B_BahasaMelayu IS NULL AND B_BahasaMelayu3 IS NULL AND B_BahasaInggeris IS NULL AND  B_Science1 IS NULL AND B_Science2 IS NULL       "
        tmpSQL += " AND  B_Sejarah IS NULL AND  B_PendidikanIslam1 IS NULL  AND  B_PendidikanIslam2 IS NULL AND  B_PendidikanMoral IS NULL AND  B_Mathematics IS NULL    "
        tmpSQL += " AND  B_Amali1 IS NULL AND  B_Teori1 IS NULL AND  B_Amali2 IS NULL AND  B_Teori2 IS NULL AND  B_Amali3 IS NULL AND  B_Teori3 IS NULL        "
        tmpSQL += " AND  B_Amali4 IS NULL AND  B_Teori4 IS NULL AND  B_Amali5 IS NULL AND  B_Teori5 IS NULL AND  B_Amali6 IS NULL"
        tmpSQL += " AND  B_Teori6 IS NULL AND  B_Amali7 IS NULL AND  B_Teori7 IS NULL AND  B_Amali8 IS NULL AND  B_Teori8 IS NULL  "
        tmpSQL += " GROUP BY kpmkv_pelajar_markah.KolejRecordID, kpmkv_kolej.Nama,kpmkv_pelajar_markah.KursusID, kpmkv_kursus.KodKursus,kpmkv_kursus.NamaKursus, kpmkv_pelajar_markah.Tahun,kpmkv_pelajar_markah.Semester,kpmkv_pelajar_markah.Sesi) t1"
        'tmpSQL += " ON"
        'tmpSQL += " t1.KolejRecordID = t2.KolejRecordID"

        getSQL2 = tmpSQL
        ''--debug
        'Response.Write(getSQL)

        Return getSQL2

    End Function
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        ' Response.Redirect("pelajar.view.aspx?PelajarID=" & strKeyID)

    End Sub


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Try
            lblMsg.Text = ""

            ExportToCSV(getSQL1)

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
        Response.AddHeader("content-disposition", "attachment;filename=PemantauanMarkah.csv")
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

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        strRet = BindData2(datRespondent2)
    End Sub

    Private Sub datRespondent2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent2.PageIndexChanging
        datRespondent2.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent2)
    End Sub

    Private Sub datRespondent2_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent2.SelectedIndexChanging
        Dim strKeyID As String = datRespondent2.DataKeys(e.NewSelectedIndex).Value.ToString

    End Sub

    Private Sub btnEksport2_Click(sender As Object, e As EventArgs) Handles btnEksport2.Click
        Try
            lblMsg.Text = ""

            ExportToCSV(getSQL2)

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
End Class