<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.ascx.cs"
    Inherits="RMC.Web.UserProfile" %>
<style type="text/css">
    .style1
    {
        height: 67px;
    }
</style>

<script type="text/javascript" language="javascript">
    jQuery(function($) {
        $("#" + '<%=TextBoxPhone.ClientID%>').mask("999-999-9999");
        $("#" + '<%=TextBoxFax.ClientID%>').mask("999-999-9999");
    });
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:Panel ID="PanelUserProfile" runat="server" DefaultButton="ButtonSave">
    <table width="100%" style="border: solid 0px black;">
        <tr>
            <th align="left" valign="middle" style="font-size: 14px; padding-left:10px; padding-top:10px; color:#06569D;">
               <%-- <h3 style="font-size: 13px;">--%>
                    
                    <%--<span style="padding-left: 200px;">--%><u>Profile</u><%--</span>--%>
                <%--</h3>--%>
            </th>
            <th align="right">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                        OnClick="ImageButtonBack_Click" />
            </th>
        </tr>
        <tr>
            <td style="height:15px;"></td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="width: 490px; background-color: Transparent; z-index: 0;">
                    <div id="divErrorMsgInner" style="width: 488px; background-color: #E8E9EA; z-index: 10;">
                        <div style="padding-left: 90px;">
                            <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="UserRegistration"
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
            <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                Company Name <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxCompanyName" MaxLength="50" runat="server" TabIndex="4" Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompanyName" runat="server"
                    SetFocusOnError="true" ErrorMessage="Required Company Name." ControlToValidate="TextBoxCompanyName"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                First Name <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxFirstName" MaxLength="50" runat="server" TabIndex="4" Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName" runat="server" ErrorMessage="Required First Name."
                    SetFocusOnError="true" ControlToValidate="TextBoxFirstName" Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                Last Name <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxLastName" MaxLength="50" runat="server" TabIndex="4" Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName" runat="server" ErrorMessage="Required Last Name."
                    SetFocusOnError="true" ControlToValidate="TextBoxLastName" Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                Phone <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPhone" MaxLength="15" runat="server" TabIndex="5" Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server" ErrorMessage="Required Phone Number."
                    SetFocusOnError="true" ControlToValidate="TextBoxPhone" Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                Fax&nbsp;&nbsp;
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxFax" MaxLength="15" runat="server" TabIndex="6" Width="160px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                Email <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxEmail" runat="server" AutoCompleteType="Email" TabIndex="7"
                    Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="Required Email Address."
                    SetFocusOnError="true" ControlToValidate="TextBoxEmail" Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server"
                    ErrorMessage="Please Enter Valid Email Address." ControlToValidate="TextBoxEmail"
                    SetFocusOnError="true" Display="None" ValidationGroup="UserRegistration" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                Password <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPassword" runat="server" MaxLength="50" TabIndex="8" Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword"
                    SetFocusOnError="true" Display="None" ErrorMessage="Required Password." ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server"
                    SetFocusOnError="true" ControlToValidate="TextBoxPassword" Display="None" ErrorMessage="Must use at least one digit and 8 to 50 Characters in Password."
                    ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,50})$" ValidationGroup="UserRegistration">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                Confirm - Password <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxConfirmPassword" runat="server" MaxLength="50" TabIndex="9"
                    Width="160px"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidatorConfirmPassword" runat="server" ControlToCompare="TextBoxConfirmPassword"
                    SetFocusOnError="true" ControlToValidate="TextBoxPassword" Display="None" ErrorMessage="Password and Confirm - Password do not match."
                    ValidationGroup="UserRegistration">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                Security Question <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxSecurityQuestion" runat="server" MaxLength="500" TabIndex="10"
                    Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecurityQuestion" runat="server"
                    SetFocusOnError="true" ErrorMessage="Required Security Question." ControlToValidate="TextBoxSecurityQuestion"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                Security Answer <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxSecurityAnswer" runat="server" MaxLength="500" TabIndex="11"
                    Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecurityAnswer" runat="server"
                    SetFocusOnError="true" ErrorMessage="Required Security Answer." ControlToValidate="TextBoxSecurityAnswer"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 5px;" width="190px">
            </td>
            <td align="left" style="padding-top: 5px;">
                <asp:Button ID="ButtonSave" runat="server" Text="Update" ValidationGroup="UserRegistration"
                    OnClick="ButtonSave_Click" CssClass="aspButton" Width="70px" TabIndex="28" />
                &nbsp;
                <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                    CssClass="aspButton" Width="70px" TabIndex="29" Visible="false" />&nbsp
                <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="aspButton" Width="70px"
                    TabIndex="29" OnClick="ButtonDelete_Click" />
            </td>
        </tr>
        <tr id="trRequestOwnerPriviledges" runat="server">
            <td width="190px">
                &nbsp;
            </td>
            <td align="left">
                <asp:Button ID="ButtonRequestOwnerPriviledges" Visible="false" runat="server" Text="Request Owner Privileges"
                    OnClick="ButtonRequestOwnerPriviledges_Click" CssClass="aspButton" Width="180px"
                    TabIndex="28" />
            </td>
        </tr>
        <tr>
            <td width="190px">
                &nbsp;
            </td>
            <td align="left">
                <asp:Button ID="ButtonRequestHospitalUnitAccess" Visible="false" runat="server" Text="Request Hospital Unit Access"
                    OnClick="ButtonRequestOwnerPriviledges_Click" CssClass="aspButton" Width="180px"
                    TabIndex="28" PostBackUrl="~/Users/SendRequest.aspx" />
            </td>
        </tr>
        <tr >
            <td colspan="2">
                <div style="height: 96px; overflow: auto; border: 1px double #06569d; width: 135px;"
                    id="divDataListRequestHospitalUnitAccess" visible="false" runat="server">
                    <asp:DataList ID="DataListRequestHospitalUnitAccess" runat="server" DataKeyField="MultiUserDemographicID"
                        CssClass="aspDataList" Width="135px">
                        <ItemTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:CheckBox ID="CheckBoxApproval" runat="server" AutoPostBack="True" Checked='<%#Eval("IsApproved") %>'
                                            ForeColor="#06569D" OnCheckedChanged="CheckBoxApproval_CheckedChanged" />
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="LabelButtonPermission" runat="server" Text='<%#Eval("Permission") %>'> </asp:Label>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonApprovedUser" runat="server" CommandArgument='<%#Eval("UserID") %>'><%#Eval("UnitName")%></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <div style="display: none; visibility: hidden;">
                                <asp:Label ID="UserIDLabel" runat="server" Text='<%# Eval("UserID") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
