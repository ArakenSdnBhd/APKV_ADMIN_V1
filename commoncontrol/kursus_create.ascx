<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kursus_create.ascx.vb" Inherits="kpmkv.kursus_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pendaftaran >> Program</td>
    </tr>
</table>
<br />
<table class="fbform">
     <tr>
        <td>Kohort:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server"  Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Bidang:</td>
        <td><asp:DropDownList ID="ddlNamaKluster" runat="server"  Width="350px"></asp:DropDownList> </td>
    </tr>
    <tr>
          <td >Sesi Pengambilan:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
     <tr>
        <td >Jenis Program:</td>
        <td><asp:CheckBoxList ID="chkJenisProgram" runat="server"  Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>SOCIAL</asp:ListItem>
             <asp:ListItem>TEKNOLOGI </asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
    <tr>
        <td>Kod Program:</td>
        <td><asp:TextBox ID="txtKod" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Nama Program:</td>
        <td><asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnCreate" runat="server" Text="Daftar Program" CssClass="fbbutton" /></td>
    </tr> 
</table>

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <br />
</div>