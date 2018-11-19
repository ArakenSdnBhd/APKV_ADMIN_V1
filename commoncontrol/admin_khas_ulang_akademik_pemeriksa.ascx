<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_khas_ulang_akademik_pemeriksa.ascx.vb" Inherits="kpmkv.admin_khas_ulang_akademik_pemeriksa" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Akademik>>Pemeriksa >>Penetapan Pemeriksa</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td>Kod Pusat:</td>
        <td>
            <asp:DropDownList ID="ddlKodPusat" runat="server" AutoPostBack="true" Width="250px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td>
            <asp:Label ID="lblNamaKolej" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Penetapan Pemeriksa</td>
    </tr>
    <tr>
           <td >Tahun Ulang:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
           </td>
    </tr>
      <tr>
         <td >Sesi Pengambilan</td>
                 <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
     <tr>
         <td >MataPelajaran</td>
                 <td><asp:DropDownList ID="ddlMataPelajaran" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
    </tr>
     <tr>
        <td colspan ="2"><asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" /></td>      
    </tr>
      </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Pemeriksa Borang Markah</td>
    </tr>
    <tr>
    <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="10" ForeColor="#333333" GridLines="None" DataKeyNames="PemeriksaID"
                Width="100%" PageSize="15" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="lblTahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Nama Pemeriksa">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaPemeriksa" runat="server" Text='<%# Bind("NamaPemeriksa")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Kod Pusat">
                        <ItemTemplate>
                            <asp:Label ID="lblKodPusat" runat="server" Text='<%# Bind("KodKolej")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Subjek">
                        <ItemTemplate>
                            <asp:Label ID="lblSubjek" runat="server" Text='<%# Bind("Subjek")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Batal"> 
                        <ItemTemplate> 
                            <asp:Button ID="btnBatal" runat="server" Text="-" CommandName="Batal" CommandArgument ='<%#Eval("PemeriksaID")%>'/></td>
                        </ItemTemplate> 
                    </asp:TemplateField> 
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
<table class="fbform">
     <tr class="fbform_header">
        <td>Pemilihan Pemeriksa</td>
         <td><asp:DropDownList ID="ddlPemeriksa" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>&nbsp;<asp:Button ID="btnSimpan" runat="server" Text="Simpan" CssClass="fbbutton" /></td>
    </tr>
     <tr>
        <td colspan="2">
            &nbsp;</td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
</div>