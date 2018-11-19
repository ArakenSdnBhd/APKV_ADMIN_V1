<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.semester.aspx.vb" Inherits="kpmkv.config_semester" %>
<%@ Register src="commoncontrol/config_semester.ascx" tagname="config_semester" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_semester ID="config_semester" runat="server" />
</asp:Content>
