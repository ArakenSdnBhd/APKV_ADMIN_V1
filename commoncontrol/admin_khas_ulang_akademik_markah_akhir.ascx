<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_khas_ulang_akademik_markah_akhir.ascx.vb" Inherits="kpmkv.admin_khas_ulang_akademik_markah_akhir" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Akademik>>Pemeriksa>>Kemasukkan Markah </td>
    </tr>
</table>
<br />

<table class="fbform" border="1">
    <tr class="fbform_header">
        <td colspan="2" style="font-size: medium; font-style: italic; color: #FF0000">Petunjuk:<br />
    </tr>
    <tr>
        <td style="font-size: medium; font-style: italic; color: #FF0000">MARKAH (yang perlu diisi)</td>
        <td style="font-size: medium; font-style: italic; color: #FF0000">CATATAN</td>
    </tr>
    <tr>
        <td style="font-size: medium; font-style: italic; color: #FF0000">1</td>
        <td style="font-size: medium; font-style: italic; color: #FF0000">Hadir Tidak Menjawab : 1</td>
    </tr>
    <tr>
        <td style="font-size: medium; font-style: italic; color: #FF0000">-1</td>
        <td style="font-size: medium; font-style: italic; color: #FF0000">Tiada Skrip : 2</td>
    </tr>
    <tr>
        <td style="font-size: medium; font-style: italic; color: #FF0000">-1</td>
        <td style="font-size: medium; font-style: italic; color: #FF0000">Tidak Hadir</td>
    </tr>
</table>
<div class="info" id="divMsg2" runat="server">
    <asp:Label ID="lblMsg2" runat="server" Text="System message..."></asp:Label>
</div>
<table class="fbform">
   <tr class="fbform_header">
        <td>Senarai Calon.</td>
    </tr>
    <tr>
       <td>
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
                            <asp:TextBox ID="Markah" runat="server" Width="30px"  Text='<%# Bind("Markah")%>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Catatan">
                        <ItemTemplate>
                            <asp:TextBox ID="Catatan" runat="server" Width="30px"  Text='<%# Bind("Catatan")%>'></asp:TextBox>
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
        <td colspan ="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" Visible="true" />&nbsp;&nbsp;
        </td>
    </tr>
<br />
   <tr>
        <td colspan="2">Kod Matapelajaran:<asp:TextBox ID="txtKodMP" runat="server" MaxLength="10" Width="100px"></asp:TextBox>            
            <asp:Button ID="btnEksport" runat="server" Text="Eksport Borang Markah" CssClass="fbbutton"/>

          </td>
</tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblPemeriksa" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblTahun" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblSesi" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblKodPusat" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblKertas" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblKolejRecorID" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>