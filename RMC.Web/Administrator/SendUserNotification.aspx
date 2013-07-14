<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendUserNotification.aspx.cs"
    Inherits="RMC.Web.Administrator.SendUserNotification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RMC :: Notification</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="PanelUserRegistration" runat="server" DefaultButton="ButtonSend">
            <center style="width: 100%;">
                <div id="divOutter" style="background-color: #06569d; z-index: 0;">
                    <div id="divInner" style="background-color: White; z-index: 10;">
                        <div style="padding-top: 10px; padding-bottom: 10px;">
                            <table width="100%">
                                <tr>
                                    <th colspan="2" align="center">
                                        <h3 style="font-size: 13px;">
                                            Send Notification
                                        </h3>
                                    </th>
                                </tr>
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
                                    <td align="right" style="color: #06569d; font-weight: bold;" valign="top" width="190px">
                                        Subject <span style="color: Red;">*</span>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="TextBoxSubject" runat="server" TabIndex="3" CssClass="aspTextBox"
                                            ForeColor="#06569D"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxSubject"
                                            Display="None" ErrorMessage="Required Message." ValidationGroup="UserRegistration"
                                            SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold;" valign="top" width="190px">
                                        Message <span style="color: Red;">*</span>
                                    </td>
                                    <td align="left" valign="top">
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
                                        <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="aspButton" Width="70px"
                                            TabIndex="28" Style="height: 26px" OnClientClick="return window.close();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="190px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </center>
            <asp:LinqDataSource ID="LinqDataSourcePermission" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                Select="new (PermissionID, Permission1, IsActive)" TableName="Permissions" Where="IsActive == true">
            </asp:LinqDataSource>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
