<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="modul_update.ascx.vb" Inherits="kpmkv.modul_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Kursus
        </td>
     </tr>
</table>
<br />
<table class="fbform">
    <tr>
        <td>Kod Kursus:</td>
        <td>
            <asp:TextBox ID="txtKod" runat="server" Width="87px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Nama Kursus:</td>
        <td>
            <asp:TextBox ID="txtNama" runat="server" Width="631px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Jam Kredit:</td>
        <td>
            <asp:TextBox ID="txtJamKredit" runat="server" Width="74px" MaxLength="250"></asp:TextBox>
            *</td>
    </tr>
    <tr>
        <td>PB Amali:</td>
        <td><asp:TextBox ID="txtPBAmali" runat="server" Width="78px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>PB Teori:</td>
        <td><asp:TextBox ID="txtPBTeori" runat="server" Width="79px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>PA Amali:</td>
        <td><asp:TextBox ID="txtPAAmali" runat="server" Width="77px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>PA Teori:</td>
        <td><asp:TextBox ID="txtPATeori" runat="server" Width="75px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;<asp:Button
                ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" />&nbsp;<asp:LinkButton
                    ID="lnkList" runat="server">|Carian Kursus</asp:LinkButton>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>
<asp:Label ID="lblKod" runat="server" Text="" Visible="false"></asp:Label>
