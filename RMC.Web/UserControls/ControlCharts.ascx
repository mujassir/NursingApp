<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlCharts.ascx.cs"
    Inherits="RMC.Web.UserControls.ControlCharts" %>
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
    function checkListbox() {
        
        if (document.getElementById('<%=DropDownListHospitalName.ClientID %>').value == "0") {
            alert("Please Select Hospital From List");
            return false;
        }
        if (document.getElementById('<%=DropDownListHospitalUnit.ClientID %>').value == "0") {
            alert("Please Select Hospital Unit From List");
            return false;
        }
        var datefrom = document.getElementById('<%=DropDownListYearMonthFrom.ClientID %>').value;
        alert(datefrom);
        var dateto = document.getElementById('<%=DropDownListYearMonthTo.ClientID %>').value;
        if ((datefrom == dateto) && (dateto != 0)) {
            alert("This report needs at least 2 months of data to run.  Check to make sure there is more than 1 month of data.");
            return false;
        }

        var arDatefrom = new Array();
        var arDateto = new Array();
        arDatefrom = datefrom.split(',');
        arDateto = dateto.split(',');
        if (datefrom != "0" && dateto == "0") {
            alert("Please enter Period To");
            return false;
        }
        if (dateto != "0" && datefrom == "0") {
            alert("Please enter Period From");
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
        if (document.getElementById('<%=DropDownListProfileCategory.ClientID %>').value == "0") {
            alert("Please Select Profile Category From List");
            return false;
        }
        if (document.getElementById('<%=DropDownListProfileSubCategory.ClientID %>').value == "0") {
            alert("Please Select Profile Sub Category From List");
            return false;
        }
    }
</script>

<script language="javascript" type="text/javascript">
    function saveImageAs(imgOrURL) {
        if (typeof imgOrURL == 'object')
            imgOrURL = imgOrURL.src;
        window.win = open(imgOrURL);
        setTimeout('win.document.execCommand("SaveAs")', 500);
    }

    function msg() {

        var imgOrURL = document.getElementById('embedImage');
        saveImageAs(imgOrURL);
    }

    function copyToClipboard() {

        var div = document.getElementById('divChart');
        div.contentEditable = 'true';
        var controlRange;
        if (document.body.createControlRange) {
            controlRange = document.body.createControlRange();
            controlRange.addElement(div);
            controlRange.execCommand('Copy');
        }
        div.contentEditable = 'false';
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

    function refresh() {
        window.location.reload();
    }

</script>

<div>
    <asp:Panel ID="PanelHospitalUnitInformation" runat="server">
        <table width="100%">
            <tr>
                <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                    <u>Run Charts</u>
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
                            <td align="center">
                                <%--<asp:UpdatePanel ID="UpdatePanelHospital" runat="server" ChildrenAsTriggers="true"
                                    UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                        <table style="padding-left: 2px;">
                                            <tr>
                                                <td align="right">
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
                                                <td style="width: 67px;">
                                                </td>
                                                <td align="right">
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
                                <table style="padding-left: 74px;">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="LabelBenchmarkingFilters" runat="server" Text="BenchMarking Filter"
                                                ForeColor="#06569d" Font-Bold="true" Font-Size="11px"></asp:Label>
                                            &nbsp&nbsp
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListBenchmarkingFilter"
                                                ForeColor="#06569D" runat="server" AppendDataBoundItems="True" TabIndex="3" DataSourceID="ObjectDataSourceBenchmarkingFilterNames"
                                                DataTextField="FilterName" DataValueField="FilterId">
                                                <asp:ListItem Value="0">Select Filter</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">No Filter</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFilter" runat="server" ControlToValidate="DropDownListBenchmarkingFilter"
                                                SetFocusOnError="true" Display="None" ErrorMessage="Must Select Filter" InitialValue="0"
                                                ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
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
                                <asp:UpdatePanel ID="UpdatePanelProfile" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table style="padding-right: 22px;">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="LabelValueAddedProfile" runat="server" Text="Profile(s) Category"
                                                        ForeColor="#06569d" Font-Bold="true" Font-Size="11px"></asp:Label>
                                                    &nbsp&nbsp
                                                </td>
                                                <td>
                                                    <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListProfileCategory" ForeColor="#06569D"
                                                        runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="DropDownListProfileCategory_SelectedIndexChanged"
                                                        TabIndex="4">
                                                        <asp:ListItem Value="0">Select Profile</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorProfileCategory" runat="server"
                                                        ControlToValidate="DropDownListProfileCategory" SetFocusOnError="true" Display="None"
                                                        ErrorMessage="Must Select Profile Category" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 25px;">
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="LabelLocationProfile" runat="server" Text="Profile Sub Category" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                    &nbsp&nbsp
                                                </td>
                                                <td>
                                                    <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListProfileSubCategory"
                                                        ForeColor="#06569D" runat="server" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceProfileSubCategory"
                                                        DataTextField="ProfileCategoryName" DataValueField="ProfileCategoryName" TabIndex="5"
                                                        OnSelectedIndexChanged="DropDownListProfileSubCategory_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select Sub Profile</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%if (DropDownListProfileCategory.SelectedValue != "0")
                                                      { %>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorProfileSubCategory" runat="server"
                                                        ControlToValidate="DropDownListProfileSubCategory" SetFocusOnError="true" Display="None"
                                                        ErrorMessage="Must Select Profile Sub Category" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                    <%} %>
                                                    <asp:ObjectDataSource ID="ObjectDataSourceProfileSubCategory" runat="server" SelectMethod="GetSubCategoryProfileByProfileId"
                                                        TypeName="RMC.BussinessService.BSReports">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="DropDownListProfileCategory" DefaultValue="0" Name="profileId"
                                                                PropertyName="SelectedValue" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="LabelValues" runat="server" Text="Values" ForeColor="#06569d" Font-Bold="true"
                                                        Font-Size="11px" Visible="false"></asp:Label>
                                                    &nbsp&nbsp
                                                </td>
                                                <td>
                                                    <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListValues" ForeColor="#06569D"
                                                        runat="server" AppendDataBoundItems="True" TabIndex="6" DataSourceID="ObjectDataSourceValues"
                                                        DataTextField="value" DataValueField="valueId" Visible="false">
                                                        <asp:ListItem Value="0">Select Value</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%if (DropDownListProfileCategory.SelectedValue != "0" && DropDownListProfileSubCategory.SelectedValue != "0")
                                                      { %>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorValues" runat="server"
                                                        ControlToValidate="DropDownListValues" SetFocusOnError="true" Display="None"
                                                        ErrorMessage="Must Select Values" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                    <%} %>
                                                    <asp:ObjectDataSource ID="ObjectDataSourceValues" runat="server" SelectMethod="GetDatabaseValuesValue"
                                                        TypeName="RMC.BussinessService.BSReports">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="DropDownListProfileSubCategory" DefaultValue="0"
                                                                Name="value" PropertyName="SelectedValue" Type="String" />
                                                            <asp:ControlParameter ControlID="DropDownListProfileCategory" DefaultValue="0" 
                                                                Name="profileCategory" PropertyName="SelectedValue" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                               <%-- <asp:UpdatePanel ID="UpdatePanelYearMonth" ChildrenAsTriggers="true" runat="server"
                                    UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                        <table style="padding-left: 18px;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="LabelYearFrom" runat="server" Text="Period From" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                    &nbsp&nbsp
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListYearMonthFrom" ForeColor="#06569D"
                                                        runat="server" AppendDataBoundItems="True" TabIndex="7">
                                                        <asp:ListItem Value="0">Select Period From</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%if (DropDownListYearMonthFrom.SelectedValue == "0" && DropDownListYearMonthTo.SelectedValue != "0")
                                                      { %>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorYearMonthFrom" runat="server"
                                                        ControlToValidate="DropDownListYearMonthFrom" Display="None" ErrorMessage="Must Select Period From"
                                                        InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                    <%} %>
                                                </td>
                                                <td style="width: 87px;">
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="LabelYearTo" runat="server" Text="Period To" ForeColor="#06569d" Font-Bold="true"
                                                        Font-Size="11px"></asp:Label>
                                                    &nbsp&nbsp
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListYearMonthTo" ForeColor="#06569D"
                                                        runat="server" AppendDataBoundItems="True" TabIndex="8">
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
                            <td style="height: 25px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="ButtonGenerateReport" runat="server" Text="Generate Report" ValidationGroup="Demographic"
                                    CssClass="aspButton" Width="130px" Visible="true" OnClick="ButtonGenerateReport_Click" OnClientClick="return checkListbox();"
                                    TabIndex="9" />&nbsp
                                <asp:Button ID="ButtonReset" runat="server" Text="Reset" Visible="True" CssClass="aspButton"
                                    Width="70px" OnClick="ButtonReset_Click" TabIndex="10" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
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
                    <asp:ObjectDataSource ID="ObjectDataSourceYear" runat="server" SelectMethod="Years"
                        TypeName="RMC.BussinessService.BSYear">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="2002" Name="startingYear" Type="Int32" />
                            <asp:Parameter DefaultValue="20" Name="noOfYears" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:MultiView ID="MultiViewCharts" runat="server">
                        <asp:View ID="ViewPieCharts" runat="server">
                        </asp:View>
                        <asp:View ID="ViewControlCharts" runat="server">
                            <table>
                                <tr>
                                    <td align="left" style="padding-left: 12px;">
                                        <asp:LinkButton ID="LinkButtonSaveImage" Font-Bold="true" Font-Size="11px" runat="server"
                                            OnClick="LinkButtonSaveImage_Click">SaveImage</asp:LinkButton>
                                    </td>
                                    <td align="right" style="padding-right: 20px;">
                                        <%--<input name="Salva Imagine" type="button" onclick="copyToClipboard();" value="CopyImage" />--%>
                                       <%-- <a href="#" onclick="copyToClipboard(); return false" style="font-size: 11px; font-weight: bold;">
                                            CopyImage</a>
                                        <asp:ImageButton ID="ImageButtonRefresh" ToolTip="Click This First if Image not Pasted"
                                            runat="server" ImageUrl="~/Images/refresh.png" TabIndex="6" OnClientClick="refresh();" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="divChart">
                                         

                                            <asp:Chart ID="ChartControlCharts" runat="server" BackColor="226, 205, 214" BorderColor="#1A3B69"
                                                BackSecondaryColor="White" BackGradientStyle="TopBottom" Width="765px" Height="450px">
                                                <Titles>
                                                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 11pt, style=Bold" ShadowOffset="3"
                                                        Text="Line Chart" Name="Title1" ForeColor="26, 59, 105">
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
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px;">
                                        <table>
                                            <tr>
                                                <td style="width: 105px;">
                                                </td>
                                                <td>
                                                    <asp:Panel ID="PanelMarker" runat="server">
                                                        <fieldset>
                                                            <legend style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                                                                Marker</legend>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelMarkerStyle" runat="server" Text="Marker Style" ForeColor="#06569d"
                                                                            Font-Bold="true" Font-Size="11px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownListMarkerStyle" runat="server" ForeColor="#06569D"
                                                                            CssClass="aspDropDownList" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceMarkerStyle"
                                                                            DataTextField="Key" DataValueField="Value" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMarkerStyle_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelMarkerSize" runat="server" Text="Marker Size" ForeColor="#06569d"
                                                                            Font-Bold="true" Font-Size="11px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownListMarkerSize" runat="server" ForeColor="#06569D"
                                                                            CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMarkerSize_SelectedIndexChanged">
                                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelMarkerColor" runat="server" Text="Marker Color" ForeColor="#06569d"
                                                                            Font-Bold="true" Font-Size="11px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownListMarkerColor" runat="server" ForeColor="#06569D"
                                                                            CssClass="aspDropDownList" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceColor"
                                                                            DataTextField="Key" DataValueField="Value" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMarkerColor_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelPointLabel" runat="server" Text="Point Label" ForeColor="#06569d"
                                                                            Font-Bold="true" Font-Size="11px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownListPointLabel" runat="server" ForeColor="#06569D"
                                                                            CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPointLabel_SelectedIndexChanged">
                                                                            <asp:ListItem Value="None" Selected="True">None</asp:ListItem>
                                                                            <asp:ListItem Value="Auto">Auto</asp:ListItem>
                                                                            <asp:ListItem Value="TopLeft">TopLeft</asp:ListItem>
                                                                            <asp:ListItem Value="Top">Top</asp:ListItem>
                                                                            <asp:ListItem Value="TopRight">TopRight</asp:ListItem>
                                                                            <asp:ListItem Value="Right">Right</asp:ListItem>
                                                                            <asp:ListItem Value="BottomRight">BottomRight</asp:ListItem>
                                                                            <asp:ListItem Value="Bottom">Bottom</asp:ListItem>
                                                                            <asp:ListItem Value="BottomLeft">BottomLeft</asp:ListItem>
                                                                            <asp:ListItem Value="Left">Left</asp:ListItem>
                                                                            <asp:ListItem Value="Center">Center</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset></asp:Panel>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="PanelChart" runat="server">
                                                        <fieldset>
                                                            <legend style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                                                                Chart</legend>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelChartType" runat="server" Text="Chart Type" ForeColor="#06569d"
                                                                            Font-Bold="true" Font-Size="11px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownListChartType" runat="server" ForeColor="#06569D" CssClass="aspDropDownList"
                                                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListChartType_SelectedIndexChanged">
                                                                            <asp:ListItem Value="Line">Line</asp:ListItem>
                                                                            <asp:ListItem Value="Spline">Spline</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelLineWidth" runat="server" Text="Line Width" ForeColor="#06569d"
                                                                            Font-Bold="true" Font-Size="11px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownListLineWidth" runat="server" ForeColor="#06569D" CssClass="aspDropDownList"
                                                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListLineWidth_SelectedIndexChanged">
                                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelColor" runat="server" Text="Color" ForeColor="#06569d" Font-Bold="true"
                                                                            Font-Size="11px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownListColor" runat="server" ForeColor="#06569D" CssClass="aspDropDownList"
                                                                            AppendDataBoundItems="True" DataSourceID="ObjectDataSourceColor" DataTextField="Key"
                                                                            DataValueField="Value" AutoPostBack="True" OnSelectedIndexChanged="DropDownListColor_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelShadowOffset" runat="server" Text="Shadow Offset" ForeColor="#06569d"
                                                                            Font-Bold="true" Font-Size="11px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownListShadowOffset" runat="server" ForeColor="#06569D"
                                                                            CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListShadowOffset_SelectedIndexChanged">
                                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset></asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ObjectDataSource ID="ObjectDataSourceColor" runat="server" SelectMethod="GetColor"
        TypeName="RMC.BussinessService.BSChartControl"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceMarkerStyle" runat="server" SelectMethod="GetMarkerStyle"
        TypeName="RMC.BussinessService.BSChartControl"></asp:ObjectDataSource>
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
</div>
