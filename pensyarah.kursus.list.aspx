<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pensyarah.kursus.list.aspx.vb" Inherits="kpmkv.pensyarah_kursus_list1" %>
<%@ Register src="commoncontrol/pensyarah.kursus.list.ascx" tagname="pensyarah" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pensyarah ID="pensyarah1" runat="server" />
</asp:Content>
