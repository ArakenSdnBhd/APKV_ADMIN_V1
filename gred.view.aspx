<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="gred.view.aspx.vb" Inherits="kpmkv.gred_view1" %>
<%@ Register src="commoncontrol/gred_view.ascx" tagname="gred_view" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:gred_view ID="gred_view" runat="server" />
</asp:Content>
