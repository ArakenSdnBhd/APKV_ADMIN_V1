<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="modul.view.aspx.vb" Inherits="kpmkv.modul_view1" %>
<%@ Register src="commoncontrol/modul_view.ascx" tagname="modul_view" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:modul_view ID="modul_view" runat="server" />
</asp:Content>
