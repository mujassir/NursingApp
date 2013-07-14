<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAdminTreeView.ascx.cs"
    Inherits="RMC.Web.UserControls.ucAdminTreeView" %>
<!--<script src="../jquery/jquery-latest.js" type ="text/javascript"></script>-->
<%-- <link rel="stylesheet" href="../CSS/screen.css"  type="text/css" />
  <link rel="stylesheet" href="../CSS/treeview.css" type="text/css" />--%>
<!--<script type="text/javascript" src="../jquery/jquery.treeview.js"></script>-->

<script type="text/javascript" src="http://dev.jquery.com/view/trunk/plugins/treeview/jquery.treeview.js"></script>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        $("#ulHospitalTree").treeview();
    });
    function TestToggle(vartest) {
        alert(vartest + " has been toggled");
    }

    function OpenWindow() {
        window.open('MonthDetail.aspx', 'windowname1', 'width=800, height=650');
    }
    function OpenErrorWindow() {
        window.open('MonthlyErrorReport.aspx', 'windowname1', 'width=800, height=650');
    }

    function deleteFile(nurseId) {

        var browserName = navigator.appName;
        if (confirm("Are you sure want to delete this File?")) {

            var filePath = 'DeleteNursePDAInfo.aspx?NurseID=' + nurseId;

            if (browserName == "Microsoft Internet Explorer") {
                window.location.href(filePath);
            }
            else {
                alert("/" + filePath);
                window.location = filePath;
            }
        }
    }

    function deleteAllFiles(year, month, hospitalUnitID) {

        var browserName = navigator.appName;
        if (confirm("Are you sure want to delete all Files?")) {

            var filePath = 'DeleteAll.aspx?Type=DataImport&Year=' + year + "&Month=" + month + "&HospitalUnitID=" + hospitalUnitID;

            if (browserName == "Microsoft Internet Explorer") {
                window.location.href(filePath);
            }
            else {
                alert("/" + filePath);
                window.location = filePath;
            }
        }
    }
</script>

<table width="99%">
    <tr>
        <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">
            <%--<h3 style="font-size: 13px;">--%>
            <u>Data Management</u>
            <%--</h3>--%>
        </th>
    </tr>
    <tr>
        <td style="height: 10px;">
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-left: 15px; font-size: 10px;">
            <div id="divTreeView" runat="server">
            </div>
        </td>
    </tr>
</table>
