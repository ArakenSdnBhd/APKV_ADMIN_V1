Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization


Public Class dashboard1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadPage()
    End Sub

    Private Sub LoadPage()
        ' ------- list-------'
        Try

            strSQL = "SELECT kpmkv_pelajar.KolejRecordID AS Kod,"
            strSQL += " COUNT(kpmkv_pelajar.PelajarID) AS Total"
            strSQL += " FROM kpmkv_pelajar"
            strSQL += " WHERE kpmkv_pelajar.statusID='2' AND kpmkv_pelajar.Tahun='2014' and kpmkv_pelajar.Semester='1'"
            strSQL += " group by  kpmkv_pelajar.KolejRecordID"
            'Response.Write(strSQL)



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


            Dim strKod, strNama, strTotal As String
            Dim strList, strList1, strList2 As String


            strKod = ""
            strNama = ""
            strTotal = ""
            strList = ""
            strList1 = ""
            strList2 = ""



            If numrows > 0 Then
                For i = 0 To numrows - 1




                    strKod = ds.Tables(0).Rows(i).Item("Kod")

                    strTotal = ds.Tables(0).Rows(i).Item("Total")


                    strList += "{x:" & strKod & ", y:" & strTotal & "},"




                Next
                strList1 = "<script type='text/javascript'>"
                strList1 += "window.onload = function () {"
                strList1 += "var chart = new CanvasJS.Chart('chartContainer', {"

                strList1 += "title: {"
                strList1 += "text: 'Jumlah Pelajar'},"
                strList1 += "data: ["
                strList1 += "{ type: 'column',"
                strList1 += "dataPoints: ["

                strList2 += " ] } ]"
                strList2 += " });"

                strList2 += " chart.render(); }"
                strList2 += "</script>"

                lblData.Text = "[" + strList + "{x:0,y:0} ]"


            End If
        Catch ex As Exception
            objConn.Dispose()
        End Try

    End Sub

  


   
End Class