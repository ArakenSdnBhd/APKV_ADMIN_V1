<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kursus_update.ascx.vb" Inherits="kpmkv.kursus_update" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Program
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px" Enabled="False">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">Sesi Pengambilan:</td>
        <td>
            <asp:DropDownList ID="ddlSesi" runat="server" AutoPostBack="false" Width="350px" Enabled="False">
            </asp:DropDownList>
        </td>
    </tr>
      <tr>
       <td style="width: 20%;">Jenis Program:</td>
        <td><asp:CheckBoxList ID="chkJenisProgram" runat="server"  Width="349px" AutoPostBack="true" RepeatDirection="Horizontal">
             <asp:ListItem>SOCIAL</asp:ListItem>
             <asp:ListItem>TEKNOLOGI</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
    <tr>
        <td colspan ="2"></td>
    </tr>
     <tr>
       <td style="width: 20%;">Nama Bidang:</td>
        <td>
            <asp:DropDownList ID="ddlNamaKluster" runat="server" Width="350px" Enabled="False">
            </asp:DropDownList>
        </td>
    </tr>
     <tr>
        <td style="width: 20%;">Kod Program:</td>
        <td><asp:TextBox ID="txtKod" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
       <td style="width: 20%;">Nama Program:</td>
        <td><asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan ="2">
       
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;<asp:Button
                ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<br />
 <table class="fbform">
    <tr class="fbform_header">
        <td>Paparan Maklumat Kursus
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="modulID"
                Width="100%" PageSize="30" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
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
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Kursus">
                        <ItemTemplate>
                            <asp:Label ID="KodModul" runat="server" Text='<%# Bind("KodModul")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Kursus">
                        <ItemTemplate>
                            <asp:Label ID="NamaModul" runat="server" Text='<%# Bind("NamaModul")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jam Kredit">
                        <ItemTemplate>
                            <asp:Label ID="JamKredit" runat="server" Text='<%# Bind("JamKredit")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="PB Amali">
                        <ItemTemplate>
                            <asp:Label ID="PBAmali" runat="server" Text='<%# Bind("PBAmali")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="PB Teori">
                        <ItemTemplate>
                            <asp:Label ID="PBTeori" runat="server" Text='<%# Bind("PBTeori")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="PA Amali">
                        <ItemTemplate>
                            <asp:Label ID="PAAmali" runat="server" Text='<%# Bind("PAAmali")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="PA Teori">
                        <ItemTemplate>
                            <asp:Label ID="PATeori" runat="server" Text='<%# Bind("PATeori")%>'></asp:Label>
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
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>
<asp:Label ID="lblKodKursus" runat="server" Text="" Visible="false"></asp:Label>
