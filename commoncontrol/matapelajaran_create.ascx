<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="matapelajaran_create.ascx.vb" Inherits="kpmkv.matapelajaran_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>Pendaftaran >> MataPelajaran</td>
    </tr>
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="4">Pendaftaran MataPelajaran</td>
    </tr>
    <tr>
        <td>Tahun:</td>
        <td colspan ="3"><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
     <tr>
        <td>Semester:</td>
        <td colspan ="3"><asp:DropDownList ID="ddlSemester" runat="server" Width="200px"  AutoPostBack ="true"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Kod MataPelajaran:</td>
        <td colspan ="3"><asp:TextBox ID="txtKodMataPelajaran" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Nama MataPelajaran:</td>
        <td colspan ="3"><asp:TextBox ID="txtNamaMataPelajaran" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Jam Kredit:</td>
        <td colspan ="3"><asp:TextBox ID="txtJamKredit" runat="server" Width="50px" MaxLength="10"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>PB:</td>
        <td><asp:TextBox ID="txtPB" runat="server" Width="50px" MaxLength="10"></asp:TextBox>*</td>
  
        <td>PA:</td>
        <td><asp:TextBox ID="txtPA" runat="server" Width="50px" MaxLength="10"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Kolum Pelajar Markah Subjek</td>
        <td><asp:DropDownList ID ="ddlSub" runat ="server" Width ="150">
            <asp:ListItem Value ="BahasaMelayu">BahasaMelayu</asp:ListItem>
            <asp:ListItem Value ="BahasaInggeris">BahasaInggeris</asp:ListItem>
            <asp:ListItem Value ="Science">Science</asp:ListItem>
            <asp:ListItem Value ="Sejarah">Sejarah</asp:ListItem>
            <asp:ListItem Value ="Mathematics">Mathematics</asp:ListItem>
            <asp:ListItem Value ="PendidikanIslam">PendidikanIslam</asp:ListItem>
            <asp:ListItem Value ="PendidikanMoral">PendidikanMoral</asp:ListItem>
            </asp:DropDownList>
        </td>
  
        <td>Kolum Pelajar Markah Gred</td>
        <td><asp:DropDownList ID ="ddlGred" runat ="server" Width ="150">
            <asp:ListItem Value ="GredBM">GredBM</asp:ListItem>
            <asp:ListItem Value ="GredBI">GredBI</asp:ListItem>
            <asp:ListItem Value ="GredSC">GredSC</asp:ListItem>
            <asp:ListItem Value ="GredSJ">GredSJ</asp:ListItem>
            <asp:ListItem Value ="GredMT">GredMT</asp:ListItem>
            <asp:ListItem Value ="GredPI">GredPI</asp:ListItem>
            <asp:ListItem Value ="GredPM">GredPM</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Jenis:</td>
        <td colspan ="3"><asp:DropDownList ID ="ddlJenis" runat ="server" >
            <asp:ListItem Value ="">-Pilih-</asp:ListItem>
            <asp:ListItem Value ="SOCIAL">SOSIAL</asp:ListItem>
            <asp:ListItem Value ="TEKNOLOGI">TEKNOLOGI</asp:ListItem>
         </asp:DropDownList></td>
    </tr>
    <tr>
        <td colspan="4"><asp:Button ID="btnCreate" runat="server" Text="Daftar" CssClass="fbbutton" />
        </td>
    </tr>
    </table>
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td>Senarai Matapelajaran
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="MatapelajaranID"
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
                     
                    <asp:TemplateField HeaderText="KodMataPelajaran">
                        <ItemTemplate>
                            <asp:Label ID="KodMataPelajaran" runat="server" Text='<%# Bind("KodMataPelajaran") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MataPelajaran">
                        <ItemTemplate>
                            <asp:Label ID="NamaMataPelajaran" runat="server" Text='<%# Bind("NamaMataPelajaran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JamKredit">
                        <ItemTemplate>
                            <asp:Label ID="JamKredit" runat="server" Text='<%# Bind("JamKredit")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PB">
                        <ItemTemplate>
                            <asp:Label ID="PB" runat="server" Text='<%# Bind("PB")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                  
                    <asp:TemplateField HeaderText="PA">
                        <ItemTemplate>
                            <asp:Label ID="PA" runat="server" Text='<%# Bind("PA")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                
                     <asp:TemplateField HeaderText="KolumSubjek">
                        <ItemTemplate>
                            <asp:Label ID="PelajarMarkahSubjek" runat="server" Text='<%# Bind("PelajarMarkahSubjek")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                         <ItemStyle VerticalAlign="Middle" HorizontalAlign ="Left"  />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="KolumGred">
                        <ItemTemplate>
                            <asp:Label ID="PelajarMarkahGred" runat="server" Text='<%# Bind("PelajarMarkahGred")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left"  VerticalAlign="Top" />
                         <ItemStyle VerticalAlign="Middle" HorizontalAlign ="Left"  />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="KolumJenis">
                        <ItemTemplate>
                            <asp:Label ID="Jenis" runat="server" Text='<%# Bind("Jenis")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                         <ItemStyle VerticalAlign="Middle" HorizontalAlign ="Left"  />
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
        <td style ="width :35%">Semester</td>
        <td style ="width :2%">:</td>
        <td colspan ="3"><asp:DropDownList ID="ddlColumnSem" runat="server" Width="50px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td style ="width :35%">Kolum Pelajar Markah Subjek</td>
        <td style ="width :2%">:</td>
        <td><asp:DropDownList ID ="ddlColumnSub" runat ="server" Width ="150">
            <asp:ListItem Value ="BahasaMelayu">BahasaMelayu</asp:ListItem>
            <asp:ListItem Value ="BahasaInggeris">BahasaInggeris</asp:ListItem>
            <asp:ListItem Value ="Science">Science</asp:ListItem>
            <asp:ListItem Value ="Sejarah">Sejarah</asp:ListItem>
            <asp:ListItem Value ="Mathematics">Mathematics</asp:ListItem>
            <asp:ListItem Value ="PendidikanIslam">PendidikanIslam</asp:ListItem>
            <asp:ListItem Value ="PendidikanMoral">PendidikanMoral</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style ="width :35%">Kolum Pelajar Markah Gred</td>
        <td style ="width :2%">:</td>
        <td><asp:DropDownList ID ="ddlColumnGred" runat ="server" Width ="150">
            <asp:ListItem Value ="GredBM">GredBM</asp:ListItem>
            <asp:ListItem Value ="GredBI">GredBI</asp:ListItem>
            <asp:ListItem Value ="GredSC">GredSC</asp:ListItem>
            <asp:ListItem Value ="GredSJ">GredSJ</asp:ListItem>
            <asp:ListItem Value ="GredMT">GredMT</asp:ListItem>
            <asp:ListItem Value ="GredPI">GredPI</asp:ListItem>
            <asp:ListItem Value ="GredPM">GredPM</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Kolum Jenis</td>
        <td>:</td>
        <td><asp:DropDownList ID ="ddlColumnJenis" runat ="server" >
            <asp:ListItem Value ="">-</asp:ListItem>
            <asp:ListItem Value ="SOCIAL" >SOSIAL</asp:ListItem>
            <asp:ListItem Value ="TEKNOLOGI">TEKNOLOGI</asp:ListItem>
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
