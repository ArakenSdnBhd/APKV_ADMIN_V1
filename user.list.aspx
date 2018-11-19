<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="user.list.aspx.vb" Inherits="kpmkv.user_list1" %>
<%@ Register src="commoncontrol/user_list.ascx" tagname="user_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:user_list ID="user_list" runat="server" />
</asp:Content>
