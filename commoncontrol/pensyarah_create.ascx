<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pensyarah_create.ascx.vb" Inherits="kpmkv.pensyarah_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pendaftaran >> Pensyarah</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td >Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Jenis Kolej:</td>
        <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Pensyarah</td>
    </tr>
     <tr>
         <td >Nama Pensyarah:</td>
         <td><asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td > Mykad:</td>
         <td> <asp:TextBox ID="txtMYKAD" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    
    <tr>
        <td >Jawatan:</td>
         <td> <asp:TextBox ID="txtJawatan" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
          <td >Gred:</td>
         <td><asp:TextBox ID="txtGred" runat="server" Width="350px" MaxLength="150"></asp:TextBox>*</td>
    </tr>
    <tr>
          <td >Telefon:</td>
         <td><asp:TextBox ID="txtTel" runat="server" Width="350px" MaxLength="150"></asp:TextBox>*</td>
    </tr>
    <tr>
         <td > Emel:</td>
         <td><asp:TextBox ID="txtEmail" runat="server" Width="350px" MaxLength="150"></asp:TextBox>*</td>
    </tr>
     <tr>
     <td >Jantina:</td>
      <td><asp:CheckBoxList ID="chkJantina" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>LELAKI</asp:ListItem>
             <asp:ListItem>PEREMPUAN</asp:ListItem>
             </asp:CheckBoxList>  
     </td> 
    </tr>
    <tr>
      <td >Kaum:</td>
      <td><asp:DropDownList ID="ddlKaum" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
         <td >Agama:</td>
         <td><asp:CheckBoxList ID="chkAgama" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>ISLAM</asp:ListItem>
             <asp:ListItem>BUKAN ISLAM</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
    <tr> 
        <td colspan="2"></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnCreate" runat="server" Text="Simpan" CssClass="fbbutton" />&nbsp;</td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>



