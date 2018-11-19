<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="prm.tahun.aspx.vb" Inherits="kpmkv.prm_tahun" %>
<%@ Register src="commoncontrol/prm_tahun.ascx" tagname="prm_tahun" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:prm_tahun ID="prm_tahun" runat="server" />
</asp:Content>
