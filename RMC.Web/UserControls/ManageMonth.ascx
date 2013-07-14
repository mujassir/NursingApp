<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageMonth.ascx.cs"
    Inherits="RMC.Web.UserControls.ManageMonth" %>
<asp:Panel ID="PanelMonth" runat="server" DefaultButton="ButtonSave" Width="100%">
    <table width="99%" style="text-align: center;">
        <tr>
            <th align="left" style="font-size: 14px; color: #06569d; padding-left: 10px; padding-top: 10px;">
                <u>Manage Months</u>
            </th>
            <th align="right" style="padding-left: 10px; padding-top: 10px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    TabIndex="6" OnClick="ImageButtonBack_Click" />
            </th>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <table width="370px">
                                <tr>
                                    <td align="right" style="padding-top: 5px;">
                                    </td>
                                    <td align="left" style="padding-top: 5px;">
                                        <table width="190px">
                                            <tr>
                                                <td align="left">
                                                    <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="HospitalInfo"
                                                        CssClass="aspButton" Width="55px" TabIndex="3" OnClick="ButtonSave_Click" />
                                                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" CssClass="aspButton" Width="55px"
                                                        ValidationGroup="HospitalInfo" Visible="false" TabIndex="4" />
                                                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="aspButton" Width="55px"
                                                        Visible="false" OnClientClick="return confirm('Are you sure you wish to delete this entry?');"
                                                        TabIndex="5" />
                                                </td>
                                                <td align="right">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        Month &nbsp&nbsp
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownListMonth" ForeColor="#06569D" runat="server" CssClass="aspDropDownList"
                                            TabIndex="2" Width="178px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                        Months &nbsp&nbsp
                                    </td>
                                    <td align="left">
                                        <asp:ListBox ID="ListBoxMonths" runat="server" ForeColor="#06569D" Height="210px"
                                            Width="181px" TabIndex="2" DataSourceID="ObjectDataSourceMonths" DataTextField="MonthName"
                                            DataValueField="MonthID"></asp:ListBox>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td align="right" style="padding-top: 5px;">
                                    </td>
                                    <td align="left" style="padding-top: 5px;">
                                        <table width="190px">
                                            <tr>
                                                <td align="left">
                                                    <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="HospitalInfo"
                                                        CssClass="aspButton" Width="55px" TabIndex="3" OnClick="ButtonSave_Click" />
                                                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" CssClass="aspButton" Width="55px"
                                                        ValidationGroup="HospitalInfo" Visible="false" TabIndex="4" />
                                                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="aspButton" Width="55px"
                                                        Visible="false" OnClientClick="return confirm('Are you sure you wish to delete this entry?');"
                                                        TabIndex="5" />
                                                </td>
                                                <td align="right">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjectDataSourceMonths" runat="server" SelectMethod="GetMonthByHospitalUnitID"
        TypeName="RMC.BussinessService.BSMonth">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="hospitalUnitID" QueryStringField="HospitalDemographicId"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="year" QueryStringField="Year" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Panel>
