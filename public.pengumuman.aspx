<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="public.pengumuman.aspx.vb" Inherits="kpmkv.public_pengumuman" %>
<%@ Register src="commoncontrol/pengumuman_list_pub.ascx" tagname="pengumuman_list_pub" tagprefix="uc3" %>
<%@ Register src="commoncontrol/pengumuman_view_pub.ascx" tagname="pengumuman_view_pub" tagprefix="uc2" %>
<%@ Register src="commoncontrol/pengumuman_view_top.ascx" tagname="pengumuman_view_top" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pengumuman_view_top ID="pengumuman_view_top1" runat="server" />
    <uc3:pengumuman_list_pub ID="pengumuman_list_pub1" runat="server" />
</asp:Content>
