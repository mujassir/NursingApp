<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportTimeRNGrid.ascx.cs"
    Inherits="RMC.Web.UserControls.ReportTimeRNGrid" %>
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

<%--<script type="text/javascript">

    $(document).ready(function() {

        document.getElementById("<%=ReportViewer1.ClientID %>").childNodes[9].childNodes[0].style.overflow = 'hidden';
    });
    
</script>--%>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td colspan="4" align="left" style="font-size: 14px; padding-left: 10px; padding-top: 5px;
            color: #06569d; font-weight: bold;">
            <%--<h3 style="font-size: 13px;">--%>
            <u>Monthly Summary Dashboard Report</u>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="left" style="padding-left: 5px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="6" OnClick="ImageButtonBack_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="4" style="margin-left: 40px" align="center">
            <div style="background-image: url('../Images/FileListHeader.gif'); color: White;
                height: 28px; font-size: 12px; font-weight: bold; vertical-align: middle; padding-left: 10px;
                padding-bottom: 4px; padding-top: 5px; text-align: left;">
                &nbsp;<asp:Label ID="LabelHospitalName" runat="server" Text="" ForeColor="White"
                    Font-Bold="true" Font-Size="12px"></asp:Label><br />
                &nbsp;<asp:Label ID="LabelHospitalUnitName" runat="server" Text="" ForeColor="White"
                    Font-Bold="true" Font-Size="12px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelFilter" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
            </div>
            <div style="height: 5px;">
            </div>
            <div style="text-align: left; padding-left: 2px;">
                <asp:LinkButton ID="LinkButtonExportReport" Font-Size="11px" runat="server" TabIndex="2"
                    OnClick="LinkButtonExportReport_Click" Font-Bold="true">ExportToExcel</asp:LinkButton>
            </div>
            <div style="height: 1px;">
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridViewReport" runat="server" DataSourceID="ObjectDataSource1"
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
            <asp:GridView ID="GridViewFunctionReport" runat="server" DataSourceID="ObjectDataSource2"
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
        <td colspan="4" align="left" style="padding-left: 5px; padding-top: 10px;">
            <asp:LinkButton ID="LinkButtonSaveImage" Font-Bold="true" Font-Size="11px" runat="server"
                OnClick="LinkButtonSaveImage_Click">SaveImage</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="margin-left: 40px; padding-bottom: 5px;" align="center">
            <asp:Chart ID="ChartTimeRN" runat="server" BackColor="226, 205, 214" BorderColor="#1A3B69"
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
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnSelecting="ObjectDataSource1_Selecting"
    SelectMethod="GetDataForTimeRNSummaryGrid" TypeName="RMC.BussinessService.BSReports">
    <SelectParameters>
        <asp:Parameter Name="activitiesID" Type="String" />
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
    SelectMethod="CalculateFunctionValuesGrid" TypeName="RMC.BussinessService.BSReports">
    <SelectParameters>
    
        <asp:Parameter Name="activitiesID" Type="String" />
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
