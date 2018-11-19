<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="gred_bmsetara_create.ascx.vb" Inherits="kpmkv.gred_bmsetara_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi >> Gred BM Setara >> Pendaftaran Baru</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Gred BM Setara
        </td>
    </tr>
    <tr>
          <td>Kohort:</td>
          <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
     <tr>
          <td>Sesi:</td>
          <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
      <tr>
         <td >Markah Mula:</td>
         <td style="color: #FF0000"><asp:TextBox ID="txtMarkahFrom" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*[-1-100]</td>
    <tr>
         <td>Markah Akhir:</td>
         <td style="color: #FF0000"><asp:TextBox ID="txtMarkahTo" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*[-1-100]</td>
    </tr>
    <tr>
           <td >Gred:</td>
           <td><asp:TextBox ID="txtGred" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
   </tr>
    <tr>
         <td >Status:</td>
         <td><asp:TextBox ID="txtStatus" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>    
    </tr>
    <tr>
           <td >Kompetensi:</td>
           <td><asp:TextBox ID="txtKompetensi" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
           <td >Jenis</td>
           <td><asp:TextBox ID="txtJenis" runat="server" Width="350px" MaxLength="50" Enabled="false"></asp:TextBox></td>
    </tr>
    <tr>
        <td></td>
        <td><asp:Button ID="btnDaftar" runat="server" Text="Daftar Baru" CssClass="fbbutton" />   &nbsp;<asp:Button ID="btlist" runat="server" Text="Senarai Gred BM Setara" CssClass="fbbutton" />
        </td>
    </tr>
   
</table>
<br />
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    <asp:Label ID="lblGred" runat="server" Text="" Visible="false"></asp:Label>
    
</div>