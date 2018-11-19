Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class gred_update
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_jenis_list()

                LoadPage()
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    
    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT DISTINCT(Jenis) FROM kpmkv_gred  ORDER BY Jenis"
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

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub LoadPage()
        strSQL = "SELECT * FROM kpmkv_gred WHERE GredID='" & Request.QueryString("GredID") & "'"
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MarkahFrom")) Then
                    txtMarkahmula.Text = ds.Tables(0).Rows(0).Item("MarkahFrom")
                Else
                    txtMarkahmula.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MarkahTo")) Then
                    txtMarkahAkhir.Text = ds.Tables(0).Rows(0).Item("MarkahTo")
                Else
                    txtMarkahAkhir.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Gred")) Then
                    txtGred.Text = ds.Tables(0).Rows(0).Item("Gred")
                Else
                    txtGred.Text = ""
                End If
                lblGred.Text = txtGred.Text   '--to check duplicate

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pointer")) Then
                    txtPointer.Text = ds.Tables(0).Rows(0).Item("Pointer")
                Else
                    txtPointer.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Status")) Then
                    txtStatus.Text = ds.Tables(0).Rows(0).Item("Status")
                Else
                    txtStatus.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kompentasi")) Then
                    txtKompetensi.Text = ds.Tables(0).Rows(0).Item("Kompentasi")
                Else
                    txtKompetensi.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Jenis")) Then
                    ddlJenis.Text = ds.Tables(0).Rows(0).Item("Jenis")
                Else
                    ddlJenis.Text = ""
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
            If kpmkv_gred_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat Gred ."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean
      
        '--txtGed
        If txtGred.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Gred!"
            txtGred.Focus()
            Return False
        End If

        If Not lblGred.Text = txtGred.Text Then       '--changes made to the Gred
            strSQL = "SELECT Gred FROM kpmv_gred_bmsetara WHERE Gred='" & oCommon.FixSingleQuotes(txtGred.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Gred telah digunakan. Sila masukkan gred yang baru."
                Return False
            End If
        End If

        '--txtMarkah
        If txtMarkahmula.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah Mula!"
            txtMarkahmula.Focus()
            Return False
        End If

        If txtMarkahAkhir.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah Akhir!"
            txtMarkahAkhir.Focus()
            Return False
        End If

        If CInt(txtMarkahmula.Text) > CInt(txtMarkahAkhir.Text) Then
            lblMsg.Text = "Markah Mula mesti lebih Kecik dari Markah Akhir"
            txtMarkahmula.Focus()
            Return False
        End If

        '--txtStatus
        If txtStatus.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan status gred!"
            txtStatus.Focus()
            Return False
        End If

        '--txtPointer
        If txtPointer.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan status pointer!"
            txtPointer.Focus()
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
    Private Function kpmkv_gred_update() As Boolean
        strSQL = "UPDATE kpmkv_gred SET MarkahFrom='" & oCommon.FixSingleQuotes(txtMarkahmula.Text) & "',MarkahTo='" & oCommon.FixSingleQuotes(txtMarkahAkhir.Text) & "',Gred='" & oCommon.FixSingleQuotes(txtGred.Text.ToUpper) & "',Pointer='" & oCommon.FixSingleQuotes(txtPointer.Text.ToUpper) & "',Status='" & oCommon.FixSingleQuotes(txtStatus.Text.ToUpper) & "',Kompentasi='" & oCommon.FixSingleQuotes(txtKompetensi.Text.ToUpper) & "',Jenis='" & oCommon.FixSingleQuotes(ddlJenis.Text.ToUpper) & "' WHERE GredID='" & Request.QueryString("GredID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If
        ''--debug
        Response.Write(strSQL)
    End Function
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strSQL = "DELETE kpmkv_gred WHERE GredID='" & Request.QueryString("GredID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya menghapuskan rekod gred tersebut."
        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub

    Protected Sub btnList_Click(sender As Object, e As EventArgs) Handles btnList.Click
        Response.Redirect("admin.gred.list.aspx")

    End Sub
End Class