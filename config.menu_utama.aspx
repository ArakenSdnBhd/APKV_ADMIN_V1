<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.menu_utama.aspx.vb" Inherits="kpmkv.config_menu_utama" %>
<%@ Register src="commoncontrol/config_menu_utama.ascx" tagname="config_menu_utama" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_menu_utama ID="config_menu_utama1" runat="server" />
</asp:Content>
