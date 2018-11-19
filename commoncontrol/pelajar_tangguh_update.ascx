<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pelajar_tangguh_update.ascx.vb" Inherits="kpmkv.pelajar_tangguh_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Calon</td>
    </tr>
    <tr><td  colspan="2"></td></tr>
     <tr>
         <td >Kolej:</td>
        <td><asp:Label ID="lblKolej" runat="server"></asp:Label><asp:Label ID="lblRecordID" runat="server" Visible ="false" ></asp:Label></td>
    </tr>
     <tr>
         <td >Peringkat Pengajian:</td>
        <td><asp:Label ID="lblPengajian" runat="server"></asp:Label></td>
    </tr>
     <tr>
         <td colspan ="2"></td>
    </tr>
    <tr>
         <td >Kohort:</td>
         <td><asp:Label ID="lblTahun" runat="server"></asp:Label></td>
    </tr>
    <tr>
          <td >Sesi Pengambilan:</td>
         <td><asp:Label ID="lblSesi" runat="server"></asp:Label></td>
    </tr>
    <tr>
         <td>Nama Bidang:</td>
         <td><asp:Label ID="lblNamaKluster" runat="server"></asp:Label></td>
    </tr>
    <tr>
         <td >Kod Program:</td>
         <td> <asp:Label ID="lblKodKursus" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
          <td >Nama Program:</td>
         <td><asp:Label ID="lblNamaKursus" runat="server"></asp:Label></td>
    </tr>
    <tr>
          <td >Nama Kelas:</td>
         <td>
            <asp:Label ID="lblNamaKelas" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
         <td colspan="2"></td>
    </tr>
     <tr>
          <td >Nama Calon: </td>
         <td>
            <asp:Label ID="lblNama" runat="server" Width="450px"></asp:Label>
        </td>
    </tr>
    <tr>
          <td >Mykad:</td>
         <td><asp:Label ID="lblMykad" runat="server"></asp:Label></td>
    </tr>
     <tr>
         <td >AngkaGiliran:</td>
         <td><asp:Label ID="lblAngkaGiliran" runat="server"></asp:Label></td>
    </tr>
    <tr>
          <td >Jantina:</td>
         <td><asp:Label ID="lblJantina" runat="server"></asp:Label></td>
         
    </tr>
    <tr>
          <td >Kaum:</td>
         <td><asp:Label ID="lblkaum" runat="server"></asp:Label></td>
    </tr>
    <tr>
         <td >Agama:</td>
         <td><asp:Label ID="lblAgama" runat="server"></asp:Label></td>
    </tr>
    <tr>
         <td >Emel:</td>
         <td>
            <asp:Label ID="txtEmail" runat="server" Width="450px"></asp:Label>
        </td>
    </tr>
      <tr>
         <td >Telefon:</td>
         <td>
            <asp:Label ID="lbltelefon" runat="server" Width="450px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>

    <tr>
          <td >Catatan:</td>
         <td>
            <asp:Textbox ID="txtCatatan" runat="server" Width="460px" Height="117px" TextMode="MultiLine"></asp:Textbox>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Daftar Calon Tangguh</td>
    </tr>
     <tr>
         <td >Kohort:</td>
         <td> <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
     <tr>
         <td >Semester:</td>
         <td> <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
         <td >Sesi Pengambilan:</td>
         <td><asp:CheckBoxList ID="chkSesi" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
      <tr>
        <td colspan="2"><asp:Button ID="btnCreate" runat="server" Text="Simpan" CssClass="fbbutton" /></td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblKod" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>


