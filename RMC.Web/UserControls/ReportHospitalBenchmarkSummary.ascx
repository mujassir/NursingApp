<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportHospitalBenchmarkSummary.ascx.cs" Inherits="RMC.Web.UserControls.ReportHospitalBenchmarkSummary" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
    Font-Size="8pt" Height="502px" Width="750px">
    <LocalReport ReportPath="RDLC\ReportHospitalBenchmarkSummary.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                Name="DataSetForReports_DataTableMatrixReport" />
        </DataSources>
    </LocalReport>
    
</rsweb:ReportViewer>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    SelectMethod="GetData" TypeName="RMC.Web.DataSetForReportsTableAdapters.">
</asp:ObjectDataSource>

