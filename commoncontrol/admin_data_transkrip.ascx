<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_data_transkrip.ascx.vb" Inherits="kpmkv.admin_data_transkrip" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Data Transkrip</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2"></td>
    </tr>
        <tr>
        <td>Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" Width="350px" AutoPostBack="true"></asp:DropDownList></td>
    </tr>
     <tr>
        <td>Nama Kolej:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
     <tr>
          <td >BM Setara Pada Tahun:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server"  Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
          <td >Sesi Pengambilan:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
 </table> 
<br />
<div class="info" id="divMsgResult" runat="server">
  <asp:Label ID="lblMsgResult" runat="server" Text="Mesej..."></asp:Label>
</div>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Calon
        </td>
    </tr>
    <tr>
       <td>Bilangan Pelajar : <asp:Label ID="lblPelajar" runat="server" Text=""></asp:Label></td>
    </tr>
   <tr>
  <br />
        <td>
            <asp:Button ID="btnKompeten" runat="server"  Text="Transkrip Kompeten" CssClass="fbbutton" Visible="true" />&nbsp;
            <asp:Button ID="btnXKompeten" runat="server" Text="Transkrip X Kompeten" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">

<asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>