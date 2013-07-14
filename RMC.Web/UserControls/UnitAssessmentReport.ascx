<%@ Control Language="C#"  AutoEventWireup="true" CodeBehind="UnitAssessmentReport.ascx.cs" Inherits="RMC.Web.UserControls.UnitAssessmentReport" %>
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
        var dateto = document.getElementById('<%=DropDownListYearMonthTo.ClientID %>').value;
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
        if (document.getElementById('<%=DropDownListProfiles.ClientID %>').value == "0") {
            alert("Please Select Profiles From List");
            return false;
        }


    }
</script>
<asp:ScriptManager ID="ScriptManagerReportTimeRNChartsPie" runat="server">
</asp:ScriptManager>
 <asp:Panel ID="PanelHospitalUnitInformation" runat="server">
<table style="height:20px;">
<tr>
<td></td></tr>
</table>
    <table width="800px">
        <tr>
            <th align="left" 
                style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                <%--<u>Time RN Summary</u>--%>
                <asp:Label ID="LabelHeading" runat="server" Text="" Font-Underline="true"></asp:Label>
            </th>
            <th>
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <td align="center">
                <table width="85%">
                    <tr>
                        <td style="padding-left: 20px;">
                            <div style="width: 20px; float: left; background-color: Transparent; z-index: 0;">
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
                           <asp:Label ID="LabelBenchmarkingFilters" runat="server" Font-Bold="true" 
                                Font-Size="11px" ForeColor="#06569d" Text="BenchMarking Filter"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownListBenchmarkingFilter" runat="server" 
                                AppendDataBoundItems="True" CssClass="aspDropDownList" 
                                DataSourceID="ObjectDataSourceBenchmarkingFilterNames" 
                                DataTextField="FilterName" DataValueField="FilterId" ForeColor="#06569D" 
                                TabIndex="1">
                                <asp:ListItem Value="0">Select Filter</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">No Filter</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFilter" runat="server" 
                                ControlToValidate="DropDownListBenchmarkingFilter" Display="None" 
                                ErrorMessage="Must Select Filter" InitialValue="0" SetFocusOnError="true" 
                                ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 70px;">
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
                            <asp:UpdatePanel ID="UpdatePanelHospital" runat="server" ChildrenAsTriggers="true"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelHospitalName" runat="server" Text="Hospital Name" ForeColor="#06569d"
                                                    Font-Bold="true" Font-Size="11px"></asp:Label>
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
                                            <td style="width: 70px;">
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label5" runat="server" Text="Hospital Unit" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                <span id="spanHospitalUnit" style="color: Red;" runat="server">*</span>
                                            </td>                                            
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListHospitalUnit" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceHospitalUnit"
                                                    DataTextField="HospitalUnitName" DataValueField="HospitalDemographicID" TabIndex="2"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DropDownListHospitalUnit_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Hospital Unit</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalUnit" runat="server"
                                                    ControlToValidate="DropDownListHospitalUnit" SetFocusOnError="true" Display="None"
                                                    ErrorMessage="Must Select Hospital Unit" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 5px;">
                                            </td>
                                        </tr>
                                       <%-- <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelYear" runat="server" Text="Year" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                <span id="span1" style="color: Red;" runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListYear" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" AutoPostBack="True" 
                                                    TabIndex="3" DataSourceID="ObjectDataSourceYear"
                                                    DataTextField="year" DataValueField="year" 
                                                    OnSelectedIndexChanged="DropDownListYear_SelectedIndexChanged" 
                                                    ondatabinding="DropDownListYear_DataBinding">
                                                    <asp:ListItem Value="0">Select Year</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" runat="server" ControlToValidate="DropDownListYear"
                                                    SetFocusOnError="true" Display="None" ErrorMessage="Must Select Year" InitialValue="0"
                                                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                <asp:ObjectDataSource ID="ObjectDataSourceYear" runat="server" SelectMethod="GetYear"
                                                    TypeName="RMC.BussinessService.BSReports">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="DropDownListHospitalUnit" DefaultValue="0" Name="hospitalUnitId"
                                                            PropertyName="SelectedValue" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td style="width: 70px;">
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" Text="Month" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                <span id="span2" style="color: Red;" runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListMonth" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" TabIndex="4" DataSourceID="ObjectDataSourceMonth"
                                                    DataTextField="month" DataValueField="monthIndex">
                                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorMonth" runat="server" ControlToValidate="DropDownListMonth"
                                                    SetFocusOnError="true" Display="None" ErrorMessage="Must Select Month" InitialValue="0"
                                                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                <asp:ObjectDataSource ID="ObjectDataSourceMonth" runat="server" SelectMethod="GetYearMonth"
                                                    TypeName="RMC.BussinessService.BSReports">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="DropDownListHospitalUnit" DefaultValue="0" Name="hospitalUnitId"
                                                            PropertyName="SelectedValue" Type="Int32" />
                                                        <asp:ControlParameter ControlID="DropDownListYear" DefaultValue="0" Name="year" PropertyName="SelectedValue"
                                                            Type="String" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>--%>
                                        <%--begin--%>
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
                                        <%--end--%>
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
                            <asp:UpdatePanel ID="UpdatePanelProfile" runat="server" ChildrenAsTriggers="true"
                                UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="padding-right: 15px;">
                                        <tr>
                                            <td align="right">
                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Profile Category" ForeColor="#06569d"
                                                    Font-Bold="true" Font-Size="11px"></asp:Label><span id="span3" style="color: Red;"
                                                        runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListProfileCategory" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" TabIndex="5" OnSelectedIndexChanged="DropDownListProfileCategory_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="0">Select Category</asp:ListItem>
                                                <%-- <asp:ListItem Value="Value Added">Value Added</asp:ListItem>
                                                    <asp:ListItem Value="Others">Others</asp:ListItem>--%>
                                                     <%--  <asp:ListItem Value="Location">Location</asp:ListItem>--%>
                                                    <asp:ListItem Value="Activities">Activities</asp:ListItem>
                                                    <asp:ListItem Value="Database Values">Database Values</asp:ListItem>
                                                    <%--<asp:ListItem Value="Special Category">Special Category</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorProfileCategory" runat="server"
                                                    ControlToValidate="DropDownListProfileCategory" SetFocusOnError="true" Display="None"
                                                    ErrorMessage="Must Select Profile Category" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 100px;">
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" Text="Profiles" ForeColor="#06569d" Font-Bold="true"
                                                    Font-Size="11px"></asp:Label>
                                                <span id="span4" style="color: Red;" runat="server">*</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListProfiles" ForeColor="#06569D"
                                                    runat="server" AppendDataBoundItems="True" TabIndex="6" DataSourceID="ObjectDataSourceProfiles"
                                                    DataTextField="ProfileName" DataValueField="ProfileTypeID" 
                                                    onselectedindexchanged="DropDownListProfiles_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Profile</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorProfiles" runat="server" ControlToValidate="DropDownListProfiles"
                                                    SetFocusOnError="true" Display="None" ErrorMessage="Must Select Profile" InitialValue="0"
                                                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
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
                                                    <%--<%if (DropDownListProfileCategory.SelectedValue != "0" && DropDownListProfileSubCategory.SelectedValue != "0")
                                                      { %>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorValues" runat="server"
                                                        ControlToValidate="DropDownListValues" SetFocusOnError="true" Display="None"
                                                        ErrorMessage="Must Select Values" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                                    <%} %>--%>
                                                    <asp:ObjectDataSource ID="ObjectDataSourceValues" runat="server" SelectMethod="GetDatabaseValuesValue"
                                                        TypeName="RMC.BussinessService.BSReports">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="DropDownListProfiles" DefaultValue="0"
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
                        <td style="height: 25px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="padding-left: 394px;">
                            <asp:Button ID="ButtonShowChart" runat="server" 
                                Text="Show Unit Assessment Report " ValidationGroup="Demographic"
                                CssClass="aspButton" Width="202px" Visible="true" 
                                OnClick="ButtonShowChart_Click"  OnClientClick="return checkListbox();" TabIndex="7" />
                            <%-- <asp:LinkButton ID="LinkButtonShowChart" runat="server" Text="Show Chart" ValidationGroup="Chart"
                    CssClass="aspLinkButton" Font-Size="11px" Width="90px" TabIndex="3" OnClick="LinkButtonShowChart_Click"></asp:LinkButton>--%>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <div style="width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--//cm--%>
    <table width="100%">
    
    
    <tr>
        <td>
            
            <div style="height: 5px;">
            </div>
            <div style="text-align: left; padding-left: 2px;">
                <%--<asp:LinkButton ID="LinkButtonExportReport" Font-Size="11px" runat="server" TabIndex="2" Visible="false"
                    OnClick="LinkButtonExportReport_Click" Font-Bold="true">ExportToExcel</asp:LinkButton>--%>
                    <asp:LinkButton ID="LinkButton3" Visible="false" Font-Size="11px" runat="server" TabIndex="2" 
                    Font-Bold="true" onclick="LinkButton3_Click">Save Image</asp:LinkButton>
            </div>
            <div style="height: 1px;">
            </div>
        </td>
    </tr>
    <tr style="display:none;"><%--<tr>--%>
        <td>
            <asp:GridView ID="GridViewFunctionReport" runat="server" 
                CellPadding="1" EmptyDataText="No Record to display." CssClass="GridViewStyle"
                HorizontalAlign="Center" 
                
                EnableModelValidation="True">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" BackColor="#80ffff" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>
        </td>
    </tr>
    <tr style="display:none;">
    <%-- <tr>--%>
        <td style=" ">
            <asp:GridView ID="GridViewReport" runat="server" 
                CellPadding="1" EmptyDataText="No Record to display." CssClass="GridViewStyle"
                HorizontalAlign="Center"  
                EnableModelValidation="True">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                <Columns>
                <asp:TemplateField>
                <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Send Message" >
                <img src="../Images/message-04.gif" alt="Send Email" width="11px" height="11px" />
                </asp:LinkButton>
                </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr style="display:none;">
     <%--<tr>--%>
        <td>
            <asp:GridView ID="GridViewtest"  runat="server" 
                CellPadding="1" EmptyDataText="No Record to display." CssClass="GridViewStyle"
                HorizontalAlign="Center" 
                EnableModelValidation="True">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                <Columns>
                <asp:TemplateField>
                <ItemTemplate>
                &nbsp;&nbsp;&nbsp;&nbsp;
                </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <%--<asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />--%>
        </td>
    </tr>
    <tr style="text-align:left">
        <td>
            
            <asp:CHART id="Chart1" runat="server" Visible="false" BackColor="239, 230, 247" Width="700px"
        Height="400px" BorderDashStyle="Solid" BackGradientStyle="TopBottom" HorizontalAlign="Center"
        BorderWidth="2px" BorderColor="#B54001">
                            <titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Nursing Time By Activity Per 12 Hour shift" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
                            </titles>
                            <%--<legends>
                                <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="false" Name="Default"></asp:Legend>
                            </legends>--%>
                            <legends>
								<asp:legend LegendStyle="Row" IsTextAutoFit="False" DockedToChartArea="ChartArea1" Docking="Bottom" IsDockedInsideChartArea="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
							</legends>
                            <borderskin SkinStyle="Emboss"></borderskin>
                            <series>
                               <%--<asp:Series YValueType="Double" XValueType="Double" Name="FullName" IsValueShownAsLabel="true" IsVisibleInLegend="false" /> --%>                   
                                <%--<asp:Series XValueType="String" IsValueShownAsLabel="true" LegendText="Top Quartile in Minutes"  ChartType="StackedColumn" Color="LightGreen"
                                    Name="Top Quartile in Minutes" BorderColor="180, 26, 59, 105" CustomProperties="DrawingStyle=Cylinder" ShadowColor="DarkGreen">
                                </asp:Series>
                                <asp:Series XValueType="String" IsValueShownAsLabel="true" Legend="Median in Minutes" ChartType="StackedColumn" Color="LightCoral"
                                    Name="Median in Minutes" BorderColor="180, 26, 59, 105" CustomProperties="DrawingStyle=Cylinder" ShadowColor="Transparent">
                                </asp:Series>--%>
                                <asp:Series Name="Top Quartile" ChartType="StackedColumn" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240"></asp:Series>
								<asp:Series Name="Median" ChartType="StackedColumn" BorderColor="180, 26, 59, 105" Color="220, 252, 180, 65"></asp:Series>
                                <asp:Series Name="Actual" ChartType="StackedColumn" BorderColor="180, 26, 59, 105" Color="225, 0, 0"></asp:Series>
                                <%--<asp:Series Name="Actual" ChartType="StackedColumn" BorderColor="180, 26, 59, 105" Color="220, 225, 220, 140"></asp:Series>--%>
                                <%--<asp:Series Name="Actual" ChartType="StackedColumn" BorderColor="180, 26, 59, 105" Color="220, 25, 120, 40"></asp:Series>--%>
                            </series>
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1"  BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="#EFE6F7" ShadowColor="DarkRed" BackGradientStyle="TopBottom">
                                    <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                                    <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8" Title="Minutes">
                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"  />
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisy>
                                    <%--<axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8" Title="">
                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IsEndLabelVisible="False"  />
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisx>--%>
                                    <%--<AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" ArrowStyle="Triangle">
                                               <LabelStyle Font="Arial, 8.25pt, style=Bold" />
                                               <MajorGrid LineColor="64, 64, 64, 64" />
                                           </AxisY>--%>
                                           <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="false">
                                               <LabelStyle Font="Arial, 8.25pt" IsStaggered="false" Interval="1" Angle="-30" />
                                               <MajorGrid LineColor="64, 64, 64, 64" />
                                    </AxisX>
                                </asp:ChartArea>
                            </chartareas>
                            </asp:CHART>
                            
               
        </td>
    </tr>
    <tr style="width:200px;">
    <td></td>
    </tr>
    <tr>
        <td class="style1">
            <asp:LinkButton ID="LinkButton2" Visible="false" Font-Size="11px"  Font-Bold="true" runat="server" onclick="LinkButton2_Click">ExportToExcel</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server"
                CellPadding="1" EmptyDataText="No Record to display." CssClass="GridViewStyle"
                HorizontalAlign="Center"  
                EnableModelValidation="True">
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
</table>
    <%--//cm end--%>
</asp:Panel>
<asp:ObjectDataSource ID="ObjectDataSourceProfiles" runat="server" SelectMethod="GetProfiles"
    TypeName="RMC.BussinessService.BSReports">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownListProfileCategory" DefaultValue="0" Name="profileCategory"
            PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceHospitalUnit" runat="server" SelectMethod="GetHospitalDemographicDetailByHospitalID"
    TypeName="RMC.BussinessService.BSHospitalDemographicDetail">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownListHospitalName" DefaultValue="0" Name="hospitalID"
            PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceProfileSubCategory" runat="server" SelectMethod="GetSubCategoryProfile"
    TypeName="RMC.BussinessService.BSReports">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownListProfileCategory" DefaultValue="0" Name="profileCategory"
            PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
                                                        <asp:ObjectDataSource ID="ObjectDataSourceBenchmarkingFilterNames" 
    runat="server" SelectMethod="GetBenchmarkFilterNames" 
    TypeName="RMC.BussinessService.BSReports" 
    onselected="ObjectDataSourceBenchmarkingFilterNames_Selected"></asp:ObjectDataSource>

    <div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>

                                                        