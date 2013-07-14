<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportTimeStudyRN.ascx.cs"
    Inherits="RMC.Web.UserControls.ReportTimeStudyRN" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="99%">
    <tr>
        <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
            <%--<h3 style="font-size: 13px;">--%>
            <u>Time RN Summary</u>
        </th>
        <th>
        </th>
        <th></th>
        <th align="right" style="padding-left: 50px; padding-top: 10px; text-align: right;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                PostBackUrl="~/Administrator/TimeRN.aspx" TabIndex="6" />
        </th>
    </tr>
    <tr>
        <td style="height: 10px;">
            <asp:ValidationSummary ID="ValidationSummaryReportTimeRN" ValidationGroup="Chart"
                runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Font-Size="11px"
                Font-Bold="true" Style="padding-top: 1px;" />
        </td>
    </tr>
    <tr>
        <td align="left" style="width: 30%; padding-left: 4px;">
            <asp:Label ID="Label1" runat="server" Text="Chart" ForeColor="#06569d" Font-Bold="true"
                Font-Size="11px"></asp:Label>
        </td>
        <td align="left" style="width: 30%;">
            <asp:Label ID="Label2" runat="server" Text="Profile Category" ForeColor="#06569d"
                Font-Bold="true" Font-Size="11px"></asp:Label>
        </td>
        <td align="left" style="width: 30%;">
            <asp:Label ID="Label3" runat="server" Text="Profile Sub Category" ForeColor="#06569d"
                Font-Bold="true" Font-Size="11px"></asp:Label>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:UpdatePanel ID="UpdatePanelProfile" runat="server" ChildrenAsTriggers="true"
                UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td align="left" style="width: 30%;">
                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListChart" ForeColor="#06569D"
                                    runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="DropDownListChart_SelectedIndexChanged"
                                    TabIndex="1">
                                    <asp:ListItem Value="0">Select Chart</asp:ListItem>
                                    <asp:ListItem Value="Line">Line</asp:ListItem>
                                    <asp:ListItem Value="Pie">Pie</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorChart" runat="server" ControlToValidate="DropDownListChart"
                                    SetFocusOnError="true" Display="None" ErrorMessage="Must Select Chart" InitialValue="0"
                                    ValidationGroup="Chart">*</asp:RequiredFieldValidator>
                            </td>
                            <td align="left" style="width: 30%; padding-left: 14px;">
                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListProfileCategory" ForeColor="#06569D"
                                    runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="DropDownListProfileCategory_SelectedIndexChanged"
                                    TabIndex="2">
                                    <asp:ListItem Value="0">Select Profile</asp:ListItem>
                                    <asp:ListItem Value="Value Added">Value Added</asp:ListItem>
                                    <asp:ListItem Value="Others">Others</asp:ListItem>
                                    <asp:ListItem Value="Location">Location</asp:ListItem>
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSourceProfileSubCategory" runat="server" SelectMethod="GetSubCategoryProfile"
                                    TypeName="RMC.BussinessService.BSReports">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DropDownListProfileCategory" DefaultValue="0" Name="profileCategory"
                                            PropertyName="SelectedValue" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <%if (DropDownListChart.SelectedValue != "0")
                                  { %>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorProfileCategory" runat="server"
                                    ControlToValidate="DropDownListProfileCategory" SetFocusOnError="true" Display="None"
                                    ErrorMessage="Must Select Profile Category" InitialValue="0" ValidationGroup="Chart">*</asp:RequiredFieldValidator>
                                <%} %>
                            </td>
                            <td align="center" style="width: 30%; padding-left: 23px;">
                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListProfileSubCategory"
                                    ForeColor="#06569D" runat="server" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceProfileSubCategory"
                                    DataTextField="ProfileCategoryName" DataValueField="ProfileCategoryName" TabIndex="3">
                                </asp:DropDownList>
                                <%if (DropDownListChart.SelectedValue == "Line" && DropDownListProfileCategory.SelectedValue != "0")
                                  { %>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorProfileSubCategory" runat="server"
                                    ControlToValidate="DropDownListProfileSubCategory" SetFocusOnError="true" Display="None"
                                    ErrorMessage="Must Select Profile Sub Category" InitialValue="0" ValidationGroup="Chart">*</asp:RequiredFieldValidator>
                                <%} %>
                            </td>
                            <td align="right">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-left: 4px;">
            <%--<asp:Button ID="ButtonGenerateChart" runat="server" Text="Show Chart" ValidationGroup="Chart"
                CssClass="aspButton" Width="90px" Visible="true" TabIndex="4" />--%>
                 <asp:LinkButton ID="LinkButtonGenerateChart" runat="server" Text="Show Chart" ValidationGroup="Chart"
                CssClass="aspLinkButton" Width="90px" Visible="true" OnClick="LinkButtonGenerateChart_Click"
                TabIndex="4" />
        </td>
        <td align="left" style="padding-left: 4px;">
            <%--<asp:Button ID="ButtonShowReport" runat="server" Text="Show Report" ValidationGroup="Demographic"
                CssClass="aspButton" Width="90px" Visible="true" TabIndex="5" />--%>
                <asp:LinkButton ID="LinkButtonShowReport" runat="server" Text="Show Report" ValidationGroup="Demographic"
                CssClass="aspLinkButton" Width="90px" Visible="true" OnClick="LinkButtonShowReport_Click"
                TabIndex="5" />
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="height: 10px;">
        </td>
    </tr>
</table>
<asp:MultiView ID="MultiViewReport" runat="server">
    <asp:View ID="ViewReport" runat="server">
        <rsweb:ReportViewer ID="ReportViewerReport" runat="server" BorderStyle="Groove" Font-Names="Verdana"
            Font-Size="8pt" Height="440px" ShowPageNavigationControls="False" ShowRefreshButton="False"
            Width="574px" ZoomPercent="90" BorderWidth="0.5pt">
            <LocalReport ReportPath="RDLC\ReportTimeRN.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceReport1" Name="BEReports" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceReport2" 
                        Name="BENationalDatabase" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceReport3" 
                        Name="BENationalDatabase1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSourceReport3" runat="server" 
            onselecting="ObjectDataSourceReport1_Selecting" SelectMethod="GetPerformance" 
            TypeName="RMC.BussinessService.BSReports">
            <SelectParameters>
                <asp:Parameter Name="valueAddedCategoryID" Type="Int32" />
                <asp:Parameter Name="OthersCategoryID" Type="Int32" />
                <asp:Parameter Name="LocationCategoryID" Type="Int32" />
                <asp:Parameter Name="hospitalUnitID" Type="Int32" />
                <asp:Parameter Name="firstYear" Type="Int32" />
                <asp:Parameter Name="lastYear" Type="Int32" />
                <asp:Parameter Name="firstMonth" Type="Int32" />
                <asp:Parameter Name="lastMonth" Type="Int32" />
                <asp:Parameter Name="bedInUnit" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="optBedInUnit" Type="Int32" />
                <asp:Parameter Name="budgetedPatient" Type="Single" />
                <asp:Parameter DefaultValue="" Name="optBudgetedPatient" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="startDate" Type="String" />
                <asp:Parameter DefaultValue="" Name="endDate" Type="String" />
                <asp:Parameter Name="electronicDocument" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="optElectronicDocument" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="docByException" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="unitType" Type="String" />
                <asp:Parameter DefaultValue="" Name="pharmacyType" Type="String" />
                <asp:Parameter DefaultValue="" Name="optHospitalSize" Type="Int32" />
                <asp:Parameter Name="hospitalSize" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceReport2" runat="server" 
            SelectMethod="GetNationalDatabase" TypeName="RMC.BussinessService.BSReports">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceReport1" runat="server" 
            onselecting="ObjectDataSourceReport1_Selecting" 
            SelectMethod="GetDataForTimeRNSummary" 
            TypeName="RMC.BussinessService.BSReports">
            <SelectParameters>
                <asp:Parameter Name="valueAddedCategoryID" Type="Int32" />
                <asp:Parameter Name="OthersCategoryID" Type="Int32" />
                <asp:Parameter Name="LocationCategoryID" Type="Int32" />
                <asp:Parameter Name="hospitalUnitID" Type="Int32" />
                <asp:Parameter Name="firstYear" Type="Int32" />
                <asp:Parameter Name="lastYear" Type="Int32" />
                <asp:Parameter Name="firstMonth" Type="Int32" />
                <asp:Parameter Name="lastMonth" Type="Int32" />
                <asp:Parameter Name="bedInUnit" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="optBedInUnit" Type="Int32" />
                <asp:Parameter Name="budgetedPatient" Type="Single" />
                <asp:Parameter DefaultValue="" Name="optBudgetedPatient" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="startDate" Type="String" />
                <asp:Parameter DefaultValue="" Name="endDate" Type="String" />
                <asp:Parameter Name="electronicDocument" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="optElectronicDocument" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="docByException" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="unitType" Type="String" />
                <asp:Parameter DefaultValue="" Name="pharmacyType" Type="String" />
                <asp:Parameter DefaultValue="" Name="optHospitalSize" Type="Int32" />
                <asp:Parameter Name="hospitalSize" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceReport4" runat="server" 
            onselecting="ObjectDataSourceReport1_Selecting" SelectMethod="GetPerformance" 
            TypeName="RMC.BussinessService.BSReports">
            <SelectParameters>
                <asp:Parameter Name="valueAddedCategoryID" Type="Int32" />
                <asp:Parameter Name="OthersCategoryID" Type="Int32" />
                <asp:Parameter Name="LocationCategoryID" Type="Int32" />
                <asp:Parameter Name="hospitalUnitID" Type="Int32" />
                <asp:Parameter Name="firstYear" Type="Int32" />
                <asp:Parameter Name="lastYear" Type="Int32" />
                <asp:Parameter Name="firstMonth" Type="Int32" />
                <asp:Parameter Name="lastMonth" Type="Int32" />
                <asp:Parameter Name="bedInUnit" Type="Int32" />
                <asp:Parameter Name="optBedInUnit" Type="Int32" />
                <asp:Parameter Name="budgetedPatient" Type="Single" />
                <asp:Parameter Name="optBudgetedPatient" Type="Int32" />
                <asp:Parameter Name="startDate" Type="String" />
                <asp:Parameter Name="endDate" Type="String" />
                <asp:Parameter Name="electronicDocument" Type="Int32" />
                <asp:Parameter Name="optElectronicDocument" Type="Int32" />
                <asp:Parameter Name="docByException" Type="Int32" />
                <asp:Parameter Name="unitType" Type="String" />
                <asp:Parameter Name="pharmacyType" Type="String" />
                <asp:Parameter Name="optHospitalSize" Type="Int32" />
                <asp:Parameter Name="hospitalSize" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:View>
    <asp:View ID="ViewChart" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Chart ID="Chart1" runat="server" BackColor="226, 205, 214" BorderColor="#1A3B69"
                        BackSecondaryColor="White" BackGradientStyle="TopBottom" Width="575px">
                        <Legends>
                            <asp:Legend IsTextAutoFit="true" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
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
                <tr>
                    <td style="padding-left: 20px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelMarker" runat="server">
                                    <fieldset>
                                    <legend style="font-size: 14px; padding-left:10px; padding-top:10px; color:#06569d;">Marker</legend>
                                        <table>
                                            <%--<tr>
                                <td>
                                    <fieldset style="width: 50px;">
                                        <legend>Marker Style</legend>
                                        <div style="padding-top: 5px;">
                                            <asp:DropDownList ID="DropDownList1" runat="server" ForeColor="#06569D" CssClass="aspDropDownList"
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Select Style</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </fieldset>
                                </td>
                                <td>
                                    <fieldset style="width: 50px;">
                                        <legend>Marker Size</legend>
                                    </fieldset>
                                </td>
                            </tr>--%>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelMarkerStyle" runat="server" Text="Marker Style" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListMarkerStyle" runat="server" ForeColor="#06569D"
                                                        CssClass="aspDropDownList" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceMarkerStyle"
                                                        DataTextField="Key" DataValueField="Value" AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownListMarkerStyle_SelectedIndexChanged">
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
                                                        CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownListMarkerSize_SelectedIndexChanged">
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
                                                        DataTextField="Key" DataValueField="Value" AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownListMarkerColor_SelectedIndexChanged">
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
                                                        CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownListPointLabel_SelectedIndexChanged">
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
                                    <legend style="font-size: 14px; padding-left:10px; padding-top:10px; color:#06569d;">Chart</legend>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelChartType" runat="server" Text="Chart Type" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListChartType" runat="server" ForeColor="#06569D" CssClass="aspDropDownList"
                                                        AppendDataBoundItems="True" AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownListChartType_SelectedIndexChanged">
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
                                                        AppendDataBoundItems="True" AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownListLineWidth_SelectedIndexChanged">
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
                                                        AppendDataBoundItems="True" DataSourceID="ObjectDataSourceColor" 
                                                        DataTextField="Key" DataValueField="Value" AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownListColor_SelectedIndexChanged">
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
                                                        CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownListShadowOffset_SelectedIndexChanged">
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
            </tr>
        </table>
    </asp:View>
</asp:MultiView>
<asp:ObjectDataSource ID="ObjectDataSourceColor" runat="server" SelectMethod="GetColor"
    TypeName="RMC.BussinessService.BSChartControl"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceMarkerStyle" runat="server" SelectMethod="GetMarkerStyle"
    TypeName="RMC.BussinessService.BSChartControl"></asp:ObjectDataSource>

