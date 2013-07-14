<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageYears.ascx.cs"
    Inherits="RMC.Web.UserControls.ManageYears" %>
<asp:Panel ID="PanelYear" runat="server" DefaultButton="ButtonSave" Width="100%">
    <table width="99%" style="text-align: center;">
        <tr>
            <th align="left" style="font-size: 14px; color: #06569d; padding-left: 10px; padding-top: 10px;">
                <u>Manage Years</u>
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
                        <td style="height: 2px;">
                            <div style="width: 390px; float: left; background-color: Transparent; z-index: 0;">
                                <div id="divErrorMsgInner" style="width: 388px; float: left; background-color: #E8E9EA;
                                    z-index: 10;">
                                    <div style="padding-left: 50px; text-align: left;">
                                        <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="HospitalInfo"
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
                                        Year <span style="color: Red;">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBoxYears" MaxLength="5" runat="server" Width="181px" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalInfo" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxYears" Display="None" ErrorMessage="Required Years"
                                            ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidatorYear" runat="server" ErrorMessage="Year must be greater than or equal to 2002."
                                            ControlToValidate="TextBoxYears" Display="None" MaximumValue="2050" MinimumValue="2002"
                                            Type="Integer" ValidationGroup="HospitalInfo">*</asp:RangeValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorHospitalName" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxYears" Display="None" ErrorMessage="Allow only numeric characters in Years."
                                            ValidationExpression="^[0-9'\s]{4,5}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                        Years &nbsp&nbsp
                                    </td>
                                    <td align="left">
                                        <asp:ListBox ID="ListBoxYears" runat="server" DataTextField="Year1" DataValueField="YearID"
                                            ForeColor="#06569D" Height="210px" Width="181px" TabIndex="2" DataSourceID="ObjectDataSourceYears">
                                        </asp:ListBox>
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
                                                        CssClass="aspButton" Width="55px" TabIndex="3" 
                                                        onclick="ButtonSave_Click" />
                                                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" CssClass="aspButton" Width="55px"  ValidationGroup="HospitalInfo"
                                                        Visible="false" TabIndex="4" />
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
    <asp:ObjectDataSource ID="ObjectDataSourceYears" runat="server" SelectMethod="GetYearByHospitalUnitID"
        TypeName="RMC.BussinessService.BSDataManagement">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="hospitalUnitID" QueryStringField="HospitalDemographicId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Panel>
