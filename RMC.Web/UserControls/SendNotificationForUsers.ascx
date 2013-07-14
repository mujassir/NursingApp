<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendNotificationForUsers.ascx.cs"
    Inherits="RMC.Web.UserControls.SendNotificationForUsers" %>

<script type="text/javascript" language="javascript">
    //    jQuery(function($) {

    //        $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    //        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    //    });
</script>

<asp:Panel ID="PanelHospitalInfo" runat="server" DefaultButton="ButtonSend">
    <div style="width: 100%;">
        <table width="100%" style="text-align: center;">
            <tr>
                <th style="font-size: 14px; color: #06569d; padding-left: 10px; padding-top: 10px;"
                    align="left">
                    <%--<h3 style="font-size: 13px;">
                        <table width="100%" style="text-align: left;">
                            <tr>
                                <td align="left" width="10%">--%>
                    <%--</td>
                                <td align="center">--%>
                    <%--<span>--%><u>Notification</u><%--</span>--%>
                    <%--</td>
                            </tr>
                        </table>
                    </h3>--%>
                </th>
                <th align="right" style="padding-left: 10px; padding-top: 10px;">
                    <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                        PostBackUrl="~/Administrator/AdminHomePage.aspx" />
                </th>
            </tr>
            <tr>
                <td style="height:10px;"></td>
            </tr>
            <%--<tr>
            <td align="left" style="color: Red; padding-left: 150px; font-weight: bold;" colspan="2">
                * indicates a mandatory fields
            </td>
        </tr>--%>
            <tr>
                <td align="center" colspan="2">
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
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Subject &nbsp&nbsp
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxSubject" MaxLength="200" runat="server" Width="400px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalInfo" runat="server"
                                                ControlToValidate="TextBoxSubject" Display="None" ErrorMessage="Required Subject."
                                                ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorHospitalName" runat="server"
                                                ControlToValidate="TextBoxHospitalName" Display="None" ErrorMessage="Allow only alphanumeric characters in Hospital Name."
                                                ValidationExpression="^[a-zA-Z0-9''-'\s]{1,200}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                            Message &nbsp;&nbsp;
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="TextBoxMessage" runat="server" Height="182px" MaxLength="100" TextMode="MultiLine"
                                                Width="400px" ForeColor="#06569D"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-top: 5px;">
                                        </td>
                                        <td align="left" style="padding-top: 5px;">
                                            <table width="190px">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="ButtonSend" runat="server" Text="Send" CssClass="aspButton" Width="55px"
                                                            OnClick="ButtonSend_Click" />
                                                    </td>
                                                    <td align="right">
                                                        &nbsp;
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
