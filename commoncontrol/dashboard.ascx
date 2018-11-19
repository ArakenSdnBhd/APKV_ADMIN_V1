<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="dashboard.ascx.vb" Inherits="kpmkv.dashboard1" %>



<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/canvasjs.min.js" />
        <asp:ScriptReference Path="~/js/excanvas.js" />
    </Scripts>
</asp:ScriptManager>

<asp:label ID ="lblData" runat ="server" ></asp:label>
<script type="text/javascript">
    var list = ;
    window.onload = function () {
        var chart = new CanvasJS.Chart("chartContainer", {

            title: {
                text: "Jumlah Pelajar"  //**Change the title here
            },
            data: [
    {
        type: "column",
        dataPoints: list }
            ]
        });

        chart.render();
    }
          </script>


 <table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Dashboard</td>
    </tr>
     </table>
<br />

<div id="chartContainer"  style="height:100%; width: 70%;"></div>

     

   
