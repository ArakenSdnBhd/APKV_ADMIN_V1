<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kolej.search.aspx.vb" Inherits="kpmkv.kolej_search1" %>
<%@ Register src="commoncontrol/kolej_search.ascx" tagname="kolej_search" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kolej_search ID="kolej_search" runat="server" />
</asp:Content>
