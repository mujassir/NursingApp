<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportHospitalBenchmark.ascx.cs"
    Inherits="RMC.Web.UserControls.ReportHospitalBenchmark" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<script type="text/javascript">

    $(document).ready(function() {

        document.getElementById("<%=ReportViewer1.ClientID %>").childNodes[9].childNodes[0].style.overflow = 'hidden';
    });
    
</script>

<table width="100%">
    <tr>
        <td colspan="4px" align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
            <%--<h3 style="font-size: 13px;">--%>
            <u>Hospital Benchmark Summary</u>
        </td>
    </tr>
    <tr>
        <td colspan="4px" align="right" style="padding-right: 5px;">
        <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="6" OnClick="ImageButtonBack_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="4px">
            <div style="background-image: url('../Images/FileListHeader.gif'); color: White;
                height: 28px; font-size: 13px; font-weight: bold; vertical-align: middle; padding-left: 10px;
                padding-bottom: 4px; padding-top: 5px;">
                <asp:Label ID="LabelFilter" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label><br />
                <asp:Label ID="LabelBegigningDate" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
                <asp:Label ID="LabelEndingDate" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
            </div>
            <table>
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
            </table>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" BorderStyle="None" Font-Names="Verdana"
                Font-Size="8pt" ZoomPercent="100" BorderWidth="0.5pt"
                SizeToReportContent="True" AsyncRendering="False" DocumentMapWidth="100%" Width="100%"
                ShowZoomControl="False" Height="100%">
                <LocalReport ReportPath="RDLC\ReportHospitalBenchmark.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="BEReports" />
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="BEFunctionNames" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </td>
    </tr>
</table>
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OnSelecting="ObjectDataSource1_Selecting"
    SelectMethod="CalculateFunctionValuesTest" TypeName="RMC.BussinessService.BSReports">
    <SelectParameters>
        <asp:Parameter DefaultValue="" Name="valueAddedCategoryID" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="OthersCategoryID" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="LocationCategoryID" Type="String"></asp:Parameter>
        <asp:Parameter Name="hospitalUnitID" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="firstYear" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="lastYear" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="firstMonth" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="lastMonth" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="bedInUnitFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optBedInUnitFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="bedInUnitTo" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optBedInUnitTo" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="budgetedPatientFrom" Type="Single"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optBudgetedPatientFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="budgetedPatientTo" Type="Single"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optBudgetedPatientTo" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="startDate" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="endDate" Type="String"></asp:Parameter>
        <asp:Parameter Name="electronicDocumentFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optElectronicDocumentFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="electronicDocumentTo" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optElectronicDocumentTo" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="docByException" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="unitType" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="pharmacyType" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="hospitalType" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optHospitalSizeFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="hospitalSizeFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optHospitalSizeTo" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="hospitalSizeTo" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="countryId" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="stateId" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="value" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="others" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="location" Type="String"></asp:Parameter>
        <asp:Parameter Name="dataPointsFrom" Type="Int32" />
        <asp:Parameter Name="optDataPointsFrom" Type="Int32" />
        <asp:Parameter Name="dataPointsTo" Type="Int32" />
        <asp:Parameter Name="optdataPointsTo" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnSelecting="ObjectDataSource1_Selecting"
    SelectMethod="GetDataForHospitalBenchmarkSummaryTest" TypeName="RMC.BussinessService.BSReports">
    <SelectParameters>
        <asp:Parameter DefaultValue="" Name="valueAddedCategoryID" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="OthersCategoryID" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="LocationCategoryID" Type="String"></asp:Parameter>
        <asp:Parameter Name="hospitalUnitID" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="firstYear" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="lastYear" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="firstMonth" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="lastMonth" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="bedInUnitFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optBedInUnitFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="bedInUnitTo" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optBedInUnitTo" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="budgetedPatientFrom" Type="Single"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optBudgetedPatientFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="budgetedPatientTo" Type="Single"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optBudgetedPatientTo" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="startDate" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="endDate" Type="String"></asp:Parameter>
        <asp:Parameter Name="electronicDocumentFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optElectronicDocumentFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="electronicDocumentTo" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optElectronicDocumentTo" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="docByException" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="unitType" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="pharmacyType" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="hospitalType" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optHospitalSizeFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="hospitalSizeFrom" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="optHospitalSizeTo" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="hospitalSizeTo" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="countryId" Type="Int32"></asp:Parameter>
        <asp:Parameter Name="stateId" Type="Int32"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="value" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="others" Type="String"></asp:Parameter>
        <asp:Parameter DefaultValue="" Name="location" Type="String"></asp:Parameter>
        <asp:Parameter Name="dataPointsFrom" Type="Int32" />
        <asp:Parameter Name="optDataPointsFrom" Type="Int32" />
        <asp:Parameter Name="dataPointsTo" Type="Int32" />
        <asp:Parameter Name="optdataPointsTo" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
