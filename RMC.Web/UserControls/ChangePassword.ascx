<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs"
    Inherits="RMC.Web.UserControls.ChangePassword" %>

<script type="text/javascript" language="javascript">
//    jQuery(function($) {

//        $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
//        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
//    });
        
        
</script>

<asp:Panel ID="PanelChangePassword" runat="server" DefaultButton="ButtonSave">
    <div style="width: 100%;">
        <table width="100%" style="text-align: center; padding-left: 5px; color: #06569d;">
            <tr>
                <%--<th align="center">
                    <h3 style="font-size: 13px;">
                      --%>  <%--<asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif" />--%>
                       <%-- <span style="font-weight: bold; color: #06569d;">Change Password</span>
                    </h3>
                </th>--%>
                <th align="left" style="font-size: 14px; padding-left:10px; padding-top:10px;">
                            <u>Change Password</u>
                         </th>
            </tr>
            <tr>
                <td style="height:10px;"></td>
            </tr>
            <%-- <tr>
            <td align="left" style="color: Red; padding-left: 150px; font-weight: bold;" colspan="2">
                * indicates a mandatory fields
            </td>
        </tr>--%>
            <tr>
                <td align="left">
                    <table>
                        <tr>
                            <td style="height: 1px;">
                                <div style="width: 302px; float: left; background-color: Transparent; z-index: 0;
                                    margin-left: 100px">
                                    <div id="divErrorMsgInner" style="width: 300px; float: left; background-color: #E8E9EA;
                                        z-index: 10;">
                                        <div style="margin-left: 17px; text-align: left;">
                                            <asp:ValidationSummary ID="ValidationSummarChangePassword" ValidationGroup="ChangePassword"
                                                runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Font-Size="11px"
                                                Font-Bold="true" Style="padding-top: 5px;" Height="40px" />
                                            <asp:Panel ID="PanelErrorMsg" runat="server" Style="padding-top: 1px;" Visible="false">
                                                <ul>
                                                    <li style="color: Red">
                                                        <asp:Label ID="LabelErrorMsg" runat="server" Font-Bold="true" Font-Size="11px" ForeColor="Red"></asp:Label>
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
                                <table style="width: 417px">
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                                            Old Password <span style="color: Red;">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxOldPassword" MaxLength="50" runat="server" Width="181px" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorOldPassword" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxOldPassword" Display="None"
                                                ErrorMessage="Required Old Password." ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                                            New Password <span style="color: Red;">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxNewPassword" MaxLength="50" runat="server" Width="181px" TabIndex="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPassword" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxNewPassword" Display="None"
                                                ErrorMessage="Required New Password." ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxNewPassword" Display="None"
                                                ErrorMessage="Must use at least one digit and 8 to 50 Characters in Password."
                                                ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,50})$" ValidationGroup="ChangePassword">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                                            Confirm Password <span style="color: Red;">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxConfirmPassword" MaxLength="50" runat="server" Width="181px" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmPassword" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxConfirmPassword" Display="None"
                                                ErrorMessage="Required Confirm Password." ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidatorConfirmPwd" runat="server" ControlToCompare="TextBoxNewPassword"
                                                SetFocusOnError="true" ControlToValidate="TextBoxConfirmPassword" Display="None"
                                                ErrorMessage="Confirm Password Not match." ValidationGroup="ChangePassword">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-top: 5px;">
                                        </td>
                                        <td align="left" style="padding-top: 5px;">
                                            <table width="190px">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="ButtonSave" runat="server" Text="Update" ValidationGroup="ChangePassword"
                                                            CssClass="aspButton" Width="60px" OnClick="ButtonSave_Click" TabIndex="4" />
                                                        &nbsp
                                                        <asp:Button ID="ButtonReset" runat="server" Visible="false" CssClass="aspButton"
                                                            Text="Reset" Width="60px" OnClick="ButtonReset_Click" TabIndex="4" />
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
    </div>
</asp:Panel>
