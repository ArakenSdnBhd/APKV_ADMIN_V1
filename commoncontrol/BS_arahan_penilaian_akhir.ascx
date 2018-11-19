<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BS_arahan_penilaian_akhir.ascx.vb" Inherits="kpmkv.BS_arahan_penilaian_akhir" %>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
       
 <script type="text/javascript">
    $(function () {
        $("[id$=txtDateStart]").datepicker({
            dateFormat: 'dd-mm-yy',
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: '/icons/calendar.gif'
        });
        $("[id$=txtDateEnd]").datepicker({
            dateFormat: 'dd-mm-yy',
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: '/icons/calendar.gif'
        });
    });
</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Bank Soalan >> Arahan Penilaian Akhir >> </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Arahan Penilaian Akhir</td>
    </tr>
  <tr>
         <td style="width: 20%;">Tahun:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
         <td style="width: 20%;">Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlKohort" runat="server" Width="350px"></asp:DropDownList>
        </td>
    </tr>
      <tr>
         <td style="width: 20%;">Sesi Pengambilan:</td>
             <td><asp:CheckBoxList ID="chkSesi" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr> 
    <tr>
           <td style="width: 20%;">Semester:</td>
             <td> <asp:DropDownList ID="ddlSemester" runat="server"  Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
       <td style="width: 20%;">Tajuk:</td>
         <td><asp:TextBox ID="txttitle" runat="server" Width="740px"></asp:TextBox></td>
    </tr>
    <tr>
       <td style="width: 20%;">Tarikh Mula:</td>
         <td><asp:TextBox ID="txtDateStart" runat="server" ReadOnly = "true"></asp:TextBox></td>
    </tr>
    <tr>
       <td style="width: 20%;">Tarikh Akhir:</td>
         <td><asp:TextBox ID="txtDateEnd" runat="server" ReadOnly = "true"></asp:TextBox></td>
    </tr>
    </table>
<br />
<div id="divImport" runat="server">
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">MuatNaik Format Fail</td>
    </tr>
     <tr>
        <td style="width: 20%;">Pilih Fail PDF: </td>
         <td>
            <asp:FileUpload ID="FlUploadpdf" runat="server" />&nbsp;
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Hanya Fail .pdf dibenarkan"
                ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ControlToValidate="FlUploadpdf"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpload" runat="server" Text="Muatnaik " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
    </table>
    </div> 

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Arahan Penilaian Akhir
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="JWID"
                Width="100%" PageSize="40" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="NamaFail">
                        <ItemTemplate>
                            <asp:Label ID="FileName" runat="server" Text='<%# Bind("FileName")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="SaizFail">
                        <ItemTemplate>
                            <asp:Label ID="FileSize" runat="server" Text='<%# Bind("FileSize")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="URLFail">
                        <ItemTemplate>
                            <asp:Label ID="FilePath" runat="server" Text='<%# Bind("FilePath")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="Kohort" runat="server" Text='<%# Bind("Kohort")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="Semester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Tajuk">
                        <ItemTemplate>
                            <asp:Label ID="Title" runat="server" Text='<%# Bind("Title")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Catatan">
                        <ItemTemplate>
                            <asp:Label ID="Description" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh Mula">
                        <ItemTemplate>
                            <asp:Label ID="DateStart" runat="server" Text='<%# Bind("DateStart")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh Akhir">
                        <ItemTemplate>
                            <asp:Label ID="DateEnd" runat="server" Text='<%# Bind("DateEnd")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Created By">
                        <ItemTemplate>
                            <asp:Label ID="CreatedBy" runat="server" Text='<%# Bind("CreatedBy")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
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
        <td class="fbform_sap">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
        &nbsp;
             </td>
    </tr>
</table>
