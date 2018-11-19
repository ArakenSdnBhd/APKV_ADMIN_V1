<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pengurusan.semester.calon.aspx.vb" Inherits="kpmkv.pengurusan_semester_calon" %>
<%@ Register src="commoncontrol/pengurusan_semester_calon.ascx" tagname="pengurusan_semester_calon" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pengurusan_semester_calon ID="pengurusan_semester_calon1" runat="server" />
</asp:Content>
