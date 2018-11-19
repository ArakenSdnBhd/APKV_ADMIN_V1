Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class kluster_update
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        Try
            If Not IsPostBack Then
                kpmkv_tahun_list()


                strSQL = "SELECT NamaKluster FROM kpmkv_kluster WHERE KlusterID='" & Request.QueryString("KlusterID") & "'"
                lblNamaKluster.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Tahun FROM kpmkv_kluster WHERE KlusterID='" & Request.QueryString("KlusterID") & "'"
                ddlTahun.SelectedValue = oCommon.getFieldValue(strSQL)
                LoadPage()
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
            ddlTahun.Items.Add(New ListItem("ALL", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub LoadPage()
        strSQL = "SELECT * FROM kpmkv_kluster WHERE KlusterID='" & Request.QueryString("KlusterID") & "'"
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

               
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaKluster")) Then
                    txtNama.Text = ds.Tables(0).Rows(0).Item("NamaKluster")
                Else
                    txtNama.Text = ""
                End If
                lblNamaKluster.Text = txtNama.Text
            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

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
                lblMsg.Text = "Berjaya mengemaskini maklumat Kluster."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean
      

        If Not lblNamaKluster.Text = txtNama.Text Then       '--changes made to the kod
            strSQL = "SELECT NamaKluster FROM kpmkv_kluster WHERE NamaKluster='" & oCommon.FixSingleQuotes(txtNama.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Nama Kluster telah digunakan. Sila masukkan Nama Kluster yang baru."
                Return False
            End If
        End If

        '--txtNama
        If txtNama.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Kluster!"
            txtNama.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function kpmkv_kursus_update() As Boolean
        strSQL = "UPDATE kpmkv_kluster WITH (UPDLOCK) SET NamaKluster='" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "', IsDeleted='N',Tahun='" & oCommon.FixSingleQuotes(ddlTahun.Text) & "' WHERE KlusterID='" & Request.QueryString("KlusterID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strSQL = "UPDATE kpmkv_kluster WITH (UPDLOCK) SET IsDeleted='Y' ,Tahun='" & oCommon.FixSingleQuotes(ddlTahun.Text) & "' WHERE KlusterID='" & Request.QueryString("KlusterID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya meghapuskan rekod Kluster tersebut."
        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub

   

End Class