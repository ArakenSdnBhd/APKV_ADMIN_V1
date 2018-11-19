Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class wajaranvokasional_update
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


                LoadPage()
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY Tahun DESC"
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

    Private Sub LoadPage()
        strSQL = "SELECT * FROM kpmkv_wajaran_v WHERE WajaranID='" & Request.QueryString("WajaranID") & "'"
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
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    ddlTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    ddlTahun.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Subjek")) Then
                    txtSubjek.Text = ds.Tables(0).Rows(0).Item("Subjek")
                Else
                    txtSubjek.Text = ""
                End If
                lblSubjek.Text = txtSubjek.Text   '--to check duplicate

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Teori")) Then
                    txtTeori.Text = ds.Tables(0).Rows(0).Item("Teori")
                Else
                    txtTeori.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Amali")) Then
                    txtAmali.Text = ds.Tables(0).Rows(0).Item("Amali")
                Else
                    txtAmali.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Jenis")) Then
                    txtJenis.Text = ds.Tables(0).Rows(0).Item("Jenis")
                Else
                    txtJenis.Text = ""
                End If



            End If
            ''--debug
            'Response.Write(strSQL)
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
            If kpmkv_wajaranvokasional_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat Wajaran Vokasional ."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean
        '--ddlTahun
        If ddlTahun.SelectedValue = "00" Then
            lblMsg.Text = "Sila pilih Tahun!"
            ddlTahun.Focus()
            Return False
        End If
        '--txtSubjek
        If txtSubjek.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Subjek!"
            txtSubjek.Focus()
            Return False
        End If

        If Not lblSubjek.Text = txtSubjek.Text Then       '--changes made to the Subjek
            strSQL = "SELECT Subjek FROM kpmkv_wajaran_a WHERE Subjek='" & oCommon.FixSingleQuotes(txtSubjek.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Subjek telah digunakan. Sila masukkan subjek yang baru."
                Return False
            End If
        End If

        '--txtTeori
        If txtTeori.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah Teori!"
            txtTeori.Focus()
            Return False
        End If

        '--txtAmali
        If txtAmali.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan markah amali!"
            txtAmali.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function kpmkv_wajaranvokasional_update() As Boolean
        strSQL = "UPDATE kpmkv_wajaran_v WITH (UPDLOCK) SET Tahun='" & oCommon.FixSingleQuotes(ddlTahun.Text) & "',Subjek='" & oCommon.FixSingleQuotes(txtSubjek.Text) & "',Teori='" & oCommon.FixSingleQuotes(txtTeori.Text.ToUpper) & "',Amali='" & oCommon.FixSingleQuotes(txtAmali.Text.ToUpper) & "' WHERE WajaranID='" & Request.QueryString("WajaranID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strSQL = "DELETE kpmkv_wajaran_v WHERE WajaranID='" & Request.QueryString("WajaranID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya menghapuskan rekod wajaran vokasional tersebut."


        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub
End Class