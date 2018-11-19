<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wajaranvokasional_view.ascx.vb" Inherits="kpmkv.wajaranvokasional_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Paparan Maklumat Wajaran Vokasional
        </td>
    </tr>
    <tr>
          <td >Tahun</td>
         <td style="text-align:center; width:1%;">:</td>
         <td><asp:Label ID="lblTahun" runat="server" Text=""></asp:Label></td>
    </tr>

    <tr>
         <td >Subjek</td>
         <td style="text-align:center; width:1%;">:</td>
         <td><asp:Label ID="lblSubjek" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
           <td >Teori</td>
           <td style="text-align:center; width:1%;">:</td>
           <td><asp:Label ID="lblTeori" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
         <td >Amali</td>
         <td style="text-align:center; width:1%;">:</td>
         <td><asp:Label ID="lblAmali" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
           <td >Jenis</td>
           <td style="text-align:center; width:1%;">:</td>
           <td><asp:Label ID="lblJenis" runat="server" Text="" Enabled="false"></asp:Label></td>
    </tr>
    
    <tr>
        <td class="fbform_sap" colspan="3">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnExecute" runat="server" Text="Kemaskini" CssClass="fbbutton" />
        </td>
    </tr>
   
</table>
<br />
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblWajaranID" runat="server" Text="" Visible="false"></asp:Label>
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>