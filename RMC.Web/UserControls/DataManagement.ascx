<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataManagement.ascx.cs"
    Inherits="RMC.Web.UserControls.DataManagement" %>
<style type="text/css">
    .Background
    {
        position: fixed;
        left: 0;
        top: 0;
        z-index: 10;
        width: 100%;
        height: 100%;
        filter: alpha(opacity=100);
        background-color: Black;
        visibility: hidden;
    }
</style>
<script type="text/javascript">

    function ConfirmMessageForAdministrator(param) {

        var where_to = confirm("Do you really want to delete this Hospital and related units and files?");
        if (where_to == true) {
            window.location.href = "DeleteHospital.aspx?" + param;
        }
    }

    function removeParentUnderline(id) {

        alert(id.parentElement);
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
    <%--<tr>
        <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">           
            <a href="../uploads/" target="_blank" style="color: #06569d; font-size: 13px; font-weight: bold; padding-right: 5px;">Raw Data Access</a>            
        </th>
    </tr>--%>
    <tr id="trAddHospital" runat="server">
        <td align="right">
            <span style="color: #06569d; font-size: 13px; font-weight: bold; padding-right: 5px;">
                (
                <asp:LinkButton ID="LinkButtonAddHospital" Font-Bold="true" Font-Size="13px" ForeColor="#06569d"
                    runat="server">Add Hospital</asp:LinkButton>
                ) </span>
        </td>
    </tr>
    <tr>
        <td>
            <hr width="100%" />
        </td>
    </tr>
    <tr>
        <td style="height: 2px;">
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-left: 5px; font-size: 10px;">
            <div id="divHospitalNames" runat="server">
            </div>
            <div id="divEmptyMessage" style="background-color: #06569d; height: 20px; padding-left: 10px;
                padding-top: 3px; color: White; font-weight: bold; font-size: 11px; vertical-align: middle;"
                runat="server" visible="false">
                No record to display.
            </div>
        </td>
    </tr>
</table>
