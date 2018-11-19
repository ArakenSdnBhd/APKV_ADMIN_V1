<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.accessright.aspx.vb" Inherits="kpmkv.admin_accessright1" %>
<%@ Register src="commoncontrol/admin_accessright.ascx" tagname="admin_accessright" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_accessright ID="admin_accessright" runat="server" />
</asp:Content>
