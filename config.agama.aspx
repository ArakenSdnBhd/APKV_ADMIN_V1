<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.agama.aspx.vb" Inherits="kpmkv.config_agama" %>
<%@ Register src="commoncontrol/config_agama.ascx" tagname="config_agama" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_agama ID="config_agama" runat="server" />
</asp:Content>
