<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileType.ascx.cs"
    Inherits="RMC.Web.UserControls.ProfileType" %>
<asp:Panel ID="PanelProfileType" runat="server" DefaultButton="ButtonSave" Width="100%">
    <table width="99%" style="text-align: center;">
        <tr>
            <th align="left" style="font-size: 14px; color: #06569d; padding-left: 10px; padding-top: 10px;">
                <%--<h3 style="font-size: 13px;">
                    <table width="100%">
                        <tr>
                            <td align="left">--%>
                <%--</td>
                            <td align="center">--%>
                <u>Profile Type</u>
                <asp:Literal ID="LiteralHospitalName" runat="server"></asp:Literal>
                <asp:Literal ID="LiteralProfileName" runat="server"></asp:Literal>
                <%--</td>
                        </tr>
                    </table>
                </h3>--%>
            </th>
            <th align="right" style="padding-left: 10px; padding-top: 10px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    PostBackUrl="~/Users/DemographicDetail.aspx" TabIndex="4" />
            </th>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
        <%--<tr>
            <td align="left" style="color: Red; padding-left: 10px; font-weight: bold;" colspan="2">
                * indicates a mandatory fields
            </td>
        </tr>--%>
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
                                                <%--<li>
                                                    <asp:Label ID="LabelErrorMsg" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </li>--%>
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
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        Profile Type <span style="color: Red;">*</span>
                                    </td>
                                    <td align="left">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxProfileType" MaxLength="200" runat="server" Width="181px"
                                                        TabIndex="1" OnTextChanged="TextBoxProfileType_TextChanged" 
                                                        AutoPostBack="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalInfo" runat="server"
                                                        SetFocusOnError="true" ControlToValidate="TextBoxProfileType" Display="None"
                                                        ErrorMessage="Required Profile Type." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorHospitalName" runat="server"
                                                        SetFocusOnError="true" ControlToValidate="TextBoxProfileType" Display="None"
                                                        ErrorMessage="Allow only alphanumeric characters in Profile Type." ValidationExpression="^[a-zA-Z0-9-'\s]{1,200}$"
                                                        ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                                </td>
                                                <td style="padding-left:2px;">
                                                    <asp:Button ID="ButtonIncremental" runat="server" Text="+" Width="24px" 
                                                        Height="24px" onclick="ButtonIncremental_Click" CssClass="aspSquareButtonDisable" />
                                                    &nbsp;
                                                    <asp:Button ID="ButtonDecremental" runat="server" Text="-" Width="24px" 
                                                        Height="24px" onclick="ButtonDecremental_Click" CssClass="aspSquareButtonDisable" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                        Profile Types &nbsp&nbsp
                                    </td>
                                    <td align="left">
                                        <asp:ListBox ID="ListBoxProfileTypes" runat="server" DataSourceID="ObjectDataSourceProfileType"
                                            DataTextField="ProfileCategory" DataValueField="ProfileCategoryID" ForeColor="#06569D"
                                            Height="210px" Width="181px" OnSelectedIndexChanged="ListBoxProfileTypes_SelectedIndexChanged"
                                            AutoPostBack="True" TabIndex="2"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-top: 5px;">
                                    </td>
                                    <td align="left" style="padding-top: 5px;">
                                        <table width="190px">
                                            <tr>
                                                <td align="left">
                                                    <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="HospitalInfo"
                                                        CssClass="aspButton" Width="55px" OnClick="ButtonSave_Click" TabIndex="3" />
                                                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" CssClass="aspButton" Width="55px" ValidationGroup="HospitalInfo"
                                                        OnClick="ButtonUpdate_Click" Visible="false" TabIndex="3" />
                                                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CssClass="aspButton" Width="55px"
                                                        OnClick="ButtonCancel_Click" Visible="false" TabIndex="3" />
                                                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="aspButton" Width="55px"
                                                        OnClick="ButtonDelete_Click" Visible="false" OnClientClick="return confirm('Are you sure you wish to delete this entry?');"
                                                        TabIndex="3" />
                                                </td>
                                                <td align="right">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjectDataSourceProfileType" runat="server" SelectMethod="GetValueAddedType"
        TypeName="RMC.BussinessService.BSProfileType">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="valuetype" QueryStringField="valuetype"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Panel>
