<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="UserRegistration.aspx.cs" Inherits="RMC.Web.Users.UserRegistration"
    Title="RMC :: User Registration" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        jQuery(function($) {

            $(document.getElementById('<%=TextBoxPrimaryPhone.ClientID %>')).mask("999-999-9999");
            $(document.getElementById('<%=TextBoxPrimaryFax.ClientID %>')).mask("999-999-9999");

            //            $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
            //            $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
        });
      
    </script>

    <asp:ScriptManager ID="ScriptManagerUserRegistration" runat="server">
    </asp:ScriptManager>
    <%--<div id="divOutter" style="background-color: #06569d; z-index: 0;">
        <div id="divInner" style="background-color: White; z-index: 10;">
            <div style="padding-top: 10px; padding-bottom: 10px;">
            </div>
        </div>
    </div>--%>
    <table width="99%">
        <tr>
            <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">
                <%--<h3 style="font-size: 13px;">--%>
                <u>User Registration</u>
                <%--</h3>--%>
            </th>
            <th align="right" style="padding-left: 10px; padding-top: 10px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    TabIndex="15" OnClick="ImageButtonBack_Click" />
            </th>
        </tr>
        <tr>
            <td style="height: 20px;">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                    <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
                        z-index: 10;">
                        <div style="padding-left: 90px;">
                            <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="UserRegistration"
                                runat="server" DisplayMode="BulletList" Font-Size="11px" Font-Bold="true" Style="padding-top: 1px;"
                                ShowMessageBox="True" ShowSummary="False" />
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
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Company Name <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxCompanyName" MaxLength="100" runat="server" TabIndex="1"
                    Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompanyName" runat="server"
                    ErrorMessage="Required Company Name." ControlToValidate="TextBoxCompanyName"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                First Name <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPrimaryFirstName" MaxLength="50" runat="server" TabIndex="2"
                    Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryFirstName" runat="server"
                    ErrorMessage="Required Primary Person's First Name." ControlToValidate="TextBoxPrimaryFirstName"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Last Name <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPrimaryLastName" MaxLength="50" runat="server" TabIndex="3"
                    Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryLastName" runat="server"
                    ErrorMessage="Required Primary Person's Last Name." ControlToValidate="TextBoxPrimaryLastName"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Phone <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPrimaryPhone" MaxLength="20" runat="server" TabIndex="4"
                    Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryPhone" runat="server"
                    ErrorMessage="Required Primary Phone Number." ControlToValidate="TextBoxPrimaryPhone"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Fax&nbsp;&nbsp;
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPrimaryFax" MaxLength="20" runat="server" TabIndex="5" Width="160px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Email <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPrimaryEmail" runat="server" AutoCompleteType="Email" TabIndex="6"
                    Width="160px" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryEmail" runat="server"
                    ErrorMessage="Required Primary Email Address." ControlToValidate="TextBoxPrimaryEmail"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPrimaryEmail" runat="server"
                    ErrorMessage="Please Enter Valid Email Address." ControlToValidate="TextBoxPrimaryEmail"
                    Display="None" ValidationGroup="UserRegistration" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Password <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPrimaryPassword" runat="server" MaxLength="50" TextMode="Password"
                    TabIndex="7" Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryPassword" runat="server"
                    ControlToValidate="TextBoxPrimaryPassword" Display="None" ErrorMessage="Required Password for Primary Person."
                    ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPrimaryPassword" runat="server"
                    ControlToValidate="TextBoxPrimaryPassword" Display="None" ErrorMessage="Must use at least one digit and 8 to 50 Characters in Password."
                    ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,50})$" ValidationGroup="UserRegistration">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Confirm Password <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPrimaryConfirmPwd" runat="server" MaxLength="50" TextMode="Password"
                    TabIndex="8" Width="160px"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidatorConfirmPwd" runat="server" ControlToCompare="TextBoxPrimaryConfirmPwd"
                    ControlToValidate="TextBoxPrimaryPassword" Display="None" ErrorMessage="Confirm Password of Primary Person does not match."
                    ValidationGroup="UserRegistration">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Security Question <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPrimarySecurityQuestion" runat="server" MaxLength="500" TabIndex="9"
                    Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecurityQuestion" runat="server"
                    ErrorMessage="Required Security Question." ControlToValidate="TextBoxPrimarySecurityQuestion"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Security Answer <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxPrimarySecurityAnswer" runat="server" MaxLength="500" TabIndex="10"
                    Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecurityAnswer" runat="server"
                    ErrorMessage="Required Security Answer." ControlToValidate="TextBoxPrimarySecurityAnswer"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold;" width="190px">
                &nbsp;
            </td>
            <td align="left">
                <cc11:CaptchaControl ID="CaptchaControlImage" runat="server" CaptchaBackgroundNoise="High"
                    CaptchaLength="5" CaptchaHeight="60" CaptchaWidth="190" CaptchaLineNoise="None"
                    CaptchaMinTimeout="5" CaptchaMaxTimeout="240" FontColor="#db4342" LineColor="192, 192, 255"
                    NoiseColor="192, 192, 255" CaptchaFontWarping="Low" Width="236px" />
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Type Code from Image <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxCaptchaText" runat="server" MaxLength="500" TabIndex="11"
                    Width="160px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCaptchaText" runat="server"
                    ErrorMessage="Must Enter the Captcha Text." ControlToValidate="TextBoxCaptchaText"
                    Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                Activate a User &nbsp;&nbsp;
            </td>
            <td align="left">
                <asp:CheckBox ID="CheckBoxActivateUser" runat="server" ForeColor="#06569D" TabIndex="12" />
            </td>
        </tr>
        <tr>
            <td style="padding-top: 5px;" width="190px">
            </td>
            <td align="left" style="padding-top: 5px;">
                <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="UserRegistration"
                    OnClick="ButtonSave_Click" CssClass="aspButton" Width="70px" TabIndex="13" Style="height: 26px" />
                &nbsp;
                <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                    CssClass="aspButton" Width="70px" TabIndex="14" Visible="false" />
            </td>
        </tr>
        <tr>
            <td width="190px">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:LinqDataSource ID="LinqDataSourcePermission" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
        Select="new (PermissionID, Permission1, IsActive)" TableName="Permissions" Where="IsActive == true">
    </asp:LinqDataSource>
</asp:Content>
