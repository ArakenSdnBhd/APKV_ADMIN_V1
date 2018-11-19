Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class kursus_kolej_create
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblKod.Text = Request.QueryString("KursusID")

                kpmkv_negeri_list()
                ddlNegeri.Text = "ALL"

                kpmkv_jenis_list()
                ddlJenis.Text = "ALL"

                kpmkv_kod_list()
                ddlKodKolej.Text = "ALL"

                lblMsg.Text = ""
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri WITH (NOLOCK) ORDER BY Negeri"
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
            ddlNegeri.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej WITH (NOLOCK) ORDER BY Jenis"
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
            ddlJenis.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kod_list()
        strSQL = "SELECT Kod FROM kpmkv_kolej WITH (NOLOCK) ORDER BY Kod"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKolej.DataSource = ds
            ddlKodKolej.DataTextField = "Kod"
            ddlKodKolej.DataValueField = "Kod"
            ddlKodKolej.DataBind()

            '--ALL
            ddlKodKolej.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
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
        Dim strOrder As String = " ORDER BY Kod,Nama ASC"

        tmpSQL = "SELECT * FROM kpmkv_kolej"
        strWhere = " WITH (NOLOCK) WHERE KolejID IS NOT NULL"


        '--Jenis
        If Not ddlJenis.Text = "ALL" Then
            strWhere += " AND Jenis='" & ddlJenis.Text & "'"
        End If

        '--Negeri
        If Not ddlNegeri.Text = "ALL" Then
            strWhere += " AND Negeri='" & ddlNegeri.Text & "'"
        End If

        '--Kod
        If Not ddlKodKolej.Text = "ALL" Then
            strWhere += " AND Kod='" & ddlKodKolej.Text & "'"
        End If

        '--Nama
        If Not ddlKodKolej.Text = "ALL" Then
            strWhere += " AND Nama='" & ddlKodKolej.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function
    Private Sub tbl_menu_check()

        Dim str As String
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

            'Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

            'If isChecked = False Then
            ' str = gvTable.Rows(i).Cells(1).Text.ToString
            str = datRespondent.DataKeys(i).Value.ToString
            strSQL = "Select * from kpmkv_kursus_kolej where KolejRecordID='" & str & "' and KursusRecordID='" & lblKod.Text & "'"
            strRet = oCommon.isExist(strSQL)
            If strRet = True Then
                cb.Checked = True
            End If
            ' End If
        Next


    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim row As GridViewRow = datRespondent.Rows(i)
                Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

                If isChecked Then
                    strSQL = "Select * from kpmkv_kursus_kolej where KolejRecordID='" & datRespondent.DataKeys(i).Value.ToString & "' and KursusRecordID='" & lblKod.Text & "' and IsSelect='1'"
                    strRet = oCommon.isExist(strSQL)
                    If strRet = True Then
                        strSQL = "UPDATE kpmkv_kursus_kolej SET KolejRecordID='" & datRespondent.DataKeys(i).Value.ToString & "',KursusRecordID='" & lblKod.Text & "', IsSelect='1')"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        divMsg.Attributes("class") = "info"
                        lblMsg.Text = "Berjaya mendaftarkan kursus baru."
                    Else
                        strSQL = "INSERT INTO kpmkv_kursus_kolej(KolejRecordID,KursusRecordID,IsSelect) VALUES('" & datRespondent.DataKeys(i).Value.ToString & "','" & lblKod.Text & "','1')"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        divMsg.Attributes("class") = "info"
                        lblMsg.Text = "Berjaya mendaftarkan kursus baru."

                    End If
                End If
            Next

        Catch ex As Exception
            divMsg.Attributes("class") = "error"

        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        'Try

        '    ''--assign modul to kursus
        '    For i As Integer = 0 To datRespondent.Rows.Count - 1
        '        Dim row As GridViewRow = datRespondent.Rows(i)
        '        Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

        '        If isChecked Then
        '            strSQL = "Select ModulRecordID from kpmkv_kursus_modul WHERE ModulRecordID='" & datRespondent.DataKeys(i).Value.ToString & "' and KursusRecordID='" & lblKod.Text & "'"
        '             If oCommon.isExist(strSQL) = True Then
        '                lblMsg.Text = "Kod Modul '" & datRespondent.DataKeys(i).Value.ToString & "' telah digunakan. Sila masukkan kod yang baru."
        '                Return False
        '            End If
        '        End If
        '    Next

        'Catch ex As Exception
        'End Try
        Return True
    End Function

    Protected Sub lnkList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.kursus.search.aspx")

    End Sub
End Class