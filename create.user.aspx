<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="create.user.aspx.vb" Inherits="kpmkv.create_user1" %>
<%@ Register src="commoncontrol/create_user.ascx" tagname="create_user" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:create_user ID="create_user" runat="server" />
</asp:Content>
