<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationProfile.ascx.cs"
    Inherits="RMC.Web.UserControls.WebUserControl1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc11" %>
<style type="text/css">
    .Background
    {
        position: fixed;
        left: 0;
        top: 0;
        z-index: 10;
        width: 100%;
        height: 100%;
        filter: alpha(opacity=60);
        opacity: 0.6;
        background-color: Black;
        visibility: hidden;
    }
</style>

<script type="text/javascript">
    function pageLoad() {

        var manager = Sys.WebForms.PageRequestManager.getInstance();

        manager.add_endRequest(endRequest);

        manager.add_beginRequest(onBeginRequest);

    }

    function onBeginRequest(sender, args) {

        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = 'Background';
        divParent.style.visibility = 'visible';
        divImage.style.visibility = 'visible';

    }

    function endRequest(sender, args) {

        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = '';
        divParent.style.visibility = 'hidden';
        divImage.style.visibility = 'hidden';
    }
</script>

<asp:ScriptManager ID="ScriptManagerRMCReportLocationProfile" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">


    function windowPopup(page, title) {

        window.open(page, '_blank', 'height=355,width=710,top=150,left=150,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false');
        return false;
    }

    //    function monthSelect() {
    //       
    //        ValidatorEnable(document.getElementById("RequiredFieldValidatorMonthFrom"), true); 
    //    }

</script>

<asp:Panel ID="PanelReportLocationProfile" runat="server">
    <table width="100%">
        <tr>
            <th align="left" style="font-size: 14px; padding-left: 20px; padding-top: 10px; color: #06569d;">
                <%--<h3 style="font-size: 13px;">--%>
                <u>From/To Trips Report</u>
                <%--</h3>--%>
            </th>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                    <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
                        z-index: 10;">
                        <div style="text-align: left; padding-left: 5px;">
                            <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="Demographic"
                                runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Font-Size="11px"
                                Font-Bold="true" Style="padding-top: 1px;" />
                            <asp:Panel ID="PanelErrorMsg" runat="server" Style="padding-top: 1px;" Visible="false">
                                <ul>
                                    <li>
                                        <asp:Label ID="LabelErrorMsg" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                    </li>
                                </ul>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="85%">
                    <tr>
                        <td colspan="4">
                            <table style="padding-left: 43px;">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="LabelBenchmarkingFilters" runat="server" Text="BenchMarking Filter"
                                            ForeColor="#06569d" Font-Bold="true" Font-Size="11px"></asp:Label>
                                        &nbsp&nbsp
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListBenchmarkingFilter"
                                            ForeColor="#06569D" runat="server" AppendDataBoundItems="True" TabIndex="1" DataSourceID="ObjectDataSourceBenchmarkingFilterNames"
                                            DataTextField="FilterName" DataValueField="FilterId" OnSelectedIndexChanged="DropDownListBenchmarkingFilter_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0" Selected="true">Select Filter</asp:ListItem>
                                            <asp:ListItem Value="1">No Filter</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorFilter" runat="server" ControlToValidate="DropDownListBenchmarkingFilter"
                                            SetFocusOnError="true" Display="None" ErrorMessage="Must Select Filter" InitialValue="0"
                                            ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="padding-left: 21px;">
                                <tr>
                                    <td align="right" style="width: 143px;">
                                        <asp:Label ID="Label1" runat="server" Text="Hospital Name" ForeColor="#06569d" Font-Bold="true"
                                            Font-Size="11px"></asp:Label>
                                        <span id="spanHospitalName" style="color: Red;" runat="server">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListHospitalName" ForeColor="#06569D"
                                            runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="DropDownListHospitalName_SelectedIndexChanged"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Select Hospital Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalName" runat="server"
                                                    ControlToValidate="DropDownListHospitalName" SetFocusOnError="true" Display="None"
                                                    ErrorMessage="Must Select Hospital Name" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td style="width: 46px;">
                                    </td>
                                    <td align="right" style="width: 130px;">
                                        <asp:Label ID="Label5" runat="server" Text="Hospital Unit" ForeColor="#06569d" Font-Bold="true"
                                            Font-Size="11px"></asp:Label>
                                        <span id="spanHospitalUnit" style="color: Red;" runat="server">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListHospitalUnit" ForeColor="#06569D"
                                            runat="server" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceHospitalUnit"
                                            DataTextField="HospitalUnitName" DataValueField="HospitalDemographicID" TabIndex="2"
                                            OnSelectedIndexChanged="DropDownListHospitalUnit_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select Hospital Unit</asp:ListItem>
                                        </asp:DropDownList>
                                        <%if (DropDownListHospitalName.SelectedIndex != 0)
                                          { %>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalUnit" runat="server"
                                            ControlToValidate="DropDownListHospitalUnit" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Must Select Hospital Unit" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                        <%} %>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:UpdatePanel ID="UpdatePanelYearMonth" ChildrenAsTriggers="true" runat="server"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="padding-right: 20px;">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelYearFrom" runat="server" Text="Period From" ForeColor="#06569d"
                                                    Font-Bold="true" Font-Size="11px"></asp:Label>
                                                &nbsp&nbsp
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListYearMonthFrom" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="DropDownListYearMonthFrom_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Period From</asp:ListItem>
                                                </asp:DropDownList>
                                                <%if (DropDownListYearMonthFrom.SelectedValue == "0" && DropDownListYearMonthTo.SelectedValue != "0")
                                                  { %>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYearMonthFrom" runat="server"
                                                    ControlToValidate="DropDownListYearMonthFrom" Display="None" ErrorMessage="Must Select Period From"
                                                    InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                <%} %>
                                            </td>
                                            <td style="width: 115px;">
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="LabelYearTo" runat="server" Text="Period To" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                &nbsp&nbsp
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListYearMonthTo" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" AutoPostBack="true" TabIndex="5" OnSelectedIndexChanged="DropDownListYearMonthTo_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Period To</asp:ListItem>
                                                </asp:DropDownList>
                                                <%if (DropDownListYearMonthTo.SelectedValue == "0" && DropDownListYearMonthFrom.SelectedValue != "0")
                                                  { %>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYearMonthTo" runat="server"
                                                    ControlToValidate="DropDownListYearMonthTo" Display="None" ErrorMessage="Must Select Period To"
                                                    InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                <%} %>
                                                <%--<asp:CompareValidator ID="CompareValidatorYearToYearFrom" runat="server" ControlToCompare="DropDownListYear"
                                                    ControlToValidate="DropDownListYearTo" ErrorMessage="Year To must be greater than or equal to Year From"
                                                    Operator="GreaterThanEqual" Type="Integer" Display="None" ValidationGroup="Demographic">*</asp:CompareValidator>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 35px;" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
                            <asp:LinkButton ID="LinkButtonLocationNames" CssClass="aspLinkButton" runat="server"
                                Text="Standardize Location Names" Font-Bold="true" Font-Size="10px" OnClick="LinkButtonLocationNames_Click1" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="ButtonGenerateReport" runat="server" Text="Generate Report" ValidationGroup="Demographic"
                                CssClass="aspButton" Width="130px" Visible="true" OnClick="ButtonGenerateReport_Click"
                                TabIndex="21" />&nbsp
                            <asp:Button ID="ButtonReset" runat="server" Text="Reset" Visible="True" CssClass="aspButton"
                                Width="70px" OnClick="ButtonReset_Click" TabIndex="22" />
                        </td>
                    </tr>
                </table>
                <%--test--%>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:ObjectDataSource ID="ObjectDataSourceBenchmarkingFilterNames" runat="server"
    SelectMethod="GetBenchmarkFilterNames" TypeName="RMC.BussinessService.BSReports">
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceHospitalUnit" runat="server" SelectMethod="GetHospitalDemographicDetailByHospitalID"
    TypeName="RMC.BussinessService.BSHospitalDemographicDetail">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownListHospitalName" DefaultValue="0" Name="hospitalID"
            PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>
