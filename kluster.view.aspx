<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kluster.view.aspx.vb" Inherits="kpmkv.kluster_view1" %>
<%@ Register src="commoncontrol/kluster.view.ascx" tagname="kluster" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kluster ID="kluster1" runat="server" />
</asp:Content>
