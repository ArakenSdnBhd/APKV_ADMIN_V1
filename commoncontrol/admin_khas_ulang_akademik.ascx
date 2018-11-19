<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_khas_ulang_akademik.ascx.vb" Inherits="kpmkv.admin_khas_ulang_akademik" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Akademik &gt;&gt; Import Calon</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">MuatNaik.</td>
    </tr>
     <tr>
         <td colspan ="2">MuatNaik Format Fail Excel:
         <asp:Button ID="btnFile" runat="server" Text="Excel" CssClass="fbbutton" Height="25px" Width="116px" /></td>
    </tr>
    <tr>
        <td>Pilih Fail Excel:
        </td>
        <td>
            <asp:FileUpload ID="FlUploadcsv" runat="server" />&nbsp;
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed"
                ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpload" runat="server" Text="Muatnaik " CssClass="fbbutton" Style="height: 26px" />&nbsp;<asp:Label
                ID="lblMsgTop" runat="server" Text="" ForeColor="Blue"></asp:Label>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Calon</td>
    </tr>
    <tr>
        <td>
            <table class="fbform">
               <tr class="fbform_header">
                  <td colspan="2">Carian </td>
               </tr>
               <tr>
                  <td>Tahun Ulang:</td>
                     <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>  
                </tr>
                <tr>
                    <td style="width: 20%;">Sesi:</td>
                    <td><asp:CheckBoxList ID="chkSesi" runat="server" Width="349px" RepeatDirection="Horizontal">
                      <asp:ListItem>1</asp:ListItem>
                      <asp:ListItem>2</asp:ListItem>
                      </asp:CheckBoxList>
                 </tr>
                <tr>
                   <td>MataPelajaran:</td>
                    <td><asp:DropDownList ID="ddlAkademik" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
                </tr>
               <tr>
                 <td>
                   <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
                  </tr>
              </table>
         </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PelajarID"
                Width="100%" PageSize="100" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="KolejRecordID">
                        <ItemTemplate>
                            <asp:Label ID="lblKolejRecordID" runat="server" Text='<%# Bind("KolejRecordID")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="lblTahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="lblSesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Program">
                        <ItemTemplate>
                            <asp:Label ID="KodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="MataPelajaran">
                        <ItemTemplate>
                            <asp:Label ID="lblMataPelajaran" runat="server" Text='<%# Bind("MataPelajaran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="PelajarID">
                        <ItemTemplate>
                            <asp:Label ID="lblPelajarID" runat="server" Text='<%# Bind("PelajarID")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Nama">
                        <ItemTemplate>
                            <asp:Label ID="lblNama" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mykad">
                        <ItemTemplate>
                            <asp:Label ID="lblMykad" runat="server" Text='<%# Bind("Mykad")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AngkaGiliran">
                        <ItemTemplate>
                            <asp:Label ID="lblAngkaGiliran" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Calon?">
                        <ItemTemplate>
                            <asp:Label ID="lblIsCalon" runat="server" Text='<%# Bind("IsCalon")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Tahun Semasa">
                        <ItemTemplate>
                            <asp:Label ID="lblIsAKATahun" runat="server" Text='<%# Bind("IsAKATahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Sesi Semasa">
                        <ItemTemplate>
                            <asp:Label ID="lblIsAKASesi" runat="server" Text='<%# Bind("IsAKASesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Tarikh?">
                        <ItemTemplate>
                            <asp:Label ID="lblIsAKADated" runat="server" Text='<%# Bind("IsAKADated")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Import">
                        <ItemTemplate>
                            <asp:Label ID="lblImportDated" runat="server" Text='<%# Bind("ImportDated")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Padam">
                        <ItemTemplate>
                            <asp:ImageButton Width ="12" Height ="12" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Anda pasti untuk padam rekod ini? Pemadaman yang dilakukan tidak boleh diubah')" runat="server" ImageUrl="~/icons/download.jpg" ToolTip="Padam Rekod"/>
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
            &nbsp;</td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">

<asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>