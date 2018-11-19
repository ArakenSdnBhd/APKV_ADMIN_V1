<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="gred_bmsetara_update.ascx.vb" Inherits="kpmkv.WebUserControl1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi >> Gred BM Setara >> Kemaskini</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Paparan Maklumat Gred BM Setara
        </td>
    </tr>
    <tr>
          <td >Tahun:</td>
          <td> <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
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
         <td style="color: #FF0000"><asp:TextBox ID="txtMarkah" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*[-1-100]</td>
    </tr>
     <tr>
         <td >Markah Akhir:</td>
         <td style="color: #FF0000"><asp:TextBox ID="txtMarkahTo" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*[-1-100]</td>
    </tr>
    <tr>
           <td>Gred:</td>
           <td><asp:TextBox ID="txtGred" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
         <td>Status:</td>
         <td><asp:TextBox ID="txtStatus" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
           <td>Kompetensi:</td>
           <td><asp:TextBox ID="txtKompetensi" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
           <td >Jenis:</td>
           <td><asp:TextBox ID="txtJenis" runat="server" Width="350px" MaxLength="50" Enabled="false"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp; &nbsp;<asp:Button
                ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" />&nbsp;&nbsp; <asp:Button ID="btlist" runat="server" Text="Senarai Gred BM Setara" CssClass="fbbutton" />
        </td>
    </tr>
   
</table>
<br />
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    <asp:Label ID="lblGred" runat="server" Text="" Visible="false"></asp:Label>
    
</div>