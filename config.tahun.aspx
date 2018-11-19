<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.tahun.aspx.vb" Inherits="kpmkv.config_tahun" %>
<%@ Register src="commoncontrol/config_tahun.ascx" tagname="config_tahun" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_tahun ID="config_tahun" runat="server" />
</asp:Content>
