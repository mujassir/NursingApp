<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeRN.ascx.cs" Inherits="RMC.Web.UserControls.TimeRN" %>
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
    .style1
    {
        width: 33%;
    }
    .style2
    {
        width: 27%;
    }
    .style3
    {
        width: 32.5%;
    }
</style>

<script type="text/javascript">
    function checkListbox() {
        if (document.getElementById('<%=DropDownListHospitalName.ClientID %>').value == "0") {
            alert("Please Select Hospital From List");
            return false;
        }
        if (document.getElementById('<%=DropDownListHospitalUnit.ClientID %>').value == "0") {
            alert("Please Select Hospital Unit From List");
            return false;
        }
        if (document.getElementById('<%=ListBoxSelectedProfiles.ClientID %>').outerText == "") {
            alert("Atleast One Profile Must Be Chosen");
            return false;
        }
        var datefrom = document.getElementById('<%=DropDownListYearMonthFrom.ClientID %>').value;
        var dateto = document.getElementById('<%=DropDownListYearMonthTo.ClientID %>').value;
        var arDatefrom = new Array();
        var arDateto = new Array();
        arDatefrom = datefrom.split(',');
        arDateto = dateto.split(',');     
        if(datefrom !="0" && dateto=="0") {            
            alert("Please Select PeriodTo");
            return false;
        }
        if (dateto != "0" && datefrom == "0") {
            alert("Please Select PeriodFrom");
            return false;
        }
        if (datefrom != "0" && dateto != "0") {
            if (parseInt(arDatefrom[1]) == parseInt(arDateto[1])) {
                if (parseInt(arDatefrom[0]) > parseInt(arDateto[0])) {
                    alert("PeriodTo Cannot Be Less Then PeriodFrom");
                    return false;
                }
            }
            if (parseInt(arDatefrom[1]) > parseInt(arDateto[1])) {
                alert("PeriodTo Cannot Be Less Then PeriodFrom");
                return false;
            } 
        }        
    }
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

<asp:ScriptManager ID="ScriptManagerRMCHospitalBenchmark" runat="server">
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

<asp:Panel ID="PanelHospitalUnitInformation" runat="server">
 
    <table width="800px">
        <tr>
            <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                <%--<u>Time Study RN Summary Report</u>--%>
                <asp:Label ID="LabelHeading" runat="server" Text="" Font-Underline="true"></asp:Label>
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
                        <td>
                            <%--<asp:UpdatePanel ID="UpdatePanelHospital" runat="server" ChildrenAsTriggers="true"
                                UpdateMode="Conditional">
                                <ContentTemplate>--%>
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalName" runat="server"
                                                    ControlToValidate="DropDownListHospitalName" SetFocusOnError="true" Display="None"
                                                    ErrorMessage="Must Select Hospital Name" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 46px;"></td>
                                            <td align="right" style="width: 130px;">
                                                <asp:Label ID="Label5" runat="server" Text="Hospital Unit" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                <span id="spanHospitalUnit" style="color: Red;" runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListHospitalUnit" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceHospitalUnit"
                                                    DataTextField="HospitalUnitName" DataValueField="HospitalDemographicID" 
                                                    TabIndex="2" AutoPostBack="True" 
                                                    onselectedindexchanged="DropDownListHospitalUnit_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Hospital Unit</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalUnit" runat="server"
                                                    ControlToValidate="DropDownListHospitalUnit" SetFocusOnError="true" Display="None"
                                                    ErrorMessage="Must Select Hospital Unit" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="padding-left: 20px;">
                                <tr>
                                    <td align="right" class="style1">
                                        <asp:Label ID="LabelBenchmarkingFilters" runat="server" Text="BenchMarking Filter"
                                            ForeColor="#06569d" Font-Bold="true" Font-Size="11px"></asp:Label>
                                        &nbsp&nbsp
                                    </td>
                                    <td align="left" style="width: 30%;">
                                        <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListBenchmarkingFilter"
                                            ForeColor="#06569D" runat="server" AppendDataBoundItems="True" TabIndex="3" DataSourceID="ObjectDataSourceBenchmarkingFilterNames"
                                            DataTextField="FilterName" DataValueField="FilterId">
                                            <asp:ListItem Value="0">Select Filter</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="true">No Filter</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFilter" runat="server" ControlToValidate="DropDownListBenchmarkingFilter"
                                            SetFocusOnError="true" Display="None" ErrorMessage="Must Select Filter" InitialValue="0"
                                            ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td align="left" style="width: 30%;">
                                    </td>
                                    <td align="left" style="width: 30%;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Profile(s)" ForeColor="#06569d" Font-Bold="true"
                                            Font-Size="11px" Font-Underline="true"></asp:Label>
                                        &nbsp&nbsp
                                    </td>
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="height: 5px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <asp:Label ID="LabelValueAddedProfile" runat="server" Text="" ForeColor="#06569d"
                                            Font-Bold="true" Font-Size="11px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ListBox CssClass="aspListBox" ID="ListBoxAvailableProfiles" runat="server" ForeColor="#06569D"
                                            SelectionMode="Multiple" Height="120px" Rows="8" TabIndex="4" ToolTip="Available Profile(s)">
                                        </asp:ListBox>
                                    </td>
                                    <td align="center" style="width: 180px;">
                                        <asp:Button ID="ButtonAdd" runat="server" Text=">" Font-Bold="true" Width="40px"
                                            Visible="true" OnClick="ButtonAdd_Click" ToolTip="Add Profile" TabIndex="5" /><br />
                                        <br />
                                        <asp:Button ID="ButtonRemove" runat="server" Text="<" Font-Bold="true" Width="40px"
                                            Visible="true" OnClick="ButtonRemove_Click" ToolTip="Remove Profile" TabIndex="7" />
                                    </td>
                                    <td>
                                        <asp:ListBox CssClass="aspListBox" ID="ListBoxSelectedProfiles" runat="server" ForeColor="#06569D"
                                            SelectionMode="Multiple" Height="120px" Rows="8" TabIndex="6" ToolTip="Selected Profile(s)">
                                        </asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorSelectedProfiles" runat="server"
                                            ControlToValidate="ListBoxSelectedProfiles" Display="None" ErrorMessage="Must Select Profiles"
                                            InitialValue="String" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:Label ID="LabelLocationProfile" runat="server" Text="" ForeColor="#06569d" Font-Bold="true"
                                            Font-Size="11px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <%--<asp:UpdatePanel ID="UpdatePanelYearMonth" ChildrenAsTriggers="true" runat="server"
                                UpdateMode="Conditional">
                                <ContentTemplate>--%>
                                    <table style="padding-right: 20px;">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelYearFrom" runat="server" Text="Period From" ForeColor="#06569d"
                                                    Font-Bold="true" Font-Size="11px"></asp:Label>
                                                &nbsp&nbsp
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListYearMonthFrom" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" TabIndex="8">
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
                                                    runat="server" AppendDataBoundItems="True" TabIndex="9">
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
                               <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 35px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="ButtonGenerateReport" runat="server" Text="Generate Report" ValidationGroup="Demographic"
                                CssClass="aspButton" Width="130px" Visible="true" OnClick="ButtonGenerateReport_Click" OnClientClick="return checkListbox();"
                                TabIndex="10" />&nbsp
                            <asp:Button ID="ButtonReset" runat="server" Text="Reset" Visible="True" CssClass="aspButton"
                                Width="70px" OnClick="ButtonReset_Click" TabIndex="11" />
                        </td>
                    </tr>
                </table>
                <%--test--%>
            </td>
        </tr>
        <tr>
            <td>
                <%--<asp:ObjectDataSource ID="ObjectDataSourcePharmacyType" runat="server" SelectMethod="GetAllPharmacyType"
                    TypeName="RMC.BussinessService.BSPharmacyType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceUnitType" runat="server" SelectMethod="GetAllUnitType"
                    TypeName="RMC.BussinessService.BSUnitType"></asp:ObjectDataSource>--%>
                <asp:ObjectDataSource ID="ObjectDataSourceProfileTypeValueAdded" runat="server" SelectMethod="GetProfileTypeValueAdded"
                    TypeName="RMC.BussinessService.BSProfileType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceProfileTypeOthers" runat="server" SelectMethod="GetProfileTypeOthers"
                    TypeName="RMC.BussinessService.BSProfileType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceProfileTypeLocation" runat="server" SelectMethod="GetProfileTypeLocation"
                    TypeName="RMC.BussinessService.BSProfileType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceHospitalUnit" runat="server" SelectMethod="GetHospitalDemographicDetailByHospitalID"
                    TypeName="RMC.BussinessService.BSHospitalDemographicDetail">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListHospitalName" DefaultValue="0" Name="hospitalID"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <%--<asp:ObjectDataSource ID="ObjectDataSourceCountry" runat="server" SelectMethod="GetAllCountries"
                    TypeName="RMC.BussinessService.BSCommon"></asp:ObjectDataSource>--%>
                <%--<asp:ObjectDataSource ID="ObjectDataSourceState" runat="server" SelectMethod="GetAllStateNamesByCountryID"
                    TypeName="RMC.BussinessService.BSCommon">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListCountry" DefaultValue="0" Name="CountryID"
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>--%>
                <%-- <asp:LinqDataSource ID="LinqDataSourceHospitalType" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                    OrderBy="HospitalTypeName" TableName="HospitalTypes">
                </asp:LinqDataSource>--%>
                <%-- <asp:ObjectDataSource ID="ObjectDataSourceOperator" runat="server" SelectMethod="Operator"
                    TypeName="RMC.BussinessService.BSCommon"></asp:ObjectDataSource>--%>
                <asp:ObjectDataSource ID="ObjectDataSourceYear" runat="server" SelectMethod="Years"
                    TypeName="RMC.BussinessService.BSYear">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="2002" Name="startingYear" Type="Int32" />
                        <asp:Parameter DefaultValue="20" Name="noOfYears" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalName" runat="server"
                    ErrorMessage="Select Hospital Name." ControlToValidate="DropDownListHospitalName"
                    Display="None" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorUnitName" runat="server" ErrorMessage="Required Unit Name."
                    ControlToValidate="TextBoxUnitName" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorUnitType" runat="server" ErrorMessage="Required Unit Type."
                    ControlToValidate="TextBoxUnitType" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorDemographic" runat="server"
                    ErrorMessage="Required Demographic." ControlToValidate="TextBoxDemographic" Display="None"
                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInUnit" runat="server"
                    SetFocusOnError="true" ErrorMessage="Required 'Beds In Units'." ControlToValidate="TextBoxBedsInUnit"
                    Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInHospital" runat="server"
                    ErrorMessage="Required 'Beds In Hospital'." ControlToValidate="TextBoxBedsInHospital"
                    Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorPatientsPerNurse" runat="server"
                    SetFocusOnError="true" ErrorMessage="Required 'Budgeted Patients Per Nurse'."
                    ControlToValidate="TextBoxPatientsPerNurse" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorPharmacyType" runat="server"
                ErrorMessage="Required Pharmacy Type." ControlToValidate="TextBoxPharmacyType"
                Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorElectronicDoc" runat="server"
                    SetFocusOnError="true" ControlToValidate="TextBoxElectronicDocumentation" Display="None"
                    ErrorMessage="Required Electronic Documentation." ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorPharmacyType" runat="server"
                    SetFocusOnError="true" ControlToValidate="ListBoxPharmacyType" Display="None"
                    ErrorMessage="Select Pharmacy Type." ValidationGroup="Demographic" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorUserType" runat="server" ControlToValidate="ListBoxUnitType"
                    SetFocusOnError="true" Display="None" ErrorMessage="Select Unit Type." ValidationGroup="Demographic"
                    InitialValue="0">*</asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        </table>
        <table>
        <tr>
        <td>
          <div style="height: 5px;">
            </div>
            <div style="text-align: left; padding-left: 2px;">
                <asp:LinkButton ID="LinkButtonExportReport" Visible="false" Font-Size="11px" runat="server" TabIndex="2"
                    OnClick="LinkButtonExportReport_Click" Font-Bold="true">ExportToExcel</asp:LinkButton>
            </div>
            <div style="height: 1px;">
            </div>
        </td>
        </tr>
        <tr>
        <td>
       <%-- DataSourceID="ObjectDataSource1"--%>
            <asp:GridView ID="GridViewReport" runat="server" 
                CellPadding="1" EmptyDataText="No Record to display." CssClass="GridViewStyle"
                HorizontalAlign="Center" OnRowDataBound="GridViewReport_RowDataBound">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>
        </td>
        </tr>
         <tr>
        <td>
       <%-- DataSourceID="ObjectDataSource2"--%>
            <asp:GridView ID="GridViewFunctionReport" runat="server" 
                CellPadding="1" EmptyDataText="No Record to display." CssClass="GridViewStyle"
                HorizontalAlign="Center" OnRowDataBound="GridViewFunctionReport_RowDataBound">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" BackColor="#80ffff" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-left: 5px; padding-top: 10px;">
            <asp:LinkButton ID="LinkButtonSaveImage" Visible="false" Font-Bold="true" Font-Size="11px" runat="server"
                OnClick="LinkButtonSaveImage_Click">SaveImage</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td style="margin-left: 40px; padding-bottom: 5px;" align="center">
            <asp:Chart ID="ChartTimeRN" runat="server" Visible="false" BackColor="226, 205, 214" BorderColor="#1A3B69"
                BackSecondaryColor="White" BackGradientStyle="TopBottom" Width="765px" Height="450px">
                <Titles>
                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 11pt, style=Bold" ShadowOffset="3"
                        Text="" Name="Title1" ForeColor="26, 59, 105">
                    </asp:Title>
                </Titles>
                <Legends>
                    <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                        Alignment="Center" Docking="Bottom" LegendStyle="Row">
                    </asp:Legend>
                </Legends>
                <BorderSkin SkinStyle="Emboss" />
                <Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" Area3DStyle-Rotation="10">
                        <Area3DStyle Rotation="10"></Area3DStyle>
                        <AxisY LineColor="64, 64, 64, 64">
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisY>
                        <AxisX LineColor="64, 64, 64, 64" IntervalAutoMode="VariableCount" Enabled="True"
                            Interval="1">
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </td>
    </tr>
    </table>
</asp:Panel>
<asp:ObjectDataSource ID="ObjectDataSourceBenchmarkingFilterNames" runat="server"
    SelectMethod="GetBenchmarkFilterNames" TypeName="RMC.BussinessService.BSReports">
</asp:ObjectDataSource>
<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>
