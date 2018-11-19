<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kolej.create.aspx.vb" Inherits="kpmkv.kolej_create1" %>
<%@ Register src="commoncontrol/kolej_create.ascx" tagname="kolej_create" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kolej_create ID="kolej_create" runat="server" />
</asp:Content>
