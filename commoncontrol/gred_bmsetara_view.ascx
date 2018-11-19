<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="gred_bmsetara_view.ascx.vb" Inherits="kpmkv.gred_bmsetara_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi >> Gred BM Setara </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Paparan Maklumat Gred BM Setara</td>
    </tr>
    <tr>
          <td>Tahun:</td>
          <td><asp:Label ID="lblTahun" runat="server" Text=""></asp:Label></td>
    </tr>
     <tr>
          <td>Sesi:</td>
          <td><asp:Label ID="lblSesi" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
         <td>Markah Mula:</td>
         <td><asp:Label ID="lblMarkahFrom" runat="server" Text=""></asp:Label></td>
        <td style="color: #FF0000">*[-1-100]</td>
    </tr>
     <tr>
         <td>Markah Akhir:</td>
         <td><asp:Label ID="lblMarkahTo" runat="server" Text=""></asp:Label></td>
         <td style="color: #FF0000">*[-1-100]</td>
    </tr>
    <tr>
           <td>Gred:</td>
           <td><asp:Label ID="lblGred" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
         <td>Status:</td>
         <td><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></td>      
    </tr>
    <tr>
           <td>Kompetensi:</td>
           <td><asp:Label ID="lblKompetensi" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
           <td>Jenis:</td>
           <td><asp:Label ID="lblJenis" runat="server" Text="" Enabled="false"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnExecute" runat="server" Text="Kemaskini" CssClass="fbbutton" />
        </td>
    </tr>
   
</table>
<br />
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblGredbsID" runat="server" Text="" Visible="false"></asp:Label>
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>