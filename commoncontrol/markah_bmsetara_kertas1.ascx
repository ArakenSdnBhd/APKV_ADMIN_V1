<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="markah_bmsetara_kertas1.ascx.vb" Inherits="kpmkv.markah_bmsetara_kertas1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pemeriksa>>Markah>>Markah BM Setara Kertas 1</td>
    </tr>
</table>
<br />

<table class="fbform">
    <tr class="fbform_header">
        <td style="font-size: medium; font-style: italic; color: #FF0000">Petunjuk:<br />
            Hadir Tidak Menjawab : 1<br />
            Tiada Skrip&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : 2<br />
            Tidak Hadir&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : 3</td>
    </tr>
    <tr>
        <td></td>
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
                    <asp:TemplateField HeaderText="Kertas 1">
                        <ItemTemplate>
                            <asp:TextBox ID="A_BahasaMelayu1" runat="server" Width="30px"  Text='<%# Bind("A_BahasaMelayu1")%>'></asp:TextBox>
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
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" Visible="true" />&nbsp;&nbsp;
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