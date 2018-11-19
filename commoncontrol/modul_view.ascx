<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="modul_view.ascx.vb" Inherits="kpmkv.modul_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Kursus</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr>
         <td style="width: 20%;">Kod Kursus:</td>
        <td> <asp:Label ID="lblKod" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%;">Nama Kursus:</td>
        <td><asp:Label ID="lblNama" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
         <td style="width: 20%;">Jam Kredit:</td>
        <td><asp:Label ID="lblJamKredit" runat="server"></asp:Label></td>
    </tr>
     <tr>
       <td style="width: 20%;">PB Amali:</td>
        <td><asp:Label ID="lblPBAmali" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%;">PB Teori:</td>
        <td><asp:Label ID="lblPBTeori" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%;">PA Amali:</td>
        <td><asp:Label ID="lblPAAmali" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%;">PA Teori:</td>
        <td><asp:Label ID="lblPATeori" runat="server"></asp:Label></td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnExecute" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>