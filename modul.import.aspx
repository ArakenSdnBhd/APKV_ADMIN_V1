<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="modul.import.aspx.vb" Inherits="kpmkv.modul_import" %>
<%@ Register src="commoncontrol/modul.import.create.ascx" tagname="modul" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:modul ID="modul1" runat="server" />
</asp:Content>
