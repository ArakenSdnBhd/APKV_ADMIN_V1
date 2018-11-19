<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kursus.create.aspx.vb" Inherits="kpmkv.kursus_create1" %>
<%@ Register src="commoncontrol/kursus_create.ascx" tagname="kursus_create" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kursus_create ID="kursus_create" runat="server" />
</asp:Content>
