<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kursus_kolej_create.ascx.vb" Inherits="kpmkv.kursus_kolej_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Program
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">Kod Bidang:</td>
        <td>
            <asp:Label ID="lblKodKluster" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">Nama Bidang:</td>
        <td>
            <asp:Label ID="lblNamaKluster" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">Kod Program:</td>
        <td>
            <asp:Label ID="lblKodKursus" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">Nama Program:</td>
        <td>
            <asp:Label ID="lblNamaKursus" runat="server"></asp:Label>
        </td>
    </tr>
   
    <tr>
        <td style="width: 15%;">Kohort:</td>
        <td>
            <asp:Label ID="lblTahun" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">&nbsp;
        </td>
    </tr>
    </table>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian Kolej
        </td>
    </tr>
<tr>
        <td>Jenis Kolej:</td>
        <td>
            <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
<tr>
        <td>Negeri:</td>
        <td>
            <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
<tr>
        <td>Kod Kolej</td>
        <td>
            <asp:DropDownList ID="ddlKodKolej" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
<tr>
        <td>Nama Kolej:</td>
        <td>
            <asp:TextBox ID="txtKod" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />
        
       
    </tr>
    <tr>
        <td colspan ="2"></td>
    </tr>
    <tr>
        <td colspan ="2">Senarai Kolej<td>
            
    </tr>
    <tr>
        <td colspan="2">
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
                    <asp:TemplateField HeaderText="Nama Pengarah">
                        <ItemTemplate>
                            <asp:Label ID="NamaPengarah" runat="server" Text='<%# Bind("NamaPengarah") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tel# Kolej">
                        <ItemTemplate>
                            <asp:Label ID="Tel" runat="server" Text='<%# Bind("Tel") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email Kolej">
                        <ItemTemplate>
                            <asp:Label ID="Email" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
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

    </tr>
    <tr>
        <td colspan="2"></td>
   <tr>
        
       <td colspan ="2">            <asp:Button ID="btnCreate" runat="server" Text="Penetapan Kursus Baru" CssClass="fbbutton" /><asp:LinkButton
                    ID="lnkList" runat="server">|Carian Kursus</asp:LinkButton></td>
    </tr>
    
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>
<asp:Label ID="lblKod" runat="server" Text="" Visible="false"></asp:Label>
