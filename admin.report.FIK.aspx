<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.report.FIK.aspx.vb" Inherits="kpmkv.admin_report_FIK" %>
<%@ Register src="commoncontrol/admin_reportFIK.ascx" tagname="admin_reportFIK" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_reportFIK ID="admin_reportFIK1" runat="server" />
</asp:Content>
