Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Public Class admin_FIK_SVM
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblMsg.Text = ""

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_negeri_list()
                ddlNegeri.Text = ""

                kpmkv_jenis_list()
                ddlJenis.Text = ""

                kpmkv_kolej_list()
                ddlKolej.Text = ""

            End If

        Catch ex As Exception
            lblMsg.Text = "Error Message:" & ex.Message
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
            ddlNegeri.Items.Add(New ListItem("-Pilih-", ""))

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
            ddlJenis.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "' ORDER BY Nama"
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
            ddlKolej.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Function ValidateJanaAll() As Boolean

        '--txtNama
        If chkSesi.Text = "" Then
            lblMsg.Text = "Sila pilih Sesi!"
            chkSesi.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function ValidateJanaKv() As Boolean


        If chkSesi.Text = "" Then
            lblMsg.Text = "Sila pilih Sesi!"
            chkSesi.Focus()
            Return False
        End If

        If ddlNegeri.SelectedValue = "" Then
            lblMsg.Text = "Sila pilih Negeri!"
            ddlNegeri.Focus()
            Return False
        End If

        If ddlKolej.Text = "" Then
            lblMsg.Text = "Sila pilih Kolej!"
            ddlKolej.Focus()
            Return False
        End If

        Return True
    End Function

    ''jana semua
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        tblContent.InnerHtml = ""

        Dim strSQL2 As String = ""

        '--validate
        If ValidateJanaAll() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        strSQL = "SELECT * FROM kpmkv_SVM WHERE IsBMTahun='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        If oCommon.isExist(strSQL) = True Then

            strSQL = "DELETE FROM kpmkv_SVM WHERE IsBMTahun='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        getSQL1()

        getSQL2()

        ''kompeten
        getSQLVOK1()

        getSQLVOK2()

        getSQLVOK3()

        getSQLVOK4()

        getSQLVOK5()

        getSQLVOK6()

        getSQLVOK7()

        getSQLVOK8()

        ''''
        getSQLMykad()
        getSQLMykadReverse()

        getSQLPNGKA()

        getSQLPNGKV()

        getSQLBM()

        updateLayakSVM()


        getSQLLayakSVM()

        getSQLLayakSVMCount()

    End Sub


    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Mykad ASC"

        '--not deleted
        tmpSQL = "SELECT Mykad FROM kpmkv_pelajar where IsDeleted='N' AND StatusID='2' AND IsCalon='1' AND Kelasid IS NOT NULL"


        '--tahun
        If Not ddlTahun.Text = "" Then
            strWhere += " AND IsBMTahun ='" & ddlTahun.Text & "'"
        End If

        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND Sesi ='" & chkSesi.Text & "'"
        End If

        If Not ddlKolej.SelectedValue = "" Then
            strWhere += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function

    Private Function getSQLSVM() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY PelajarID ASC"

        '--not deleted
        tmpSQL = "SELECT PelajarID FROM kpmkv_SVM where IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"

        If Not ddlKolej.SelectedValue = "" Then
            strWhere += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        getSQLSVM = tmpSQL & strWhere & strOrder

        Return getSQLSVM

    End Function


    'insert data in kpmkv_svm
    Private Sub getSQL1()
        Dim strMykad As String = ""
        lblStep1.Text = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQL)
        Dim dt As DataTable = GetData(cmd)

        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("Mykad")

            strSQL = "INSERT INTO kpmkv_SVM(PelajarID,KolejRecordID,IsBMTahun,Tahun,Sesi,Semester,Nama,"
            strSQL += " Mykad,AngkaGiliran,KodKursus)"
            strSQL += " SELECT  kpmkv_pelajar.PelajarID, kpmkv_pelajar.KolejRecordID,'" & ddlTahun.Text & "',"
            strSQL += " kpmkv_pelajar.Tahun,kpmkv_pelajar.Sesi,kpmkv_pelajar.Semester,kpmkv_pelajar.Nama,"
            strSQL += "  kpmkv_pelajar.MYKAD,kpmkv_pelajar.AngkaGiliran, "
            strSQL += " kpmkv_kursus.KodKursus FROM kpmkv_pelajar"
            strSQL += " LEFT OUTER JOIN kpmkv_kursus ON kpmkv_kursus.KursusID=kpmkv_pelajar.KursusID "
            strSQL += " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2'"
            strSQL += " AND kpmkv_pelajar.Mykad='" & strMykad & "' AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
            strSQL += " AND kpmkv_pelajar.Kelasid IS NOT NULL"


            If Not ddlKolej.SelectedValue = "" Then
                strSQL += " AND kpmkv_pelajar.KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
            End If

            strRet = oCommon.ExecuteSQL(strSQL)

            If Not strRet = "0" Then
                lblStep1.Text = "system error:" & strRet

                Exit Sub
            End If

        Next

        lblStep1.Text = "Peringkat 1 Berjaya"
    End Sub



    Private Sub getSQL2()
        Dim IntPelajarID As Integer = 0
        Dim StrGredV1 As String = ""
        Dim StrGredV2 As String = ""
        Dim StrGredV3 As String = ""
        Dim StrGredV4 As String = ""
        Dim StrGredV5 As String = ""
        Dim StrGredV6 As String = ""
        Dim StrGredV7 As String = ""
        Dim StrGredV8 As String = ""
        Dim StrGredBMSetara As String = ""
        Dim StrPNGKA As String = ""
        Dim StrPNGKV As String = ""
        Dim StrPNGKK As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQLSVM)
        Dim dt As DataTable = GetData(cmd)

        For i As Integer = 0 To dt.Rows.Count - 1
            IntPelajarID = dt.Rows(i)("PelajarID")

            strSQL += " SELECT GredV1,GredV2,GredV3,GredV4,GredV5,GredV6,GredV7,GredV8,GredBMSetara,PNGKA,PNGKV,PNGKK"
            strSQL += " FROM kpmkv_pelajar_markah"
            strSQL += " WHERE PelajarID ='" & IntPelajarID & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Detail As Array
            ar_Detail = strRet.Split("|")
            StrGredV1 = ar_Detail(0)
            StrGredV2 = ar_Detail(1)
            StrGredV3 = ar_Detail(2)
            StrGredV4 = ar_Detail(3)
            StrGredV5 = ar_Detail(4)
            StrGredV6 = ar_Detail(5)
            StrGredV7 = ar_Detail(6)
            StrGredV8 = ar_Detail(7)
            StrGredBMSetara = ar_Detail(8)
            StrPNGKA = ar_Detail(9)
            StrPNGKV = ar_Detail(10)
            StrPNGKK = ar_Detail(11)

            strSQL = " UPDATE kpmkv_SVM SET GredV1='" & StrGredV1 & "',GredV2='" & StrGredV2 & "',GredV3='" & StrGredV3 & "',GredV4='" & StrGredV4 & "',"
            strSQL += " GredV5='" & StrGredV5 & "',GredV6='" & StrGredV6 & "',GredV7='" & StrGredV7 & "',"
            strSQL += " GredV8='" & StrGredV8 & "',GredBMSetara='" & StrGredBMSetara & "',PNGKA='" & StrPNGKA & "',PNGKV='" & StrPNGKV & "',PNGKK='" & StrPNGKK & "'"
            strSQL += " WHERE PelajarID ='" & IntPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If Not strRet = "0" Then
                lblStep2.Text = "system error:" & strRet

                Exit Sub
            End If
        Next

        lblStep2.Text = "Peringkat 2 Berjaya"
    End Sub

    'delete if gred x kompeten
    Private Sub getSQLVOK1()

        strSQL = "UPDATE kpmkv_SVM SET IsLayak='0' WHERE IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND gredv1 NOT IN ('A', 'A-', 'B+', 'B', 'B-') "

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            lblStep3.Text = "Peringkat 3 Berjaya"
        Else
            lblStep3.Text = "system error:" & strRet

            Exit Sub
        End If

    End Sub

    Private Sub getSQLVOK2()
        lblStep3.Text = ""

        strSQL = "UPDATE kpmkv_SVM SET IsLayak='0' WHERE IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND gredv2 NOT IN ('A', 'A-', 'B+', 'B', 'B-') "

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            lblStep3.Text = "Peringkat 3 Berjaya"
        Else
            lblStep3.Text = "system error:" & strRet

            Exit Sub
        End If
    End Sub

    Private Sub getSQLVOK3()
        lblStep3.Text = ""
        strSQL = "UPDATE kpmkv_SVM SET IsLayak='0' WHERE IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND gredv3 NOT IN ('A', 'A-', 'B+', 'B', 'B-', 'NULL','') "

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            lblStep3.Text = "Peringkat 3 Berjaya"
        Else
            lblStep3.Text = "system error:" & strRet

            Exit Sub
        End If

    End Sub

    Private Sub getSQLVOK4()
        lblStep3.Text = ""
        strSQL = "UPDATE kpmkv_SVM SET IsLayak='0' WHERE IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND gredv4 NOT IN ('A', 'A-', 'B+', 'B', 'B-', 'NULL','') "

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            lblStep3.Text = "Peringkat 3 Berjaya"
        Else
            lblStep3.Text = "system error:" & strRet

            Exit Sub
        End If

    End Sub

    Private Sub getSQLVOK5()
        lblStep3.Text = ""

        strSQL = "UPDATE kpmkv_SVM SET IsLayak='0' WHERE IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND gredv5 NOT IN ('A', 'A-', 'B+', 'B', 'B-', 'NULL','') "

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            lblStep3.Text = "Peringkat 3 Berjaya"
        Else
            lblStep3.Text = "system error:" & strRet

            Exit Sub
        End If
    End Sub

    Private Sub getSQLVOK6()
        lblStep3.Text = ""

        strSQL = "UPDATE kpmkv_SVM SET IsLayak='0' WHERE IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND gredv6 NOT IN ('A', 'A-', 'B+', 'B', 'B-', 'NULL','') "

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            lblStep3.Text = "Peringkat 3 Berjaya"
        Else
            lblStep3.Text = "system error:" & strRet

            Exit Sub
        End If

    End Sub

    Private Sub getSQLVOK7()
        lblStep3.Text = ""

        strSQL = "UPDATE kpmkv_SVM SET IsLayak='0' WHERE IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND gredv7 NOT IN ('A', 'A-', 'B+', 'B', 'B-', 'NULL','') "

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            lblStep3.Text = "Peringkat 3 Berjaya"
        Else
            lblStep3.Text = "system error:" & strRet

            Exit Sub
        End If

    End Sub

    Private Sub getSQLVOK8()
        lblStep3.Text = ""

        strSQL = "UPDATE kpmkv_SVM SET IsLayak='0' WHERE IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND gredv8 NOT IN ('A', 'A-', 'B+', 'B', 'B-', 'NULL','') "

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            lblStep3.Text = "Peringkat 3 Berjaya"
        Else
            lblStep3.Text = "system error:" & strRet

            Exit Sub
        End If
    End Sub

    'ispngka
    Private Sub getSQLPNGKA()

        strSQL = "UPDATE kpmkv_SVM SET IsPNGKA='1'"
        strSQL += " WHERE IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "' AND Semester='4'"
        strSQL += " AND PNGKA >='2'"

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        'If strRet = "0" Then
        '    lblStep4.Text = "Peringkat 4 Berjaya"
        'Else
        '    lblStep4.Text = "system error:" & strRet

        '    Exit Sub
        'End If
    End Sub

    'íspngkv
    Private Sub getSQLPNGKV()

        strSQL = "UPDATE kpmkv_SVM SET IsPNGKV='1'"
        strSQL += " WHERE  IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "' AND Semester='4'"
        strSQL += " AND PNGKV >='2.67' "

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)


    End Sub

    'issetara
    Private Sub getSQLBM()

        strSQL = "UPDATE kpmkv_SVM SET IsSETARA='1'"
        strSQL += " WHERE  IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "' AND Semester='4'"
        strSQL += " AND GredBMSetara IN ('E','D','C','C+','B','B+','A-','A','A+')"

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        'If strRet = "0" Then
        '    lblStep6.Text = "Peringkat 6 Berjaya"
        'Else
        '    lblStep6.Text = "system error:" & strRet

        '    Exit Sub
        'End If
    End Sub

    'islayak for sem 4 (if semua sem islayak= 1 bru sem 4 layak=1)
    Private Sub getSQLMykad()

        Try

            strSQL = "SELECT Mykad,IsLayak FROM kpmkv_SVM WHERE IsBMTahun ='" & ddlTahun.Text & "' "
            strSQL += " AND Sesi='" & chkSesi.Text & "' "

            If Not ddlKolej.SelectedValue = "" Then
                strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
            End If

            strSQL += " GROUP BY Mykad ,IsLayak having sum(islayak)=4 "


            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            Dim numrows As Integer

            numrows = MyTable.Rows.Count

            Dim strMykad As String

            strMykad = ""

            If numrows > 0 Then
                For i = 0 To numrows - 1

                    strMykad = ds.Tables(0).Rows(i).Item("Mykad")

                    strSQL = " UPDATE kpmkv_SVM SET Islayak='1'"
                    strSQL += " WHERE Mykad ='" & strMykad & "' AND Semester='4'"
                    strSQL += " AND IsBMTahun ='" & ddlTahun.Text & "' "
                    strSQL += " AND Sesi='" & chkSesi.Text & "' "

                    If Not ddlKolej.SelectedValue = "" Then
                        strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
                    End If

                    strRet = oCommon.ExecuteSQL(strSQL)

                Next
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try


    End Sub

    'if total islayak semua sem <4 then update islayak sem 4=0
    Private Sub getSQLMykadReverse()

        Try

            strSQL = "SELECT Mykad,IsLayak FROM kpmkv_SVM WHERE IsBMTahun ='" & ddlTahun.Text & "' "
            strSQL += " AND Sesi='" & chkSesi.Text & "' "

            If Not ddlKolej.SelectedValue = "" Then
                strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
            End If

            strSQL += " GROUP BY Mykad ,IsLayak having sum(islayak)<4 "


            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            Dim numrows As Integer

            numrows = MyTable.Rows.Count

            Dim strMykad As String

            strMykad = ""

            If numrows > 0 Then
                For i = 0 To numrows - 1

                    strMykad = ds.Tables(0).Rows(i).Item("Mykad")

                    strSQL = " UPDATE kpmkv_SVM SET Islayak='0'"
                    strSQL += " WHERE Mykad ='" & strMykad & "' AND Semester='4'"
                    strSQL += " AND IsBMTahun ='" & ddlTahun.Text & "' "
                    strSQL += " AND Sesi='" & chkSesi.Text & "' "

                    If Not ddlKolej.SelectedValue = "" Then
                        strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
                    End If

                    strRet = oCommon.ExecuteSQL(strSQL)

                Next
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try


    End Sub



    'layakSVM
    Private Sub updateLayakSVM()
        Dim strSQL As String
        Dim strWhere As String = ""
        strSQL = "UPDATE kpmkv_SVM SET LayakSVM='1'"
        strSQL += " WHERE Semester='4' AND IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND IsLayak='1' AND IsPNGKA ='1' AND IsPNGKV ='1' AND IsSETARA ='1'"

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)


        If strRet = "0" Then
            lblStep4.Text = "Peringkat 4 Berjaya"
        Else
            lblStep4.Text = "system error:" & strRet

            Exit Sub
        End If

        'If strRet = "0" Then
        '    lblStep8.Text = "Peringkat 8 Berjaya"
        'Else
        '    lblStep8.Text = "system error:" & strRet

        '    Exit Sub
        'End If

    End Sub

    Private Function getSQLLayakSVM() As String
        Dim strSQL As String
        Dim strWhere As String = ""
        strSQL = "SELECT KolejRecordID,Tahun,Sesi,Nama,MYKAD,AngkaGiliran,KodKursus,GredBMSetara,PNGKA, PNGKV,PNGKK FROM kpmkv_SVM"
        strWhere = " WHERE Semester='4' AND IsBMTahun ='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strWhere += " AND IsLayak='1' AND IsPNGKA ='1' AND IsPNGKV ='1' AND IsSETARA ='1'"

        If Not ddlKolej.SelectedValue = "" Then
            strWhere += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        getSQLLayakSVM = strSQL & strWhere

        Return getSQLLayakSVM

    End Function

    Private Sub getSQLLayakSVMCount()

        strSQL = " SELECT Count(PelajarID) AS COUNTP"
        strSQL += " FROM kpmkv_SVM"
        strSQL += " WHERE Semester='4'"
        strSQL += " AND IsBMTahun ='" & ddlTahun.Text & "'"
        strSQL += " AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND LayakSVM='1'"

        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        lblCOUNTP.Text = " ( Jumlah Layak SVM :" + oCommon.getFieldValue(strSQL) + " )"

        ''--debug 
        'Response.Write(getSQLSEM1)


    End Sub

    Private Function getSQLLayakSVMExcel() As String
        Dim strSQL As String
        Dim strWhere As String = ""
        strSQL = "SELECT kpmkv_kolej.Nama,kpmkv_SVM.Tahun,kpmkv_SVM.Sesi,kpmkv_SVM.Nama,kpmkv_SVM.MYKAD,kpmkv_SVM.AngkaGiliran,kpmkv_SVM.KodKursus,kpmkv_SVM.GredBMSetara,kpmkv_SVM.PNGKA,kpmkv_SVM.PNGKV,kpmkv_SVM.PNGKK "
        strSQL += " FROM kpmkv_SVM,kpmkv_kolej"
        strWhere = " WHERE kpmkv_kolej.RecordID = kpmkv_SVM.KolejRecordID AND kpmkv_SVM.Semester ='4'"
        strWhere += " AND kpmkv_SVM.IsBMTahun ='" & ddlTahun.Text & "' AND kpmkv_SVM.Sesi ='" & chkSesi.Text & "'"
        strWhere += " AND kpmkv_SVM.IsLayak='1' AND kpmkv_SVM.IsPNGKA ='1' AND kpmkv_SVM.IsPNGKV ='1' AND kpmkv_SVM.IsSETARA ='1'"

        If Not ddlKolej.SelectedValue = "" Then
            strWhere += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If

        getSQLLayakSVMExcel = strSQL & strWhere
        ''--debug
        'Response.Write(getSQLSEM1)

        Return getSQLLayakSVMExcel

    End Function

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Try
            lblMsg.Text = ""

            ExportToCSV(getSQLLayakSVMExcel)

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try

    End Sub

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=FileLayakSVM.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

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



    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        tblContent.InnerHtml = ""

        kpmkv_jenis_list()
        ddlJenis.SelectedValue = ""

        ddlKolej.SelectedValue = ""
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        tblContent.InnerHtml = ""

        kpmkv_kolej_list()
        ddlKolej.Text = ""

    End Sub



    ''jana by kv
    Protected Sub btnJana_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnJana.Click
        lblMsg.Text = ""
        tblContent.InnerHtml = ""
        '--validate
        If ValidateJanaKv() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        strSQL = "SELECT * FROM kpmkv_SVM WHERE IsBMTahun='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        If oCommon.isExist(strSQL) = True Then

            strSQL = "DELETE FROM kpmkv_SVM WHERE IsBMTahun='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        getSQL1()

        getSQL2()

        ''kompeten
        getSQLVOK1()

        getSQLVOK2()

        getSQLVOK3()

        getSQLVOK4()

        getSQLVOK5()

        getSQLVOK6()

        getSQLVOK7()

        getSQLVOK8()

        ''''
        getSQLMykad()

        getSQLMykadReverse()


        getSQLPNGKA()

        getSQLPNGKV()

        getSQLBM()

        updateLayakSVM()

        getSQLLayakSVM()

        getSQLLayakSVMCount()


    End Sub


    ''''''''''''''jana berperingkat''''''''''''''''''''''


    'step 1

    Protected Sub btnStep1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStep1.Click
        lblStep1.Text = ""
        lblStep2.Text = ""
        lblStep3.Text = ""
        lblStep4.Text = ""

        lblMsg.Text = ""

        '--validate
        If ValidateJanaAll() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        strSQL = "SELECT * FROM kpmkv_SVM WHERE IsBMTahun='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        If Not ddlKolej.SelectedValue = "" Then
            strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        End If
        If oCommon.isExist(strSQL) = True Then

            strSQL = "DELETE FROM kpmkv_SVM WHERE IsBMTahun='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
            If Not ddlKolej.SelectedValue = "" Then
                strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
            End If
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        getSQL1()

    End Sub


    Protected Sub btnStep2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStep2.Click
        lblStep2.Text = ""
        lblStep3.Text = ""
        lblStep4.Text = ""

        lblMsg.Text = ""

        '--validate
        If ValidateJanaAll() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        getSQL2()

    End Sub

    'step 3
    Protected Sub btnStep3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStep3.Click
        lblStep3.Text = ""
        lblStep4.Text = ""

        lblMsg.Text = ""

        '--validate
        If ValidateJanaAll() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        getSQLVOK1()

        getSQLVOK2()

        getSQLVOK3()

        getSQLVOK4()

        getSQLVOK5()

        getSQLVOK6()

        getSQLVOK7()

        getSQLVOK8()

    End Sub

    'step 4
    Protected Sub btnStep4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStep4.Click
        lblStep4.Text = ""

        lblMsg.Text = ""

        '--validate
        If ValidateJanaAll() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        getSQLPNGKA()
        getSQLPNGKV()
        getSQLBM()
        getSQLMykad()
        getSQLMykadReverse()

        updateLayakSVM()

        getSQLLayakSVMCount()
    End Sub

    Private Sub CleanseData()
        Try

            strSQL = "SELECT Mykad FROM kpmkv_SVM WHERE IsBMTahun ='" & ddlTahun.Text & "' "
            strSQL += " AND Sesi='" & chkSesi.Text & "' "

            If Not ddlKolej.SelectedValue = "" Then
                strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
            End If

            If ddlCondition.SelectedValue = "1" Then

                strSQL += " GROUP BY Mykad having Count(Mykad) > 4 "

            ElseIf ddlCondition.SelectedValue = "2" Then

                strSQL += " GROUP BY Mykad having Count(Mykad) < 4 "

            ElseIf ddlCondition.SelectedValue = "3" Then

                strSQL += " GROUP BY Mykad having Count(Mykad) > 8 "
            End If

                Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            Dim numrows As Integer

            numrows = MyTable.Rows.Count

            Dim strMykad, strMykadText, strNamaText, strAGText, strIsBmTahunText As String
            Dim strSesiText, strSemesterText, strKodKursusText, strMenu As String

            strMykad = ""
            strMykadText = ""
            strNamaText = ""
            strAGText = ""
            strIsBmTahunText = ""
            strSesiText = ""
            strSemesterText = ""
            strKodKursusText = ""
            strMenu = ""

            If numrows > 0 Then
                For i = 0 To numrows - 1

                    strMykad = ds.Tables(0).Rows(i).Item("Mykad")

                    strSQL = " SELECT Nama,Mykad,AngkaGiliran,KodKursus,Semester,Sesi"
                    strSQL += " FROM kpmkv_SVM"
                    strSQL += " WHERE Mykad ='" & strMykad & "'"
                    strSQL += " AND IsBMTahun ='" & ddlTahun.Text & "' "
                    strSQL += " AND Sesi='" & chkSesi.Text & "' "

                    If Not ddlKolej.SelectedValue = "" Then
                        strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
                    End If

                    Dim strConn1 As String = ConfigurationManager.AppSettings("ConnectionString")
                    Dim objConn1 As SqlConnection = New SqlConnection(strConn1)
                    Dim sqlDA1 As New SqlDataAdapter(strSQL, objConn1)

                    Dim ds1 As DataSet = New DataSet
                    sqlDA1.Fill(ds1, "AnyTable")

                    Dim nCount1 As Integer = 1
                    Dim MyTable1 As DataTable = New DataTable
                    MyTable1 = ds1.Tables(0)
                    Dim numrows1 As Integer

                    numrows1 = MyTable1.Rows.Count


                    For k = 0 To numrows1 - 1
                        strNamaText = ds1.Tables(0).Rows(k).Item("Nama")
                        strMykadText = ds1.Tables(0).Rows(k).Item("Mykad")
                        strAGText = ds1.Tables(0).Rows(k).Item("AngkaGiliran")
                        strSesiText = ds1.Tables(0).Rows(k).Item("Sesi")
                        strSemesterText = ds1.Tables(0).Rows(k).Item("Semester")
                        strKodKursusText = ds1.Tables(0).Rows(k).Item("KodKursus")


                        strMenu += "<tr>"
                        strMenu += "<td style ='border-Bottom:solid 1px black;border-Right:solid 1px grey;width:35%;padding:2px'>" & strNamaText & "</td>"
                        strMenu += "<td style ='border-Bottom:solid 1px black;border-Right:solid 1px grey;width:10%;text-align:center;padding:2px'>" & strMykadText & "</td>"
                        strMenu += "<td style ='border-Bottom:solid 1px black;border-Right:solid 1px grey;width:10%;text-align:center;padding:2px'>" & strAGText & "</td>"
                        strMenu += "<td style ='border-Bottom:solid 1px black;border-Right:solid 1px grey;width:10%;text-align:center;padding:2px'>" & strKodKursusText & "</td>"
                        strMenu += "<td style ='border-Bottom:solid 1px black;border-Right:solid 1px grey;width:10%;text-align:center;padding:2px'>" & strSesiText & "</td>"
                        strMenu += "<td style ='border-Bottom:solid 1px black;border-Right:solid 1px grey;width:10%;text-align:center;padding:2px'>" & strSemesterText & "</td>"
                        strMenu += "<td style ='border-Bottom:solid 1px black;padding:2px'><asp:button ID='btnDelete' runat=</td>"
                        strMenu += "</tr>"


                    Next


                Next
                tblContent.InnerHtml = strMenu
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnCari_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCari.Click
        tblContent.InnerHtml = ""

        CleanseData()

        If ddlCondition.SelectedValue = "2" Then
            btnDelete.Visible = True
        ElseIf ddlCondition.SelectedValue = "1" Then
            btnDelete.Visible = False
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        lblMsg.Text = ""
        Try

            strSQL = "SELECT Mykad FROM kpmkv_SVM WHERE IsBMTahun ='" & ddlTahun.Text & "' "
            strSQL += " AND Sesi='" & chkSesi.Text & "' "

            If Not ddlKolej.SelectedValue = "" Then
                strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
            End If

            If ddlCondition.SelectedValue = "1" Then

                strSQL += " GROUP BY Mykad having Count(Mykad) > 4 "

            ElseIf ddlCondition.SelectedValue = "2" Then

                strSQL += " GROUP BY Mykad having Count(Mykad) < 4 "
            End If

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            Dim numrows As Integer

            numrows = MyTable.Rows.Count

            Dim strMykad As String

            strMykad = ""

            If numrows > 0 Then
                For i = 0 To numrows - 1

                    strMykad = ds.Tables(0).Rows(i).Item("Mykad")

                    strSQL = " DELETE FROM kpmkv_SVM"
                    strSQL += " WHERE Mykad ='" & strMykad & "'"
                    strSQL += " AND IsBMTahun ='" & ddlTahun.Text & "' "
                    strSQL += " AND Sesi='" & chkSesi.Text & "' "

                    If Not ddlKolej.SelectedValue = "" Then
                        strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
                    End If

                    strRet = oCommon.ExecuteSQL(strSQL)

                Next
            End If

            If strRet = "0" Then

                lblMsg.Text = "Rekod Berjaya Dipadam"
                tblContent.InnerHtml = ""
                getSQLLayakSVMCount()

            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "System Error:" & strRet

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
End Class