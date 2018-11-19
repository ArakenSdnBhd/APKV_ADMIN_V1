<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kursus.list.year.aspx.vb" Inherits="kpmkv.kursus_list_year" %>
<%@ Register src="commoncontrol/kursus.list.by.year.ascx" tagname="kursus" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kursus ID="kursus1" runat="server" />
</asp:Content>
