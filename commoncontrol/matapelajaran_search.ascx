<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="matapelajaran_search.ascx.vb" Inherits="kpmkv.matapelajaran_search" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian MataPelajaran</td>
       
    </tr>
    <tr>
        <td>Kohort:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
       
    </tr>
    <tr>
        <td>Kod MataPelajaran:</td>
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
        <td class="fbform_sap" colspan="2">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
       
    </tr>
        </table>
<br />
<div class="info" id="divMsgTop" runat="server">
<asp:Label ID="lblMsgTop" runat="server" Text=""></asp:Label>
</div>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai MataPelajaran</td>
    
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
                     <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                  
                    <asp:TemplateField HeaderText="Kod MataPelajaran">
                        <ItemTemplate>
                            <asp:Label ID="KodMataPelajaran" runat="server" Text='<%# Bind("KodMataPelajaran")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama MataPelajaran">
                        <ItemTemplate>
                            <asp:Label ID="NamaMataPelajaran" runat="server" Text='<%# Bind("NamaMataPelajaran")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jam Kredit">
                        <ItemTemplate>
                            <asp:Label ID="JamKredit" runat="server" Text='<%# Bind("JamKredit")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="PILIH" />
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
        <td colspan="2"></td>
       
    </tr>
    <tr>
        <td colspan="2">
          <asp:Button ID="btnExecute" runat="server" Text="Eksport" CssClass="fbbutton" />
        </td>
       
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
</div>