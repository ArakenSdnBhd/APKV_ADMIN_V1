<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="gred_update.ascx.vb" Inherits="kpmkv.gred_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi >> Gred Akademik Dan Vokasional >> Kemaskini</td>
    </tr>
</table>
<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Paparan Maklumat Gred Akademik Dan Vokasional
        </td>
    </tr>
    <tr>
         <td>Markah Mula:</td>
         <td style="color: #FF0000"><asp:TextBox ID="txtMarkahmula" runat="server" Width="350px" MaxLength="350"></asp:TextBox>*[-1-100]</td>
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
        <td colspan="3"><asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;<asp:Button
                ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" /> &nbsp;<asp:Button
                ID="btnList" runat="server" Text="Senarai Gred" CssClass="fbbutton" />
        </td>
    </tr>
   
</table>
<br />
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    <asp:Label ID="lblGred" runat="server" Text="" Visible="false"></asp:Label>
    
</div>