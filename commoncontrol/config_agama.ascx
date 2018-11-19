﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_agama.ascx.vb" Inherits="kpmkv.config_agama1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi Umum >> Agama
        </td>
     </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Agama
        </td>
     </tr>
    <tr>
         <td >Agama:</td>
         <td><asp:TextBox ID="txtAgama" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnUpdate" runat="server" Text="Daftar Baru" CssClass="fbbutton" />
        </td>
    </tr>
   
   
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Senarai Parameter
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="AgamaID"
                Width="100%" PageSize="10" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Parameter">
                        <ItemTemplate>
                            <asp:Label ID="Agama" runat="server" Text='<%# Bind("Agama")%>'></asp:Label>
                        </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
              </asp:TemplateField>
                     <asp:TemplateField>
                        <ItemTemplate>
                      <%--      ADD THE DELETE LINK BUTTON---%>
                            <asp:LinkButton runat="server" OnClientClick="return confirm('Anda pasti untuk Batal?');"
                                CommandName="DELETE">Batal</asp:LinkButton>
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
        <td class="fbform_sap" colspan="5">&nbsp;</td>
    </tr>
    
   </table>
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    <asp:Label ID="lblAgama" runat="server" Text="" Visible="false"></asp:Label>
    
</div>