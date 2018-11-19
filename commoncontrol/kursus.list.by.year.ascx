<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kursus.list.by.year.ascx.vb" Inherits="kpmkv.kursus_list_by_year" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pemilihan Program Baru Vokasional.</td>
    </tr>
    <tr>
        <td>Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Kod Program:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>
      <tr>
        <td colspan="2"><asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" />&nbsp;</td>
           </tr>
    <tr>
        <td>&nbsp;</td>
        <td> 
          <INPUT type="Button" value="Pilih Kursus" onClick="window.open('kursus.popup.aspx','mywindow','width=500,height=400,left=250,top=75,scrollbars=yes,screenx=0,screenY=100')">
        </td>
        
   </tr>
   
    <tr class="fbform_header">
        <td colspan="2">&nbsp;</td>
    </tr>
   
    <tr class="fbform_header">
        <td colspan="2">Senarai Kursus.
        </td>
    </tr>
    <tr>
         <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="KodKursus"
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
                      <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Nama Kluster">
                        <ItemTemplate>
                            <asp:Label ID="NamaKluster" runat="server" Text='<%# Bind("NamaKluster")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Kod Kursus">
                        <ItemTemplate>
                            <asp:Label ID="KodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Kursus">
                        <ItemTemplate>
                            <asp:Label ID="NamaKursus" runat="server" Text='<%# Bind("NamaKursus")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Pilih">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField> 
                     <asp:TemplateField>
                    <ItemTemplate>
                        <%--ADD THE DELETE LINK BUTTON--%>
                        <asp:LinkButton Runat="server" OnClientClick ="return confirm('Are you sure you?');"
                            CommandName="Mansuhkan">Mansuh</asp:LinkButton>
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
        <td colspan="2">Sila Pilih Sesi :
            <asp:DropDownList ID="ddlSesi" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        &nbsp;<asp:Button ID="btnCreate" runat="server" Text="Simpan" CssClass="fbbutton" />&nbsp;</td>
           </tr>
    </table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>

