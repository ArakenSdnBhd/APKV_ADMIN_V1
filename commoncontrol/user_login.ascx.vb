Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class user_login
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""


    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("LoginID") = ""
            Session("UserGroupCode") = ""
            Session("Nama") = ""
            Session("Negeri") = ""

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        Try

            If isValidLogin() = True Then


                strSQL = "SELECT kpmkv_users.Nama,kpmkv_users.Negeri,tbl_ctrl_usergroup.UserGroupCode FROM kpmkv_users  "
                strSQL += " LEFT OUTER JOIN tbl_ctrl_usergroup ON kpmkv_users.Usertype=tbl_ctrl_usergroup.UserGroup "
                strSQL += "WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oCommon.FixSingleQuotes(txtPwd.Text) & "'"
                strRet = oCommon.getFieldValueEx(strSQL)
                'Response.Write(strSQL)
                ''--get user info
                Dim ar_user_login As Array
                ar_user_login = strRet.Split("|")

                Session("Nama") = ar_user_login(0)
                Session("Negeri") = ar_user_login(1)
                Session("UserGroupCode") = ar_user_login(2)

                'Response.Cookies("kpmkv_loginid").Value = txtLoginID.Text
                'Response.Redirect("admin.login.success.aspx")

                Select Case Session("UserGroupCode")
                    Case "A02"       'SU-KOLEJ'
                        divMsg.Attributes("class") = "error"
                        lblMsg.Text = "Login ID atau Kata Laluan tidak sah!"
                    Case "A03"       'PENSYARAH'
                        divMsg.Attributes("class") = "error"
                        lblMsg.Text = "Login ID atau Kata Laluan tidak sah!"
                    Case "A04"       'PELAJAR'
                        divMsg.Attributes("class") = "error"
                        lblMsg.Text = "Login ID atau Kata Laluan tidak sah!"
                    Case Else
                        Response.Cookies("kpmkv_loginid").Value = txtLoginID.Text
                        Response.Redirect("admin.login.success.aspx")
                End Select

            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Login ID atau Kata Laluan tidak sah!"
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try



        ' ''--login name
        'strSQL = "Select LoginID,pwd from kpmkv_users where LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oCommon.FixSingleQuotes(txtPwd.Text) & "'"
        'If oCommon.isExist(strSQL) = True Then
        '    'strSQL = "SELECT Nama, UserType,Negeri FROM kpmkv_users WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oCommon.FixSingleQuotes(txtPwd.Text) & "'"
        '    'strRet = oCommon.getFieldValueEx(strSQL)

        '    ' ''--get user info
        '    'Dim ar_user_login As Array
        '    'ar_user_login = strRet.Split("|")

        '    'Session("Nama") = ar_user_login(0)
        '    'Session("UserType") = ar_user_login(1)
        '    'Session("Negeri") = ar_user_login(2)


        '    Response.Cookies("kpmkv_loginid").Value = txtLoginID.Text
        '    Response.Redirect("admin.login.success.aspx")

        '    'Select Case Session("UserType")
        '    '    Case "ADMIN"       'ADMIN'
        '    '        Response.Cookies("kpmkv_loginid").Value = txtLoginID.Text
        '    '        Response.Redirect("admin.login.success.aspx")
        '    '    Case "SU-KOLEJ"       'SU-KOLEJ'
        '    '        Response.Cookies("kpmkv_loginid").Value = txtLoginID.Text
        '    '        Response.Redirect("kolej.login.success.aspx")
        '    '    Case Else
        '    '        Response.Redirect("default.aspx")
        '    'End Select

        'Else
        '    divMsg.Attributes("class") = "error"
        '    lblMsg.Text = "Login ID atau Kata Laluan tidak sah!"
        'End If

        'Catch ex As Exception
        '    lblMsg.Text = "System Error:" & ex.Message
        'End Try


    End Sub
    Private Function isValidLogin() As Boolean
        strSQL = "Select LoginID,pwd from kpmkv_users where LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oCommon.FixSingleQuotes(txtPwd.Text) & "'"
        strRet = oCommon.isExist(strSQL)

        If strRet = True Then
           
            ''--insert into user_login_log
            strSQL = "INSERT INTO user_login_log (LoginIDNo,LogDatetime,Action) VALUES('" & oCommon.FixSingleQuotes(txtLoginID.Text) & "','" & oCommon.getNow & "','LOGIN')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "Failed to log user login. " & strRet
                Return False
            End If

            Return True
        Else
            ''--insert into user_login_log. INVALID_PWD. just transaction log
            strSQL = "INSERT INTO user_login_log (LoginIDNo,LogDatetime,Action) VALUES('" & oCommon.FixSingleQuotes(txtLoginID.Text) & "','" & oCommon.getNow & "','INVALID_PWD')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "Failed to log INVALID_PWD user login. " & strRet
                Return False
            End If

            ''--insert into user_login_session. INVALID_PWD. Refresh this after unlock. use this to lock
            strSQL = "INSERT INTO user_login_session (LoginIDNo,LogDatetime,Action) VALUES('" & oCommon.FixSingleQuotes(txtLoginID.Text) & "','" & oCommon.getNow & "','INVALID_PWD')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "Failed to log INVALID_PWD user login session. " & strRet
                Return False
            End If

            lblMsg.Text = "Invalid Login ID, Password or User Type!"
            Return False
        End If


    End Function
End Class