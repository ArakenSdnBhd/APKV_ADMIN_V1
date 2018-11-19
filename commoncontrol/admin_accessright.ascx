<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_accessright.ascx.vb" Inherits="kpmkv.admin_accessright" %>
<table class="fbform">
    <tr class ="fbform_header">
            <td colspan ="2">Penetapan Akses Pengguna</td>
    </tr>
    <tr>
            <td style ="width :20%">Menu Utama</td>
            <td><asp:DropDownList ID="ddlmenu" runat="server" AutoPostBack ="true"  Width="248px"></asp:DropDownList></td>
    </tr>
     <tr>
            <td style ="width :20%">Sub Menu</td>
            <td><asp:DropDownList ID="ddlsubmenu" runat="server"  Width="248px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td colspan ="2"><asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" /></td>
    </tr>
    </table>
<br />
<div class="info" id="DivMsgTop" runat="server">
    <asp:Label ID="lblMsgTop" runat="server" Text="Mesej..."></asp:Label>
</div>
<br />
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Senarai Menu 
            </td>
        </tr>
        <%-- 1st table- header --%>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ParentMenuCode"
                Width="100%" PageSize="11" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                     <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Menu ">
                        <ItemTemplate><asp:Label ID="SubMenuText" runat="server" Text='<%# Bind("SubMenuText")%>'></asp:Label></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pilih Akses">
                         <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" /></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
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

<table class="fbform">
    <tr class ="fbform_header">
        <td colspan ="2">Tetapan Akses Kepada :<asp:DropDownList ID="ddlgroup"  runat="server"  Width="248px"></asp:DropDownList>
        <asp:CheckBox runat ="server"  ID ="chkAll" Text ="Pilih Semua Akses" />
        </td>
    </tr>
    <tr>
        <td><asp:Button ID="btnUpdate" runat="server" Text="Simpan" CssClass="fbbutton" /></td>
    </tr>
</table>
<br />
<div id ="divdetail" runat ="server" >
<table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Senarai Akses  <asp:Label ID ="lblgroup" runat ="server" ></asp:Label>
            </td>
        </tr>
        <%-- 2nd table- header --%>
        <tr>
            <td>
                <asp:GridView ID="datRespondent2" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ParentMenuCode"
                Width="100%" PageSize="11" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                     <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Menu Utama ">
                        <ItemTemplate><asp:Label ID="HeaderText" runat="server" Text='<%# Bind("HeaderText")%>'></asp:Label></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Menu ">
                        <ItemTemplate><asp:Label ID="SubMenuText" runat="server" Text='<%# Bind("SubMenuText")%>'></asp:Label></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Batal"> 
                        <ItemTemplate> 
                            <asp:Button ID="btnBatal" runat="server" Text="-" CommandName="Batal" CommandArgument ='<%#Eval("ParentMenuCode")%>'/></td>
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
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

<br />
    </div>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Mesej.."></asp:Label>
    </div>