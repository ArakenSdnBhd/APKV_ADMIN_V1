<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="user.view.aspx.vb" Inherits="kpmkv.user_view1" %>
<%@ Register src="commoncontrol/user_view.ascx" tagname="user_view" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:user_view ID="user_view" runat="server" />
</asp:Content>
