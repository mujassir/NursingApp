<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUs.ascx.cs" Inherits="RMC.Web.UserControls.ContactUs" %>
<asp:ScriptManager ID="ScriptManagerUserRegistration" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    jQuery(function($) {

//        $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
//        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });
</script>

<%--<asp:Panel ID="PanelUserRegistration" runat="server" DefaultButton="ButtonSend">--%>
    <center style="width: 100%;">
        <%--<div id="divOutter" style="background-color: #06569d; z-index: 0;">
            <div id="divInner" style="background-color: White; z-index: 10;">--%>
                <%--<div style="padding-top: 10px; padding-bottom: 10px;">--%>
                    <table width="100%">
                        <tr>
                            <%--<th colspan="2" align="center">
                                <h3 style="font-size: 13px;">
                                    Contact Us
                                </h3>
                            </th>--%>
                            <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">
                            <u>Contact Us</u>
                         </th>
                        </tr>
                        <tr>
                            <td style="height:10px;"></td>
                        </tr>
                        <%--<tr>
                        <td align="left" style="color: Red; padding-left: 70px; font-weight: bold;" colspan="2">
                            * indicates a mandatory fields.
                        </td>
                    </tr>--%>
                        <tr>
                            <td colspan="2" align="left">
                                <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                                    <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
                                        z-index: 10;">
                                        <div style="padding-left: 90px;">
                                            <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="UserRegistration"
                                                runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" Font-Size="11px"
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
                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px; padding-top: 12px;" valign="top" width="190px">
                                Message <span style="color: Red;">*</span>
                            </td>
                            <td align="left" valign="top" style="padding-top: 12px;">
                                <asp:TextBox ID="TextBoxMessage" runat="server" Height="91px" MaxLength="4000" TabIndex="4"
                                    TextMode="MultiLine" Width="215px" ForeColor="#06569D"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalInfo" runat="server"
                                    ControlToValidate="TextBoxMessage" Display="None" ErrorMessage="Required Message."
                                    ValidationGroup="UserRegistration" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 5px;" width="190px">
                            </td>
                            <td align="left" style="padding-top: 5px;">
                                <asp:Button ID="ButtonSend" runat="server" Text="Send" ValidationGroup="UserRegistration"
                                    CssClass="aspButton" Width="70px" TabIndex="28" Style="height: 26px" OnClick="ButtonSend_Click" />
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="190px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                <%--</div>--%>
            <%--</div>--%>
        <%--</div>--%>
    </center>
    <asp:LinqDataSource ID="LinqDataSourcePermission" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
        Select="new (PermissionID, Permission1, IsActive)" TableName="Permissions" Where="IsActive == true">
    </asp:LinqDataSource>
<%--</asp:Panel>--%>
