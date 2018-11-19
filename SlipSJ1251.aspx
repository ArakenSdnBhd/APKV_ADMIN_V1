<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="SlipSJ1251.aspx.vb" Inherits="kpmkv.SlipSJ1251" %>

<%@ Register Src="~/commoncontrol/slip_SJ1251.ascx" TagPrefix="uc1" TagName="slip_SJ1251" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:slip_SJ1251 runat="server" id="slip_SJ1251" />
</asp:Content>
