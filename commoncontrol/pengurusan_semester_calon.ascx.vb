Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class pengurusan_semester_calon1
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
                ddlNegeri.Text = "0"

                kpmkv_jenis_list()
                ddlJenis.Text = "0"

                kpmkv_kolej_list()
                ddlKolej.Text = "0"

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_semester_list()
                ddlSemesterTransfer.Text = "0"

                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    
    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri ORDER BY Negeri"
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
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej ORDER BY Jenis"
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
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
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
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester ORDER BY Semester"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester.DataSource = ds
            ddlSemester.DataTextField = "Semester"
            ddlSemester.DataValueField = "Semester"
            ddlSemester.DataBind()
            '--ALL
            ddlSemester.Items.Add(New ListItem("-Pilih-", "0"))

            ddlSemesterTransfer.DataSource = ds
            ddlSemesterTransfer.DataTextField = "Semester"
            ddlSemesterTransfer.DataValueField = "Semester"
            ddlSemesterTransfer.DataBind()
            '--ALL
            ddlSemesterTransfer.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Protected Sub btnPindah_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPindah.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                DivMsgTop.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_pelajar_transfer() = True Then

                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Semester Calon berjaya dipindahkan"

                DivMsgTop.Attributes("class") = "info"
                lblMsgTop.Text = "Semester Calon berjaya dipindahkan"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Semester Calon tidak berjaya dipindahkan"

                DivMsgTop.Attributes("class") = "error"
                lblMsgTop.Text = "Semester Calon tidak berjaya dipindahkan"

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
    Private Function kpmkv_pelajar_transfer() As Boolean
        Dim strmykad As String
        Dim cmd As New SqlCommand(getSQL)
        Dim dt As DataTable = GetData(cmd)

        Try
            For i As Integer = 0 To dt.Rows.Count - 1
                strmykad = dt.Rows(i)("Mykad").ToString()
                Dim strPelajarID As Integer
                strSQL = "SELECT Mykad FROM kpmkv_pelajar "
                strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND Tahun='" & ddlTahun.SelectedValue & "'"
                strSQL += " AND Semester='" & ddlSemesterTransfer.SelectedValue & "' AND Sesi ='" & chkSesi.Text & "' AND StatusID='2'"
                strSQL += " AND Mykad='" & strmykad & "'"
                If oCommon.isExist(strSQL) = True Then
                Else
                    strSQL = "INSERT INTO kpmkv_pelajar (KolejRecordID,Pengajian,Tahun,Semester,Sesi,KursusID,AngkaGiliran,"
                    strSQL += "Nama,Mykad,Tel,Email,Jantina,Kaum,Agama,StatusID,JenisCalonID,IsDeleted)"
                    strSQL += " SELECT KolejRecordID,Pengajian,'" & ddlTahun.SelectedValue & "',"
                    strSQL += "'" & ddlSemesterTransfer.SelectedValue & "',Sesi,KursusID,AngkaGiliran,"
                    strSQL += " Nama,'" & strmykad & "',Tel,Email,Jantina,Kaum,Agama,StatusID,JenisCalonID,IsDeleted FROM Kpmkv_pelajar "
                    strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND Tahun='" & ddlTahun.SelectedValue & "' "
                    strSQL += " AND Semester='" & ddlSemester.SelectedValue & "' AND Sesi ='" & chkSesi.Text & "' AND StatusID='2' "
                    strSQL += " And Mykad='" & strmykad & "' And IsDeleted='N'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

                'check pelajarid
                strSQL = "SELECT PelajarID FROM kpmkv_pelajar "
                strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND Tahun='" & ddlTahun.SelectedValue & "'"
                strSQL += " AND Semester='" & ddlSemesterTransfer.SelectedValue & "' AND Sesi ='" & chkSesi.Text & "' AND StatusID='2'"
                strSQL += " AND Mykad='" & strmykad & "'"
                strPelajarID = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT PelajarID FROM kpmkv_pelajar_markah "
                strSQL += " WHERE KolejRecordID='" & ddlKolej.Text & "' AND Tahun='" & ddlTahun.SelectedValue & "'"
                strSQL += " AND Semester='" & ddlSemesterTransfer.SelectedValue & "' AND Sesi ='" & chkSesi.Text & "'"
                strSQL += " AND PelajarID='" & strPelajarID & "'"
                If oCommon.isExist(strSQL) = True Then
                Else
                    strSQL = "INSERT INTO kpmkv_pelajar_markah (PelajarID,KolejRecordID,KursusID,Tahun,Semester,Sesi)"
                    strSQL += " SELECT PelajarID,KolejRecordID,KursusID,Tahun,Semester,Sesi FROM Kpmkv_pelajar"
                    strSQL += " WHERE PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

            Next

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Calon sudah wujud pada semester berkenaan"
            Return False
        End Try

        Return True
    End Function
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
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi ASC"

        tmpSQL = "SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi, kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, "
        tmpSQL += " kpmkv_kursus.KodKursus, kpmkv_kelas.NamaKelas"
        tmpSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN"
        tmpSQL += " kpmkv_status ON kpmkv_pelajar.StatusID = kpmkv_status.StatusID LEFT OUTER JOIN kpmkv_kelas ON kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
        strWhere = " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2'"

        'tahun
        If Not ddlTahun.Text = "0" Then
            strWhere += " AND kpmkv_pelajar.Tahun='" & ddlTahun.Text & "'"
        End If

        '--semester
        If Not ddlSemester.Text = "0" Then
            strWhere += " AND kpmkv_pelajar.Semester='" & ddlSemester.SelectedValue & " '"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanged(sender As Object, e As EventArgs) Handles datRespondent.PageIndexChanged
        datRespondent.DataBind()

    End Sub
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

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""
        strRet = BindData(datRespondent)

    End Sub
   
    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
    End Sub
    Private Function ValidatePage() As Boolean

        If ddlSemesterTransfer.Text <= ddlSemester.Text Then
            lblMsg.Text = "Sila pilih semester akan datang"
            lblMsgTop.Text = "Sila pilih semester akan datang"
            ddlSemesterTransfer.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_jenis_list()
        ddlJenis.Text = "0"
        kpmkv_kolej_list()
        ddlKolej.Text = "0"
    End Sub
End Class