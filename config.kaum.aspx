<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.kaum.aspx.vb" Inherits="kpmkv.config_kaum" %>
<%@ Register src="commoncontrol/config_kaum.ascx" tagname="config_kaum" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_kaum ID="config_kaum" runat="server" />
</asp:Content>
