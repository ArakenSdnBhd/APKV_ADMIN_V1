<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pelajar_search.ascx.vb"
    Inherits="kpmkv.pelajar_search" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian Dan Penyelengaraan>>Calon>>Senarai Calon</td>
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
        <td><asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
</table>
<br />
<table class="fbform">
     <tr class="fbform_header">
        <td colspan="2">Maklumat Program</td>
    </tr>
    <tr>
           <td style="width: 20%;">Kohort:</td>
             <td><asp:DropDownList ID="ddlTahun" runat="server"  Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
           <td style="width: 20%;">Semester:</td>
             <td> <asp:DropDownList ID="ddlSemester" runat="server" Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
         <td style="width: 20%;">Sesi Pengambilan:</td>
             <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Width="350px">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
     <tr>
         <td style="width: 20%;">Kod Program:</td>
         <td><asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td> 
    </tr>
     <tr>
         <td style="width: 20%;">Nama Kelas:</td>
         <td><asp:DropDownList ID="ddlNamaKelas" runat="server" Width="350px"></asp:DropDownList></td> 
    </tr>
      <tr>
        <td style="width: 20%;">Jenis Calon:</td>
        <td><asp:DropDownList ID="ddlJenisCalon" runat="server"  Width="350px"></asp:DropDownList></td> 
    </tr>
</table>
<br />
<table class="fbform">
     <tr class="fbform_header">
        <td colspan="2">Maklumat Calon</td>
    </tr>
<tr>
         <td style="width: 20%;">Nama Calon:</td>
          <td><asp:TextBox ID="txtNama" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
     <tr>
         <td style="width: 20%;">Mykad:</td>
         <td><asp:TextBox ID="txtMYKAD" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
     <tr>
         <td style="width: 20%;">Angka Giliran:</td>
         <td><asp:TextBox ID="txtAngkaGiliran" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
    <tr>
         <td colspan="2"><asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
    </tr>
 </table> 
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Calon
        </td>
    </tr>
    <tr>
        <td>
             <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PelajarID"
                Width="100%" PageSize="50" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Kolej">
                        <ItemTemplate>
                            <asp:Label ID="NamaKolej" runat="server" Text='<%# Bind("NamaKolej")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="Semester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama">
                        <ItemTemplate>
                            <asp:Label ID="NamaPelajar" runat="server" Text='<%# Bind("NamaPelajar")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mykad">
                        <ItemTemplate>
                            <asp:Label ID="Mykad" runat="server" Text='<%# Bind("Mykad")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Angka Giliran">
                        <ItemTemplate>
                            <asp:Label ID="AngkaGiliran" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Program">
                        <ItemTemplate>
                            <asp:Label ID="KodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Kelas">
                        <ItemTemplate>
                            <asp:Label ID="NamaKelas" runat="server" Text='<%# Bind("NamaKelas")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jantina">
                        <ItemTemplate>
                            <asp:Label ID="Jantina" runat="server" Text='<%# Bind("Jantina")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kaum">
                        <ItemTemplate>
                            <asp:Label ID="Kaum" runat="server" Text='<%# Bind("Kaum")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Agama">
                        <ItemTemplate>
                            <asp:Label ID="Agama" runat="server" Text='<%# Bind("Agama")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Jenis Calon">
                        <ItemTemplate>
                            <asp:Label ID="JenisCalon" runat="server" Text='<%# Bind("JenisCalon")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="PILIH" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
    </table>
<br />
<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>
