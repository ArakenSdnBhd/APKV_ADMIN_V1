<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.wajaranvokasional.search.aspx.vb" Inherits="kpmkv.admin_wajaranvokasional_search" %>
<%@ Register src="commoncontrol/wajaranvokasional_search.ascx" tagname="wajaranvokasional_search" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wajaranvokasional_search ID="wajaranvokasional_search" runat="server" />
</asp:Content>
