<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_reportBMSetara.ascx.vb" Inherits="kpmkv.admin_reportBMSetara" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian </td>
    </tr>
    <tr>
       <td style="width: 10%;">Kohort:</td>
             <td><asp:DropDownList ID="ddlTahun" runat="server" Width="100px">
            </asp:DropDownList>
        </td>  
    </tr>
     <tr>
        <td style="width: 20%;">Sesi:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
       <tr>
        <td style="width: 20%;">Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" Width="350px" AutoPostBack="true"></asp:DropDownList></td>
    </tr>
       <tr>
        <td>Nama Kolej:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>      <td style="width: 10%;"> <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
    </tr>
 </table> 
<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan ="2">Senarai Kolej bagi Tahun:  <asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>
            </td>
        </tr>

    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames=""
                Width="100%" PageSize="100" CssClass="gridview_footer" EnableModelValidation="True" Font-Names="Arial" Font-Size="Small">
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
                            <asp:Label ID="Negeri" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField HeaderText="Kod">
                        <ItemTemplate>
                            <asp:Label ID="Kod" runat="server" Text='<%# Bind("Kod")%>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Nama">
                        <ItemTemplate>
                            <asp:Label ID="Nama" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                           <HeaderStyle Width="70%"/>
                        <ItemStyle Width="70%" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                       <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="SESI" runat="server" Text='<%# Bind("SESI")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <%--  <asp:TemplateField HeaderText="SEM1">
                        <ItemTemplate>
                            <asp:Label ID="SEM1" runat="server" Text='<%# Bind("SEM1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="SEM2">
                        <ItemTemplate>
                            <asp:Label ID="SEM2" runat="server" Text='<%# Bind("SEM2")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="SEM3">
                        <ItemTemplate>
                            <asp:Label ID="SEM3" runat="server" Text='<%# Bind("SEM3")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="SEM4">
                        <ItemTemplate>
                            <asp:Label ID="SEM4" runat="server" Text='<%# Bind("SEM4")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="ULANG">
                        <ItemTemplate>
                            <asp:Label ID="ULANG" runat="server" Text='<%# Bind("ULANG")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah">
                        <ItemTemplate>
                            <asp:Label ID="Total" runat="server" Text='<%# Bind("Total")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
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
     <tr style="text-align: center">
        <td colspan="2">
            <asp:Button ID="btnExport" runat="server" Text="Eksport " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
    </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Rumusan</td>
    </tr>
    <tr>
        <td>
            Jumlah Keseluruhan Pelajar SEM4:<td><asp:Label ID="lblSem1" runat="server" Text="0"></asp:Label>
        </td>
    </tr>
      <tr>
        <td>
            Jumlah Keseluruhan Pelajar SEM4-ULANG:<td><asp:Label ID="lblSem2" runat="server" Text="0"></asp:Label>
        </td>
    </tr>
      <tr>
        <td>
            Jumlah Keseluruhan Pelajar:<td><asp:Label ID="lblJumlah" runat="server" Text="0"></asp:Label>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>