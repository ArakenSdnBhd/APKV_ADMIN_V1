<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.menu.aspx.vb" Inherits="kpmkv.config_menu2" %>
<%@ Register src="commoncontrol/config_menu.ascx" tagname="config_menu" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_menu ID="config_menu1" runat="server" />
</asp:Content>
