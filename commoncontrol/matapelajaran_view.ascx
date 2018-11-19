<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="matapelajaran_view.ascx.vb" Inherits="kpmkv.matapelajaran_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat MataPelajaran</td>
    </tr>
    <tr>
        <td>Kod MataPelajaran:</td>
        <td>
            <asp:Label ID="lblKodMataPelajaran" runat="server"></asp:Label>
            *
        </td>
    </tr>
    <tr>
        <td>Nama MataPelajaran:</td>
        <td>
            <asp:Label ID="lblNamaMataPelajaran" runat="server"></asp:Label>
            *
        </td>
    </tr>
    <tr>
        <td>Jam Kredit:</td>
        <td>
            <asp:Label ID="lblJamKredit" runat="server"></asp:Label>
            *</td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnExecute" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>
