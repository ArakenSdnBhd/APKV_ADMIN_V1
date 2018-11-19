<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="matapelajaran_update.ascx.vb" Inherits="kpmkv.matapelajaran_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat MataPelajaran</td>
    </tr>
    <tr>
        <td>Kod MataPelajaran:</td>
        <td>
            <asp:TextBox ID="txtKodMataPelajaran" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            *
        </td>
    </tr>
    <tr>
        <td>Nama MataPelajaran:</td>
        <td>
            <asp:TextBox ID="txtNamaMataPelajaran" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            *
        </td>
    </tr>
    <tr>
        <td>Jam Kredit:</td>
        <td>
            <asp:TextBox ID="txtJamKredit" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            *</td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;<asp:Button
                ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" />&nbsp;<asp:LinkButton
                    ID="lnkList" runat="server">|Carian MataPelajaran</asp:LinkButton>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>
<asp:Label ID="lblKodMataPelajaran" runat="server" Text="" Visible="false"></asp:Label>
