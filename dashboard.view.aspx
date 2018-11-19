<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="dashboard.view.aspx.vb" Inherits="kpmkv.dashboard_view" %>
<%@ Register src="commoncontrol/dashboard.ascx" tagname="dashboard" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:dashboard ID="dashboard1" runat="server" />
</asp:Content>
