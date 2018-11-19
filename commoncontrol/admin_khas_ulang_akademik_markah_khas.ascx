<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_khas_ulang_akademik_markah_khas.ascx.vb" Inherits="kpmkv.admin_khas_ulang_akademik_markah_khas" %>
<script type="text/javascript">
    function deleteConfirm(pubid) {
        var result = confirm('Anda pasti untuk padam ?');
        if (result) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Akademik&gt;&gt;Pemeriksa &gt;&gt;Borang Markah Khas</td>
    </tr>
</table>
<br />
<table class="fbform">
<tr class="fbform_header">
        <td>Senarai Calon Borang Markah Khas.</td>
    </tr>
    <tr>
       <td>
       <asp:GridView ID="gridView" DataKeyNames="KhasID" runat="server" AutoGenerateColumns="False" ShowFooter="True" HeaderStyle-Font-Bold="true"
        onrowdeleting="gridView_RowDeleting"  onrowediting="gridView_RowEditing" onrowcommand="gridView_RowCommand" OnRowDataBound="gridView_RowDataBound" CellPadding="4" 
        EnableModelValidation="True" ForeColor="#333333" GridLines="None"
        Width="100%" PageSize="40"  CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <%--<AlternatingRowStyle BackColor="White" ForeColor="#284775" />--%>
        <Columns>
        <asp:TemplateField HeaderText="KhasID" Visible="False">
        <ItemTemplate>
        <asp:Label ID="txtKhasID" runat="server" Text='<%#Eval("KhasID")%>'/>
       </ItemTemplate>
       <EditItemTemplate>
        <asp:Label ID="lblKhasID" runat="server" width="10px" Text='<%#Eval("KhasID")%>'/>
      </EditItemTemplate>
    <FooterTemplate>
<%--        <asp:TextBox ID="inKhasID" width="40px" runat="server"/>--%>
<%--        <asp:RequiredFieldValidator ID="vKhasID" runat="server" ControlToValidate="inKhasID" Text="?" ValidationGroup="validaiton"/>--%>
    </FooterTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Mykad">
      <ItemTemplate>
         <asp:Label ID="lblMykad" runat="server" Text='<%#Eval("Mykad")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtMykad" width="100px"  runat="server" Text='<%#Eval("Mykad") %>'/>
     </EditItemTemplate>
     <FooterTemplate>
         <asp:TextBox ID="inMykad"  width="100px" runat="server"/>
<%--         <asp:RequiredFieldValidator ID="vMykad" runat="server" ControlToValidate="inMykad" Text="?" ValidationGroup="validaiton"/>--%>
     </FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField HeaderText="AngkaGiliran">
     <ItemTemplate>
         <asp:Label ID="lblAngkaGiliran" runat="server" Text='<%#Eval("AngkaGiliran")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtAngkaGiliran" width="100px" runat="server" Text='<%#Eval("AngkaGiliran")%>'/>
     </EditItemTemplate>
    <FooterTemplate>
        <asp:TextBox ID="inAngkaGiliran" width="100px"  runat="server"/>
<%--        <asp:RequiredFieldValidator ID="vAngkaGiliran" runat="server" ControlToValidate="inAngkaGiliran" Text="?" ValidationGroup="validaiton"/>--%>
         </FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField HeaderText="KodKursus">
     <ItemTemplate>
         <asp:Label ID="lblKodKursus" runat="server" Text='<%#Eval("KodKursus")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtKodKursus" width="100px" runat="server" Text='<%#Eval("KodKursus")%>'/>
     </EditItemTemplate>
    <FooterTemplate>
        <asp:TextBox ID="inKodKursus" width="100px" runat="server" />
<%--    <asp:RequiredFieldValidator ID="vKodKursus" runat="server" ControlToValidate="inKodKursus" />--%>
    </FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Matapelajaran">
     <ItemTemplate>
         <asp:Label ID="lblMatapelajaran" runat="server" Text='<%#Eval("Matapelajaran")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtMatapelajaran" width="200px" runat="server" Text='<%#Eval("Matapelajaran")%>'/>
     </EditItemTemplate>
    <FooterTemplate>
        <asp:TextBox ID="inMatapelajaran" width="200px" runat="server" />
<%--    <asp:RequiredFieldValidator ID="vMatapelajaran" runat="server" ControlToValidate="inMatapelajaran" />--%>
    </FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Markah">
     <ItemTemplate>
         <asp:Label ID="lblMarkah" runat="server" Text='<%#Eval("Markah")%>'/>
     </ItemTemplate>
    <EditItemTemplate>
         <asp:TextBox ID="txtMarkah" width="100px"  runat="server" Text='<%#Eval("Markah")%>'/>
     </EditItemTemplate>
    <FooterTemplate>
        <asp:TextBox ID="inMarkah" width="100px" runat="server"/>
<%--        <asp:RequiredFieldValidator ID="vMarkah" runat="server" ControlToValidate="inMarkah" Text="?" ValidationGroup="validation"/>--%>
    </FooterTemplate>
 </asp:TemplateField>
    <asp:TemplateField HeaderText="Catatan">
     <ItemTemplate>
         <asp:Label ID="lblCatatan" runat="server" Text='<%#Eval("Catatan")%>'/>
     </ItemTemplate>
    <EditItemTemplate>
         <asp:TextBox ID="txtCatatan" width="300px"  runat="server" Text='<%#Eval("Catatan")%>'/>
     </EditItemTemplate>
    <FooterTemplate>
        <asp:TextBox ID="inCatatan" width="300px" runat="server"/>
<%--        <asp:RequiredFieldValidator ID="vCatatan" runat="server" ControlToValidate="inCatatan" Text="?" ValidationGroup="validaiton"/>--%>
    </FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField>
    <EditItemTemplate>
<%--        <asp:Button ID="ButtonUpdate" runat="server" CommandName="Update"  Text="Update"  />
        <asp:Button ID="ButtonCancel" runat="server" CommandName="Cancel"  Text="Cancel" />--%>
    </EditItemTemplate>
    <ItemTemplate>
<%--        <asp:Button ID="ButtonEdit" runat="server" CommandName="Edit"  Text="Edit"  />--%>
        <asp:Button ID="ButtonDelete" runat="server" CommandName="Delete"  Text="Delete"  />
    </ItemTemplate>
    <FooterTemplate>
        <asp:Button ID="ButtonAdd" runat="server" CommandName="AddNew"  Text="Add New Row" ValidationGroup="validaiton" />
    </FooterTemplate>
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
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td style="text-align: center" >Borang Markah Khas</td>
    </tr>
   
    <tr>
         <td style="text-align:center"><asp:Button ID="btnCetak" runat="server" Text="Cetak" CssClass="fbbutton"/>&nbsp;<asp:HyperLink ID="HyPDF2" runat="server" Target="_blank"
                    Visible="false">Klik disini untuk muat turun.</asp:HyperLink> </td>
    </tr>
</table>



<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblType" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblTahun" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblMatapelajaran" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>