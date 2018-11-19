<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="gred_view.ascx.vb" Inherits="kpmkv.gred_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi >> Gred Akademik Dan Vokasional</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Gred 
        </td>
    </tr>
    <tr>
         <td>Markah Mula:</td>
         <td><asp:Label ID="lblMarkahMula" runat="server" Text=""></asp:Label></td>
    </tr>
     <tr>
         <td>Markah Akhir:</td>
         <td><asp:Label ID="lblMarkahAkhir" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
           <td>Gred:</td>
           <td><asp:Label ID="lblGred" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
           <td>Pointer:</td>
           <td><asp:Label ID="lblPointer" runat="server" Text=""></asp:Label></td>
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
        <td colspan="2"><asp:Button ID="btnExecute" runat="server" Text="Kemaskini" CssClass="fbbutton" />
        </td>
    </tr>
   
</table>
<br />
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblGredbsID" runat="server" Text="" Visible="false"></asp:Label>
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>