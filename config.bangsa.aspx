<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.bangsa.aspx.vb" Inherits="kpmkv.config_bangsa" %>
<%@ Register src="commoncontrol/config_bangsa.ascx" tagname="config_bangsa" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_bangsa ID="config_bangsa" runat="server" />
</asp:Content>
