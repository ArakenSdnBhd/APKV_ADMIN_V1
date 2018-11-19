<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kursus.import.aspx.vb" Inherits="kpmkv.kursus_import1" %>
<%@ Register src="commoncontrol/kursus_import.ascx" tagname="kursus_import" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kursus_import ID="kursus_import" runat="server" />
</asp:Content>
