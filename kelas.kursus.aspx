<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kelas.kursus.aspx.vb" Inherits="kpmkv.kelas_kursus1" %>
<%@ Register src="commoncontrol/kelas_kursus.ascx" tagname="kelas_kursus" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kelas_kursus ID="kelas_kursus" runat="server" />
</asp:Content>
