<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnitType.ascx.cs" Inherits="RMC.Web.UserControls.UnitType" %>
<%--<script type="text/javascript" language="javascript">
    jQuery(function($) {

        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });
</script>--%>
<asp:Panel ID="PanelUnitType" runat="server" DefaultButton="ButtonSave" Width="100%">
    <table width="99%" style="text-align: center;">
        <tr>
            <th align="left" style="font-size: 14px; color: #06569d; padding-left: 10px; padding-top: 10px;">
                <%--<h3 style="font-size: 13px;">
                    <table width="100%">
                        <tr>
                            <td align="left">--%>
                <%--</td>
                            <td align="center">--%>
                <u>Unit Type</u>
                <asp:Literal ID="LiteralHospitalName" runat="server"></asp:Literal>
                <asp:Literal ID="LiteralUnitName" runat="server"></asp:Literal>
                <%--</td>
                        </tr>
                    </table>
                </h3>--%>
            </th>
            <th align="right" style="padding-left: 10px; padding-top: 10px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    TabIndex="6" onclick="ImageButtonBack_Click" />
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
                                        Unit Type <span style="color: Red;">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBoxUnitType" MaxLength="200" runat="server" Width="181px" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalInfo" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxUnitType" Display="None" ErrorMessage="Required Unit Type."
                                            ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorHospitalName" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxUnitType" Display="None" ErrorMessage="Allow only alphanumeric characters in Unit Type."
                                            ValidationExpression="^[a-zA-Z0-9''-'\s]{1,200}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                        Unit Types &nbsp&nbsp
                                    </td>
                                    <td align="left">
                                        <asp:ListBox ID="ListBoxUnitTypes" runat="server" DataSourceID="ObjectDataSourceUnitType"
                                            DataTextField="UnitTypeName" DataValueField="UnitTypeID" ForeColor="#06569D"
                                            Height="210px" Width="181px" TabIndex="2"></asp:ListBox>
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
                                                    <asp:Button ID="ButtonReset" runat="server" Text="Reset" CssClass="aspButton" Width="55px"
                                                        OnClick="ButtonReset_Click" Visible="false" TabIndex="4" />
                                                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="aspButton" Width="55px"
                                                        OnClick="ButtonDelete_Click" Visible="false" OnClientClick="return confirm('Are you sure you wish to delete this entry?');"
                                                        TabIndex="5" />
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
    <asp:ObjectDataSource ID="ObjectDataSourceUnitType" runat="server" SelectMethod="GetAllUnitType"
        TypeName="RMC.BussinessService.BSUnitType"></asp:ObjectDataSource>
</asp:Panel>
