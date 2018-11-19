<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kolej_list.ascx.vb"Inherits="kpmkv.kolej_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Pendaftaran >> Kolej</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian
        </td>
    </tr>
    <tr>
        <td>Negeri:</td>
        <td>
            <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
        <td>Jenis Kolej:
        </td>
        <td>
            <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td>
            <asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="200"></asp:TextBox>
        </td>
        <td>Kod Kolej:</td>
        <td>
            <asp:TextBox ID="txtKod" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4"><asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;<asp:Button ID="btnCreate" runat="server" Text="Daftar Baru " CssClass="fbbutton" />
        </td>
    </tr>
    </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Kolej
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="RecordID"
                Width="100%" PageSize="25" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="Negeri" runat="server" Text='<%# Bind("Negeri") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Kolej">
                        <ItemTemplate>
                            <asp:Label ID="Kod" runat="server" Text='<%# Bind("Kod") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Kolej">
                        <ItemTemplate>
                            <asp:Label ID="Nama" runat="server" Text='<%# Bind("Nama") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jenis Kolej">
                        <ItemTemplate>
                            <asp:Label ID="Jenis" runat="server" Text='<%# Bind("Jenis") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Telefon">
                        <ItemTemplate>
                            <asp:Label ID="Tel" runat="server" Text='<%# Bind("Tel")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Emel">
                        <ItemTemplate>
                            <asp:Label ID="Email" runat="server" Text='<%# Bind("Email")%>'></asp:Label>
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
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>
