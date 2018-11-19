<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="gred_create.ascx.vb" Inherits="kpmkv.gred_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi >> Gred Akademik Dan Vokasional >> Pendaftaran Baru</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Paparan Maklumat Akademik Dan Vokasional 
        </td>
    </tr>
    <tr>
         <td>Markah Mula:</td>
         <td style="color: #FF0000"><asp:TextBox ID="txtMarkahMula" runat="server" Width="350px" MaxLength="350"></asp:TextBox>*[-1-100]</td>
    </tr>
     <tr>
         <td>Markah Akhir:</td>
         <td style="color: #FF0000"><asp:TextBox ID="txtMarkahAkhir" runat="server" Width="350px" MaxLength="350"></asp:TextBox>*[-1-100]</td>
    </tr>
    <tr>
           <td>Gred:</td>
           <td><asp:TextBox ID="txtGred" runat="server" Width="350px" MaxLength="350"></asp:TextBox></td>
    </tr>
    <tr>
           <td>Pointer:</td>
           <td><asp:TextBox ID="txtPointer" runat="server" Width="350px" MaxLength="350"></asp:TextBox></td>
    </tr>
    <tr>
         <td>Status:</td>
         <td><asp:TextBox ID="txtStatus" runat="server" Width="350px" MaxLength="350"></asp:TextBox></td>
    </tr>
    <tr>
           <td>Kompetensi:</td>
           <td><asp:TextBox ID="txtKompetensi" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
           <td>Jenis:</td>
           <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnDaftar" runat="server" Text="Daftar Baru" CssClass="fbbutton" />&nbsp;<asp:Button ID="btnList" runat="server" Text="Senarai Gred" CssClass="fbbutton" />
        </td>
    </tr>
   
</table>
<br />
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    <asp:Label ID="lblGred" runat="server" Text="" Visible="false"></asp:Label>
    
</div>