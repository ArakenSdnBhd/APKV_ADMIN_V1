<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kluster.view.ascx.vb" Inherits="kpmkv.kluster_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Carian >> Bidang >> Paparan Bidang</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Bidang</td>
    </tr>
    <tr>
        <td style="width:20%;">Kohort:</td>
        <td ><asp:Label ID="ddlTahun" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Nama Bidang:</td>
        <td><asp:Label ID="lblNama" runat="server"></asp:Label></td>
    </tr>
    
    <tr>
        <td colspan="2"></td>
        
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnExecute" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
</div>