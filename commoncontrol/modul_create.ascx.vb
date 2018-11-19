Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class modul_create
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

                kpmkv_kursus_list()

                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
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

    Private Sub kpmkv_kursus_list()

        strSQL = "SELECT KursusID,NamaKursus FROM kpmkv_kursus WHERE Tahun='" & ddlTahun.Text & "' and Sesi='" & chkSesi.Text & "' AND IsDeleted='N' ORDER BY NamaKursus"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        ' Response.Write(strSQL)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKursus.DataSource = ds
            ddlKursus.DataTextField = "NamaKursus"
            ddlKursus.DataValueField = "KursusID"
            ddlKursus.DataBind()

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
            If kpmkv_modul_create() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mendaftarkan Modul baru."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean

        '--ddlsesi
        If chkSesi.Text = "" Then
            lblMsg.Text = "Sila pilih Sesi!"
            chkSesi.Focus()
            Return False
        End If

        '--txtNama
        If txtNamaModul.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Modul!"
            txtNamaModul.Focus()
            Return False
        End If

        '--txtKod
        If txtKodModul.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Kod Modul!"
            txtKodModul.Focus()
            Return False
        End If

        If ddlPBPAM.SelectedValue = "" Then
            lblMsg.Text = "Sila Pilih Kolum PBPA!"
            ddlPBPAM.Focus()
            Return False
        End If
        If ddlGredMV.SelectedValue = "" Then
            lblMsg.Text = "Sila Pilih Kolum Gred!"
            ddlGredV.Focus()
            Return False
        End If

        strSQL = "SELECT KodModul FROM kpmkv_modul WHERE KodModul='" & txtKodModul.Text & "' AND  Tahun='" & ddlTahun.Text & "' AND  Semester='" & ddlSemester.Text & "'"
        strSQL += " AND  Sesi='" & chkSesi.Text & "' AND  IsDeleted='N'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Kod Modul telah digunakan. Sila masukkan kod yang baru."
            Return False
        Else
            Return True
        End If

        Return True
    End Function
    Private Function kpmkv_modul_create() As Boolean

        strSQL = "INSERT INTO kpmkv_modul (Semester,KodModul,NamaModul,JamKredit,IsDeleted,Tahun,Sesi,KursusID,PBAmali,PBTeori,PAAmali,PATeori,PelajarMarkahPBPA,PelajarMarkahGred) "
        strSQL += "VALUES ('" & ddlSemester.SelectedValue & "','" & oCommon.FixSingleQuotes(txtKodModul.Text.ToUpper) & "',"
        strSQL += " '" & oCommon.FixSingleQuotes(txtNamaModul.Text.ToUpper) & "','" & txtJamKredit.Text & "','N',"
        strSQL += "'" & ddlTahun.SelectedValue & "','" & chkSesi.Text & "','" & ddlKursus.SelectedValue & "',"
        strSQL += "'" & txtPBAmali.Text & "','" & txtPBTeori.Text & "','" & txtPAAmali.Text & "',"
        strSQL += " '" & txtPATeori.Text & "','" & ddlPBPAM.SelectedValue & "','" & ddlGredV.SelectedValue & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        End If
    End Function
    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kursus_list()

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
        tmpSQL = "SELECT * FROM kpmkv_Modul WHERE Tahun='" & oCommon.FixSingleQuotes(ddlTahun.SelectedValue) & "'"

        If Not ddlSemester.SelectedValue = "" Then
            strWhere += " AND Semester ='" & oCommon.FixSingleQuotes(ddlSemester.SelectedValue) & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND Sesi ='" & chkSesi.Text & "'"
        End If

        If Not ddlKursus.SelectedValue = "" Then
            strWhere += " AND KursusID='" & oCommon.FixSingleQuotes(ddlKursus.SelectedValue) & "'"
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

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged

        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged

        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddlKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKursus.SelectedIndexChanged

        strRet = BindData(datRespondent)

    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            For i As Integer = 0 To datRespondent.Rows.Count - 1

                Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

                If cb.Checked = True Then

                    Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

                    strSQL = " UPDATE kpmkv_modul SET PelajarMarkahPBPA='" & oCommon.FixSingleQuotes(ddlColumnPBPA.SelectedValue) & "',"
                    strSQL += "PelajarMarkahGred='" & oCommon.FixSingleQuotes(ddlGredV.SelectedValue) & "' WHERE ModulID='" & strkey & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If Not strRet = 0 Then

                        divMsg.Attributes("class") = "error"
                        lblMsg.Text = "Kolum Modul tidak berjaya dikemaskini"
                    End If
                End If
            Next


            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Kolum Modul berjaya dikemaskini"
            strRet = BindData(datRespondent)
        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error. " & ex.Message
        End Try


    End Sub
End Class