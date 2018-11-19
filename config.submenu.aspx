<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.submenu.aspx.vb" Inherits="kpmkv.config_submenu2" %>
<%@ Register src="commoncontrol/config_submenu.ascx" tagname="config_submenu" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_submenu ID="config_submenu1" runat="server" />
</asp:Content>
