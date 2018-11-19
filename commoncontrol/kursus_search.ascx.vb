Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class kursus_search
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

                kpmkv_namakluster_list()
                ddlKluster.Text = "0"

                kpmkv_kodkursus_list()


                lblMsg.Text = ""
                'strRet = BindData(datRespondent)
            End If


        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try

    End Sub
    Private Sub kpmkv_namakluster_list()
        strSQL = "SELECT KlusterID,NamaKluster FROM kpmkv_kluster  WHERE Tahun = '" & ddlTahun.SelectedValue & "' AND IsDeleted='N' ORDER BY NamaKluster"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKluster.DataSource = ds
            ddlKluster.DataTextField = "NamaKluster"
            ddlKluster.DataValueField = "KlusterID"
            ddlKluster.DataBind()

            '--ALL
            ddlKluster.Items.Add(New ListItem("PILIH", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kodkursus_list()
        strSQL = "SELECT KodKursus,KursusID FROM kpmkv_kursus WHERE Tahun='" & ddlTahun.SelectedValue & "' AND Sesi='" & chkSesi.Text & "'"
        strSQL += " AND IsDeleted='N' AND KlusterID='" & ddlKluster.SelectedValue & "' AND JenisKursus='" & chkJenisProgram.Text & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKursus.DataSource = ds
            ddlKodKursus.DataTextField = "KodKursus"
            ddlKodKursus.DataValueField = "KursusID"
            ddlKodKursus.DataBind()

            '--ALL
            ddlKodKursus.Items.Add(New ListItem("PILIH", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

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
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
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
        Dim strOrder As String = " ORDER BY KodKursus ASC"

        tmpSQL = "SELECT Distinct * FROM kpmkv_kursus "
        strWhere = " WHERE Tahun='" & ddlTahun.SelectedValue & "' AND IsDeleted='N' "

        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND Sesi='" & chkSesi.Text & "' "
        End If

        '--jenis program
        If Not chkJenisProgram.Text = "" Then
            strWhere += " AND JenisKursus='" & chkJenisProgram.Text & "' "
        End If

        '--kluster
        If Not ddlKluster.Text = "0" Then
            strWhere += " AND KlusterID='" & ddlKluster.SelectedValue & "' "
        End If

        '--kod
        If Not ddlKodKursus.Text = "0" Then
            strWhere += " AND KursusID='" & ddlKodKursus.Text & "'"
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
        End Try
        con.Close()
        sda.Dispose()
        con.Dispose()

    End Function
    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value
        Response.Redirect("kursus.view.aspx?KursusID=" & strKeyID)
    End Sub

    Private Sub chkJenisProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkJenisProgram.SelectedIndexChanged
        If Not chkJenisProgram.Text = "" And Not chkSesi.Text = "0" Then
            kpmkv_kodkursus_list()
        End If
    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_namakluster_list()
        ddlKluster.Text = "0"
    End Sub
End Class