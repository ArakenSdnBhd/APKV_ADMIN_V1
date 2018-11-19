<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="permohonan.berpindah.pensyarah.update.ascx.vb" Inherits="kpmkv.permohonan_berpindah_pensyarah_update" %>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

 <script type="text/javascript">
     $(function () {
         $("[id$=txtEventDate]").datepicker({
             dateFormat: 'dd-mm-yy',
             showOn: 'button',
             buttonImageOnly: true,
             buttonImage: '/icons/calendar.gif'
         });
     });
</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Permohonan Berpindah >> Calon >> Kemaskini Permohonan Berpindah </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Pensyarah</td>
    </tr>
    <tr>
         <td >Nama Pensyarah:</td>
        <td><asp:Label ID="lblNama" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
         <td >Jawatan:</td>
        <td><asp:Label ID="lblJawatan" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
         <td >Gred:</td>
        <td><asp:Label ID="lblGred" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
         <td >Mykad:</td>
        <td><asp:Label ID="lblMYKAD" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
         <td >Telefon:</td>
        <td><asp:Label ID="lblTel" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
         <td >Emel:</td>
        <td><asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
         <td >Jantina:</td>
        <td><asp:Label ID="lblJantina" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
          <td >Kaum:</td>
        <td>
            <asp:Label ID="lblKaum" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
         <td >Agama:</td>
        <td>
            <asp:Label ID="lblAgama" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td >Status:</td>
        <td><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
          Permohonan Berpindah Pensyarah - Pilihan Kolej
        </td>
    </tr>
     <tr>
          <td >Jenis Kolej:</td>
         <td><asp:DropDownList ID="ddlJenisKolej" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>*
        </td>
    </tr>
    <tr>
          <td >Nama Kolej:</td>
         <td><asp:DropDownList ID="ddlNamaKolej" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>*
        </td>
    </tr>
 </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
          Permohonan Berpindah Pensyarah - Pengesahan 
        </td>
    </tr> 
    <tr>
        <td colspan="2">
        <asp:CheckBox ID="chkconfirm" runat="server" text=""/>
           <asp:Label ID="lblConfirm" runat="server" Text="Pensyarah ini telah mendapat kelulusan berpindah daripada bahagian BPTV pada tarikh " Font-Bold="False" ForeColor="Red"></asp:Label>:<asp:TextBox ID="txtEventDate" runat="server" ReadOnly = "true"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Label ID="Label1" runat="server" Text="dan surat rujukan bernombor " ForeColor="Red"></asp:Label>:<asp:TextBox ID="txtRef" runat="server" Width="200px" MaxLength="25"></asp:TextBox>*
</td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnUpdate" runat="server" Text="Simpan" CssClass="fbbutton"/></td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
  <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
