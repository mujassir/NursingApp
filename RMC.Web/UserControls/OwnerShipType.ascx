<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OwnerShipType.ascx.cs"
    Inherits="RMC.Web.UserControls.OwnerShipType" %>
<%--<script type="text/javascript" language="javascript">
    jQuery(function($) {

        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });
</script>--%>
<asp:Panel ID="PanelPharmacyType" runat="server" DefaultButton="ButtonSave" Width="100%">
    <table width="100%" style="text-align: center;">
        <tr>
            <th align="left" style="font-size: 14px; color: #06569d; padding-top: 10px; padding-left: 10px;">
                <%--<h3 style="font-size: 13px;">
                    <table width="100%">
                        <tr>
                            <td align="left">--%>
                <%--</td>
                            <td align="center">--%>
                <u>Ownership Type</u>
                <asp:Literal ID="LiteralHospitalName" runat="server"></asp:Literal>
                <asp:Literal ID="LiteralUnitName" runat="server"></asp:Literal>
                <%--</td>
                        </tr>
                    </table>
                </h3>--%>
            </th>
            <th style="padding-top: 10px; padding-left: 10px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    TabIndex="5" OnClick="ImageButtonBack_Click" />
            </th>
        </tr>
        <tr>
            <td style="height: 5px;">
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td style="height: 2px;">
                            <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                                <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
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
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        Ownership Type <span style="color: Red;">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBoxOwnershipType" MaxLength="200" runat="server" Width="181px"
                                            TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalType" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxOwnershipType" Display="None"
                                            ErrorMessage="Required Ownership Type." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorHospitalType" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxOwnershipType" Display="None"
                                            ErrorMessage="Allow only alphanumeric characters in Ownership Type." ValidationExpression="^[a-zA-Z0-9''-'\s]{1,200}$"
                                            ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                        Ownership Types &nbsp&nbsp
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:ListBox ID="ListBoxOwnershipTypes" runat="server" SelectionMode="Multiple" Width="183px"
                                            ForeColor="#06569D" Height="186px" DataSourceID="LinqDataSourceOwnership" DataTextField="OwnerShipTypeName"
                                            DataValueField="OwnerShipTypeID" TabIndex="2"></asp:ListBox>
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
                                                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="aspButton" Width="55px"
                                                        Visible="false" OnClientClick="return confirm(&quot;Are you sure you wish to delete this entry?&quot;);"
                                                        OnClick="ButtonDelete_Click" TabIndex="4" />
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
</asp:Panel>
<asp:LinqDataSource ID="LinqDataSourceOwnership" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
    TableName="OwnerShipTypes" OrderBy="OwnerShipTypeName">
</asp:LinqDataSource>
