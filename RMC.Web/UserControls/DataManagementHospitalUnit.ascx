<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataManagementHospitalUnit.ascx.cs"
    Inherits="RMC.Web.UserControls.DataManagementHospitalUnit" %>
<script language="javascript" type="text/javascript">

    function ConfirmMessageForAdministratorHospitalUnit(param) {

        var where_to = confirm("Do you really want to delete this Hospital Unit and related files?");
        if (where_to == true) {
            window.location.href = "DeleteHospital.aspx?" + param;
        }
    }
    
</script>
<table width="99%">
    <tr>
        <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">
            <u>Data Management</u>
        </th>
        <th align="right" style="padding-top: 10px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="6" OnClick="ImageButtonBack_Click" />
        </th>
    </tr>
    <tr>
        <td style="height: 10px;" colspan="2">
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <div style="padding-left: 15px;">
                <asp:LinkButton ID="LinkButtonHospitalIndex" Font-Bold="true" Font-Size="11px" runat="server"
                    OnClick="LinkButtonHospitalIndex_Click">Hospital Index</asp:LinkButton>
            </div>
            <div style="padding-left: 30px; padding-top: 2px;">
                <div style="float:left;width:92%;">
                    <asp:LinkButton ID="LinkButtonHospitalInformation" Font-Bold="true" Font-Size="11px"
                        runat="server" OnClick="LinkButtonHospitalInformation_Click">LinkButton</asp:LinkButton>
                </div>
                <div style="float:left;width:7%;" id="divEditHospital" runat="server">                    
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
            <span style="color: #06569d; font-size: 13px; font-weight: bold; padding-right: 5px;">
                (
                <asp:LinkButton ID="LinkButtonAddUnit" Font-Bold="true" Font-Size="13px" ForeColor="#06569d"
                    runat="server">Add Unit</asp:LinkButton>
                )</span>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr width="100%" />
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 2px;">
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-left: 5px; font-size: 10px;" colspan="2">
            <div id="divHospitalUnitNames" runat="server">
            </div>
            <div id="divEmptyMessage" style="background-color: #06569d; height: 20px; padding-left: 10px;
                padding-top: 3px; color: White; font-weight: bold; font-size: 11px; vertical-align: middle;"
                runat="server" visible="false">
                No record to display.
            </div>
        </td>
    </tr>
</table>
