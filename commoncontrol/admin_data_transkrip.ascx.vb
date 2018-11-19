Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData
Imports Microsoft.Office.Interop

Public Class admin_data_transkrip
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strJenisKursus As String = ""
    Dim strValue As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_negeri_list()
                ddlNegeri.Text = "0"

                kpmkv_kolej_list()
                ddlKolej.Text = "0"
                lblMsg.Text = ""

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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
            'lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.Text & "' ORDER BY Nama ASC"
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
            'lblMsg.Text = "System Error:" & ex.Message

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
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQLX() As String
        Dim tmpSQL As String
        Dim strOrder As String = " GROUP BY kpmkv_SVM.Mykad ORDER BY kpmkv_SVM.Mykad"
        Dim strWhere As String = ""

        tmpSQL = "SELECT Distinct kpmkv_SVM.MyKad FROM kpmkv_SVM "
        tmpSQL += " INNER JOIN kpmkv_kolej ON kpmkv_kolej.RecordID=kpmkv_SVM.KolejRecordID"
        strWhere = " WHERE kpmkv_SVM.Semester='4' AND kpmkv_SVM.LayakSVM='0'"

        '--Negeri
        If Not ddlNegeri.Text = "0" Then
            strWhere += " AND kpmkv_kolej.Negeri ='" & ddlNegeri.Text & "'"
        End If

        '--kolej
        If Not ddlKolej.Text = "0" Then
            strWhere += " AND kpmkv_SVM.KolejRecordID ='" & ddlKolej.Text & "'"
        End If

        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_SVM.Sesi ='" & chkSesi.Text & "'"
        End If

        If Not ddlTahun.SelectedValue = "" Then
            strWhere += " AND kpmkv_SVM.IsBMTahun='" & ddlTahun.SelectedValue & "'"
        End If

        ''--tahun
        getSQLX = tmpSQL & strWhere & strOrder

        Return getSQLX()


    End Function

    Private Function getSQLDB() As String
        Dim tmpSQL As String

        'tmpSQL = "SELECT MyKad FROM kpmkv_transkrip_svm GROUP BY NamaKolej,Mykad"
        tmpSQL = "SELECT s.MyKad FROM kpmkv_transkrip_svm as s"
        tmpSQL += " LEFT JOIN kpmkv_kolej as k ON s.NamaKolej =k.Nama "
        tmpSQL += " WHERE s.ISBMTahun='" & ddlTahun.SelectedValue & "'"
        tmpSQL += " AND k.recordID='" & ddlKolej.SelectedValue & "'"

        getSQLDB = tmpSQL

        Return getSQLDB

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strOrder As String = " GROUP BY kpmkv_SVM.Mykad ORDER BY kpmkv_SVM.Mykad"
        Dim strWhere As String = ""

        tmpSQL = "SELECT Distinct kpmkv_SVM.MyKad FROM kpmkv_SVM "
        tmpSQL += " INNER JOIN kpmkv_kolej ON kpmkv_kolej.RecordID=kpmkv_SVM.KolejRecordID"
        strWhere = " WHERE kpmkv_SVM.Semester='4' "

        If strValue = "Kompeten" Then
            strWhere += " And kpmkv_SVM.LayakSVM='1'"

        ElseIf strValue = "NotKompeten" Then
            strWhere += " And kpmkv_SVM.LayakSVM='0'"

        End If

        '--Negeri
        If Not ddlNegeri.Text = "0" Then
            strWhere += " AND kpmkv_kolej.Negeri ='" & ddlNegeri.Text & "'"
        End If

        '--kolej
        If Not ddlKolej.Text = "0" Then
            strWhere += " AND kpmkv_SVM.KolejRecordID ='" & ddlKolej.Text & "'"
        End If

        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_SVM.Sesi ='" & chkSesi.Text & "'"
        End If

        If Not ddlTahun.SelectedValue = "" Then
            strWhere += " AND kpmkv_SVM.IsBMTahun='" & ddlTahun.SelectedValue & "'"
        End If

        ''--tahun
        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function

    Private Sub getStep1()
        Dim strNama As String = ""
        Dim strMykad As String = ""
        Dim strAngkaGiliran As String = ""
        Dim strTahun As String = ""
        Dim strKursusID As Integer = 0
        Dim strNamaKolej As String = ""
        Dim strRecordID As String = ""
        Dim strNegeri As String = ""
        Dim strNamaKursus As String = ""
        Dim strNamaKluster As String = ""
        Dim strSesi As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQL)
        Dim dt As DataTable = GetData(cmd)
        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("MyKad")

            strSQL = "SELECT DISTINCT Nama,AngkaGiliran,KursusID,KolejRecordID FROM kpmkv_pelajar WHERE MyKad='" & strMykad & "'"
            strSQL += " AND IsBMTahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.Text & "' AND Semester='4'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Detail1 As Array
            ar_Detail1 = strRet.Split("|")
            strNama = ar_Detail1(0)
            strAngkaGiliran = ar_Detail1(1)
            strKursusID = ar_Detail1(2)
            strRecordID = ar_Detail1(3)
            '1
            strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & strRecordID & "'"
            strNamaKolej = oCommon.getFieldValue(strSQL)
            '1.1
            strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE RecordID='" & strRecordID & "'"
            strNegeri = oCommon.getFieldValue(strSQL)
            '2
            strSQL = "SELECT DISTINCT NamaKursus FROM kpmkv_kursus WHERE KursusID='" & strKursusID & "'"
            strNamaKursus = oCommon.getFieldValue(strSQL)
            '3
            strSQL = "SELECT DISTINCT kpmkv_kluster.NamaKluster FROM kpmkv_kursus_kolej LEFT OUTER JOIN kpmkv_kursus"
            strSQL += " ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN kpmkv_kluster"
            strSQL += " ON kpmkv_kursus.KlusterID = kpmkv_kluster.KlusterID"
            strSQL += " WHERE kpmkv_kursus.KursusID='" & strKursusID & "'"
            strSQL += " AND kpmkv_kursus_kolej.KolejRecordID='" & strRecordID & "'"
            strSQL += " GROUP BY kpmkv_kluster.NamaKluster,kpmkv_kluster.KlusterID"
            strNamaKluster = oCommon.getFieldValue(strSQL)

            '--Insert kpmkv_transkrip_svm
            strSQL = "INSERT INTO kpmkv_transkrip_svm(Nama,Mykad,AngkaGiliran,Sesi,IsBMTahun,NamaKolej,Negeri,NamaKursus,NamaKluster)VALUES('" & oCommon.FixSingleQuotes(strNama) & "','" & strMykad & "','" & strAngkaGiliran & "','" & chkSesi.Text & "','" & ddlTahun.Text & "','" & strNamaKolej & "','" & strNegeri & "','" & strNamaKursus & "','" & strNamaKluster & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
        Next

    End Sub
    Private Sub GETSEM1()
        'Sem1
        Dim strMykad As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQLDB)
        Dim dt As DataTable = GetData(cmd)
        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("Mykad")

            Dim IntPelajarIDSem1 As Integer = 0
            Dim strTahunSem1 As String = ""
            Dim strKursusID As Integer = 0

            strSQL = "SELECT PelajarID,Tahun,KursusID FROM kpmkv_pelajar WHERE Mykad='" & strMykad & "' AND Semester='1'"
            strSQL += " AND IsDeleted='N' AND StatusID='2'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Sem1 As Array
            ar_Sem1 = strRet.Split("|")
            IntPelajarIDSem1 = ar_Sem1(0)
            strTahunSem1 = ar_Sem1(1)
            strKursusID = ar_Sem1(2)

            'get jeniskursus
            strSQL = "SELECT JenisKursus FROM kpmkv_Kursus WHERE KursusID='" & strKursusID & "'"
            strJenisKursus = oCommon.getFieldValue(strSQL)

            '--BahasaMelayu
            Dim strKodBM1 As String = ""
            Dim strNamaBM1 As String = ""
            Dim strJKBM1 As String = ""
            Dim strGredBM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1'"
            strSQL += " AND NamaMataPelajaran='BAHASA MELAYU' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BM1 As Array
            ar_BM1 = strRet.Split("|")
            strKodBM1 = ar_BM1(0)
            strNamaBM1 = ar_BM1(1)
            strJKBM1 = ar_BM1(2)

            strSQL = "SELECT GredBM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD1SEM1='" & strKodBM1 & "', NAM1SEM1='" & strNamaBM1 & "',JK1SEM1='" & strJKBM1 & "',MOD1SEM1='" & strGredBM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--bi 
            Dim strKodBI1 As String = ""
            Dim strNamaBI1 As String = ""
            Dim strJKBI1 As String = ""
            Dim strGredBI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='BAHASA INGGERIS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BI1 As Array
            ar_BI1 = strRet.Split("|")
            strKodBI1 = ar_BI1(0)
            strNamaBI1 = ar_BI1(1)
            strJKBI1 = ar_BI1(2)

            strSQL = "SELECT GredBI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD2SEM1='" & strKodBI1 & "', NAM2SEM1='" & strNamaBI1 & "',JK2SEM1='" & strJKBI1 & "',MOD2SEM1='" & strGredBI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--MATHEMATIC
            Dim strKodMT1 As String = ""
            Dim strNamaMT1 As String = ""
            Dim strJKMT1 As String = ""
            Dim strGredMT1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' "
            strSQL += " AND NamaMataPelajaran='MATHEMATICS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_MT1 As Array
            ar_MT1 = strRet.Split("|")
            strKodMT1 = ar_MT1(0)
            strNamaMT1 = ar_MT1(1)
            strJKMT1 = ar_MT1(2)

            strSQL = "SELECT GredMT FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredMT1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD3SEM1='" & strKodMT1 & "', NAM3SEM1='" & strNamaMT1 & "',JK3SEM1='" & strJKMT1 & "',MOD3SEM1='" & strGredMT1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SCIENCE
            Dim strKodSC1 As String = ""
            Dim strNamaSC1 As String = ""
            Dim strJKSC1 As String = ""
            Dim strGredSC1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='SCIENCE' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SC1 As Array
            ar_SC1 = strRet.Split("|")
            strKodSC1 = ar_SC1(0)
            strNamaSC1 = ar_SC1(1)
            strJKSC1 = ar_SC1(2)

            strSQL = "SELECT GredSC FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSC1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD4SEM1='" & strKodSC1 & "', NAM4SEM1='" & strNamaSC1 & "',JK4SEM1='" & strJKSC1 & "',MOD4SEM1='" & strGredSC1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SEJARAH
            Dim strKodSJ1 As String = ""
            Dim strNamaSJ1 As String = ""
            Dim strJKSJ1 As String = ""
            Dim strGredSJ1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='SEJARAH' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SJ1 As Array
            ar_SJ1 = strRet.Split("|")
            strKodSJ1 = ar_SJ1(0)
            strNamaSJ1 = ar_SJ1(1)
            strJKSJ1 = ar_SJ1(2)

            strSQL = "SELECT GredSJ FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSJ1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD5SEM1='" & strKodSJ1 & "', NAM5SEM1='" & strNamaSJ1 & "',JK5SEM1='" & strJKSJ1 & "',MOD5SEM1='" & strGredSJ1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN ISLAM
            Dim strKodPI1 As String = ""
            Dim strNamaPI1 As String = ""
            Dim strJKPI1 As String = ""
            Dim strGredPI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='PENDIDIKAN ISLAM' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PI1 As Array
            ar_PI1 = strRet.Split("|")
            strKodPI1 = ar_PI1(0)
            strNamaPI1 = ar_PI1(1)
            strJKPI1 = ar_PI1(2)

            strSQL = "SELECT GredPI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD6SEM1='" & strKodPI1 & "', NAM6SEM1='" & strNamaPI1 & "',JK6SEM1='" & strJKPI1 & "',MOD6SEM1='" & strGredPI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN MORAL
            Dim strKodPM1 As String = ""
            Dim strNamaPM1 As String = ""
            Dim strJKPM1 As String = ""
            Dim strGredPM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='PENDIDIKAN MORAL' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PM1 As Array
            ar_PM1 = strRet.Split("|")
            strKodPM1 = ar_PM1(0)
            strNamaPM1 = ar_PM1(1)
            strJKPM1 = ar_PM1(2)

            strSQL = "SELECT GredPM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD7SEM1='" & strKodPM1 & "', NAM7SEM1='" & strNamaPM1 & "',JK7SEM1='" & strJKPM1 & "',MOD7SEM1='" & strGredPM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''----VOKASIONAL
            Dim strKodM1 As String = ""
            Dim strNamaM1 As String = ""
            Dim strJKM1 As String = ""
            Dim strGredM1 As String = ""
            ''Modul1
            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='1'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M1 As Array
            ar_M1 = strRet.Split("|")
            strKodM1 = ar_M1(0)
            strNamaM1 = ar_M1(1)
            strJKM1 = ar_M1(2)

            strSQL = "SELECT GredV1 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD8SEM1='" & strKodM1 & "', NAM8SEM1='" & strNamaM1 & "',JK8SEM1='" & strJKM1 & "',MOD8SEM1='" & strGredM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul2
            Dim strKodM2 As String = ""
            Dim strNamaM2 As String = ""
            Dim strJKM2 As String = ""
            Dim strGredM2 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='2'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M2 As Array
            ar_M2 = strRet.Split("|")
            strKodM2 = ar_M2(0)
            strNamaM2 = ar_M2(1)
            strJKM2 = ar_M2(2)

            strSQL = "SELECT GredV2 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM2 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD9SEM1='" & strKodM2 & "', NAM9SEM1='" & strNamaM2 & "',JK9SEM1='" & strJKM2 & "',MOD9SEM1='" & strGredM2 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul3
            Dim strKodM3 As String = ""
            Dim strNamaM3 As String = ""
            Dim strJKM3 As String = ""
            Dim strGredM3 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='3'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M3 As Array
            ar_M3 = strRet.Split("|")
            strKodM3 = ar_M3(0)
            strNamaM3 = ar_M3(1)
            strJKM3 = ar_M3(2)

            strSQL = "SELECT GredV3 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM3 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD10SEM1='" & strKodM3 & "', NAM10SEM1='" & strNamaM3 & "',JK10SEM1='" & strJKM3 & "',MOD10SEM1='" & strGredM3 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul4
            Dim strKodM4 As String = ""
            Dim strNamaM4 As String = ""
            Dim strJKM4 As String = ""
            Dim strGredM4 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='4'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M4 As Array
                ar_M4 = strRet.Split("|")
                strKodM4 = ar_M4(0)
                strNamaM4 = ar_M4(1)
                strJKM4 = ar_M4(2)

                strSQL = "SELECT GredV4 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM4 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD11SEM1='" & strKodM4 & "', NAM11SEM1='" & strNamaM4 & "',JK11SEM1='" & strJKM4 & "',MOD11SEM1='" & strGredM4 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
            ''Modul5
            Dim strKodM5 As String = ""
            Dim strNamaM5 As String = ""
            Dim strJKM5 As String = ""
            Dim strGredM5 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='5'"

            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M5 As Array
                ar_M5 = strRet.Split("|")
                strKodM5 = ar_M5(0)
                strNamaM5 = ar_M5(1)
                strJKM5 = ar_M5(2)

                strSQL = "SELECT GredV5 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM5 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD12SEM1='" & strKodM5 & "', NAM12SEM1='" & strNamaM5 & "',JK12SEM1='" & strJKM5 & "',MOD12SEM1='" & strGredM5 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
            ''Modul6
            Dim strKodM6 As String = ""
            Dim strNamaM6 As String = ""
            Dim strJKM6 As String = ""
            Dim strGredM6 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='6'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M6 As Array
                ar_M6 = strRet.Split("|")
                strKodM6 = ar_M6(0)
                strNamaM6 = ar_M6(1)
                strJKM6 = ar_M6(2)

                strSQL = "SELECT GredV6 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM6 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD13SEM1='" & strKodM6 & "', NAM13SEM1='" & strNamaM6 & "',JK13SEM1='" & strJKM6 & "',MOD13SEM1='" & strGredM6 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul7
            Dim strKodM7 As String = ""
            Dim strNamaM7 As String = ""
            Dim strJKM7 As String = ""
            Dim strGredM7 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='7'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M7 As Array
                ar_M7 = strRet.Split("|")
                strKodM7 = ar_M7(0)
                strNamaM7 = ar_M7(1)
                strJKM7 = ar_M7(2)

                strSQL = "SELECT GredV7 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM7 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD14SEM1='" & strKodM7 & "', NAM14SEM1='" & strNamaM7 & "',JK14SEM1='" & strJKM7 & "',MOD14SEM1='" & strGredM7 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul8
            Dim strKodM8 As String = ""
            Dim strNamaM8 As String = ""
            Dim strJKM8 As String = ""
            Dim strGredM8 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='8'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M8 As Array
                ar_M8 = strRet.Split("|")
                strKodM8 = ar_M8(0)
                strNamaM8 = ar_M8(1)
                strJKM8 = ar_M8(2)

                strSQL = "SELECT GredV8 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM8 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD15SEM1='" & strKodM8 & "', NAM15SEM1='" & strNamaM8 & "',JK15SEM1='" & strJKM8 & "',MOD15SEM1='" & strGredM8 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
        Next 'pelajar
    End Sub
    Private Sub GETSEM2()
        'Sem1
        Dim strMykad As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQLDB)
        Dim dt As DataTable = GetData(cmd)
        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("Mykad")

            Dim IntPelajarIDSem1 As Integer = 0
            Dim strTahunSem1 As String = ""
            Dim strKursusID As Integer = 0

            strSQL = "SELECT PelajarID,Tahun,KursusID FROM kpmkv_pelajar WHERE Mykad='" & strMykad & "' AND Semester='2'"
            strSQL += " AND IsDeleted='N' AND StatusID='2'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Sem1 As Array
            ar_Sem1 = strRet.Split("|")
            IntPelajarIDSem1 = ar_Sem1(0)
            strTahunSem1 = ar_Sem1(1)
            strKursusID = ar_Sem1(2)

            '--BahasaMelayu
            Dim strKodBM1 As String = ""
            Dim strNamaBM1 As String = ""
            Dim strJKBM1 As String = ""
            Dim strGredBM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='BAHASA MELAYU' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BM1 As Array
            ar_BM1 = strRet.Split("|")
            strKodBM1 = ar_BM1(0)
            strNamaBM1 = ar_BM1(1)
            strJKBM1 = ar_BM1(2)

            strSQL = "SELECT GredBM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD1SEM2='" & strKodBM1 & "', NAM1SEM2='" & strNamaBM1 & "',JK1SEM2='" & strJKBM1 & "',MOD1SEM2='" & strGredBM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--bi 
            Dim strKodBI1 As String = ""
            Dim strNamaBI1 As String = ""
            Dim strJKBI1 As String = ""
            Dim strGredBI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='BAHASA INGGERIS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BI1 As Array
            ar_BI1 = strRet.Split("|")
            strKodBI1 = ar_BI1(0)
            strNamaBI1 = ar_BI1(1)
            strJKBI1 = ar_BI1(2)

            strSQL = "SELECT GredBI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD2SEM2='" & strKodBI1 & "', NAM2SEM2='" & strNamaBI1 & "',JK2SEM2='" & strJKBI1 & "',MOD2SEM2='" & strGredBI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--MATHEMATIC
            Dim strKodMT1 As String = ""
            Dim strNamaMT1 As String = ""
            Dim strJKMT1 As String = ""
            Dim strGredMT1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='MATHEMATICS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_MT1 As Array
            ar_MT1 = strRet.Split("|")
            strKodMT1 = ar_MT1(0)
            strNamaMT1 = ar_MT1(1)
            strJKMT1 = ar_MT1(2)

            strSQL = "SELECT GredMT FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredMT1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD3SEM2='" & strKodMT1 & "', NAM3SEM2='" & strNamaMT1 & "',JK3SEM2='" & strJKMT1 & "',MOD3SEM2='" & strGredMT1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SCIENCE
            Dim strKodSC1 As String = ""
            Dim strNamaSC1 As String = ""
            Dim strJKSC1 As String = ""
            Dim strGredSC1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='SCIENCE' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SC1 As Array
            ar_SC1 = strRet.Split("|")
            strKodSC1 = ar_SC1(0)
            strNamaSC1 = ar_SC1(1)
            strJKSC1 = ar_SC1(2)

            strSQL = "SELECT GredSC FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSC1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD4SEM2='" & strKodSC1 & "', NAM4SEM2='" & strNamaSC1 & "',JK4SEM2='" & strJKSC1 & "',MOD4SEM2='" & strGredSC1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SEJARAH
            Dim strKodSJ1 As String = ""
            Dim strNamaSJ1 As String = ""
            Dim strJKSJ1 As String = ""
            Dim strGredSJ1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='SEJARAH' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SJ1 As Array
            ar_SJ1 = strRet.Split("|")
            strKodSJ1 = ar_SJ1(0)
            strNamaSJ1 = ar_SJ1(1)
            strJKSJ1 = ar_SJ1(2)

            strSQL = "SELECT GredSJ FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSJ1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD5SEM2='" & strKodSJ1 & "', NAM5SEM2='" & strNamaSJ1 & "',JK5SEM2='" & strJKSJ1 & "',MOD5SEM2='" & strGredSJ1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN ISLAM
            Dim strKodPI1 As String = ""
            Dim strNamaPI1 As String = ""
            Dim strJKPI1 As String = ""
            Dim strGredPI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='PENDIDIKAN ISLAM' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PI1 As Array
            ar_PI1 = strRet.Split("|")
            strKodPI1 = ar_PI1(0)
            strNamaPI1 = ar_PI1(1)
            strJKPI1 = ar_PI1(2)

            strSQL = "SELECT GredPI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD6SEM2='" & strKodPI1 & "', NAM6SEM2='" & strNamaPI1 & "',JK6SEM2='" & strJKPI1 & "',MOD6SEM2='" & strGredPI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN MORAL
            Dim strKodPM1 As String = ""
            Dim strNamaPM1 As String = ""
            Dim strJKPM1 As String = ""
            Dim strGredPM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='PENDIDIKAN MORAL' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PM1 As Array
            ar_PM1 = strRet.Split("|")
            strKodPM1 = ar_PM1(0)
            strNamaPM1 = ar_PM1(1)
            strJKPM1 = ar_PM1(2)

            strSQL = "SELECT GredPM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD7SEM2='" & strKodPM1 & "', NAM7SEM2='" & strNamaPM1 & "',JK7SEM2='" & strJKPM1 & "',MOD7SEM2='" & strGredPM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''----VOKASIONAL
            Dim strKodM1 As String = ""
            Dim strNamaM1 As String = ""
            Dim strJKM1 As String = ""
            Dim strGredM1 As String = ""
            ''Modul1
            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='1'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M1 As Array
            ar_M1 = strRet.Split("|")
            strKodM1 = ar_M1(0)
            strNamaM1 = ar_M1(1)
            strJKM1 = ar_M1(2)

            strSQL = "SELECT GredV1 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD8SEM2='" & strKodM1 & "', NAM8SEM2='" & strNamaM1 & "',JK8SEM2='" & strJKM1 & "',MOD8SEM2='" & strGredM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul2
            Dim strKodM2 As String = ""
            Dim strNamaM2 As String = ""
            Dim strJKM2 As String = ""
            Dim strGredM2 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='2'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M2 As Array
            ar_M2 = strRet.Split("|")
            strKodM2 = ar_M2(0)
            strNamaM2 = ar_M2(1)
            strJKM2 = ar_M2(2)

            strSQL = "SELECT GredV2 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM2 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD9SEM2='" & strKodM2 & "', NAM9SEM2='" & strNamaM2 & "',JK9SEM2='" & strJKM2 & "',MOD9SEM2='" & strGredM2 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul3
            Dim strKodM3 As String = ""
            Dim strNamaM3 As String = ""
            Dim strJKM3 As String = ""
            Dim strGredM3 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='3'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M3 As Array
                ar_M3 = strRet.Split("|")
                strKodM3 = ar_M3(0)
                strNamaM3 = ar_M3(1)
                strJKM3 = ar_M3(2)

                strSQL = "SELECT GredV3 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM3 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD10SEM2='" & strKodM3 & "', NAM10SEM2='" & strNamaM3 & "',JK10SEM2='" & strJKM3 & "',MOD10SEM2='" & strGredM3 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul4
            Dim strKodM4 As String = ""
            Dim strNamaM4 As String = ""
            Dim strJKM4 As String = ""
            Dim strGredM4 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='4'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M4 As Array
                ar_M4 = strRet.Split("|")
                strKodM4 = ar_M4(0)
                strNamaM4 = ar_M4(1)
                strJKM4 = ar_M4(2)

                strSQL = "SELECT GredV4 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM4 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD11SEM2='" & strKodM4 & "', NAM11SEM2='" & strNamaM4 & "',JK11SEM2='" & strJKM4 & "',MOD11SEM2='" & strGredM4 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul5
            Dim strKodM5 As String = ""
            Dim strNamaM5 As String = ""
            Dim strJKM5 As String = ""
            Dim strGredM5 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='5'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M5 As Array
                ar_M5 = strRet.Split("|")
                strKodM5 = ar_M5(0)
                strNamaM5 = ar_M5(1)
                strJKM5 = ar_M5(2)

                strSQL = "SELECT GredV5 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM5 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD12SEM2='" & strKodM5 & "', NAM12SEM2='" & strNamaM5 & "',JK12SEM2='" & strJKM5 & "',MOD12SEM2='" & strGredM5 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul6
            Dim strKodM6 As String = ""
            Dim strNamaM6 As String = ""
            Dim strJKM6 As String = ""
            Dim strGredM6 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='6'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M6 As Array
                ar_M6 = strRet.Split("|")
                strKodM6 = ar_M6(0)
                strNamaM6 = ar_M6(1)
                strJKM6 = ar_M6(2)

                strSQL = "SELECT GredV6 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM6 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD13SEM2='" & strKodM6 & "', NAM13SEM2='" & strNamaM6 & "',JK13SEM2='" & strJKM6 & "',MOD13SEM2='" & strGredM6 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul7
            Dim strKodM7 As String = ""
            Dim strNamaM7 As String = ""
            Dim strJKM7 As String = ""
            Dim strGredM7 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='7'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M7 As Array
                ar_M7 = strRet.Split("|")
                strKodM7 = ar_M7(0)
                strNamaM7 = ar_M7(1)
                strJKM7 = ar_M7(2)

                strSQL = "SELECT GredV7 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM7 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD14SEM2='" & strKodM7 & "', NAM14SEM2='" & strNamaM7 & "',JK14SEM2='" & strJKM7 & "',MOD14SEM2='" & strGredM7 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul8
            Dim strKodM8 As String = ""
            Dim strNamaM8 As String = ""
            Dim strJKM8 As String = ""
            Dim strGredM8 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='8'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M8 As Array
                ar_M8 = strRet.Split("|")
                strKodM8 = ar_M8(0)
                strNamaM8 = ar_M8(1)
                strJKM8 = ar_M8(2)

                strSQL = "SELECT GredV8 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM8 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD15SEM2='" & strKodM8 & "', NAM15SEM2='" & strNamaM8 & "',JK15SEM2='" & strJKM8 & "',MOD15SEM2='" & strGredM8 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
        Next 'pelajar
    End Sub
    Private Sub GETSEM3()
        'Sem1
        Dim strMykad As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQLDB)
        Dim dt As DataTable = GetData(cmd)
        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("Mykad")

            Dim IntPelajarIDSem1 As Integer = 0
            Dim strTahunSem1 As String = ""
            Dim strKursusID As Integer = 0


            strSQL = "SELECT PelajarID,Tahun,KursusID FROM kpmkv_pelajar WHERE Mykad='" & strMykad & "' AND Semester='3'"
            strSQL += " AND IsDeleted='N' AND StatusID='2'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Sem1 As Array
            ar_Sem1 = strRet.Split("|")
            IntPelajarIDSem1 = ar_Sem1(0)
            strTahunSem1 = ar_Sem1(1)
            strKursusID = ar_Sem1(2)

            'get jeniskursus
            strSQL = "SELECT JenisKursus FROM kpmkv_Kursus WHERE KursusID='" & strKursusID & "'"
            strJenisKursus = oCommon.getFieldValue(strSQL)

            '--BahasaMelayu
            Dim strKodBM1 As String = ""
            Dim strNamaBM1 As String = ""
            Dim strJKBM1 As String = ""
            Dim strGredBM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='BAHASA MELAYU' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BM1 As Array
            ar_BM1 = strRet.Split("|")
            strKodBM1 = ar_BM1(0)
            strNamaBM1 = ar_BM1(1)
            strJKBM1 = ar_BM1(2)

            strSQL = "SELECT GredBM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD1SEM3='" & strKodBM1 & "', NAM1SEM3='" & strNamaBM1 & "',JK1SEM3='" & strJKBM1 & "',MOD1SEM3='" & strGredBM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--bi 
            Dim strKodBI1 As String = ""
            Dim strNamaBI1 As String = ""
            Dim strJKBI1 As String = ""
            Dim strGredBI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='BAHASA INGGERIS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BI1 As Array
            ar_BI1 = strRet.Split("|")
            strKodBI1 = ar_BI1(0)
            strNamaBI1 = ar_BI1(1)
            strJKBI1 = ar_BI1(2)

            strSQL = "SELECT GredBI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD2SEM3='" & strKodBI1 & "', NAM2SEM3='" & strNamaBI1 & "',JK2SEM3='" & strJKBI1 & "',MOD2SEM3='" & strGredBI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--MATHEMATIC
            Dim strKodMT1 As String = ""
            Dim strNamaMT1 As String = ""
            Dim strJKMT1 As String = ""
            Dim strGredMT1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='MATHEMATICS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_MT1 As Array
            ar_MT1 = strRet.Split("|")
            strKodMT1 = ar_MT1(0)
            strNamaMT1 = ar_MT1(1)
            strJKMT1 = ar_MT1(2)

            strSQL = "SELECT GredMT FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredMT1 = oCommon.getFieldValue(strSQL)

            'MT for teknologi or social  
            strSQL = "SELECT  JenisKursus From kpmkv_kursus Where KursusID='" & strKursusID & "'"
            Dim strJenisKursusMAT As String = oCommon.getFieldValue(strSQL)

            'Social
            Dim strNamaMAT As String
            Dim strKodMAT As String = ""
            If strJenisKursusMAT = "SOCIAL" Then
                strNamaMAT = "MATHEMATICS FOR SOCIAL"
                strKodMAT = "AMT3101"
            Else
                strNamaMAT = "MATHEMATICS FOR TECHNOLOGY"
                strKodMAT = "AMT3091"
            End If

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD3SEM3='" & strKodMAT & "', NAM3SEM3='" & strNamaMAT & "',JK3SEM3='" & strJKMT1 & "',MOD3SEM3='" & strGredMT1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SCIENCE
            Dim strKodSC1 As String = ""
            Dim strNamaSC1 As String = ""
            Dim strJKSC1 As String = ""
            Dim strGredSC1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='SCIENCE' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SC1 As Array
            ar_SC1 = strRet.Split("|")
            strKodSC1 = ar_SC1(0)
            strNamaSC1 = ar_SC1(1)
            strJKSC1 = ar_SC1(2)

            strSQL = "SELECT GredSC FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSC1 = oCommon.getFieldValue(strSQL)

            'Sc for teknologi or social  
            strSQL = "SELECT  JenisKursus From kpmkv_kursus Where KursusID='" & strKursusID & "'"
            Dim strJenisKursusMT As String = oCommon.getFieldValue(strSQL)

            'Social
            Dim strNamaMT As String
            Dim strKodMT As String = ""
            If strJenisKursusMT = "SOCIAL" Then
                strNamaMT = "SCIENCE FOR SOCIAL"
                strKodMT = "AMT3131"
            Else
                strNamaMT = "SCIENCE FOR TECHNOLOGY"
                strKodMT = "AMT3121"
            End If

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD4SEM3='" & strKodMT & "', NAM4SEM3='" & strNamaMT & "',JK4SEM3='" & strJKSC1 & "',MOD4SEM3='" & strGredSC1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SEJARAH
            Dim strKodSJ1 As String = ""
            Dim strNamaSJ1 As String = ""
            Dim strJKSJ1 As String = ""
            Dim strGredSJ1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='SEJARAH' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SJ1 As Array
            ar_SJ1 = strRet.Split("|")
            strKodSJ1 = ar_SJ1(0)
            strNamaSJ1 = ar_SJ1(1)
            strJKSJ1 = ar_SJ1(2)

            strSQL = "SELECT GredSJ FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSJ1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD5SEM3='" & strKodSJ1 & "', NAM5SEM3='" & strNamaSJ1 & "',JK5SEM3='" & strJKSJ1 & "',MOD5SEM3='" & strGredSJ1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN ISLAM
            Dim strKodPI1 As String = ""
            Dim strNamaPI1 As String = ""
            Dim strJKPI1 As String = ""
            Dim strGredPI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='PENDIDIKAN ISLAM' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PI1 As Array
            ar_PI1 = strRet.Split("|")
            strKodPI1 = ar_PI1(0)
            strNamaPI1 = ar_PI1(1)
            strJKPI1 = ar_PI1(2)

            strSQL = "SELECT GredPI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD6SEM3='" & strKodPI1 & "', NAM6SEM3='" & strNamaPI1 & "',JK6SEM3='" & strJKPI1 & "',MOD6SEM3='" & strGredPI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN MORAL
            Dim strKodPM1 As String = ""
            Dim strNamaPM1 As String = ""
            Dim strJKPM1 As String = ""
            Dim strGredPM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='PENDIDIKAN MORAL' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PM1 As Array
            ar_PM1 = strRet.Split("|")
            strKodPM1 = ar_PM1(0)
            strNamaPM1 = ar_PM1(1)
            strJKPM1 = ar_PM1(2)

            strSQL = "SELECT GredPM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD7SEM3='" & strKodPM1 & "', NAM7SEM3='" & strNamaPM1 & "',JK7SEM3='" & strJKPM1 & "',MOD7SEM3='" & strGredPM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''----VOKASIONAL
            Dim strKodM1 As String = ""
            Dim strNamaM1 As String = ""
            Dim strJKM1 As String = ""
            Dim strGredM1 As String = ""
            ''Modul1
            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='1'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M1 As Array
                ar_M1 = strRet.Split("|")
                strKodM1 = ar_M1(0)
                strNamaM1 = ar_M1(1)
                strJKM1 = ar_M1(2)

                strSQL = "SELECT GredV1 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM1 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD8SEM3='" & strKodM1 & "', NAM8SEM3='" & strNamaM1 & "',JK8SEM3='" & strJKM1 & "',MOD8SEM3='" & strGredM1 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul2
            Dim strKodM2 As String = ""
            Dim strNamaM2 As String = ""
            Dim strJKM2 As String = ""
            Dim strGredM2 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='2'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M2 As Array
                ar_M2 = strRet.Split("|")
                strKodM2 = ar_M2(0)
                strNamaM2 = ar_M2(1)
                strJKM2 = ar_M2(2)

                strSQL = "SELECT GredV2 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM2 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD9SEM3='" & strKodM2 & "', NAM9SEM3='" & strNamaM2 & "',JK9SEM3='" & strJKM2 & "',MOD9SEM3='" & strGredM2 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul3
            Dim strKodM3 As String = ""
            Dim strNamaM3 As String = ""
            Dim strJKM3 As String = ""
            Dim strGredM3 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='3'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M3 As Array
                ar_M3 = strRet.Split("|")
                strKodM3 = ar_M3(0)
                strNamaM3 = ar_M3(1)
                strJKM3 = ar_M3(2)

                strSQL = "SELECT GredV3 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM3 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD10SEM3='" & strKodM3 & "', NAM10SEM3='" & strNamaM3 & "',JK10SEM3='" & strJKM3 & "',MOD10SEM3='" & strGredM3 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul4
            Dim strKodM4 As String = ""
            Dim strNamaM4 As String = ""
            Dim strJKM4 As String = ""
            Dim strGredM4 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='4'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M4 As Array
                ar_M4 = strRet.Split("|")
                strKodM4 = ar_M4(0)
                strNamaM4 = ar_M4(1)
                strJKM4 = ar_M4(2)

                strSQL = "SELECT GredV4 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM4 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD11SEM3='" & strKodM4 & "', NAM11SEM3='" & strNamaM4 & "',JK11SEM3='" & strJKM4 & "',MOD11SEM3='" & strGredM4 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul5
            Dim strKodM5 As String = ""
            Dim strNamaM5 As String = ""
            Dim strJKM5 As String = ""
            Dim strGredM5 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='5'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M5 As Array
                ar_M5 = strRet.Split("|")
                strKodM5 = ar_M5(0)
                strNamaM5 = ar_M5(1)
                strJKM5 = ar_M5(2)

                strSQL = "SELECT GredV5 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM5 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD12SEM3='" & strKodM5 & "', NAM12SEM3='" & strNamaM5 & "',JK12SEM3='" & strJKM5 & "',MOD12SEM3='" & strGredM5 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul6
            Dim strKodM6 As String = ""
            Dim strNamaM6 As String = ""
            Dim strJKM6 As String = ""
            Dim strGredM6 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='6'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M6 As Array
                ar_M6 = strRet.Split("|")
                strKodM6 = ar_M6(0)
                strNamaM6 = ar_M6(1)
                strJKM6 = ar_M6(2)

                strSQL = "SELECT GredV6 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM6 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD13SEM3='" & strKodM6 & "', NAM13SEM3='" & strNamaM6 & "',JK13SEM3='" & strJKM6 & "',MOD13SEM3='" & strGredM6 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul7
            Dim strKodM7 As String = ""
            Dim strNamaM7 As String = ""
            Dim strJKM7 As String = ""
            Dim strGredM7 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='7'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M7 As Array
                ar_M7 = strRet.Split("|")
                strKodM7 = ar_M7(0)
                strNamaM7 = ar_M7(1)
                strJKM7 = ar_M7(2)

                strSQL = "SELECT GredV7 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM7 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD14SEM3='" & strKodM7 & "', NAM14SEM3='" & strNamaM7 & "',JK14SEM3='" & strJKM7 & "',MOD14SEM3='" & strGredM7 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul8
            Dim strKodM8 As String = ""
            Dim strNamaM8 As String = ""
            Dim strJKM8 As String = ""
            Dim strGredM8 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='8'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M8 As Array
                ar_M8 = strRet.Split("|")
                strKodM8 = ar_M8(0)
                strNamaM8 = ar_M8(1)
                strJKM8 = ar_M8(2)

                strSQL = "SELECT GredV8 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM8 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD15SEM3='" & strKodM8 & "', NAM15SEM3='" & strNamaM8 & "',JK15SEM3='" & strJKM8 & "',MOD15SEM3='" & strGredM8 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
        Next 'pelajar
    End Sub
    Private Sub GETSEM4()
        'Sem1
        Dim strMykad As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQLDB)
        Dim dt As DataTable = GetData(cmd)
        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("Mykad")

            Dim IntPelajarIDSem1 As Integer = 0
            Dim strTahunSem1 As String = ""
            Dim strKursusID As Integer = 0

            strSQL = "SELECT PelajarID,Tahun,KursusID FROM kpmkv_pelajar WHERE Mykad='" & strMykad & "' AND Semester='4'"
            strSQL += " AND IsDeleted='N' AND StatusID='2'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Sem1 As Array
            ar_Sem1 = strRet.Split("|")
            IntPelajarIDSem1 = ar_Sem1(0)
            strTahunSem1 = ar_Sem1(1)
            strKursusID = ar_Sem1(2)

            '--BahasaMelayu
            Dim strKodBM1 As String = ""
            Dim strNamaBM1 As String = ""
            Dim strJKBM1 As String = ""
            Dim strGredBM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='BAHASA MELAYU' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BM1 As Array
            ar_BM1 = strRet.Split("|")
            strKodBM1 = ar_BM1(0)
            strNamaBM1 = ar_BM1(1)
            strJKBM1 = ar_BM1(2)

            strSQL = "SELECT GredBM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD1SEM4='" & strKodBM1 & "', NAM1SEM4='" & strNamaBM1 & "',JK1SEM4='" & strJKBM1 & "',MOD1SEM4='" & strGredBM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--bi 
            Dim strKodBI1 As String = ""
            Dim strNamaBI1 As String = ""
            Dim strJKBI1 As String = ""
            Dim strGredBI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='BAHASA INGGERIS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BI1 As Array
            ar_BI1 = strRet.Split("|")
            strKodBI1 = ar_BI1(0)
            strNamaBI1 = ar_BI1(1)
            strJKBI1 = ar_BI1(2)

            strSQL = "SELECT GredBI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD2SEM4='" & strKodBI1 & "', NAM2SEM4='" & strNamaBI1 & "',JK2SEM4='" & strJKBI1 & "',MOD2SEM4='" & strGredBI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--MATHEMATIC
            Dim strKodMT1 As String = ""
            Dim strNamaMT1 As String = ""
            Dim strJKMT1 As String = ""
            Dim strGredMT1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='MATHEMATICS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_MT1 As Array
            ar_MT1 = strRet.Split("|")
            strKodMT1 = ar_MT1(0)
            strNamaMT1 = ar_MT1(1)
            strJKMT1 = ar_MT1(2)

            strSQL = "SELECT GredMT FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredMT1 = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT  JenisKursus From kpmkv_kursus Where KursusID='" & strKursusID & "'"
            Dim strJenisKursusMAT As String = oCommon.getFieldValue(strSQL)

            Dim strNamaMAT As String
            Dim strKodMAT As String
            If strJenisKursusMAT = "SOCIAL" Then
                strNamaMAT = "MATHEMATICS FOR SOCIAL"
                strKodMAT = "AMT4101"
            Else
                strNamaMAT = "MATHEMATICS FOR TECHNOLOGY"
                strKodMAT = "AMT4091"
            End If

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD3SEM4='" & strKodMAT & "', NAM3SEM4='" & strNamaMAT & "',JK3SEM4='" & strJKMT1 & "',MOD3SEM4='" & strGredMT1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SCIENCE
            Dim strKodSC1 As String = ""
            Dim strNamaSC1 As String = ""
            Dim strJKSC1 As String = ""
            Dim strGredSC1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='SCIENCE' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SC1 As Array
            ar_SC1 = strRet.Split("|")
            strKodSC1 = ar_SC1(0)
            strNamaSC1 = ar_SC1(1)
            strJKSC1 = ar_SC1(2)

            strSQL = "SELECT GredSC FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSC1 = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT  JenisKursus From kpmkv_kursus Where KursusID='" & strKursusID & "'"
            Dim strJenisKursusMT As String = oCommon.getFieldValue(strSQL)

            Dim strNamaMT As String
            Dim strKodMT As String

            If strJenisKursusMT = "SOCIAL" Then
                strNamaMT = "SCIENCE FOR SOCIAL"
                strKodMT = "AMT4131"
            Else
                strNamaMT = "SCIENCE FOR TECHNOLOGY"
                strKodMT = "AMT4121"
            End If

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD4SEM4='" & strKodMT & "', NAM4SEM4='" & strNamaMT & "',JK4SEM4='" & strJKSC1 & "',MOD4SEM4='" & strGredSC1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SEJARAH
            Dim strKodSJ1 As String = ""
            Dim strNamaSJ1 As String = ""
            Dim strJKSJ1 As String = ""
            Dim strGredSJ1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='SEJARAH' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SJ1 As Array
            ar_SJ1 = strRet.Split("|")
            strKodSJ1 = ar_SJ1(0)
            strNamaSJ1 = ar_SJ1(1)
            strJKSJ1 = ar_SJ1(2)

            strSQL = "SELECT GredSJ FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSJ1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD5SEM4='" & strKodSJ1 & "', NAM5SEM4='" & strNamaSJ1 & "',JK5SEM4='" & strJKSJ1 & "',MOD5SEM4='" & strGredSJ1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN ISLAM
            Dim strKodPI1 As String = ""
            Dim strNamaPI1 As String = ""
            Dim strJKPI1 As String = ""
            Dim strGredPI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='PENDIDIKAN ISLAM' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PI1 As Array
            ar_PI1 = strRet.Split("|")
            strKodPI1 = ar_PI1(0)
            strNamaPI1 = ar_PI1(1)
            strJKPI1 = ar_PI1(2)

            strSQL = "SELECT GredPI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD6SEM4='" & strKodPI1 & "', NAM6SEM4='" & strNamaPI1 & "',JK6SEM4='" & strJKPI1 & "',MOD6SEM4='" & strGredPI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN MORAL
            Dim strKodPM1 As String = ""
            Dim strNamaPM1 As String = ""
            Dim strJKPM1 As String = ""
            Dim strGredPM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='PENDIDIKAN MORAL' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PM1 As Array
            ar_PM1 = strRet.Split("|")
            strKodPM1 = ar_PM1(0)
            strNamaPM1 = ar_PM1(1)
            strJKPM1 = ar_PM1(2)

            strSQL = "SELECT GredPM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD7SEM4='" & strKodPM1 & "', NAM7SEM4='" & strNamaPM1 & "',JK7SEM4='" & strJKPM1 & "',MOD7SEM4='" & strGredPM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''----VOKASIONAL
            Dim strKodM1 As String = ""
            Dim strNamaM1 As String = ""
            Dim strJKM1 As String = ""
            Dim strGredM1 As String = ""
            ''Modul1
            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='1'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M1 As Array
            ar_M1 = strRet.Split("|")
            strKodM1 = ar_M1(0)
            strNamaM1 = ar_M1(1)
            strJKM1 = ar_M1(2)

            strSQL = "SELECT GredV1 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD8SEM4='" & strKodM1 & "', NAM8SEM4='" & strNamaM1 & "',JK8SEM4='" & strJKM1 & "',MOD8SEM4='" & strGredM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul2
            Dim strKodM2 As String = ""
            Dim strNamaM2 As String = ""
            Dim strJKM2 As String = ""
            Dim strGredM2 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='2'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M2 As Array
            ar_M2 = strRet.Split("|")
            strKodM2 = ar_M2(0)
            strNamaM2 = ar_M2(1)
            strJKM2 = ar_M2(2)

            strSQL = "SELECT GredV2 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM2 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD9SEM4='" & strKodM2 & "', NAM9SEM4='" & strNamaM2 & "',JK9SEM4='" & strJKM2 & "',MOD9SEM4='" & strGredM2 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul3
            Dim strKodM3 As String = ""
            Dim strNamaM3 As String = ""
            Dim strJKM3 As String = ""
            Dim strGredM3 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='3'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M3 As Array
                ar_M3 = strRet.Split("|")
                strKodM3 = ar_M3(0)
                strNamaM3 = ar_M3(1)
                strJKM3 = ar_M3(2)

                strSQL = "SELECT GredV3 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM3 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD10SEM4='" & strKodM3 & "', NAM10SEM4='" & strNamaM3 & "',JK10SEM4='" & strJKM3 & "',MOD10SEM4='" & strGredM3 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul4
            Dim strKodM4 As String = ""
            Dim strNamaM4 As String = ""
            Dim strJKM4 As String = ""
            Dim strGredM4 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='4'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M4 As Array
                ar_M4 = strRet.Split("|")
                strKodM4 = ar_M4(0)
                strNamaM4 = ar_M4(1)
                strJKM4 = ar_M4(2)

                strSQL = "SELECT GredV4 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM4 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD11SEM4='" & strKodM4 & "', NAM11SEM4='" & strNamaM4 & "',JK11SEM4='" & strJKM4 & "',MOD11SEM4='" & strGredM4 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul5
            Dim strKodM5 As String = ""
            Dim strNamaM5 As String = ""
            Dim strJKM5 As String = ""
            Dim strGredM5 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='5'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M5 As Array
                ar_M5 = strRet.Split("|")
                strKodM5 = ar_M5(0)
                strNamaM5 = ar_M5(1)
                strJKM5 = ar_M5(2)

                strSQL = "SELECT GredV5 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM5 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD12SEM4='" & strKodM5 & "', NAM12SEM4='" & strNamaM5 & "',JK12SEM4='" & strJKM5 & "',MOD12SEM4='" & strGredM5 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul6
            Dim strKodM6 As String = ""
            Dim strNamaM6 As String = ""
            Dim strJKM6 As String = ""
            Dim strGredM6 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='6'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M6 As Array
                ar_M6 = strRet.Split("|")
                strKodM6 = ar_M6(0)
                strNamaM6 = ar_M6(1)
                strJKM6 = ar_M6(2)

                strSQL = "SELECT GredV6 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM6 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD13SEM4='" & strKodM6 & "', NAM13SEM4='" & strNamaM6 & "',JK13SEM4='" & strJKM6 & "',MOD13SEM4='" & strGredM6 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul7
            Dim strKodM7 As String = ""
            Dim strNamaM7 As String = ""
            Dim strJKM7 As String = ""
            Dim strGredM7 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='7'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M7 As Array
                ar_M7 = strRet.Split("|")
                strKodM7 = ar_M7(0)
                strNamaM7 = ar_M7(1)
                strJKM7 = ar_M7(2)

                strSQL = "SELECT GredV7 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM7 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD14SEM4='" & strKodM7 & "', NAM14SEM4='" & strNamaM7 & "',JK14SEM4='" & strJKM7 & "',MOD14SEM4='" & strGredM7 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul8
            Dim strKodM8 As String = ""
            Dim strNamaM8 As String = ""
            Dim strJKM8 As String = ""
            Dim strGredM8 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='8'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M8 As Array
                ar_M8 = strRet.Split("|")
                strKodM8 = ar_M8(0)
                strNamaM8 = ar_M8(1)
                strJKM8 = ar_M8(2)

                strSQL = "SELECT GredV8 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM8 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD15SEM4='" & strKodM8 & "', NAM15SEM4='" & strNamaM8 & "',JK15SEM4='" & strJKM8 & "',MOD15SEM4='" & strGredM8 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            'last
            Dim strGredBMSetara As String = ""
            Dim IntPNGKA As String = ""
            Dim IntPNGKV As String = ""
            Dim IntPNGK As String = ""
            Dim IntJum_JamKredit_Akademik As String = ""
            Dim IntJum_JamKredit_Vokasional As String = ""

            strSQL = " SELECT GredBMSetara,PNGKA,PNGKV,PNGKK,Jum_JamKredit_Akademik,Jum_JamKredit_Vokasional FROM kpmkv_pelajar_markah WHERE Semester='4' AND PelajarID='" & IntPelajarIDSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_G9 As Array
                ar_G9 = strRet.Split("|")
                strGredBMSetara = ar_G9(0)
                IntPNGKA = ar_G9(1)
                IntPNGKV = ar_G9(2)
                IntPNGK = ar_G9(3)
                IntJum_JamKredit_Akademik = ar_G9(4)
                IntJum_JamKredit_Vokasional = ar_G9(5)

                strSQL = "UPDATE kpmkv_transkrip_svm SET BMSetara='" & strGredBMSetara & "', PNGK_AKA='" & IntPNGKA & "',PNGK_VOK='" & IntPNGKV & "',PNGK='" & IntPNGK & "', JUMLAH_AKA='" & IntJum_JamKredit_Akademik & "',JUMLAH_VOK='" & IntJum_JamKredit_Vokasional & "'  WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
        Next 'pelajar
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
    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_kolej_list()
        ddlKolej.Text = "0"

    End Sub
    Private Function getSQLEXCEL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY NamaKolej,NamaKluster,NamaKursus ASC"

        '--not deleted
        tmpSQL = "SELECT s.* FROM kpmkv_transkrip_svm as s"
        tmpSQL += " LEFT JOIN kpmkv_kolej as k ON s.NamaKolej =k.Nama "
        tmpSQL += " WHERE s.ISBMTahun='" & ddlTahun.SelectedValue & "'"
        tmpSQL += " AND k.recordID='" & ddlKolej.SelectedValue & "'"


        getSQLEXCEL = tmpSQL & strOrder

        Return getSQLEXCEL

    End Function
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=FileExportDataTranskrip.csv")
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

    Protected Sub btnKompeten_Click(sender As Object, e As EventArgs) Handles btnKompeten.Click
        lblMsg.Text = ""

        strValue = "Kompeten"

        strSQL = "SELECT * FROM kpmkv_transkrip_svm WHERE IsBMTahun='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        If oCommon.isExist(strSQL) = True Then

            strSQL = "DELETE FROM kpmkv_transkrip_svm WHERE IsBMTahun='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If
        getStep1()
        GETSEM1()
        GETSEM2()
        GETSEM3()
        GETSEM4()
        ExportToCSV(getSQLEXCEL)
    End Sub

    Private Sub btnXKompeten_Click(sender As Object, e As EventArgs) Handles btnXKompeten.Click
        lblMsg.Text = ""

        strValue = "NotKompeten"

        strSQL = "SELECT * FROM kpmkv_transkrip_svm WHERE IsBMTahun='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
        If oCommon.isExist(strSQL) = True Then

            strSQL = "DELETE FROM kpmkv_transkrip_svm WHERE IsBMTahun='" & ddlTahun.Text & "' AND Sesi ='" & chkSesi.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        getStep1()
        GETSEM1()
        GETSEM2()
        GETSEM3()
        GETSEM4()
        ExportToCSV(getSQLEXCEL)

        'getStep1X()
        'GETSEM1X()
        'GETSEM2X()
        'GETSEM3X()
        'GETSEM4X()
        'ExportToCSV(getSQLEXCEL)
    End Sub



    Private Sub getStep1X()
        Dim strNama As String = ""
        Dim strMykad As String = ""
        Dim strAngkaGiliran As String = ""
        Dim strTahun As String = ""
        Dim strKursusID As Integer = 0
        Dim strNamaKolej As String = ""
        Dim strRecordID As String = ""
        Dim strNegeri As String = ""
        Dim strNamaKursus As String = ""
        Dim strNamaKluster As String = ""
        Dim strSesi As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQLX)
        Dim dt As DataTable = GetData(cmd)
        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("MyKad")

            strSQL = "SELECT DISTINCT Nama,AngkaGiliran,KursusID,KolejRecordID FROM kpmkv_pelajar WHERE MyKad='" & strMykad & "'"
            strSQL += " AND IsBMTahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.Text & "' AND Semester='4'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Detail1 As Array
            ar_Detail1 = strRet.Split("|")
            strNama = ar_Detail1(0)
            strAngkaGiliran = ar_Detail1(1)
            strKursusID = ar_Detail1(2)
            strRecordID = ar_Detail1(3)

            '1
            strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & strRecordID & "'"
            strNamaKolej = oCommon.getFieldValue(strSQL)
            '1.1
            strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE RecordID='" & strRecordID & "'"
            strNegeri = oCommon.getFieldValue(strSQL)
            '2
            strSQL = "SELECT DISTINCT NamaKursus FROM kpmkv_kursus WHERE KursusID='" & strKursusID & "'"
            strNamaKursus = oCommon.getFieldValue(strSQL)
            '3
            strSQL = "SELECT DISTINCT kpmkv_kluster.NamaKluster FROM kpmkv_kursus_kolej LEFT OUTER JOIN kpmkv_kursus"
            strSQL += " ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN kpmkv_kluster"
            strSQL += " ON kpmkv_kursus.KlusterID = kpmkv_kluster.KlusterID"
            strSQL += " WHERE kpmkv_kursus.KursusID='" & strKursusID & "'"
            strSQL += " AND kpmkv_kursus_kolej.KolejRecordID='" & strRecordID & "'"
            strSQL += " GROUP BY kpmkv_kluster.NamaKluster,kpmkv_kluster.KlusterID"
            strNamaKluster = oCommon.getFieldValue(strSQL)

            '--Insert kpmkv_transkrip_svm
            strSQL = "INSERT INTO kpmkv_transkrip_svm(Nama,Mykad,AngkaGiliran,Sesi,IsBMTahun,NamaKolej,Negeri,NamaKursus,NamaKluster)VALUES('" & oCommon.FixSingleQuotes(strNama) & "','" & strMykad & "','" & strAngkaGiliran & "','" & chkSesi.Text & "','" & ddlTahun.Text & "','" & strNamaKolej & "','" & strNegeri & "','" & strNamaKursus & "','" & strNamaKluster & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
        Next
    End Sub
    Private Sub GETSEM1X()
        'Sem1
        Dim strMykad As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQLDB)
        Dim dt As DataTable = GetData(cmd)
        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("Mykad")

            Dim IntPelajarIDSem1 As Integer = 0
            Dim strTahunSem1 As String = ""
            Dim strKursusID As Integer = 0

            strSQL = "SELECT PelajarID,Tahun,KursusID FROM kpmkv_pelajar WHERE Mykad='" & strMykad & "' AND Semester='1'"
            strSQL += " AND IsDeleted='N' AND StatusID='2'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Sem1 As Array
            ar_Sem1 = strRet.Split("|")
            IntPelajarIDSem1 = ar_Sem1(0)
            strTahunSem1 = ar_Sem1(1)
            strKursusID = ar_Sem1(2)

            '--BahasaMelayu
            Dim strKodBM1 As String = ""
            Dim strNamaBM1 As String = ""
            Dim strJKBM1 As String = ""
            Dim strGredBM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='BAHASA MELAYU' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BM1 As Array
            ar_BM1 = strRet.Split("|")
            strKodBM1 = ar_BM1(0)
            strNamaBM1 = ar_BM1(1)
            strJKBM1 = ar_BM1(2)

            strSQL = "SELECT GredBM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD1SEM1='" & strKodBM1 & "', NAM1SEM1='" & strNamaBM1 & "',JK1SEM1='" & strJKBM1 & "',MOD1SEM1='" & strGredBM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--bi 
            Dim strKodBI1 As String = ""
            Dim strNamaBI1 As String = ""
            Dim strJKBI1 As String = ""
            Dim strGredBI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='BAHASA INGGERIS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BI1 As Array
            ar_BI1 = strRet.Split("|")
            strKodBI1 = ar_BI1(0)
            strNamaBI1 = ar_BI1(1)
            strJKBI1 = ar_BI1(2)

            strSQL = "SELECT GredBI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD2SEM1='" & strKodBI1 & "', NAM2SEM1='" & strNamaBI1 & "',JK2SEM1='" & strJKBI1 & "',MOD2SEM1='" & strGredBI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--MATHEMATIC
            Dim strKodMT1 As String = ""
            Dim strNamaMT1 As String = ""
            Dim strJKMT1 As String = ""
            Dim strGredMT1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='MATHEMATICS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_MT1 As Array
            ar_MT1 = strRet.Split("|")
            strKodMT1 = ar_MT1(0)
            strNamaMT1 = ar_MT1(1)
            strJKMT1 = ar_MT1(2)

            strSQL = "SELECT GredMT FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredMT1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD3SEM1='" & strKodMT1 & "', NAM3SEM1='" & strNamaMT1 & "',JK3SEM1='" & strJKMT1 & "',MOD3SEM1='" & strGredMT1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SCIENCE
            Dim strKodSC1 As String = ""
            Dim strNamaSC1 As String = ""
            Dim strJKSC1 As String = ""
            Dim strGredSC1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='SCIENCE' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SC1 As Array
            ar_SC1 = strRet.Split("|")
            strKodSC1 = ar_SC1(0)
            strNamaSC1 = ar_SC1(1)
            strJKSC1 = ar_SC1(2)

            strSQL = "SELECT GredSC FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSC1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD4SEM1='" & strKodSC1 & "', NAM4SEM1='" & strNamaSC1 & "',JK4SEM1='" & strJKSC1 & "',MOD4SEM1='" & strGredSC1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SEJARAH
            Dim strKodSJ1 As String = ""
            Dim strNamaSJ1 As String = ""
            Dim strJKSJ1 As String = ""
            Dim strGredSJ1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='SEJARAH' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SJ1 As Array
            ar_SJ1 = strRet.Split("|")
            strKodSJ1 = ar_SJ1(0)
            strNamaSJ1 = ar_SJ1(1)
            strJKSJ1 = ar_SJ1(2)

            strSQL = "SELECT GredSJ FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSJ1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD5SEM1='" & strKodSJ1 & "', NAM5SEM1='" & strNamaSJ1 & "',JK5SEM1='" & strJKSJ1 & "',MOD5SEM1='" & strGredSJ1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN ISLAM
            Dim strKodPI1 As String = ""
            Dim strNamaPI1 As String = ""
            Dim strJKPI1 As String = ""
            Dim strGredPI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='PENDIDIKAN ISLAM' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PI1 As Array
            ar_PI1 = strRet.Split("|")
            strKodPI1 = ar_PI1(0)
            strNamaPI1 = ar_PI1(1)
            strJKPI1 = ar_PI1(2)

            strSQL = "SELECT GredPI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD6SEM1='" & strKodPI1 & "', NAM6SEM1='" & strNamaPI1 & "',JK6SEM1='" & strJKPI1 & "',MOD6SEM1='" & strGredPI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN MORAL
            Dim strKodPM1 As String = ""
            Dim strNamaPM1 As String = ""
            Dim strJKPM1 As String = ""
            Dim strGredPM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='1' AND NamaMataPelajaran='PENDIDIKAN MORAL' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PM1 As Array
            ar_PM1 = strRet.Split("|")
            strKodPM1 = ar_PM1(0)
            strNamaPM1 = ar_PM1(1)
            strJKPM1 = ar_PM1(2)

            strSQL = "SELECT GredPM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD7SEM1='" & strKodPM1 & "', NAM7SEM1='" & strNamaPM1 & "',JK7SEM1='" & strJKPM1 & "',MOD7SEM1='" & strGredPM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''----VOKASIONAL
            Dim strKodM1 As String = ""
            Dim strNamaM1 As String = ""
            Dim strJKM1 As String = ""
            Dim strGredM1 As String = ""
            ''Modul1
            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='1'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M1 As Array
            ar_M1 = strRet.Split("|")
            strKodM1 = ar_M1(0)
            strNamaM1 = ar_M1(1)
            strJKM1 = ar_M1(2)

            strSQL = "SELECT GredV1 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD8SEM1='" & strKodM1 & "', NAM8SEM1='" & strNamaM1 & "',JK8SEM1='" & strJKM1 & "',MOD8SEM1='" & strGredM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul2
            Dim strKodM2 As String = ""
            Dim strNamaM2 As String = ""
            Dim strJKM2 As String = ""
            Dim strGredM2 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='2'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M2 As Array
            ar_M2 = strRet.Split("|")
            strKodM2 = ar_M2(0)
            strNamaM2 = ar_M2(1)
            strJKM2 = ar_M2(2)

            strSQL = "SELECT GredV2 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM2 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD9SEM1='" & strKodM2 & "', NAM9SEM1='" & strNamaM2 & "',JK9SEM1='" & strJKM2 & "',MOD9SEM1='" & strGredM2 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul3
            Dim strKodM3 As String = ""
            Dim strNamaM3 As String = ""
            Dim strJKM3 As String = ""
            Dim strGredM3 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='3'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M3 As Array
            ar_M3 = strRet.Split("|")
            strKodM3 = ar_M3(0)
            strNamaM3 = ar_M3(1)
            strJKM3 = ar_M3(2)

            strSQL = "SELECT GredV3 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM3 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD10SEM1='" & strKodM3 & "', NAM10SEM1='" & strNamaM3 & "',JK10SEM1='" & strJKM3 & "',MOD10SEM1='" & strGredM3 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul4
            Dim strKodM4 As String = ""
            Dim strNamaM4 As String = ""
            Dim strJKM4 As String = ""
            Dim strGredM4 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='4'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M4 As Array
                ar_M4 = strRet.Split("|")
                strKodM4 = ar_M4(0)
                strNamaM4 = ar_M4(1)
                strJKM4 = ar_M4(2)

                strSQL = "SELECT GredV4 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM4 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD11SEM1='" & strKodM4 & "', NAM11SEM1='" & strNamaM4 & "',JK11SEM1='" & strJKM4 & "',MOD11SEM1='" & strGredM4 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
            ''Modul5
            Dim strKodM5 As String = ""
            Dim strNamaM5 As String = ""
            Dim strJKM5 As String = ""
            Dim strGredM5 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='5'"

            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M5 As Array
                ar_M5 = strRet.Split("|")
                strKodM5 = ar_M5(0)
                strNamaM5 = ar_M5(1)
                strJKM5 = ar_M5(2)

                strSQL = "SELECT GredV5 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM5 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD12SEM1='" & strKodM5 & "', NAM12SEM1='" & strNamaM5 & "',JK12SEM1='" & strJKM5 & "',MOD12SEM1='" & strGredM5 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
            ''Modul6
            Dim strKodM6 As String = ""
            Dim strNamaM6 As String = ""
            Dim strJKM6 As String = ""
            Dim strGredM6 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='6'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M6 As Array
                ar_M6 = strRet.Split("|")
                strKodM6 = ar_M6(0)
                strNamaM6 = ar_M6(1)
                strJKM6 = ar_M6(2)

                strSQL = "SELECT GredV6 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM6 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD13SEM1='" & strKodM6 & "', NAM13SEM1='" & strNamaM6 & "',JK13SEM1='" & strJKM6 & "',MOD13SEM1='" & strGredM6 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul7
            Dim strKodM7 As String = ""
            Dim strNamaM7 As String = ""
            Dim strJKM7 As String = ""
            Dim strGredM7 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='7'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M7 As Array
                ar_M7 = strRet.Split("|")
                strKodM7 = ar_M7(0)
                strNamaM7 = ar_M7(1)
                strJKM7 = ar_M7(2)

                strSQL = "SELECT GredV7 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM7 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD14SEM1='" & strKodM7 & "', NAM14SEM1='" & strNamaM7 & "',JK14SEM1='" & strJKM7 & "',MOD14SEM1='" & strGredM7 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul8
            Dim strKodM8 As String = ""
            Dim strNamaM8 As String = ""
            Dim strJKM8 As String = ""
            Dim strGredM8 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='1' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='8'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M8 As Array
                ar_M8 = strRet.Split("|")
                strKodM8 = ar_M8(0)
                strNamaM8 = ar_M8(1)
                strJKM8 = ar_M8(2)

                strSQL = "SELECT GredV8 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM8 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD15SEM1='" & strKodM8 & "', NAM15SEM1='" & strNamaM8 & "',JK15SEM1='" & strJKM8 & "',MOD15SEM1='" & strGredM8 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
        Next 'pelajar
    End Sub
    Private Sub GETSEM2X()
        'Sem1
        Dim strMykad As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQLDB)
        Dim dt As DataTable = GetData(cmd)
        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("Mykad")

            Dim IntPelajarIDSem1 As Integer = 0
            Dim strTahunSem1 As String = ""
            Dim strKursusID As Integer = 0

            strSQL = "SELECT PelajarID,Tahun,KursusID FROM kpmkv_pelajar WHERE Mykad='" & strMykad & "' AND Semester='2'"
            strSQL += " AND IsDeleted='N' AND StatusID='2'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Sem1 As Array
            ar_Sem1 = strRet.Split("|")
            IntPelajarIDSem1 = ar_Sem1(0)
            strTahunSem1 = ar_Sem1(1)
            strKursusID = ar_Sem1(2)

            '--BahasaMelayu
            Dim strKodBM1 As String = ""
            Dim strNamaBM1 As String = ""
            Dim strJKBM1 As String = ""
            Dim strGredBM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='BAHASA MELAYU' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BM1 As Array
            ar_BM1 = strRet.Split("|")
            strKodBM1 = ar_BM1(0)
            strNamaBM1 = ar_BM1(1)
            strJKBM1 = ar_BM1(2)

            strSQL = "SELECT GredBM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD1SEM2='" & strKodBM1 & "', NAM1SEM2='" & strNamaBM1 & "',JK1SEM2='" & strJKBM1 & "',MOD1SEM2='" & strGredBM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--bi 
            Dim strKodBI1 As String = ""
            Dim strNamaBI1 As String = ""
            Dim strJKBI1 As String = ""
            Dim strGredBI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='BAHASA INGGERIS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BI1 As Array
            ar_BI1 = strRet.Split("|")
            strKodBI1 = ar_BI1(0)
            strNamaBI1 = ar_BI1(1)
            strJKBI1 = ar_BI1(2)

            strSQL = "SELECT GredBI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD2SEM2='" & strKodBI1 & "', NAM2SEM2='" & strNamaBI1 & "',JK2SEM2='" & strJKBI1 & "',MOD2SEM2='" & strGredBI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--MATHEMATIC
            Dim strKodMT1 As String = ""
            Dim strNamaMT1 As String = ""
            Dim strJKMT1 As String = ""
            Dim strGredMT1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='MATHEMATICS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_MT1 As Array
            ar_MT1 = strRet.Split("|")
            strKodMT1 = ar_MT1(0)
            strNamaMT1 = ar_MT1(1)
            strJKMT1 = ar_MT1(2)

            strSQL = "SELECT GredMT FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredMT1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD3SEM2='" & strKodMT1 & "', NAM3SEM2='" & strNamaMT1 & "',JK3SEM2='" & strJKMT1 & "',MOD3SEM2='" & strGredMT1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SCIENCE
            Dim strKodSC1 As String = ""
            Dim strNamaSC1 As String = ""
            Dim strJKSC1 As String = ""
            Dim strGredSC1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='SCIENCE' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SC1 As Array
            ar_SC1 = strRet.Split("|")
            strKodSC1 = ar_SC1(0)
            strNamaSC1 = ar_SC1(1)
            strJKSC1 = ar_SC1(2)

            strSQL = "SELECT GredSC FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSC1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD4SEM2='" & strKodSC1 & "', NAM4SEM2='" & strNamaSC1 & "',JK4SEM2='" & strJKSC1 & "',MOD4SEM2='" & strGredSC1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SEJARAH
            Dim strKodSJ1 As String = ""
            Dim strNamaSJ1 As String = ""
            Dim strJKSJ1 As String = ""
            Dim strGredSJ1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='SEJARAH' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SJ1 As Array
            ar_SJ1 = strRet.Split("|")
            strKodSJ1 = ar_SJ1(0)
            strNamaSJ1 = ar_SJ1(1)
            strJKSJ1 = ar_SJ1(2)

            strSQL = "SELECT GredSJ FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSJ1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD5SEM2='" & strKodSJ1 & "', NAM5SEM2='" & strNamaSJ1 & "',JK5SEM2='" & strJKSJ1 & "',MOD5SEM2='" & strGredSJ1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN ISLAM
            Dim strKodPI1 As String = ""
            Dim strNamaPI1 As String = ""
            Dim strJKPI1 As String = ""
            Dim strGredPI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='PENDIDIKAN ISLAM' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PI1 As Array
            ar_PI1 = strRet.Split("|")
            strKodPI1 = ar_PI1(0)
            strNamaPI1 = ar_PI1(1)
            strJKPI1 = ar_PI1(2)

            strSQL = "SELECT GredPI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD6SEM2='" & strKodPI1 & "', NAM6SEM2='" & strNamaPI1 & "',JK6SEM2='" & strJKPI1 & "',MOD6SEM2='" & strGredPI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN MORAL
            Dim strKodPM1 As String = ""
            Dim strNamaPM1 As String = ""
            Dim strJKPM1 As String = ""
            Dim strGredPM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='2' AND NamaMataPelajaran='PENDIDIKAN MORAL' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PM1 As Array
            ar_PM1 = strRet.Split("|")
            strKodPM1 = ar_PM1(0)
            strNamaPM1 = ar_PM1(1)
            strJKPM1 = ar_PM1(2)

            strSQL = "SELECT GredPM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD7SEM2='" & strKodPM1 & "', NAM7SEM2='" & strNamaPM1 & "',JK7SEM2='" & strJKPM1 & "',MOD7SEM2='" & strGredPM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''----VOKASIONAL
            Dim strKodM1 As String = ""
            Dim strNamaM1 As String = ""
            Dim strJKM1 As String = ""
            Dim strGredM1 As String = ""
            ''Modul1
            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='1'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M1 As Array
            ar_M1 = strRet.Split("|")
            strKodM1 = ar_M1(0)
            strNamaM1 = ar_M1(1)
            strJKM1 = ar_M1(2)

            strSQL = "SELECT GredV1 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD8SEM2='" & strKodM1 & "', NAM8SEM2='" & strNamaM1 & "',JK8SEM2='" & strJKM1 & "',MOD8SEM2='" & strGredM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul2
            Dim strKodM2 As String = ""
            Dim strNamaM2 As String = ""
            Dim strJKM2 As String = ""
            Dim strGredM2 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='2'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M2 As Array
            ar_M2 = strRet.Split("|")
            strKodM2 = ar_M2(0)
            strNamaM2 = ar_M2(1)
            strJKM2 = ar_M2(2)

            strSQL = "SELECT GredV2 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM2 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD9SEM2='" & strKodM2 & "', NAM9SEM2='" & strNamaM2 & "',JK9SEM2='" & strJKM2 & "',MOD9SEM2='" & strGredM2 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul3
            Dim strKodM3 As String = ""
            Dim strNamaM3 As String = ""
            Dim strJKM3 As String = ""
            Dim strGredM3 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='3'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M3 As Array
                ar_M3 = strRet.Split("|")
                strKodM3 = ar_M3(0)
                strNamaM3 = ar_M3(1)
                strJKM3 = ar_M3(2)

                strSQL = "SELECT GredV3 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM3 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD10SEM2='" & strKodM3 & "', NAM10SEM2='" & strNamaM3 & "',JK10SEM2='" & strJKM3 & "',MOD10SEM2='" & strGredM3 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul4
            Dim strKodM4 As String = ""
            Dim strNamaM4 As String = ""
            Dim strJKM4 As String = ""
            Dim strGredM4 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='4'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M4 As Array
                ar_M4 = strRet.Split("|")
                strKodM4 = ar_M4(0)
                strNamaM4 = ar_M4(1)
                strJKM4 = ar_M4(2)

                strSQL = "SELECT GredV4 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM4 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD11SEM2='" & strKodM4 & "', NAM11SEM2='" & strNamaM4 & "',JK11SEM2='" & strJKM4 & "',MOD11SEM2='" & strGredM4 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul5
            Dim strKodM5 As String = ""
            Dim strNamaM5 As String = ""
            Dim strJKM5 As String = ""
            Dim strGredM5 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='5'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M5 As Array
                ar_M5 = strRet.Split("|")
                strKodM5 = ar_M5(0)
                strNamaM5 = ar_M5(1)
                strJKM5 = ar_M5(2)

                strSQL = "SELECT GredV5 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM5 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD12SEM2='" & strKodM5 & "', NAM12SEM2='" & strNamaM5 & "',JK12SEM2='" & strJKM5 & "',MOD12SEM2='" & strGredM5 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul6
            Dim strKodM6 As String = ""
            Dim strNamaM6 As String = ""
            Dim strJKM6 As String = ""
            Dim strGredM6 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='6'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M6 As Array
                ar_M6 = strRet.Split("|")
                strKodM6 = ar_M6(0)
                strNamaM6 = ar_M6(1)
                strJKM6 = ar_M6(2)

                strSQL = "SELECT GredV6 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM6 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD13SEM2='" & strKodM6 & "', NAM13SEM2='" & strNamaM6 & "',JK13SEM2='" & strJKM6 & "',MOD13SEM2='" & strGredM6 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul7
            Dim strKodM7 As String = ""
            Dim strNamaM7 As String = ""
            Dim strJKM7 As String = ""
            Dim strGredM7 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='7'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M7 As Array
                ar_M7 = strRet.Split("|")
                strKodM7 = ar_M7(0)
                strNamaM7 = ar_M7(1)
                strJKM7 = ar_M7(2)

                strSQL = "SELECT GredV7 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM7 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD14SEM2='" & strKodM7 & "', NAM14SEM2='" & strNamaM7 & "',JK14SEM2='" & strJKM7 & "',MOD14SEM2='" & strGredM7 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul8
            Dim strKodM8 As String = ""
            Dim strNamaM8 As String = ""
            Dim strJKM8 As String = ""
            Dim strGredM8 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='2' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='8'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M8 As Array
                ar_M8 = strRet.Split("|")
                strKodM8 = ar_M8(0)
                strNamaM8 = ar_M8(1)
                strJKM8 = ar_M8(2)

                strSQL = "SELECT GredV8 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM8 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD15SEM2='" & strKodM8 & "', NAM15SEM2='" & strNamaM8 & "',JK15SEM2='" & strJKM8 & "',MOD15SEM2='" & strGredM8 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
        Next 'pelajar
    End Sub
    Private Sub GETSEM3X()
        'Sem1
        Dim strMykad As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQLDB)
        Dim dt As DataTable = GetData(cmd)
        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("Mykad")

            Dim IntPelajarIDSem1 As Integer = 0
            Dim strTahunSem1 As String = ""
            Dim strKursusID As Integer = 0

            strSQL = "SELECT PelajarID,Tahun,KursusID FROM kpmkv_pelajar WHERE Mykad='" & strMykad & "' AND Semester='3'"
            strSQL += " AND IsDeleted='N' AND StatusID='2'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Sem1 As Array
            ar_Sem1 = strRet.Split("|")
            IntPelajarIDSem1 = ar_Sem1(0)
            strTahunSem1 = ar_Sem1(1)
            strKursusID = ar_Sem1(2)

            '--BahasaMelayu
            Dim strKodBM1 As String = ""
            Dim strNamaBM1 As String = ""
            Dim strJKBM1 As String = ""
            Dim strGredBM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='BAHASA MELAYU' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BM1 As Array
            ar_BM1 = strRet.Split("|")
            strKodBM1 = ar_BM1(0)
            strNamaBM1 = ar_BM1(1)
            strJKBM1 = ar_BM1(2)

            strSQL = "SELECT GredBM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD1SEM3='" & strKodBM1 & "', NAM1SEM3='" & strNamaBM1 & "',JK1SEM3='" & strJKBM1 & "',MOD1SEM3='" & strGredBM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--bi 
            Dim strKodBI1 As String = ""
            Dim strNamaBI1 As String = ""
            Dim strJKBI1 As String = ""
            Dim strGredBI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='BAHASA INGGERIS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BI1 As Array
            ar_BI1 = strRet.Split("|")
            strKodBI1 = ar_BI1(0)
            strNamaBI1 = ar_BI1(1)
            strJKBI1 = ar_BI1(2)

            strSQL = "SELECT GredBI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD2SEM3='" & strKodBI1 & "', NAM2SEM3='" & strNamaBI1 & "',JK2SEM3='" & strJKBI1 & "',MOD2SEM3='" & strGredBI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--MATHEMATIC
            Dim strKodMT1 As String = ""
            Dim strNamaMT1 As String = ""
            Dim strJKMT1 As String = ""
            Dim strGredMT1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='MATHEMATICS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_MT1 As Array
            ar_MT1 = strRet.Split("|")
            strKodMT1 = ar_MT1(0)
            strNamaMT1 = ar_MT1(1)
            strJKMT1 = ar_MT1(2)

            strSQL = "SELECT GredMT FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredMT1 = oCommon.getFieldValue(strSQL)

            'MT for teknologi or social  
            strSQL = "SELECT  JenisKursus From kpmkv_kursus Where KursusID='" & strKursusID & "'"
            Dim strJenisKursusMAT As String = oCommon.getFieldValue(strSQL)

            'Social
            Dim strNamaMAT As String
            Dim strKodMAT As String = ""
            If strJenisKursusMAT = "SOCIAL" Then
                strNamaMAT = "MATHEMATICS FOR SOCIAL"
                strKodMAT = "AMT3101"
            Else
                strNamaMAT = "MATHEMATICS FOR TECHNOLOGY"
                strKodMAT = "AMT3091"
            End If

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD3SEM3='" & strKodMAT & "', NAM3SEM3='" & strNamaMAT & "',JK3SEM3='" & strJKMT1 & "',MOD3SEM3='" & strGredMT1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SCIENCE
            Dim strKodSC1 As String = ""
            Dim strNamaSC1 As String = ""
            Dim strJKSC1 As String = ""
            Dim strGredSC1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='SCIENCE' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SC1 As Array
            ar_SC1 = strRet.Split("|")
            strKodSC1 = ar_SC1(0)
            strNamaSC1 = ar_SC1(1)
            strJKSC1 = ar_SC1(2)

            strSQL = "SELECT GredSC FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSC1 = oCommon.getFieldValue(strSQL)

            'Sc for teknologi or social  
            strSQL = "SELECT  JenisKursus From kpmkv_kursus Where KursusID='" & strKursusID & "'"
            Dim strJenisKursusMT As String = oCommon.getFieldValue(strSQL)

            'Social
            Dim strNamaMT As String
            Dim strKodMT As String = ""
            If strJenisKursusMT = "SOCIAL" Then
                strNamaMT = "SCIENCE FOR SOCIAL"
                strKodMT = "AMT3131"
            Else
                strNamaMT = "SCIENCE FOR TECHNOLOGY"
                strKodMT = "AMT3121"
            End If

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD4SEM3='" & strKodMT & "', NAM4SEM3='" & strNamaMT & "',JK4SEM3='" & strJKSC1 & "',MOD4SEM3='" & strGredSC1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SEJARAH
            Dim strKodSJ1 As String = ""
            Dim strNamaSJ1 As String = ""
            Dim strJKSJ1 As String = ""
            Dim strGredSJ1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='SEJARAH' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SJ1 As Array
            ar_SJ1 = strRet.Split("|")
            strKodSJ1 = ar_SJ1(0)
            strNamaSJ1 = ar_SJ1(1)
            strJKSJ1 = ar_SJ1(2)

            strSQL = "SELECT GredSJ FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSJ1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD5SEM3='" & strKodSJ1 & "', NAM5SEM3='" & strNamaSJ1 & "',JK5SEM3='" & strJKSJ1 & "',MOD5SEM3='" & strGredSJ1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN ISLAM
            Dim strKodPI1 As String = ""
            Dim strNamaPI1 As String = ""
            Dim strJKPI1 As String = ""
            Dim strGredPI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='PENDIDIKAN ISLAM' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PI1 As Array
            ar_PI1 = strRet.Split("|")
            strKodPI1 = ar_PI1(0)
            strNamaPI1 = ar_PI1(1)
            strJKPI1 = ar_PI1(2)

            strSQL = "SELECT GredPI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD6SEM3='" & strKodPI1 & "', NAM6SEM3='" & strNamaPI1 & "',JK6SEM3='" & strJKPI1 & "',MOD6SEM3='" & strGredPI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN MORAL
            Dim strKodPM1 As String = ""
            Dim strNamaPM1 As String = ""
            Dim strJKPM1 As String = ""
            Dim strGredPM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='3' AND NamaMataPelajaran='PENDIDIKAN MORAL' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PM1 As Array
            ar_PM1 = strRet.Split("|")
            strKodPM1 = ar_PM1(0)
            strNamaPM1 = ar_PM1(1)
            strJKPM1 = ar_PM1(2)

            strSQL = "SELECT GredPM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD7SEM3='" & strKodPM1 & "', NAM7SEM3='" & strNamaPM1 & "',JK7SEM3='" & strJKPM1 & "',MOD7SEM3='" & strGredPM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''----VOKASIONAL
            Dim strKodM1 As String = ""
            Dim strNamaM1 As String = ""
            Dim strJKM1 As String = ""
            Dim strGredM1 As String = ""
            ''Modul1
            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='1'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M1 As Array
                ar_M1 = strRet.Split("|")
                strKodM1 = ar_M1(0)
                strNamaM1 = ar_M1(1)
                strJKM1 = ar_M1(2)

                strSQL = "SELECT GredV1 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM1 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD8SEM3='" & strKodM1 & "', NAM8SEM3='" & strNamaM1 & "',JK8SEM3='" & strJKM1 & "',MOD8SEM3='" & strGredM1 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul2
            Dim strKodM2 As String = ""
            Dim strNamaM2 As String = ""
            Dim strJKM2 As String = ""
            Dim strGredM2 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='2'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M2 As Array
                ar_M2 = strRet.Split("|")
                strKodM2 = ar_M2(0)
                strNamaM2 = ar_M2(1)
                strJKM2 = ar_M2(2)

                strSQL = "SELECT GredV2 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM2 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD9SEM3='" & strKodM2 & "', NAM9SEM3='" & strNamaM2 & "',JK9SEM3='" & strJKM2 & "',MOD9SEM3='" & strGredM2 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul3
            Dim strKodM3 As String = ""
            Dim strNamaM3 As String = ""
            Dim strJKM3 As String = ""
            Dim strGredM3 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='3'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M3 As Array
                ar_M3 = strRet.Split("|")
                strKodM3 = ar_M3(0)
                strNamaM3 = ar_M3(1)
                strJKM3 = ar_M3(2)

                strSQL = "SELECT GredV3 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM3 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD10SEM3='" & strKodM3 & "', NAM10SEM3='" & strNamaM3 & "',JK10SEM3='" & strJKM3 & "',MOD10SEM3='" & strGredM3 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul4
            Dim strKodM4 As String = ""
            Dim strNamaM4 As String = ""
            Dim strJKM4 As String = ""
            Dim strGredM4 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='4'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M4 As Array
                ar_M4 = strRet.Split("|")
                strKodM4 = ar_M4(0)
                strNamaM4 = ar_M4(1)
                strJKM4 = ar_M4(2)

                strSQL = "SELECT GredV4 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM4 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD11SEM3='" & strKodM4 & "', NAM11SEM3='" & strNamaM4 & "',JK11SEM3='" & strJKM4 & "',MOD11SEM3='" & strGredM4 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul5
            Dim strKodM5 As String = ""
            Dim strNamaM5 As String = ""
            Dim strJKM5 As String = ""
            Dim strGredM5 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='5'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M5 As Array
                ar_M5 = strRet.Split("|")
                strKodM5 = ar_M5(0)
                strNamaM5 = ar_M5(1)
                strJKM5 = ar_M5(2)

                strSQL = "SELECT GredV5 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM5 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD12SEM3='" & strKodM5 & "', NAM12SEM3='" & strNamaM5 & "',JK12SEM3='" & strJKM5 & "',MOD12SEM3='" & strGredM5 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul6
            Dim strKodM6 As String = ""
            Dim strNamaM6 As String = ""
            Dim strJKM6 As String = ""
            Dim strGredM6 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='6'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M6 As Array
                ar_M6 = strRet.Split("|")
                strKodM6 = ar_M6(0)
                strNamaM6 = ar_M6(1)
                strJKM6 = ar_M6(2)

                strSQL = "SELECT GredV6 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM6 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD13SEM3='" & strKodM6 & "', NAM13SEM3='" & strNamaM6 & "',JK13SEM3='" & strJKM6 & "',MOD13SEM3='" & strGredM6 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul7
            Dim strKodM7 As String = ""
            Dim strNamaM7 As String = ""
            Dim strJKM7 As String = ""
            Dim strGredM7 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='7'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M7 As Array
                ar_M7 = strRet.Split("|")
                strKodM7 = ar_M7(0)
                strNamaM7 = ar_M7(1)
                strJKM7 = ar_M7(2)

                strSQL = "SELECT GredV7 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM7 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD14SEM3='" & strKodM7 & "', NAM14SEM3='" & strNamaM7 & "',JK14SEM3='" & strJKM7 & "',MOD14SEM3='" & strGredM7 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul8
            Dim strKodM8 As String = ""
            Dim strNamaM8 As String = ""
            Dim strJKM8 As String = ""
            Dim strGredM8 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='3' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='8'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M8 As Array
                ar_M8 = strRet.Split("|")
                strKodM8 = ar_M8(0)
                strNamaM8 = ar_M8(1)
                strJKM8 = ar_M8(2)

                strSQL = "SELECT GredV8 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM8 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD15SEM3='" & strKodM8 & "', NAM15SEM3='" & strNamaM8 & "',JK15SEM3='" & strJKM8 & "',MOD15SEM3='" & strGredM8 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
        Next 'pelajar
    End Sub
    Private Sub GETSEM4X()
        'Sem1
        Dim strMykad As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQLDB)
        Dim dt As DataTable = GetData(cmd)
        For i As Integer = 0 To dt.Rows.Count - 1
            strMykad = dt.Rows(i)("Mykad")

            Dim IntPelajarIDSem1 As Integer = 0
            Dim strTahunSem1 As String = ""
            Dim strKursusID As Integer = 0

            strSQL = "SELECT PelajarID,Tahun,KursusID FROM kpmkv_pelajar WHERE Mykad='" & strMykad & "' AND Semester='4'"
            strSQL += " AND IsDeleted='N' AND StatusID='2'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Sem1 As Array
            ar_Sem1 = strRet.Split("|")
            IntPelajarIDSem1 = ar_Sem1(0)
            strTahunSem1 = ar_Sem1(1)
            strKursusID = ar_Sem1(2)

            '--BahasaMelayu
            Dim strKodBM1 As String = ""
            Dim strNamaBM1 As String = ""
            Dim strJKBM1 As String = ""
            Dim strGredBM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='BAHASA MELAYU' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BM1 As Array
            ar_BM1 = strRet.Split("|")
            strKodBM1 = ar_BM1(0)
            strNamaBM1 = ar_BM1(1)
            strJKBM1 = ar_BM1(2)

            strSQL = "SELECT GredBM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD1SEM4='" & strKodBM1 & "', NAM1SEM4='" & strNamaBM1 & "',JK1SEM4='" & strJKBM1 & "',MOD1SEM4='" & strGredBM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--bi 
            Dim strKodBI1 As String = ""
            Dim strNamaBI1 As String = ""
            Dim strJKBI1 As String = ""
            Dim strGredBI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='BAHASA INGGERIS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_BI1 As Array
            ar_BI1 = strRet.Split("|")
            strKodBI1 = ar_BI1(0)
            strNamaBI1 = ar_BI1(1)
            strJKBI1 = ar_BI1(2)

            strSQL = "SELECT GredBI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredBI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD2SEM4='" & strKodBI1 & "', NAM2SEM4='" & strNamaBI1 & "',JK2SEM4='" & strJKBI1 & "',MOD2SEM4='" & strGredBI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--MATHEMATIC
            Dim strKodMT1 As String = ""
            Dim strNamaMT1 As String = ""
            Dim strJKMT1 As String = ""
            Dim strGredMT1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='MATHEMATICS' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_MT1 As Array
            ar_MT1 = strRet.Split("|")
            strKodMT1 = ar_MT1(0)
            strNamaMT1 = ar_MT1(1)
            strJKMT1 = ar_MT1(2)

            strSQL = "SELECT  JenisKursus From kpmkv_kursus Where KursusID='" & strKursusID & "'"
            Dim strJenisKursusMAT As String = oCommon.getFieldValue(strSQL)

            Dim strNamaMAT As String
            Dim strKodMAT As String
            If strJenisKursusMAT = "SOCIAL" Then
                strNamaMAT = "MATHEMATICS FOR SOCIAL"
                strKodMAT = "AMT4101"
            Else
                strNamaMAT = "MATHEMATICS FOR TECHNOLOGY"
                strKodMAT = "AMT4091"
            End If

            strSQL = "SELECT GredMT FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredMT1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD3SEM4='" & strKodMAT & "', NAM3SEM4='" & strNamaMAT & "',JK3SEM4='" & strJKMT1 & "',MOD3SEM4='" & strGredMT1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SCIENCE
            Dim strKodSC1 As String = ""
            Dim strNamaSC1 As String = ""
            Dim strJKSC1 As String = ""
            Dim strGredSC1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='SCIENCE' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SC1 As Array
            ar_SC1 = strRet.Split("|")
            strKodSC1 = ar_SC1(0)
            strNamaSC1 = ar_SC1(1)
            strJKSC1 = ar_SC1(2)

            strSQL = "SELECT  JenisKursus From kpmkv_kursus Where KursusID='" & strKursusID & "'"
            Dim strJenisKursusMT As String = oCommon.getFieldValue(strSQL)

            Dim strNamaSC As String
            Dim strKodSC As String

            If strJenisKursusMT = "SOCIAL" Then
                strNamaSC = "SCIENCE FOR SOCIAL"
                strKodSC = "AMT4131"
            Else
                strNamaSC = "SCIENCE FOR TECHNOLOGY"
                strKodSC = "AMT4121"
            End If
            strSQL = "SELECT GredSC FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSC1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD4SEM4='" & strKodSC & "', NAM4SEM4='" & strNamaSC & "',JK4SEM4='" & strJKSC1 & "',MOD4SEM4='" & strGredSC1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--SEJARAH
            Dim strKodSJ1 As String = ""
            Dim strNamaSJ1 As String = ""
            Dim strJKSJ1 As String = ""
            Dim strGredSJ1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='SEJARAH' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_SJ1 As Array
            ar_SJ1 = strRet.Split("|")
            strKodSJ1 = ar_SJ1(0)
            strNamaSJ1 = ar_SJ1(1)
            strJKSJ1 = ar_SJ1(2)

            strSQL = "SELECT GredSJ FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredSJ1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD5SEM4='" & strKodSJ1 & "', NAM5SEM4='" & strNamaSJ1 & "',JK5SEM4='" & strJKSJ1 & "',MOD5SEM4='" & strGredSJ1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN ISLAM
            Dim strKodPI1 As String = ""
            Dim strNamaPI1 As String = ""
            Dim strJKPI1 As String = ""
            Dim strGredPI1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='PENDIDIKAN ISLAM' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PI1 As Array
            ar_PI1 = strRet.Split("|")
            strKodPI1 = ar_PI1(0)
            strNamaPI1 = ar_PI1(1)
            strJKPI1 = ar_PI1(2)

            strSQL = "SELECT GredPI FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPI1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD6SEM4='" & strKodPI1 & "', NAM6SEM4='" & strNamaPI1 & "',JK6SEM4='" & strJKPI1 & "',MOD6SEM4='" & strGredPI1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''--PENDIDIKAN MORAL
            Dim strKodPM1 As String = ""
            Dim strNamaPM1 As String = ""
            Dim strJKPM1 As String = ""
            Dim strGredPM1 As String = ""

            strSQL = "SELECT KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE SUBSTRING(KodMataPelajaran,4,1)='4' AND NamaMataPelajaran='PENDIDIKAN MORAL' AND Tahun='" & strTahunSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_PM1 As Array
            ar_PM1 = strRet.Split("|")
            strKodPM1 = ar_PM1(0)
            strNamaPM1 = ar_PM1(1)
            strJKPM1 = ar_PM1(2)

            strSQL = "SELECT GredPM FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredPM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD7SEM4='" & strKodPM1 & "', NAM7SEM4='" & strNamaPM1 & "',JK7SEM4='" & strJKPM1 & "',MOD7SEM4='" & strGredPM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''----VOKASIONAL
            Dim strKodM1 As String = ""
            Dim strNamaM1 As String = ""
            Dim strJKM1 As String = ""
            Dim strGredM1 As String = ""
            ''Modul1
            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='1'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M1 As Array
            ar_M1 = strRet.Split("|")
            strKodM1 = ar_M1(0)
            strNamaM1 = ar_M1(1)
            strJKM1 = ar_M1(2)

            strSQL = "SELECT GredV1 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD8SEM4='" & strKodM1 & "', NAM8SEM4='" & strNamaM1 & "',JK8SEM4='" & strJKM1 & "',MOD8SEM4='" & strGredM1 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul2
            Dim strKodM2 As String = ""
            Dim strNamaM2 As String = ""
            Dim strJKM2 As String = ""
            Dim strGredM2 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='2'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Modul info
            Dim ar_M2 As Array
            ar_M2 = strRet.Split("|")
            strKodM2 = ar_M2(0)
            strNamaM2 = ar_M2(1)
            strJKM2 = ar_M2(2)

            strSQL = "SELECT GredV2 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
            strGredM2 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_transkrip_svm SET KOD9SEM4='" & strKodM2 & "', NAM9SEM4='" & strNamaM2 & "',JK9SEM4='" & strJKM2 & "',MOD9SEM4='" & strGredM2 & "' WHERE Mykad='" & strMykad & "'"
            strRet = oCommon.getFieldValue(strSQL)

            ''Modul3
            Dim strKodM3 As String = ""
            Dim strNamaM3 As String = ""
            Dim strJKM3 As String = ""
            Dim strGredM3 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='3'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M3 As Array
                ar_M3 = strRet.Split("|")
                strKodM3 = ar_M3(0)
                strNamaM3 = ar_M3(1)
                strJKM3 = ar_M3(2)

                strSQL = "SELECT GredV3 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM3 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD10SEM4='" & strKodM3 & "', NAM10SEM4='" & strNamaM3 & "',JK10SEM4='" & strJKM3 & "',MOD10SEM4='" & strGredM3 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul4
            Dim strKodM4 As String = ""
            Dim strNamaM4 As String = ""
            Dim strJKM4 As String = ""
            Dim strGredM4 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='4'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M4 As Array
                ar_M4 = strRet.Split("|")
                strKodM4 = ar_M4(0)
                strNamaM4 = ar_M4(1)
                strJKM4 = ar_M4(2)

                strSQL = "SELECT GredV4 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM4 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD11SEM4='" & strKodM4 & "', NAM11SEM4='" & strNamaM4 & "',JK11SEM4='" & strJKM4 & "',MOD11SEM4='" & strGredM4 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul5
            Dim strKodM5 As String = ""
            Dim strNamaM5 As String = ""
            Dim strJKM5 As String = ""
            Dim strGredM5 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='5'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M5 As Array
                ar_M5 = strRet.Split("|")
                strKodM5 = ar_M5(0)
                strNamaM5 = ar_M5(1)
                strJKM5 = ar_M5(2)

                strSQL = "SELECT GredV5 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM5 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD12SEM4='" & strKodM5 & "', NAM12SEM4='" & strNamaM5 & "',JK12SEM4='" & strJKM5 & "',MOD12SEM4='" & strGredM5 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul6
            Dim strKodM6 As String = ""
            Dim strNamaM6 As String = ""
            Dim strJKM6 As String = ""
            Dim strGredM6 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='6'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M6 As Array
                ar_M6 = strRet.Split("|")
                strKodM6 = ar_M6(0)
                strNamaM6 = ar_M6(1)
                strJKM6 = ar_M6(2)

                strSQL = "SELECT GredV6 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM6 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD13SEM4='" & strKodM6 & "', NAM13SEM4='" & strNamaM6 & "',JK13SEM4='" & strJKM6 & "',MOD13SEM4='" & strGredM6 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul7
            Dim strKodM7 As String = ""
            Dim strNamaM7 As String = ""
            Dim strJKM7 As String = ""
            Dim strGredM7 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='7'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M7 As Array
                ar_M7 = strRet.Split("|")
                strKodM7 = ar_M7(0)
                strNamaM7 = ar_M7(1)
                strJKM7 = ar_M7(2)

                strSQL = "SELECT GredV7 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM7 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD14SEM4='" & strKodM7 & "', NAM14SEM4='" & strNamaM7 & "',JK14SEM4='" & strJKM7 & "',MOD14SEM4='" & strGredM7 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Modul8
            Dim strKodM8 As String = ""
            Dim strNamaM8 As String = ""
            Dim strJKM8 As String = ""
            Dim strGredM8 As String = ""

            strSQL = "SELECT KodModul, NamaModul, JamKredit FROM  kpmkv_modul WHERE Tahun='" & strTahunSem1 & "' AND Semester='4' AND KursusID='" & strKursusID & "'"
            strSQL += " AND SUBSTRING(KodModul,6,1)='8'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_M8 As Array
                ar_M8 = strRet.Split("|")
                strKodM8 = ar_M8(0)
                strNamaM8 = ar_M8(1)
                strJKM8 = ar_M8(2)

                strSQL = "SELECT GredV8 FROM kpmkv_pelajar_markah WHERE PelajarID='" & IntPelajarIDSem1 & "'"
                strGredM8 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_transkrip_svm SET KOD15SEM4='" & strKodM8 & "', NAM15SEM4='" & strNamaM8 & "',JK15SEM4='" & strJKM8 & "',MOD15SEM4='" & strGredM8 & "' WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            'last
            Dim strGredBMSetara As String = ""
            Dim IntPNGKA As String = ""
            Dim IntPNGKV As String = ""
            Dim IntPNGK As String = ""
            Dim IntJum_JamKredit_Akademik As String = ""
            Dim IntJum_JamKredit_Vokasional As String = ""

            strSQL = " SELECT GredBMSetara,PNGKA,PNGKV,PNGKK,Jum_JamKredit_Akademik,Jum_JamKredit_Vokasional FROM kpmkv_pelajar_markah WHERE Semester='4' AND PelajarID='" & IntPelajarIDSem1 & "'"
            strRet = oCommon.getFieldValueEx(strSQL)

            If Not strRet = "" Then
                ' ''--get Modul info
                Dim ar_G9 As Array
                ar_G9 = strRet.Split("|")
                strGredBMSetara = ar_G9(0)
                IntPNGKA = ar_G9(1)
                IntPNGKV = ar_G9(2)
                IntPNGK = ar_G9(3)
                IntJum_JamKredit_Akademik = ar_G9(4)
                IntJum_JamKredit_Vokasional = ar_G9(5)

                strSQL = "UPDATE kpmkv_transkrip_svm SET BMSetara='" & strGredBMSetara & "', PNGK_AKA='" & IntPNGKA & "',PNGK_VOK='" & IntPNGKV & "',PNGK='" & IntPNGK & "', JUMLAH_AKA='" & IntJum_JamKredit_Akademik & "',JUMLAH_VOK='" & IntJum_JamKredit_Vokasional & "'  WHERE Mykad='" & strMykad & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If
        Next 'pelajar
    End Sub
End Class



