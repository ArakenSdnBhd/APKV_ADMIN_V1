<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.wajaran.search.aspx.vb" Inherits="kpmkv.admin_wajaran_search" %>
<%@ Register src="commoncontrol/wajaran_search.ascx" tagname="wajaran_search" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wajaran_search ID="wajaran_search" runat="server" />
</asp:Content>
