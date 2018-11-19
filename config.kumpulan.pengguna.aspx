<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.kumpulan.pengguna.aspx.vb" Inherits="kpmkv.config_kumpulan_pengguna" %>
<%@ Register src="commoncontrol/config_kumpulan_pengguna.ascx" tagname="config_kumpulan_pengguna" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_kumpulan_pengguna ID="config_kumpulan_pengguna1" runat="server" />
</asp:Content>
