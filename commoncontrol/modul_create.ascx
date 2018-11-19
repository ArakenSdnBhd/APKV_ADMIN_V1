<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="modul_create.ascx.vb" Inherits="kpmkv.modul_create" %>
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="4">Pendaftaran >> Kursus</td>
    </tr>
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr>
        <td>Kohort:</td>
        <td colspan ="3"><asp:DropDownList ID="ddlTahun" runat="server" Width="200px" AutoPostBack ="true" ></asp:DropDownList></td>
    </tr>
     <tr>
        <td>Semester:</td>
        <td colspan ="3"><asp:DropDownList ID="ddlSemester" runat="server" Width="200px"  AutoPostBack ="true"></asp:DropDownList></td>
    </tr>
     <tr>
          <td>Sesi Pengambilan:</td>
        <td colspan ="3"><asp:CheckBoxList ID="chkSesi" runat="server"  AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
    <tr>
        <td>Kod Program:</td>
        <td colspan ="3"> <asp:DropDownList ID="ddlKursus" runat="server"  Width="350px" AutoPostBack ="true" ></asp:DropDownList></td>
    </tr>

    <tr>
        <td>Kod Kursus:</td>
        <td colspan ="3"><asp:TextBox ID="txtKodModul" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Nama Kursus:</td>
        <td colspan ="3"><asp:TextBox ID="txtNamaModul" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Jam Kredit:</td>
        <td colspan ="3"> <asp:TextBox ID="txtJamKredit" runat="server" Width="50px" MaxLength="10"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>PB Amali:</td>
        <td><asp:TextBox ID="txtPBAmali" runat="server" Width="50px" MaxLength="250"></asp:TextBox>*</td>
        <td>PB Teori:</td>
        <td><asp:TextBox ID="txtPBTeori" runat="server" Width="50px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
   
    <tr>
        <td>PA Amali:</td>
        <td><asp:TextBox ID="txtPAAmali" runat="server" Width="50px" MaxLength="250"></asp:TextBox>*</td>
         <td>PA Teori:</td>
        <td><asp:TextBox ID="txtPATeori" runat="server" Width="50px" MaxLength="250"></asp:TextBox>*</td>
   
    </tr>
     <tr>
        <td>Kolum Pelajar Markah PBPA :</td>
        <td><asp:DropDownList ID ="ddlPBPAM" runat ="server" Width ="150">
            <asp:ListItem Value ="PBPAM1">PBPAM1</asp:ListItem>
            <asp:ListItem Value ="PBPAM2">PBPAM2</asp:ListItem>
            <asp:ListItem Value ="PBPAM3">PBPAM3</asp:ListItem>
            <asp:ListItem Value ="PBPAM4">PBPAM4</asp:ListItem>
            <asp:ListItem Value ="PBPAM5">PBPAM5</asp:ListItem>
            <asp:ListItem Value ="PBPAM6">PBPAM6</asp:ListItem>
            <asp:ListItem Value ="PBPAM7">PBPAM7</asp:ListItem>
            <asp:ListItem Value ="PBPAM8">PBPAM8</asp:ListItem>
            </asp:DropDownList>
        </td>
   
        <td>Kolum Pelajar Markah Gred :</td>
        <td><asp:DropDownList ID ="ddlGredMV" runat ="server" Width ="150">
            <asp:ListItem Value ="GredV1">GredV1</asp:ListItem>
            <asp:ListItem Value ="GredV2">GredV2</asp:ListItem>
            <asp:ListItem Value ="GredV3">GredV3</asp:ListItem>
            <asp:ListItem Value ="GredV4">GredV4</asp:ListItem>
            <asp:ListItem Value ="GredV5">GredV5</asp:ListItem>
            <asp:ListItem Value ="GredV6">GredV6</asp:ListItem>
            <asp:ListItem Value ="GredV7">GredV7</asp:ListItem>
            <asp:ListItem Value ="GredV8">GredV8</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
   
    <tr>
        <td colspan="4"><asp:Button ID="btnCreate" runat="server" Text="Daftar Kursus" CssClass="fbbutton" /></td>
    </tr>
    </table>

<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td>Senarai Modul
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ModulID"
                Width="100%" PageSize="100" CssClass="gridview_footer">
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
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Sem">
                        <ItemTemplate>
                            <asp:Label ID="Semester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KodModul">
                        <ItemTemplate>
                            <asp:Label ID="KodModul" runat="server" Text='<%# Bind("KodModul") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Modul">
                        <ItemTemplate>
                            <asp:Label ID="NamaModul" runat="server" Text='<%# Bind("NamaModul")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JamKredit">
                        <ItemTemplate>
                            <asp:Label ID="JamKredit" runat="server" Text='<%# Bind("JamKredit")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PBAmali">
                        <ItemTemplate>
                            <asp:Label ID="PBAmali" runat="server" Text='<%# Bind("PBAmali")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PBTeori">
                        <ItemTemplate>
                            <asp:Label ID="PBTeori" runat="server" Text='<%# Bind("PBTeori")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PAAmali">
                        <ItemTemplate>
                            <asp:Label ID="PAAmali" runat="server" Text='<%# Bind("PAAmali")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="PATeori">
                        <ItemTemplate>
                            <asp:Label ID="PATeori" runat="server" Text='<%# Bind("PATeori")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                         <ItemStyle VerticalAlign="Middle" HorizontalAlign ="Center"  />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="PBPAM">
                        <ItemTemplate>
                            <asp:Label ID="PelajarMarkahPBPA" runat="server" Text='<%# Bind("PelajarMarkahPBPA")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                         <ItemStyle VerticalAlign="Middle" HorizontalAlign ="Center"  />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="GredV">
                        <ItemTemplate>
                            <asp:Label ID="PelajarMarkahGred" runat="server" Text='<%# Bind("PelajarMarkahGred")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                         <ItemStyle VerticalAlign="Middle" HorizontalAlign ="Center"  />
                    </asp:TemplateField>
                      <asp:TemplateField >
                         <HeaderTemplate >
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckUncheckAll" />
                            </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect"  runat="server" />
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle VerticalAlign="Middle" />
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
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Pemilihan Kolum   </td>
    </tr>
    <tr>
        <td style ="width :35%">Kolum Pelajar Markah PBPA</td>
        <td style ="width :2%">:</td>
        <td><asp:DropDownList ID ="ddlColumnPBPA" runat ="server" Width ="150">
            <asp:ListItem Value ="PBPAM1">PBPAM1</asp:ListItem>
            <asp:ListItem Value ="PBPAM2">PBPAM2</asp:ListItem>
            <asp:ListItem Value ="PBPAM3">PBPAM3</asp:ListItem>
            <asp:ListItem Value ="PBPAM4">PBPAM4</asp:ListItem>
            <asp:ListItem Value ="PBPAM5">PBPAM5</asp:ListItem>
            <asp:ListItem Value ="PBPAM6">PBPAM6</asp:ListItem>
            <asp:ListItem Value ="PBPAM7">PBPAM7</asp:ListItem>
            <asp:ListItem Value ="PBPAM8">PBPAM8</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style ="width :35%">Kolum Pelajar Markah Gred</td>
        <td style ="width :2%">:</td>
        <td><asp:DropDownList ID ="ddlGredV" runat ="server" Width ="150">
            <asp:ListItem Value ="GredV1">GredV1</asp:ListItem>
            <asp:ListItem Value ="GredV2">GredV2</asp:ListItem>
            <asp:ListItem Value ="GredV3">GredV3</asp:ListItem>
            <asp:ListItem Value ="GredV4">GredV4</asp:ListItem>
            <asp:ListItem Value ="GredV5">GredV5</asp:ListItem>
            <asp:ListItem Value ="GredV6">GredV6</asp:ListItem>
            <asp:ListItem Value ="GredV7">GredV7</asp:ListItem>
            <asp:ListItem Value ="GredV8">GredV8</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan ="3"><asp:Button ID ="btnSave" runat="server" Text="Simpan" CssClass="fbbutton"/></td>
    </tr>
    </table> 
<br />
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>

