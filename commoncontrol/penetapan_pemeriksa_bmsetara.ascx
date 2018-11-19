<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="penetapan_pemeriksa_bmsetara.ascx.vb" Inherits="kpmkv.penetapan_pemeriksa_bmsetara" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pemeriksa>>Penetapan Pemeriksa >> Jana Penetapan Pemeriksa</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td>Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Jenis Kolej:</td>
        <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList>
    </tr>
     <tr>
        <td >Kod Pusat:</td>
        <td><asp:DropDownList ID="ddlKodPusat" runat="server" AutoPostBack="true" Width="250px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td><asp:Label ID="lblNamaKolej" runat="server" Text=""></asp:Label>
</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Penetapan Pemeriksa</td>
    </tr>
    <tr>
           <td >Tahun Semasa:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
         <td >Sesi Pengambilan</td>
                 <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
     <tr>
         <td >Kertas No</td>
                 <td><asp:CheckBoxList ID="chkKertas" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
     <tr>
        <td colspan ="2"><asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" /></td>      
    </tr>
      </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Pemeriksa Borang Markah</td>
    </tr>
    <tr>
    <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="10" ForeColor="#333333" GridLines="None" DataKeyNames="PemeriksaID"
                Width="100%" PageSize="15" CssClass="gridview_footer">
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
                            <asp:Label ID="lblKohort" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi Pengambilan">
                        <ItemTemplate>
                            <asp:Label ID="lblSesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Nama Pemeriksa">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaPemeriksa" runat="server" Text='<%# Bind("NamaPemeriksa")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Kod Pusat">
                        <ItemTemplate>
                            <asp:Label ID="lblKodPusat" runat="server" Text='<%# Bind("KodKolej")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Kertas No">
                        <ItemTemplate>
                            <asp:Label ID="lblKertasNo" runat="server" Text='<%# Bind("KertasNo")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Batal"> 
                        <ItemTemplate> 
                            <asp:Button ID="btnBatal" runat="server" Text="-" CommandName="Batal" CommandArgument ='<%#Eval("PemeriksaID")%>'/></td>
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
</table>
<br />
<table class="fbform">
     <tr class="fbform_header">
        <td>Pemilihan Pemeriksa</td>
         <td><asp:DropDownList ID="ddlPemeriksa" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>&nbsp;<asp:Button ID="btnSimpan" runat="server" Text="Simpan" CssClass="fbbutton" /></td>
    </tr>
     <tr>
        <td colspan="2">
            &nbsp;</td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
</div>