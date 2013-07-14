<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAssessmentReport.ascx.cs" Inherits="RMC.Web.UserControls.UserAssessmentReport" %>
<link href="../CSS/ControlStyles.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .style1
    {
        height: 17px;
    }
</style>
<table width="100%">
    <tr>
        <td align="left" style="font-size: 14px; padding-left: 10px; padding-top: 5px; color: #06569d;
            font-weight: bold;">
            <%--<h3 style="font-size: 13px;">--%>
            <u>Unit Assessment Summary</u>
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-left: 5px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="6" OnClick="ImageButtonBack_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <div id="divHeader" style="background-image: url('../Images/FileListHeader.gif');
                color: White; height: 28px; font-size: 13px; font-weight: bold; vertical-align: middle;
                padding-left: 10px; padding-bottom: 4px; padding-top: 5px;">
                <asp:Label ID="LabelFilter" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label><br />
                <asp:Label ID="LabelMonthname" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
                <asp:Label ID="LabelYearName" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="LabelHospitalname" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="LabelHospitalUnitname" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
            </div>
            <div style="height: 5px;">
            </div>
            <div style="text-align: left; padding-left: 2px;">
                <%--<asp:LinkButton ID="LinkButtonExportReport" Font-Size="11px" runat="server" TabIndex="2" Visible="false"
                    OnClick="LinkButtonExportReport_Click" Font-Bold="true">ExportToExcel</asp:LinkButton>--%>
                    <asp:LinkButton ID="LinkButton3" Font-Size="11px" runat="server" TabIndex="2" 
                    Font-Bold="true" onclick="LinkButton3_Click">Save Image</asp:LinkButton>
            </div>
            <div style="height: 1px;">
            </div>
        </td>
    </tr>
    <tr style="display:none;"><%--<tr>--%>
        <td>
            <asp:GridView ID="GridViewFunctionReport" runat="server" DataSourceID="ObjectDataSource2"
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
            <asp:GridView ID="GridViewReport" runat="server" DataSourceID="ObjectDataSource1"
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
            <asp:GridView ID="GridViewtest"  runat="server" DataSourceID="ObjectDataSource3"
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
    <tr style="text-align:center">
        <td>
            
            <asp:CHART id="Chart1" runat="server" BackColor="239, 230, 247" Width="700px"
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
                                    <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8" Title="">
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
            <asp:LinkButton ID="LinkButton2" Font-Size="11px"  Font-Bold="true" runat="server" onclick="LinkButton2_Click">ExportToExcel</asp:LinkButton>
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
<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
    SelectMethod="CalculateData" TypeName="RMC.BussinessService.BSReports" 
    onselected="ObjectDataSource3_Selected"></asp:ObjectDataSource>


<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnSelecting="ObjectDataSource1_Selecting"
    SelectMethod="GetDataForHospitalBenchmarkSummaryGridUnitID" TypeName="RMC.BussinessService.BSReports">
    <SelectParameters>
        <asp:Parameter Name="dbValues" Type="String" />
        <asp:Parameter Name="ProfileCategoryValue" Type="String" />
        <asp:Parameter Name="ProfileSubCategoryValue" Type="String" />
        <asp:Parameter Name="valueAddedCategoryID" Type="String" />
        <asp:Parameter Name="OthersCategoryID" Type="String" />
        <asp:Parameter Name="LocationCategoryID" Type="String" />
        <asp:Parameter Name="hospitalUnitID" Type="Int32" />
        <asp:Parameter Name="firstYear" Type="Int32" />
        <asp:Parameter Name="lastYear" Type="Int32" />
        <asp:Parameter Name="firstMonth" Type="Int32" />
        <asp:Parameter Name="lastMonth" Type="Int32" />
        <asp:Parameter Name="bedInUnitFrom" Type="Int32" />
        <asp:Parameter Name="optBedInUnitFrom" Type="Int32" />
        <asp:Parameter Name="bedInUnitTo" Type="Int32" />
        <asp:Parameter Name="optBedInUnitTo" Type="Int32" />
        <asp:Parameter Name="budgetedPatientFrom" Type="Single" />
        <asp:Parameter Name="optBudgetedPatientFrom" Type="Int32" />
        <asp:Parameter Name="budgetedPatientTo" Type="Single" />
        <asp:Parameter Name="optBudgetedPatientTo" Type="Int32" />
        <asp:Parameter Name="startDate" Type="String" />
        <asp:Parameter Name="endDate" Type="String" />
        <asp:Parameter Name="electronicDocumentFrom" Type="Int32" />
        <asp:Parameter Name="optElectronicDocumentFrom" Type="Int32" />
        <asp:Parameter Name="electronicDocumentTo" Type="Int32" />
        <asp:Parameter Name="optElectronicDocumentTo" Type="Int32" />
        <asp:Parameter Name="docByException" Type="Int32" />
        <asp:Parameter Name="unitType" Type="String" />
        <asp:Parameter Name="pharmacyType" Type="String" />
        <asp:Parameter Name="hospitalType" Type="String" />
        <asp:Parameter Name="optHospitalSizeFrom" Type="Int32" />
        <asp:Parameter Name="hospitalSizeFrom" Type="Int32" />
        <asp:Parameter Name="optHospitalSizeTo" Type="Int32" />
        <asp:Parameter Name="hospitalSizeTo" Type="Int32" />
        <asp:Parameter Name="countryId" Type="Int32" />
        <asp:Parameter Name="stateId" Type="Int32" />
        <asp:Parameter Name="activities" Type="String" />
        <asp:Parameter Name="value" Type="String" />
        <asp:Parameter Name="others" Type="String" />
        <asp:Parameter Name="location" Type="String" />
        <asp:Parameter Name="dataPointsFrom" Type="Int32" />
        <asp:Parameter Name="optDataPointsFrom" Type="Int32" />
        <asp:Parameter Name="dataPointsTo" Type="Int32" />
        <asp:Parameter Name="optdataPointsTo" Type="Int32" />
        <asp:Parameter Name="configName" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OnSelecting="ObjectDataSource2_Selecting"
    SelectMethod="CalculateFunctionValuesGridForUnitAssessment" TypeName="RMC.BussinessService.BSReports">
    <SelectParameters>
        <asp:Parameter Name="dbValues" Type="String" />
        <asp:Parameter Name="ProfileCategoryValue" Type="String" />
        <asp:Parameter Name="ProfileSubCategoryValue" Type="String" />
        <asp:Parameter Name="valueAddedCategoryID" Type="String" />
        <asp:Parameter Name="OthersCategoryID" Type="String" />
        <asp:Parameter Name="LocationCategoryID" Type="String" />
        <asp:Parameter Name="hospitalUnitID" Type="Int32" />
        <asp:Parameter Name="firstYear" Type="Int32" />
        <asp:Parameter Name="lastYear" Type="Int32" />
        <asp:Parameter Name="firstMonth" Type="Int32" />
        <asp:Parameter Name="lastMonth" Type="Int32" />
        <asp:Parameter Name="bedInUnitFrom" Type="Int32" />
        <asp:Parameter Name="optBedInUnitFrom" Type="Int32" />
        <asp:Parameter Name="bedInUnitTo" Type="Int32" />
        <asp:Parameter Name="optBedInUnitTo" Type="Int32" />
        <asp:Parameter Name="budgetedPatientFrom" Type="Single" />
        <asp:Parameter Name="optBudgetedPatientFrom" Type="Int32" />
        <asp:Parameter Name="budgetedPatientTo" Type="Single" />
        <asp:Parameter Name="optBudgetedPatientTo" Type="Int32" />
        <asp:Parameter Name="startDate" Type="String" />
        <asp:Parameter Name="endDate" Type="String" />
        <asp:Parameter Name="electronicDocumentFrom" Type="Int32" />
        <asp:Parameter Name="optElectronicDocumentFrom" Type="Int32" />
        <asp:Parameter Name="electronicDocumentTo" Type="Int32" />
        <asp:Parameter Name="optElectronicDocumentTo" Type="Int32" />
        <asp:Parameter Name="docByException" Type="Int32" />
        <asp:Parameter Name="unitType" Type="String" />
        <asp:Parameter Name="pharmacyType" Type="String" />
        <asp:Parameter Name="hospitalType" Type="String" />
        <asp:Parameter Name="optHospitalSizeFrom" Type="Int32" />
        <asp:Parameter Name="hospitalSizeFrom" Type="Int32" />
        <asp:Parameter Name="optHospitalSizeTo" Type="Int32" />
        <asp:Parameter Name="hospitalSizeTo" Type="Int32" />
        <asp:Parameter Name="countryId" Type="Int32" />
        <asp:Parameter Name="stateId" Type="Int32" />
        <asp:Parameter Name="activities" Type="String" />
        <asp:Parameter Name="value" Type="String" />
        <asp:Parameter Name="others" Type="String" />
        <asp:Parameter Name="location" Type="String" />
        <asp:Parameter Name="dataPointsFrom" Type="Int32" />
        <asp:Parameter Name="optDataPointsFrom" Type="Int32" />
        <asp:Parameter Name="dataPointsTo" Type="Int32" />
        <asp:Parameter Name="optdataPointsTo" Type="Int32" />
        <asp:Parameter Name="configName" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
