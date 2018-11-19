Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class kursus_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        Try
            If Not IsPostBack Then

                LoadPage()
                kpmkv_tahun_list()
                kpmkv_sesi_list()

                kpmkv_namakluster_list()

                strSQL = "SELECT Kodkursus FROM kpmkv_kursus WHERE KursusID='" & Request.QueryString("KursusID") & "'"
                lblKodKursus.Text = oCommon.getFieldValue(strSQL)



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
    Private Sub kpmkv_sesi_list()
        strSQL = "SELECT Sesi FROM kpmkv_sesi  ORDER BY SesiID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSesi.DataSource = ds
            ddlSesi.DataTextField = "Sesi"
            ddlSesi.DataValueField = "Sesi"
            ddlSesi.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_namakluster_list()
        strSQL = "SELECT NamaKluster,KlusterID FROM kpmkv_kluster WHERE Tahun='" & ddlTahun.Text & "' ORDER BY NamaKluster ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNamaKluster.DataSource = ds
            ddlNamaKluster.DataTextField = "NamaKluster"
            ddlNamaKluster.DataValueField = "KlusterID"
            ddlNamaKluster.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub LoadPage()
        strSQL = "SELECT kpmkv_kluster.NamaKluster,kpmkv_kursus.JenisKursus,kpmkv_kursus.KodKursus,kpmkv_kursus.Namakursus,"
        strSQL += " kpmkv_kursus.Tahun, kpmkv_kursus.Sesi"
        strSQL += " FROM kpmkv_kursus"
        strSQL += " LEFT OUTER JOIN kpmkv_kluster ON kpmkv_kluster.KlusterID=kpmkv_kursus.KlusterID"
        strSQL += " WHERE kpmkv_kursus.KursusID='" & Request.QueryString("KursusID") & "'"
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("JenisKursus")) Then
                    chkJenisProgram.Text = ds.Tables(0).Rows(0).Item("JenisKursus")
                Else
                    chkJenisProgram.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaKluster")) Then
                    ddlNamaKluster.Text = ds.Tables(0).Rows(0).Item("NamaKluster")
                Else
                    ddlNamaKluster.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KodKursus")) Then
                    txtKod.Text = ds.Tables(0).Rows(0).Item("KodKursus")
                Else
                    txtKod.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Namakursus")) Then
                    txtNama.Text = ds.Tables(0).Rows(0).Item("NamaKursus")
                Else
                    txtNama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    ddlTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    ddlTahun.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sesi")) Then
                    ddlSesi.Text = ds.Tables(0).Rows(0).Item("Sesi")
                Else
                    ddlSesi.Text = ""
                End If

            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
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
        Dim strOrder As String = " ORDER BY Tahun,Semester,Sesi ASC"

        tmpSQL = "SELECT * FROM kpmkv_modul WHERE Tahun='" & ddlTahun.Text & "' AND Sesi='" & ddlSesi.Text & "'"
        strWhere = " AND IsDeleted<>'Y' AND KursusID='" & Request.QueryString("KursusID") & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_kursus_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat Kursus."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        'If Not lblKod.Text = txtKod.Text Then       '--changes made to the kod
        '    strSQL = "SELECT KodKursus FROM kpmkv_kursus WHERE KodKursus='" & oCommon.FixSingleQuotes(txtKod.Text) & "'"
        '    If oCommon.isExist(strSQL) = True Then
        '        lblMsg.Text = "Kod Kursus telah digunakan. Sila masukkan kod yang baru."
        '        Return False
        '    End If
        'End If
        Return True
    End Function

    Private Function kpmkv_kursus_update() As Boolean
        'update kpmkv_kursus_txn table
        strSQL = "UPDATE kpmkv_kursus SET NamaKursus='" & txtNama.Text & "', KodKursus='" & txtKod.Text & "' WHERE KursusID='" & Request.QueryString("KursusID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strSQL = "UPDATE kpmkv_kursus SET IsDeleted='Y' WHERE KursusID='" & Request.QueryString("KursusID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya meghapuskan rekod Kursus tersebut."
        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub

End Class