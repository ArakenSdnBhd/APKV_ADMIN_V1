<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pelajar_create.ascx.vb" Inherits="kpmkv.pelajar_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pendaftaran >> Calon Baru</td>
    </tr>
    </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td >Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" Width="350px"></asp:DropDownList></td>
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
        <td colspan="2">Maklumat Calon</td>
    </tr>
    <tr>
         <td >Peringkat Pengajian:</td>
         <td><asp:Label ID="Label1" runat="server" Text="PRA DIPLOMA"></asp:Label></td>
    </tr>
    <tr>
        <td >Kohort:</td>
         <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
          <td >Semester:</td>
         <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
         <td >Sesi Pengambilan:</td>
         <td><asp:CheckBoxList ID="chkSesi" runat="server" Width="349px" RepeatDirection="Horizontal" AutoPostBack="true">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>

    </tr>
     <tr>
         <td >Nama Bidang:</td>
        <td><asp:DropDownList ID="ddlKluster" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td> 
    </tr>
   <tr>
         <td >Kod Program:</td>
         <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="true" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
          <td>Nama Kelas:</td>
         <td>
            <asp:DropDownList ID="ddlNamaKelas" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
         </td>
    </tr>
    <tr>
         <td colspan ="2"></td>
    </tr>
    <tr>
          <td >Nama Calon: </td>
         <td>
            <asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
          <td >Mykad:</td>
         <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
          <td >Jantina:</td>
         <td><asp:CheckBoxList ID="chkJantina" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>LELAKI</asp:ListItem>
             <asp:ListItem>PEREMPUAN</asp:ListItem>
             </asp:CheckBoxList>   
    </tr>
    <tr>
          <td >Kaum:</td>
         <td>
            <asp:DropDownList ID="ddlKaum" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
         <td >Agama:</td>
         <td><asp:CheckBoxList ID="chkAgama" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>ISLAM</asp:ListItem>
             <asp:ListItem>LAIN-LAIN</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
    <tr>
         <td >Emel:</td>
         <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>

    <tr>
         <td >Catatan
            :</td>
         <td>
            <asp:TextBox ID="txtCatatan" runat="server" Width="350px" MaxLength="250" Height="117px"></asp:TextBox>
        </td>
    </tr>
     <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnCreate" runat="server" Text="Simpan" CssClass="fbbutton" />&nbsp;</td>
    </tr>
</table>

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    
</div>
