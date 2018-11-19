<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.jantina.aspx.vb" Inherits="kpmkv.config_jantina" %>
<%@ Register src="commoncontrol/config_jantina.ascx" tagname="config_jantina" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_jantina ID="config_jantina" runat="server" />
</asp:Content>
