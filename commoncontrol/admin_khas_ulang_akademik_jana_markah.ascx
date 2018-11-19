<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_khas_ulang_akademik_jana_markah.ascx.vb" Inherits="kpmkv.admin_khas_ulang_akademik_jana_markah" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Akademik >> Borang Markah >> Jana Borang Markah</td>
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
        <td colspan="2">Borang Pengisian Markah Bertulis Akademik</td>
    </tr>
    <tr>
        <td>Tahun Peperiksaan:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Sesi Peperiksaan:</td>
        <td>
            <asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="350px" RepeatDirection="Horizontal">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
    </tr>
</table>
<asp:Label ID="lblMsg2" runat="server" Text="System message..."></asp:Label>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Calon.</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PelajarID"
                Width="100%" PageSize="40" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mykad">
                        <ItemTemplate>
                            <asp:Label ID="lblMykad" runat="server" Text='<%# Bind("Mykad")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AngkaGiliran">
                        <ItemTemplate>
                            <asp:Label ID="AngkaGiliran" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Markah">
                        <ItemTemplate>
                            <asp:TextBox ID="Markah" runat="server" Width="30px" Text='<%# Bind("Markah")%>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catatan">
                        <ItemTemplate>
                            <asp:TextBox ID="Catatan" runat="server" Width="30px" Text='<%# Bind("Catatan")%>'></asp:TextBox>
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

    <tr>
        <td colspan="2">Kod Matapelajaran:
         <asp:TextBox ID="txtKodMP" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
            &nbsp;<asp:Button ID="btnPrint" runat="server" Text="Cetak Borang Markah" CssClass="fbbutton" />&nbsp;
        </td>

    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
</div>
