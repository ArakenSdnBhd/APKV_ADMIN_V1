Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class kursus_list_by_year
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' lnkList.Attributes.Add("onclick", "javascript:Open();")

        Try
            If Not IsPostBack Then

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_kodkursus_list()
                ddlKodKursus.Text = "ALL"

                kpmkv_sesi_list()
                ddlSesi.Text = "1"

                'lblKodKursus.Text = Request.QueryString("KodKursus")
                'If Not lblKodKursus.Text = "" Then
                strRet = BindData(datRespondent)
                tbl_menu_check()
                'End If
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun WITH (NOLOCK) ORDER BY TahunID"
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
            ddlTahun.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kodkursus_list()
        strSQL = "SELECT KodKursus FROM kpmkv_kursus WITH (NOLOCK) ORDER BY KursusID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKursus.DataSource = ds
            ddlKodKursus.DataTextField = "KodKursus"
            ddlKodKursus.DataValueField = "KodKursus"
            ddlKodKursus.DataBind()

            '--ALL
            ddlKodKursus.Items.Add(New ListItem("ALL", "ALL"))
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_sesi_list()
        strSQL = "SELECT Sesi FROM kpmkv_sesi WITH (NOLOCK) ORDER BY SesiID"
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
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub tbl_menu_check()
        Dim str As String
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

            str = datRespondent.DataKeys(i).Value.ToString
            strSQL = "Select Distinct KodKursus from kpmkv_kursus_txn where KodKursus='" & str & "' and Tahun='" & ddlTahun.Text & "'"
            strRet = oCommon.isExist(strSQL)
            If strRet = True Then
                cb.Checked = True
            End If
            ' End If
        Next

    End Sub
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""
        Dim str As String
        Try

            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim row As GridViewRow = datRespondent.Rows(i)
                Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked



                str = datRespondent.DataKeys(i).Value.ToString
                Dim strTahun As String = CType(datRespondent.Rows(i).FindControl("Tahun"), Label).Text
                Dim strNamaKluster As String = CType(datRespondent.Rows(i).FindControl("NamaKluster"), Label).Text
                Dim strNamaKursus As String = CType(datRespondent.Rows(i).FindControl("NamaKursus"), Label).Text
                'Dim Semester As String = str.Substring(0, 4) '--ETE101 ....

                If isChecked Then
                    'check kursus 
                    strSQL = "Select * from kpmkv_kursus_txn WHERE Kodkursus='" & str & "' and Tahun='" & ddlTahun.Text & "'"
                    If oCommon.isExist(strSQL) = False Then
                        'check modul exist
                        strSQL = "Select * from kpmkv_modul WHERE KodModul= str.Substring(0, 4)"
                        If oCommon.isExist(strSQL) = True Then
                            'insert kpmkv_kursus_txn table
                            strSQL = "INSERT INTO kpmkv_kursus_txn(Tahun,Sesi,Semester,KodKursus,KodModul,NamaModul,JamKredit)"
                            strSQL += "SELECT '" & ddlTahun.Text & "','" & ddlSesi.Text & "',Semester,'" & str & "',KodModul,NamaModul,JamKredit from kpmkv_modul WHERE Kodkursus='" & str & "'"
                            strRet = oCommon.Bulk_Transfer(strSQL, "kpmkv_kursus_txn")
                            If Not strRet = "0" Then
                                lblMsg.Text = "System Error:" & strRet
                                Exit Sub
                            End If

                            'update kpmkv_kursus_txn table
                            strSQL = "UPDATE kpmkv_kursus_txn WITH (UPDLOCK) SET NamaKluster='" & strNamaKluster & "', NamaKursus='" & strNamaKursus & "' WHERE KodKursus='" & str & "' AND Tahun='" & ddlTahun.Text & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                            If Not strRet = "0" Then
                                'delete kpmkv_modul_txn
                                strSQL = "Delete from kpmkv_kursus_txn WHERE Kodkursus='" & str & "' AND Tahun='" & ddlTahun.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                                lblMsg.Text = "System Error:" & strRet
                                Exit Sub
                            End If
                        Else
                            lblMsg.Text = "Sila daftar Modul bagi Kursus:" & str
                            Exit Sub
                        End If
                        End If
                        End If
            Next

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
        End Try

        tbl_menu_check()

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
        Dim strOrder As String = " ORDER BY Tahun ASC"

        tmpSQL = "SELECT * FROM kpmkv_kursus"
        strWhere = " WITH (NOLOCK) WHERE IsDeleted='N'"

        '--tahun
        If Not ddlTahun.Text = "ALL" Then
            strWhere += " AND Tahun='" & ddlTahun.Text & "'"
        End If

        '--kod
        If Not ddlKodKursus.Text = "ALL" Then
            strWhere += " AND KodKursus='" & ddlKodKursus.Text & "'"
        End If


        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
        tbl_menu_check()
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

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        tbl_menu_check()
    End Sub
End Class