<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kolej.list.aspx.vb" Inherits="kpmkv.kolej_list1" %>
<%@ Register src="commoncontrol/kolej_list.ascx" tagname="kolej_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kolej_list ID="kolej_list" runat="server" />
</asp:Content>
