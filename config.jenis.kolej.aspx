<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.jenis.kolej.aspx.vb" Inherits="kpmkv.config_jenis_kolej" %>
<%@ Register src="commoncontrol/config_jenis_kolej.ascx" tagname="config_jenis_kolej" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_jenis_kolej ID="config_jenis_kolej" runat="server" />
</asp:Content>
