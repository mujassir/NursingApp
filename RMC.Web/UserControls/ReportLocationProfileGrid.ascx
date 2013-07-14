<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportLocationProfileGrid.ascx.cs"
    Inherits="RMC.Web.UserControls.ReportLocationProfileGrid" %>
<table width="100%">
    <tr>
        <td colspan="4" align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px;
            color: #06569d; font-weight: bold;">
            <%--<h3 style="font-size: 13px;">--%>
            <u>From/To Trips Report</u>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="left" style="padding-left: 5px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="6" OnClick="ImageButtonBack_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="4" style="margin-left: 40px">
            <div style="background-image: url('../Images/FileListHeader.gif'); color: White;
                height: 28px; font-size: 12px; font-weight: bold; vertical-align: middle; padding-left: 10px;
                padding-bottom: 4px; padding-top: 4px;">
                <%--<asp:Label ID="LabelHospitalName" runat="server" Text="" ForeColor="White"
                    Font-Bold="true" Font-Size="12px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;<asp:Label ID="LabelHospitalUnitName" runat="server" Text="" ForeColor="White"
                    Font-Bold="true" Font-Size="12px"></asp:Label><br />--%>
                <asp:Label ID="LabelFilter" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label><br />
                <asp:Label ID="LabelBegigningDate" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
                <asp:Label ID="LabelEndingDate" runat="server" Text="" ForeColor="White" Font-Bold="true"
                    Font-Size="12px"></asp:Label>
            </div>
            <div style="height: 5px;">
            </div>
            <div style="text-align: left; padding-left: 5px;">
                <asp:LinkButton ID="LinkButtonExportReport" Font-Size="11px" runat="server" TabIndex="2"
                    OnClick="LinkButtonExportReport_Click" Font-Bold="true">ExportToExcel</asp:LinkButton>
            </div>
            <div style="height: 1px;">
            </div>
            <asp:GridView  ID="GridViewReport" runat="server" DataSourceID="ObjectDataSource1"
                CellPadding="3" EmptyDataText="No Record to display." CssClass="GridViewStyle"
                HorizontalAlign="Center" OnRowDataBound="GridViewReport_RowDataBound">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnSelecting="ObjectDataSource1_Selecting"
                SelectMethod="GetDataForLocationProfileReportGrid" TypeName="RMC.BussinessService.BSReports">
                <SelectParameters>
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
                    <asp:Parameter Name="configName" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
</table>
