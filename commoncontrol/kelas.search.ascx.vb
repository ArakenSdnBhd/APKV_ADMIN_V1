Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class kelas_search
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_negeri_list()
                ddlNegeri.Text = ""

                kpmkv_jenis_list()
                ddlJenis.Text = ""

                kpmkv_kolej_list()
                ddlKolej.Text = ""

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year


                lblMsg.Text = ""
                ' strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY Tahun"
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
    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri  ORDER BY Negeri"
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
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej  ORDER BY Jenis"
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
        If ddlNegeri.SelectedItem.Value <> "ALL" Then
            strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej  WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
        Else
            strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej  ORDER BY Nama ASC"
        End If
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
    Private Sub kpmkv_namakelas_list()
        strSQL = "SELECT kpmkv_kelas.NamaKelas FROM  kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.Text & "' AND kpmkv_kelas.Tahun='" & ddlTahun.Text & "' ORDER BY kpmkv_kelas.Tahun"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNamaKelas.DataSource = ds
            ddlNamaKelas.DataTextField = "NamaKelas"
            ddlNamaKelas.DataValueField = "NamaKelas"
            ddlNamaKelas.DataBind()

            '--ALL
            ddlNamaKelas.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""
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
        Dim strOrder As String = " ORDER BY kpmkv_kolej.Nama,kpmkv_kursus.Tahun ASC"

        tmpSQL = "SELECT kpmkv_kolej.Nama, kpmkv_kelas_kursus.KelasKursusID, kpmkv_kelas.KelasID, kpmkv_kursus.Tahun, kpmkv_kursus.Sesi, kpmkv_kluster.NamaKluster,"
        tmpSQL += " kpmkv_kursus.KodKursus,kpmkv_kursus.NamaKursus, kpmkv_kelas.NamaKelas FROM kpmkv_kelas_kursus"
        tmpSQL += " LEFT OUTER JOIN kpmkv_kursus_kolej ON kpmkv_kursus_kolej.KursusID = kpmkv_kelas_kursus.KursusID "
        tmpSQL += " LEFT OUTER JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_kursus_kolej.KolejRecordID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID "
        tmpSQL += " LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID "
        tmpSQL += " LEFT OUTER JOIN kpmkv_kluster ON kpmkv_kursus.KlusterID = kpmkv_kluster.KlusterID "
        strWhere = " WHERE kpmkv_kelas.IsDeleted='N'"

        '--kolej
        If Not ddlKolej.Text = "0" Then
            strWhere += " AND kpmkv_kolej.RecordID='" & ddlKolej.Text & "'"
        End If

        '--Tahun
        If Not ddlTahun.Text = "0" Then
            strWhere += " AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "'"
        End If

        '--NamaKelas
        If Not ddlNamaKelas.Text = "0" Then
            strWhere += " AND kpmkv_kelas.NamaKelas='" & ddlNamaKelas.Text & "'"
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

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        Dim strKelasID As Integer = datRespondent.DataKeys(e.RowIndex).Values("KelasID")
        Try
            If Not strKelasID = Session("strKelasID") Then
                strSQL = "DELETE FROM kpmkv_pensyarah_modul WHERE KelasID='" & strKelasID & "'"
                If strRet = oCommon.ExecuteSQL(strSQL) = 0 Then
                    'update kelas pelajar
                    strSQL = "Update kpmkv_pelajar Set KelasID=NULL WHERE KelasID='" & strKelasID & "' AND KolejRecordID='" & lblKolejID.Text & "'"
                    If strRet = oCommon.ExecuteSQL(strSQL) = 0 Then
                        'delete penetapan kursus -kelas
                        strSQL = "DELETE FROM kpmkv_kelas_kursus WHERE KelasID='" & strKelasID & "'"
                        If strRet = oCommon.ExecuteSQL(strSQL) = 0 Then
                            divMsgTop.Attributes("class") = "info"
                            lblMsgTop.Text = "Kelas berjaya dipadamkan"
                            divMsg.Attributes("class") = "info"
                            lblMsg.Text = "Kelas berjaya dipadamkan"
                            Session("strKelasID") = ""
                        Else
                            divMsgTop.Attributes("class") = "error"
                            lblMsgTop.Text = "Kelas tidak berjaya dipadamkan"
                            divMsg.Attributes("class") = "error"
                            lblMsg.Text = "Kelas tidak berjaya dipadamkan"
                            Session("strKelasID") = ""
                        End If

                    Else
                        divMsgTop.Attributes("class") = "error"
                        lblMsgTop.Text = "Kelas tidak berjaya dipadamkan"
                        divMsg.Attributes("class") = "error"
                        lblMsg.Text = "Kelas tidak berjaya dipadamkan"
                        Session("strKelasID") = ""
                    End If

                Else
                    divMsgTop.Attributes("class") = "error"
                    lblMsgTop.Text = "Kelas tidak berjaya dipadamkan"
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kelas tidak berjaya dipadamkan"
                    Session("strKelasID") = ""
                End If
            Else
                Session("strKelasID") = ""
            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
        End Try

        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Redirect("kelas.view.aspx?KelasID=" & strKeyID)

    End Sub
   
    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_namakelas_list()
    End Sub
    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
        
    End Sub

    Private Sub ddlKolej_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKolej.SelectedIndexChanged
        kpmkv_namakelas_list()
    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_jenis_list()
        kpmkv_kolej_list()
    End Sub
End Class