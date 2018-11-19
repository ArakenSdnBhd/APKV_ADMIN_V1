<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_FIK_SVM.ascx.vb" Inherits="kpmkv.admin_FIK_SVM" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">FIK Layak SVM</td>
    </tr>
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="2">Jana Keseluruhan BM Setara</td>
    </tr>
   <tr>
        <td>BM Setara Pada Tahun:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" ></asp:DropDownList></td>  
    </tr>
    <tr>
        <td style="width: 20%;">Sesi:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>
        </td>
    </tr>
 </table> 
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="2">Jana BM Setara Mengikut Kolej</td>
    </tr>
    <tr>
        <td style="width: 20%;">Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" Width="350px" AutoPostBack="true"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Jenis Kolej:</td>
        <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td colspan ="2"> &nbsp;</td>
    </tr>
    <tr>
        <td style ="width :10%"><asp:Button ID="btnSearch" runat="server" Text="Jana Keseluruhan" CssClass="fbbutton" /></td> 
         <td> <asp:Button ID="btnJana" runat="server" Text="Jana Mengikut Kolej" CssClass="fbbutton" /></td> 
    </tr>
    </table>
<br />

<table class="fbform" style ="width :100%; " >
    <tr class="fbform_header">
        <td colspan="4">Jana Berperingkat</td>
    </tr>
    <tr>
        <td style ="text-align :center ">1.</td>
        <td style ="width :50%;font-style :italic ">Get Data from table kpmkv_pelajar & Insert into table kpmkv_svm</td>
        <td style ="width :15%;"><asp:Button ID="btnStep1" runat="server" Text="Jana Peringkat 1" CssClass="fbbutton" /></td>
        <td>Status :[ <asp:Label ID ="lblStep1" runat ="server" ></asp:Label> ]</td>
    </tr>
    
    <tr>
        <td style ="text-align :center ">2.</td>
        <td style ="font-style :italic ">Get [grade] from kpmkv_pelajar_markah & Update into table kpmkv_svm  by [IsBMTahun]</td>
        <td><asp:Button ID="btnStep2" runat="server" Text="Jana Peringkat 2" CssClass="fbbutton" /></td>
        <td>Status :[ <asp:Label ID ="lblStep2" runat ="server" ></asp:Label> ]</td>
    </tr>
    <tr>
         <td style ="text-align :center ">3.</td>
        <td style ="font-style :italic ">Update Record by semester isLayak='0' for uncompetent grade</td>
        <td><asp:Button ID="btnStep3" runat="server" Text="Jana Peringkat 3" CssClass="fbbutton" /></td>
        <td>Status :[ <asp:Label ID ="lblStep3" runat ="server" ></asp:Label> ]</td>
    </tr>
    <tr>
         <td style ="text-align :center ">4.</td>
        <td style ="font-style :italic ">Update Status (PNGKA,PNGKV,ISSETARA,Kompetensi,Layak SVM)</td>
        <td><asp:Button ID="btnStep4" runat="server" Text="Jana Peringkat 4" CssClass="fbbutton" /></td>
        <td>Status :[ <asp:Label ID ="lblStep4" runat ="server" ></asp:Label> ]</td>
    </tr>
 
    <tr>
        <td colspan ="4"></td>
    </tr>
</table>
<br />

<table class="fbform" style ="width :100%; " >
    <tr class="fbform_header">
        <td colspan="7">Pembersihan Data</td>

    </tr>
    </table>
 <asp:Panel ID ="pnl" runat ="server"  ScrollBars ="Vertical" Height ="250">
<table class="fbform" style ="width :100%; " >
    <tr>
        <td colspan ="7">
            <asp:DropDownList ID ="ddlCondition" runat ="server" >
            <asp:ListItem Value ="1">Jumlah Semester > 4</asp:ListItem>
            <asp:ListItem Value ="2">Jumlah Semester < 4</asp:ListItem>
            <asp:ListItem Value ="3">Jumlah Semester > 8</asp:ListItem>
            </asp:DropDownList>

            &nbsp; <asp:Button ID ="btnCari" Text ="Cari" runat ="server" />
        </td>
    </tr>

         <tr class="fbform_header" >
      
        <td style ="border-Spacing:initial;text-align :center ">Nama</td>
        <td style ="border-Spacing:initial;text-align :center ">Mykad</td>
        <td style ="border-Spacing:initial;text-align :center ">AngkaGiliran</td>
        <td style ="border-Spacing:initial;text-align :center ">KodKursus</td>
        <td style ="border-Spacing:initial;text-align :center ">Sesi</td>
        <td style ="border-Spacing:initial;text-align :center">Semester</td>
        <td style ="border-Spacing:initial">Padam</td>


    </tr>
    <span id="tblContent" runat="server"></span>
    
    </table>
</asp:Panel>
<table style="width :100%" class="fbform">
    <tr>
        <td><asp:button ID ="btnDelete" runat ="server" Text ="Padam Rekod" CssClass="fbbutton" Visible ="false"  /></td>
    </tr>
</table>
<br />


<table class="fbform">
    <tr class="fbform_header">
      <td colspan="2">
          <div class="info" id="divMsg" runat="server">
           <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
            <asp:Label ID="lblCOUNTP" runat="server" Text=""></asp:Label>

         </div>
            <asp:Button ID="btnExport" runat="server" Text="Eksport " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
    </table>
<br />
