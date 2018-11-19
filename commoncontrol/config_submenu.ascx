<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_submenu.ascx.vb" Inherits="kpmkv.config_submenu1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi>> Menu >> Sub Menu</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Sub Menu
        </td>
    </tr>
     <tr>
        <td>Paparan Sistem:</td>
        <td><asp:DropDownList ID ="ddlJenis" runat ="server" AutoPostBack ="true" >
            <asp:ListItem  Value="ADMIN">ADMIN</asp:ListItem>
            <asp:ListItem Value ="SUKOLEJ">SUKOLEJ</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
         <td>Menu Utama:</td>
         <td><asp:DropDownList ID ="ddlmenuutama" runat ="server" AutoPostBack ="true" ></asp:DropDownList>
    </tr>
     <tr>
         <td>Menu:</td>
         <td><asp:DropDownList ID ="ddlmenu" runat ="server" Width="350px" AutoPostBack ="true"></asp:DropDownList>
    </tr>
     <tr>
         <td>Sub Menu:</td>
         <td><asp:TextBox ID="txtsubmenu" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
         <td>Kod:</td>
         <td><asp:TextBox ID="txtKod" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
         <td>SubMenu Link:</td>
         <td><asp:TextBox ID="txtlink" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
         <td>SubMenuImg:</td>
         <td><asp:TextBox ID="txtmenuimg" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
    </tr>
     <tr>
        <td>Idx:</td>
        <td><asp:TextBox ID ="txtIdx" runat ="server" Width ="50px" ></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="btnUpdate" runat="server" Text="Daftar Baru" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
       <td colspan="2">Senarai Parameter</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="SubMenuCode"
                Width="100%" PageSize="40" CssClass="gridview_footer">
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
                            <asp:Label ID="SubMenuText" runat="server" Text='<%# Bind("SubMenuText")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod">
                        <ItemTemplate>
                            <asp:Label ID="SubMenuCode" runat="server" Text='<%# Bind("SubMenuCode")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SubMenuLink">
                        <ItemTemplate>
                            <asp:Label ID="SubMenuLink" runat="server" Text='<%# Bind("SubMenuLink")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SubMenuImg">
                        <ItemTemplate>
                            <asp:Label ID="SubMenuImg" runat="server" Text='<%# Bind("SubMenuImg")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Idx">
                        <ItemTemplate>
                            <asp:Label ID="Idx" runat="server" Text='<%# Bind("Idx")%>'></asp:Label>
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
    <asp:Label ID="lblSubMenuCode" runat="server" Text="" Visible="false"></asp:Label>
    
</div>