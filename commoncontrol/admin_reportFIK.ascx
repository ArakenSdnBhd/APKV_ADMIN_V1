<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_reportFIK.ascx.vb" Inherits="kpmkv.admin_reportFIK" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian </td>
    </tr>
    <tr>
           <td>BM Setara Pada Tahun:</td>
             <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="100px">
            </asp:DropDownList>
        </td>  
    </tr>
      <tr>
         <td style="width: 20%;">Sesi:</td>
              <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
      <tr>
        <td style="width: 20%;">Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
    </tr>
 </table> 
<div class="info" id="divMsgTop" runat="server">
<asp:Label ID="lblMsgTop" runat="server" Text=""></asp:Label>
</div>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan ="2">Senarai Rumusan Layak SVM Mengikut Kolej.</td>
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
                    <asp:TemplateField HeaderText="RecordID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="KolejRecordID" runat="server" Text='<%# Bind("KolejRecordID")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama">
                        <ItemTemplate>
                            <asp:Label ID="Nama" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="BM Setara">
                        <ItemTemplate>
                            <asp:Label ID="IsBMTahun" runat="server" Text='<%# Bind("IsBMTahun")%>'></asp:Label>
                             <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%"/>
                        </ItemTemplate>
                        </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                       <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="Jumlah Calon">
                        <ItemTemplate>
                            <asp:Label ID="total" runat="server" Text='<%# Bind("total")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="X Layak SVM">
                        <ItemTemplate>
                            <asp:Label ID="C_XLayak" runat="server" Text='<%# Bind("C_XLayak")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Layak SVM">
                        <ItemTemplate>
                            <asp:Label ID="C_Layak" runat="server" Text='<%# Bind("C_Layak")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="VOK Kompeten">
                        <ItemTemplate>
                            <asp:Label ID="C_VOK" runat="server" Text='<%# Bind("C_VOK")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PNGKA">
                        <ItemTemplate>
                            <asp:Label ID="C_PNGKA" runat="server" Text='<%# Bind("C_PNGKA")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="PNGKV">
                        <ItemTemplate>
                            <asp:Label ID="C_PNGKV" runat="server" Text='<%# Bind("C_PNGKV")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"/>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Gred BM Setara">
                        <ItemTemplate>
                            <asp:Label ID="C_BM" runat="server" Text='<%# Bind("C_BM")%>'></asp:Label>
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
        <td colspan="2" style="font-size: small; color: #FF0000">NOTA:</td>
    </tr>
    <tr>
           <td style="font-size: small; color: #FF0000">** PURATA NILAI GRED KUMULATIF AKADEMIK(PNGKA)&nbsp;&nbsp;&nbsp;&nbsp;   : >= 2</td>
    </tr>
      <tr>
         <td style="font-size: small; color: #FF0000">** PURATA NILAI GRED KUMULATIF VOKASIONAL(PNGKV)&nbsp; : >= 2.67</td>
    </tr>
 </table> 
<br />
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>
