Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class matapelajaran_create
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_semester_list()
                kpmkv_semester_list2()
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY TahunID"
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

            '--ALL
            ddlTahun.Items.Add(New ListItem("-Semua Tahun-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

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
    Private Sub kpmkv_semester_list2()
        strSQL = "SELECT Semester FROM kpmkv_semester ORDER BY SemesterID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlColumnSem.DataSource = ds
            ddlColumnSem.DataTextField = "Semester"
            ddlColumnSem.DataValueField = "Semester"
            ddlColumnSem.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_matapelajaran_create() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mendaftarkan MataPelajaran baru."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean

        '--txtNama
        If txtNamaMataPelajaran.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Nama MataPelajaran!"
            txtNamaMataPelajaran.Focus()
            Return False
        End If

        '--txtKod
        If txtKodMataPelajaran.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Kod MataPelajaran!"
            txtKodMataPelajaran.Focus()
            Return False
        End If

        strSQL = "SELECT * FROM kpmkv_matapelajaran WHERE KodMataPelajaran='" & txtKodMataPelajaran.Text & "' and NamaMataPelajaran='" & txtNamaMataPelajaran.Text & "' and IsDeleted='N' and Tahun='" & ddlTahun.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Kod MataPelajaran telah digunakan. Sila masukkan kod yang baru."
            Return False
        Else
            Return True
        End If

        Return True
    End Function
    Private Function kpmkv_matapelajaran_create() As Boolean

        strSQL = "INSERT INTO kpmkv_matapelajaran (KodMataPelajaran,NamaMataPelajaran,JamKredit,IsDeleted,Tahun,PB,PA,Semester,"
        strSQL += " PelajarMarkahSubjek,PelajarMarkahGred,Jenis) "
        strSQL += "VALUES ('" & oCommon.FixSingleQuotes(txtKodMataPelajaran.Text.ToUpper) & "',"
        strSQL += " '" & oCommon.FixSingleQuotes(txtNamaMataPelajaran.Text.ToUpper) & "','" & txtJamKredit.Text & "','N',"
        strSQL += " '" & ddlTahun.SelectedValue & "','" & txtPB.Text & "','" & txtPA.Text & "',"
        strSQL += " '" & ddlSemester.SelectedValue & "','" & ddlSub.SelectedValue & "','" & ddlGred.SelectedValue & "' ,"
        strSQL += " '" & ddlJenis.SelectedValue & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        End If

    End Function

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged

        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged

        strRet = BindData(datRespondent)

    End Sub


    Protected Sub CheckUncheckAll(sender As Object, e As System.EventArgs)
        Dim chk1 As CheckBox
        chk1 = DirectCast(datRespondent.HeaderRow.Cells(0).FindControl("chkAll"), CheckBox)
        For Each row As GridViewRow In datRespondent.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("chkSelect"), CheckBox)
            chk.Checked = chk1.Checked
        Next
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
        Dim strOrder As String = " "

        '--not deleted
        tmpSQL = "SELECT * FROM kpmkv_Matapelajaran WHERE Tahun='" & oCommon.FixSingleQuotes(ddlTahun.SelectedValue) & "'"

        If Not ddlSemester.SelectedValue = "" Then
            strWhere += " AND Semester ='" & oCommon.FixSingleQuotes(ddlSemester.SelectedValue) & "'"
        End If



        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
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


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            For i As Integer = 0 To datRespondent.Rows.Count - 1

                Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

                If cb.Checked = True Then

                    Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

                    strSQL = " UPDATE kpmkv_matapelajaran SET PelajarMarkahSubjek='" & oCommon.FixSingleQuotes(ddlColumnSub.SelectedValue) & "',"
                    strSQL += "PelajarMarkahGred='" & oCommon.FixSingleQuotes(ddlColumnGred.SelectedValue) & "',"
                    strSQL += " Jenis='" & ddlColumnJenis.SelectedValue & "'"
                    strSQL += " WHERE MatapelajaranID='" & strkey & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If Not strRet = 0 Then

                        divMsg.Attributes("class") = "error"
                        lblMsg.Text = "Kolum Matapelajaran tidak berjaya dikemaskini"
                    End If
                End If
            Next


            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Kolum Matapelajaran berjaya dikemaskini"
            strRet = BindData(datRespondent)
        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error. " & ex.Message
        End Try


    End Sub
End Class