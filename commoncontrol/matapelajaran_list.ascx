<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="matapelajaran_list.ascx.vb" Inherits="kpmkv.matapelajaran_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian MataPelajaran
        </td>
    </tr>
    <tr>
        <td>Kod MataPelajaran:
        </td>
        <td>
            <asp:DropDownList ID="ddlKodMataPelajaran" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>

    </tr>
    <tr>
        <td>Nama MataPelajaran:
        </td>
        <td>
            <asp:TextBox ID="txtNamaMataPelajaran" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
        </td>

    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            Senarai MataPelajaran</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="MataPelajaranID"
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
                   
                    <asp:TemplateField HeaderText="Kod MataPelajaran">
                        <ItemTemplate>
                            <asp:Label ID="KodMataPelajaran" runat="server" Text='<%# Bind("KodMataPelajaran")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama MataPelajaran">
                        <ItemTemplate>
                            <asp:Label ID="NamaMataPelajaran" runat="server" Text='<%# Bind("NamaMataPelajaran")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jam Kredit">
                        <ItemTemplate>
                            <asp:Label ID="JamKredit" runat="server" Text='<%# Bind("JamKredit")%>'></asp:Label>
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
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnExecute" runat="server" Text="Import" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<br />

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
</div>