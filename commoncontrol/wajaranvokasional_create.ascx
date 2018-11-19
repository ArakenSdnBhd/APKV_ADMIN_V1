<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wajaranvokasional_create.ascx.vb" Inherits="kpmkv.wajaranvokasional_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Paparan Maklumat Wajaran Vokasional
        </td>
    </tr>
    <tr>
          <td >Tahun</td>
         <td style="text-align:center; width:1%;">:</td>
         <td> <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
         <td >Subjek</td>
         <td style="text-align:center; width:1%;">:</td>
         <td><asp:TextBox ID="txtSubjek" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
           <td >Teori</td>
           <td style="text-align:center; width:1%;">:</td>
           <td><asp:TextBox ID="txtTeori" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
         <td >Amali</td>
         <td style="text-align:center; width:1%;">:</td>
         <td><asp:TextBox ID="txtAmali" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
           <td >Jenis</td>
           <td style="text-align:center; width:1%;">:</td>
           <td><asp:TextBox ID="txtJenis" runat="server" Width="350px" MaxLength="50" Enabled="false"></asp:TextBox></td>
    </tr>
    
    <tr>
        <td class="fbform_sap" colspan="3">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />
        </td>
    </tr>
   
</table>
<br />
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    <asp:Label ID="lblSubjek" runat="server" Text="" Visible="false"></asp:Label>
    
</div>