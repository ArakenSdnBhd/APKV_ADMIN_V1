<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.sesi.aspx.vb" Inherits="kpmkv.config_sesi" %>
<%@ Register src="commoncontrol/config_sesi.ascx" tagname="config_sesi" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_sesi ID="config_sesi" runat="server" />
</asp:Content>
