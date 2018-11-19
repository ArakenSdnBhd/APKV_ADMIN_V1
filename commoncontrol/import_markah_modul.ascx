<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="import_markah_modul.ascx.vb" Inherits="kpmkv.import_modul_markah1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Vokasional >> Import Markah Vokasional >> </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
       <td style="width: 20%;">Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 20%;">Jenis Kolej:</td>
        <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList>
    </tr>
    <tr>
       <td style="width: 20%;">Nama Kolej:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
    </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pentaksiran Vokasional</td>
    </tr>
     <tr>
         <td style="width: 20%;">Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
         <td style="width: 20%;">Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" Width="350px"></asp:DropDownList>
        </td>
    </tr>
   
     <tr>
         <td style="width: 20%;">Sesi Pengambilan:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
     <tr>
         <td style="width: 20%;">Kod Program:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>        
       <td style="width: 20%;">Kelas:</td>
          <td><asp:DropDownList ID="ddlKelas" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
   <tr>
        <td style="width: 20%;">Markah: </td>
         <td><asp:CheckBoxList ID="chkResult" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
             <asp:ListItem Value="PB">Pentaksiran Berterusan</asp:ListItem>
             <asp:ListItem Value="PA">Penilaian Akhir</asp:ListItem>
             </asp:CheckBoxList>
         </td>
    </tr>
   </table>
<br />
<div id="divImport" runat="server">
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Import Markah Vokasional</td>
    </tr>
    <tr>
         <td colspan ="2">MuatNaik Format Fail Excel:
         <asp:Button ID="btnFile" runat="server" Text="Excel" CssClass="fbbutton" Height="25px" Width="116px" /></td>
    </tr>
    <tr>
        <td style="width: 20%;">Pilih Fail Excel:
        </td>
         <td>
            <asp:FileUpload ID="FlUploadcsv" runat="server" />&nbsp;
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed"
                ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpload" runat="server" Text="Muatnaik " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
    </table>
    </div> 

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    
</div>