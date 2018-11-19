<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kolej_view.ascx.vb" Inherits="kpmkv.kolej_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Carian Dan Penyelenggaraan >> Kolej >> Paparan Maklumat Kolej.</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Paparan Maklumat Kolej
        </td>
    </tr>
    <tr>
        <td>Jenis Kolej</td>
        <td style="width:2%;">:</td>
        <td ><asp:Label ID="lblJenis" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Kod Kolej</td>
        <td>:</td>
        <td><asp:Label ID="lblKod" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Nama Kolej</td>
        <td>:</td>
        <td><asp:Label ID="lblNama" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Telefon</td>
        <td>:</td>
        <td><asp:Label ID="lblTel" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Fax</td>
        <td>:</td>
        <td><asp:Label ID="lblFax" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Emel</td>
        <td>:</td>
        <td><asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Alamat</td>
        <td>:</td>
        <td><asp:Label ID="lblAlamat1" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td><asp:Label ID="lblAlamat2" runat="server" Text=""></asp:Label></td>
    </tr>
     <tr>
        <td>Poskod</td>
         <td>:</td>
        <td><asp:Label ID="lblPoskod" runat="server" Text=""></asp:Label></td>
    </tr>
     <tr>
        <td>Bandar</td>
         <td>:</td>
        <td><asp:Label ID="lblBandar" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td class="auto-style3">Negeri</td>
        <td>:</td>
        <td><asp:Label ID="lblNegeri" runat="server" Text=""></asp:Label> </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="3">Maklumat Lain
        </td>
    </tr>
    <tr>
        <td>Nama Pengarah</td>
        <td>:</td>
        <td><asp:Label ID="lblNamaPengarah" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Emel</td>
        <td>:</td>
        <td><asp:Label ID="lblEmailPengarah" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Telefon Bimbit</td>
        <td>:</td>
        <td><asp:Label ID="lblBimbitPengarah" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Telefon</td>
        <td>:</td>
        <td><asp:Label ID="lblTelPengarah" runat="server" Text=""></asp:Label> </td>
    </tr>
    <tr>
        <td colspan="3">&nbsp;</td>
        
    </tr>
    <tr>
        <td>Nama KJPP</td>
        <td>:</td>
        <td><asp:Label ID="lblNamaKJPP" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Emel</td>
        <td>:</td>
        <td><asp:Label ID="lblEmailKJPP" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Telefon Bimbit</td>
        <td>:</td>
        <td><asp:Label ID="lblBimbitKJPP" runat="server"></asp:Label><td>
    </tr>
    <tr>
        <td>Telefon</td>
        <td>:</td>
        <td><asp:Label ID="lblTelKJPP" runat="server"></asp:Label> </td>
    </tr>
    <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
    <tr>
        <td>Nama SUP</td>
        <td>:</td>
        <td><asp:Label ID="lblNamaSUP" runat="server"></asp:Label></td>
    </tr>
     <tr>
        <td>Jawatan</td>
         <td>:</td>
        <td><asp:Label ID="lblJawatanSUP" runat="server"></asp:Label></td>
    </tr>
    <tr>
         <td>Gred</td>
        <td>:</td>
         <td><asp:Label ID="lblGredSUP" runat="server"></asp:Label></td>
    </tr>
    <tr>
         <td>Emel</td>
        <td>:</td>
         <td><asp:Label ID="lblEmailSUP" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Telefon Bimbit</td>
        <td>:</td>
         <td><asp:Label ID="lblBimbitSUP" runat="server"></asp:Label></td>
    </tr>
    <tr>
          <td>Telefon</td>
        <td>:</td>
         <td><asp:Label ID="lblTelSUP" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="3">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnExecute" runat="server" Text="Kemaskini" CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
          <asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>
