<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kluster.create.ascx.vb" Inherits="kpmkv.kluster_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Pendaftaran >> Bidang</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pendaftaran Bidang</td>
    </tr>
     <tr>
        <td>Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Bidang:</td>
        <td>
            <asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    
    <tr>
        <td colspan ="2">
            <asp:Button ID="btnCreate" runat="server" Text="Daftar Baru" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>