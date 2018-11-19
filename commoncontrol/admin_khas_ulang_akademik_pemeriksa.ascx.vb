Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class admin_khas_ulang_akademik_pemeriksa
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_kodpusat_list()

                kpmkv_kod_list()

                strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & ddlKodPusat.Text & "'"
                lblNamaKolej.Text = oCommon.getFieldValue(strSQL)

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_pemeriksa_list()
            End If

        Catch ex As Exception
            lblmsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub ddlKodPusat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodPusat.SelectedIndexChanged

        strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID = '" & ddlKodPusat.SelectedValue & "'"
        lblNamaKolej.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub kpmkv_kodpusat_list()
        strSQL = "SELECT Kod, RecordID FROM kpmkv_kolej ORDER BY Kod ASC "

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodPusat.DataSource = ds
            ddlKodPusat.DataTextField = "Kod"
            ddlKodPusat.DataValueField = "RecordID"
            ddlKodPusat.DataBind()

            ddlKodPusat.Items.Insert(0, "-PILIH-")

        Catch ex As Exception
            lblmsg.Text = "System Error:" & ex.Message
        Finally
            objConn.Dispose()
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
    Private Sub kpmkv_kod_list()
        strSQL = "SELECT Distinct NamaMataPelajaran FROM kpmkv_matapelajaran ORDER BY NamaMataPelajaran"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMataPelajaran.DataSource = ds
            ddlMataPelajaran.DataTextField = "NamaMataPelajaran"
            ddlMataPelajaran.DataValueField = "NamaMataPelajaran"
            ddlMataPelajaran.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_pemeriksa_list()
        strSQL = "SELECT Nama FROM kpmkv_users WHERE UserType='AKADEMIK-PEMERIKSA' ORDER BY Nama"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPemeriksa.DataSource = ds
            ddlPemeriksa.DataTextField = "Nama"
            ddlPemeriksa.DataValueField = "Nama"
            ddlPemeriksa.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        lblmsg.Text = ""

        strSQL = "SELECT UserID FROM kpmkv_users WHERE Nama='" & oCommon.FixSingleQuotes(ddlPemeriksa.Text) & "'"
        Dim StrUserID As Integer = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID='" & ddlKodPusat.SelectedValue & "'"
        Dim strPusat As String = oCommon.getFieldValue(strSQL)

        Try
            strSQL = "INSERT INTO kpmkv_pemeriksa(NamaPemeriksa, UserID, Tahun, Semester, Sesi, KodKolej, Subjek) "
            strSQL += "VALUES ('" & oCommon.FixSingleQuotes(ddlPemeriksa.Text) & "','" & StrUserID & "','" & ddlTahun.Text & "','4','" & chkSesi.Text & "','" & strPusat & "','" & ddlMataPelajaran.Text & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                divMsg.Attributes("class") = "info"
                lblmsg.Text = "Berjaya mendaftarkan Pemeriksa."
            Else
                divMsg.Attributes("class") = "error"
            End If

            strRet = BindData(datRespondent)
        Catch ex As Exception
            lblmsg.Text = ex.Message
        End Try

    End Sub
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblmsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblmsg.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function
    Private Function getSQL() As String
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY PemeriksaID, Tahun, Sesi ASC"
        'FileID,Tahun,Sesi,NamaKolej,KolejID
        '--not deleted
        tmpSQL = "SELECT * FROM kpmkv_pemeriksa"
        strWhere = " WHERE PemeriksaID IS NOT NULL"

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND Tahun ='" & ddlTahun.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND Sesi ='" & chkSesi.Text & "'"
        End If

        '--matapelajaran
        If Not ddlMataPelajaran.Text = "" Then
            strWhere += " AND Subjek ='" & ddlMataPelajaran.Text & "'"
        End If
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        ' Response.Redirect("pelajar.view.aspx?PelajarID=" & strKeyID)

    End Sub

    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblmsg.Text = ""
        If (e.CommandName = "Batal") = True Then

            Dim PemeriksaID = Int32.Parse(e.CommandArgument.ToString())

            strSQL = "DELETE FROM kpmkv_pemeriksa WHERE PemeriksaID='" & PemeriksaID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Pemeriksa berjaya dipadamkan"
            Else
                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Pemeriksa tidak berjaya dipadamkan"
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

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblmsg.Text = ""
        strRet = BindData(datRespondent)

    End Sub

End Class