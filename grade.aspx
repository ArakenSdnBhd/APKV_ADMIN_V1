<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="grade.aspx.vb" Inherits="kpmkv.grade" %>
<%@ Register src="commoncontrol/admin_gred.ascx" tagname="admin_gred" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_gred ID="admin_gred1" runat="server" />
</asp:Content>
