<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="wajaranakademik.view.aspx.vb" Inherits="kpmkv.wajaranakademik_view1" %>
<%@ Register src="commoncontrol/wajaranakademik_view.ascx" tagname="wajaranakademik_view" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wajaranakademik_view ID="wajaranakademik_view" runat="server" />
</asp:Content>
