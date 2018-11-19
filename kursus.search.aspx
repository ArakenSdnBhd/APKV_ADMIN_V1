<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kursus.search.aspx.vb" Inherits="kpmkv.kursus_search1" %>
<%@ Register src="commoncontrol/kursus_search.ascx" tagname="kursus_search" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kursus_search ID="kursus_search" runat="server" />
</asp:Content>
