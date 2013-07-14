<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowSpecialTypeData.ascx.cs"
    Inherits="RMC.Web.UserControls.ShowSpecialTypeData" %>
<table width="99%">
    <tr>
        <th align="left" style="font-size: 16px; padding-left: 10px; padding-top: 10px; color: #06569d;
            font-weight: bold;">
            <u>Special Type Data</u>
        </th>
        <th align="right" style="padding-top: 10px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="1" OnClick="ImageButtonBack_Click" />
        </th>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <asp:FormView ID="FormViewNurseInfo" runat="server" DataSourceID="LinqDataSourceNurseInfo"
                Width="760px">
                <ItemTemplate>
                    <table cellspacing="10px" width="99%">
                        <tr>
                            <td align="right" style="font-weight: bold; font-size: 11px; color: #06569d;">
                                Nurse Name <span style="padding-left: 5px;"></span>
                            </td>
                            <td align="left">
                                <asp:Label ID="NurseNameLabel" Font-Size="11px" ForeColor="#06569d" runat="server"
                                    Text='<%# Eval("NurseName") %>' />
                            </td>
                            <td align="right" style="font-weight: bold; font-size: 11px; color: #06569d;">
                                PDA UserName <span style="padding-left: 5px;"></span>
                            </td>
                            <td align="left">
                                <asp:Label ID="PDAUserNameLabel" Font-Size="11px" ForeColor="#06569d" runat="server"
                                    Text='<%# Eval("PDAUserName") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="font-weight: bold; font-size: 11px; color: #06569d;">
                                Config Name <span style="padding-left: 5px;"></span>
                            </td>
                            <td align="left">
                                <asp:Label ID="ConfigNameLabel" ForeColor="#06569d" Font-Size="11px" runat="server"
                                    Text='<%# Eval("ConfigName") %>' />
                            </td>
                            <td align="right" style="font-weight: bold; font-size: 11px; color: #06569d;">
                                Patients Per Nurse <span style="padding-left: 5px;"></span>
                            </td>
                            <td align="left">
                                <asp:Label ID="PatientsPerNurseLabel" ForeColor="#06569d" Font-Size="11px" runat="server"
                                    Text='<%# Eval("PatientsPerNurse") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="font-weight: bold; font-size: 11px; color: #06569d;">
                                File Reference <span style="padding-left: 5px;"></span>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="FileRefferenceLabel" ForeColor="#06569d" Font-Size="11px" runat="server"
                                    Text='<%# Eval("FileRefference") %>' />
                            </td>
                        </tr>
                    </table>
                    <%--FileRefference:
                    <asp:Label ID="FileRefferenceLabel" runat="server" Text='<%# Bind("FileRefference") %>' />
                    <br />--%>
                </ItemTemplate>
            </asp:FormView>
            <asp:LinqDataSource ID="LinqDataSourceNurseInfo" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                Select="new (NurseName, HospitalDemographicID, ConfigName, PatientsPerNurse, PDAUserName, FileRefference)"
                TableName="NursePDAInfos" Where="NurseID == @NurseID">
                <WhereParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="NurseID" QueryStringField="NurseID"
                        Type="Int32" />
                </WhereParameters>
            </asp:LinqDataSource>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <asp:MultiView ID="MultiViewFileRecords" runat="server">
                <asp:View ID="ViewSuperAdminOwner" runat="server">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:LinkButton ID="LinkButtonDeleteSelected" runat="server" Font-Bold="true" Font-Size="11px"
                                    OnClick="LinkButtonDeleteSelected_Click" OnClientClick="return confirm('Are you sure you wish to delete these record(s)?');">Delete Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="GridViewErrorValidation" runat="server" Width="760px" AutoGenerateColumns="False"
                                    CssClass="GridViewStyle" CellPadding="5" EmptyDataText="No Record to display."
                                    GridLines="None" HorizontalAlign="Left" 
                                    DataSourceID="ObjectDataSourceShowSpecialData" DataKeyNames="SpecialTypeID">
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                                    <Columns>
                                     <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDelete" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Date" HeaderText="Date" />
                                        <asp:BoundField DataField="Time" HeaderText="Time" />
                                        <asp:BoundField DataField="SpecialCategory" HeaderText="Special Category" />
                                        <asp:BoundField DataField="SpecialActivity" HeaderText="Special Activity" />
                                    </Columns>
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSourceShowSpecialData" runat="server" SelectMethod="GetSpecialTypeData"
                                    TypeName="RMC.BussinessService.BSNursePDADetail"
                                    DataObjectTypeName="RMC.BusinessEntities.BEValidationSpecialType" 
                                    DeleteMethod="DeleteNursePDASpecialType">
                                    <SelectParameters>
                                        <asp:QueryStringParameter DefaultValue="0" Name="NurseID" QueryStringField="NurseID"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="ViewUsers" runat="server">
                    <asp:GridView ID="GridViewValidDataForUsers" runat="server" Width="760px" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" CellPadding="5" EmptyDataText="No Record to display."
                         GridLines="None" HorizontalAlign="Left" 
                        DataSourceID="ObjectDataSourceValidDataForUsers" DataKeyNames="SpecialTypeID">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                            <asp:BoundField DataField="Time" HeaderText="Time" />
                            <asp:BoundField DataField="SpecialCategory" HeaderText="Special Category" />
                            <asp:BoundField DataField="SpecialActivity" HeaderText="Special Activity" />
                        </Columns>
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSourceValidDataForUsers" runat="server" 
                        SelectMethod="GetSpecialTypeData" 
                        TypeName="RMC.BussinessService.BSNursePDADetail" 
                        DataObjectTypeName="RMC.BusinessEntities.BEValidationSpecialType" 
                        DeleteMethod="DeleteNursePDASpecialType">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="0" Name="NurseID" 
                                QueryStringField="NurseID" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
            </asp:MultiView>
        </td>
    </tr>
</table>
