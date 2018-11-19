<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.penetapan.pemeriksa.aspx.vb" Inherits="kpmkv.admin_penetapan_pemeriksa" %>
<%@ Register src="commoncontrol/penetapan_pemeriksa_bmsetara.ascx" tagname="penetapan_pemeriksa_bmsetara" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:penetapan_pemeriksa_bmsetara ID="penetapan_pemeriksa_bmsetara1" runat="server" />
</asp:Content>
