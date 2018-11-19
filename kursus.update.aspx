<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kursus.update.aspx.vb" Inherits="kpmkv.kursus_update1" %>
<%@ Register src="commoncontrol/kursus_update.ascx" tagname="kursus_update" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kursus_update ID="kursus_update" runat="server" />
</asp:Content>
