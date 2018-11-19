<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kluster.update.ascx.vb" Inherits="kpmkv.kluster_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Carian >> Bidang >> Kemaskini Maklumat Bidang</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Bidang</td>
    </tr>
     <tr>
        <td style="width:20%;">Kohort:</td>
        <td > <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"> </asp:DropDownList></td>
    </tr>
    <tr>
        <td>Nama Bidang:</td>
        <td>
            <asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    
    <tr>
        <td colspan ="2"></td>
    </tr>
      <tr>
       
        <td colspan ="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;<asp:Button
                ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>
<asp:Label ID="lblNamaKluster" runat="server" Text="" Visible="false"></asp:Label>

