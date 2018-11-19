<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="prm_tahun.ascx.vb" Inherits="kpmkv.prm_tahun1" %>
<div>
    <table class="fbform" style="width:100%">
        <tr>
            <td>
               <a href="#" runat ="server" id="SaveFunction"><img title="Save" style="vertical-align: middle;" src="icons/save.png" width="20" height="20" alt="::"/></a>
               |<a href ="#" runat ="server" id ="Refresh" ><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="20" height="20" alt="::"/></a>
               
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr class="fbform_header">
            <td>Konfigurasi</td>
        </tr>
    </table>
</div>
<br />
<div id="addform">
<table class="fbform" style="width :100%">
    <tr>
         <td style ="width :20%">Tahun</td>
         <td style ="width :2%">:</td>
         <td><asp:TextBox ID="txtTahun" runat="server"  Width="350px" ></asp:TextBox></td>
    </tr>
    <tr>
         <td>Kod</td>
         <td>:</td>
         <td><asp:TextBox ID="txtCode" runat="server" Width="350"></asp:TextBox></td>
    </tr>
    <tr>
         <td>RunningNo's Digit </td>
         <td>:</td>
         <td><asp:TextBox ID="txtDigit" runat="server" Width="100"></asp:TextBox>
             <span style ="font-size :11px; color :darkgrey ">&nbsp; (cth: 6 digit = 000000)</span>
         </td>
    </tr>
    <tr>
         <td>RunningNo Terakhir</td>
         <td>:</td>
         <td><asp:TextBox ID="txtLastNo" runat="server" Width="100" Enabled ="false" ></asp:TextBox></td>
    </tr>
    <%--<tr>
         <td>Status</td>
         <td>:</td>
         <td><asp:DropDownList ID ="ddlStatus" runat ="server" >
             <asp:ListItem Value ="Y">Y = Aktif</asp:ListItem>
             <asp:ListItem Value ="N">Y = Tidak Aktif</asp:ListItem>
             </asp:DropDownList></td>
    </tr>--%>
   
</table>
</div>
<br />

<%-- List --%>
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td>Senarai Konfigurasi Tahun </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="Panel" runat="server" ScrollBars="vertical" Height="230" Width="100%">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="false"  
             CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="TahunID"
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
                    <asp:TemplateField HeaderText="Tahun" >
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="25%" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod">
                        <ItemTemplate>
                            <asp:Label ID="Kod" runat="server" Text='<%# Bind("Kod")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign ="Center" />
                        <HeaderStyle HorizontalAlign="center" VerticalAlign="Top" Width ="25%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RunnningNo's Digit">
                        <ItemTemplate>
                            <asp:Label ID="RunningNoDigit" runat="server" Text='<%# Bind("RunningNoDigit")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width ="35%" />
                        <ItemStyle VerticalAlign="Middle"  />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="RunnningNo Terakhir">
                        <ItemTemplate>
                            <asp:Label ID="LastRunningNo" runat="server" Text='<%# Bind("LastRunningNo")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width ="35%" />
                        <ItemStyle VerticalAlign="Middle"  />
                    </asp:TemplateField>
                     <%--<asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign ="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width ="10%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>--%>
                    
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>

                           <span runat="server" style="float:right">

                           <a href ="prm.tahun.aspx?edit=<%#Eval("TahunID")%>"><img title="Edit"  src="icons/edit.png" width="13" height="13" alt="::"/></a>
                          
                            </span> 
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="right" VerticalAlign="Top"  /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                   
                     
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Center" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
                </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="3"></td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblMsg" runat="server" Text="Message.."></asp:Label>
</div>