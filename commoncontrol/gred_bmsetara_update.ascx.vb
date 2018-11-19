Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class WebUserControl1
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
        strSQL = "SELECT * FROM kpmkv_gred_bmsetara WHERE GredbsID='" & Request.QueryString("GredbsID") & "'"
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sesi")) Then
                    chkSesi.Text = ds.Tables(0).Rows(0).Item("Sesi")
                Else
                    chkSesi.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MarkahFrom")) Then
                    txtMarkah.Text = ds.Tables(0).Rows(0).Item("MarkahFrom")
                Else
                    txtMarkah.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MarkahTo")) Then
                    txtMarkahTo.Text = ds.Tables(0).Rows(0).Item("MarkahTo")
                Else
                    txtMarkahTo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Gred")) Then
                    txtGred.Text = ds.Tables(0).Rows(0).Item("Gred")
                Else
                    txtGred.Text = ""
                End If
                lblGred.Text = txtGred.Text   '--to check duplicate

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Status")) Then
                    txtStatus.Text = ds.Tables(0).Rows(0).Item("Status")
                Else
                    txtStatus.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kompetensi")) Then
                    txtKompetensi.Text = ds.Tables(0).Rows(0).Item("Kompetensi")
                Else
                    txtKompetensi.Text = ""
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
            If kpmkv_gred_bmsetara_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat Gred BM Setara ."
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
        'sesi
        If chkSesi.Text = "" Then
            lblMsg.Text = "Sila pilih Sesi!"
            chkSesi.Focus()
            Return False
        End If

        '--txtGed
        If txtGred.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Gred!"
            txtGred.Focus()
            Return False
        End If

        If Not lblGred.Text = txtGred.Text Then       '--changes made to the Gred
            strSQL = "SELECT Gred FROM kpmkv_gred_bmsetara WHERE Gred='" & oCommon.FixSingleQuotes(txtGred.Text) & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.Text & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Gred telah digunakan. Sila masukkan gred yang baru."
                Return False
            End If
        End If

        '--txtMarkah
        If txtMarkah.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah!"
            txtMarkah.Focus()
            Return False
        End If

        '--txtMarkahTo
        If txtMarkahTo.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah!"
            txtMarkahTo.Focus()
            Return False
        End If

        If CInt(txtMarkah.Text) > CInt(txtMarkahTo.Text) Then
            lblMsg.Text = "Markah Mula mesti lebih Kecik dari Markah Akhir"
            txtMarkah.Focus()
            Return False
        End If

        '--txtStatus
        If txtStatus.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan status gred!"
            txtStatus.Focus()
            Return False
        End If

        '--txtKompetensi
        If txtKompetensi.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Kompetensi!"
            txtKompetensi.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function kpmkv_gred_bmsetara_update() As Boolean
        strSQL = "UPDATE kpmkv_gred_bmsetara SET Tahun='" & oCommon.FixSingleQuotes(ddlTahun.Text) & "',Sesi='" & oCommon.FixSingleQuotes(chkSesi.Text) & "',MarkahFrom='" & oCommon.FixSingleQuotes(txtMarkah.Text) & "',MarkahTo='" & oCommon.FixSingleQuotes(txtMarkahTo.Text) & "',Gred='" & oCommon.FixSingleQuotes(txtGred.Text.ToUpper) & "',Status='" & oCommon.FixSingleQuotes(txtStatus.Text.ToUpper) & "',Kompetensi='" & oCommon.FixSingleQuotes(txtKompetensi.Text.ToUpper) & "' WHERE GredbsID='" & Request.QueryString("GredbsID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strSQL = "DELETE kpmkv_gred_bmsetara WHERE GredbsID='" & Request.QueryString("GredbsID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya menghapuskan rekod gred tersebut."
        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub
    
    Protected Sub btlist_Click(sender As Object, e As EventArgs) Handles btlist.Click
        Response.Redirect("gred.bmsetara.list.aspx")
    End Sub
End Class