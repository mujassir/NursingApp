<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="DemographicDetail.aspx.cs" Inherits="RMC.Web.Users.DemographicDetail"
    Title="RMC :: Hospital Demographic Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManagerDemographicDetail" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" language="javascript">
        jQuery(function($) {

            $(document.getElementById('<%=TextBoxBedsInUnit.ClientID%>')).numeric();
            $(document.getElementById('<%=TextBoxPatientsPerNurse.ClientID%>')).numeric({ allow: "." });
            $(document.getElementById('<%=TextBoxElectronicDocumentation.ClientID%>')).numeric({ allow: "." });

            $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
            $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
        });
   
    </script>

    <asp:Panel ID="PanelDemographicDetail" runat="server" DefaultButton="ButtonSave">
        <table width="100%" style="text-align: center;">
            <tr>
                <th>
                    <h3 style="font-size: 13px;">
                        Hospital Unit Information
                    </h3>
                </th>
            </tr>
            <%--<tr>
            <td align="left" style="color: Red; padding-left: 70px; font-weight: bold;">
                * indicates a mandatory fields.
            </td>
        </tr>--%>
            <tr>
                <td style="padding-left: 120px;">
                    <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                        <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
                            z-index: 10;">
                            <div style="text-align: left; padding-left: 25px;">
                                <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="Demographic"
                                    runat="server" DisplayMode="BulletList" Font-Size="11px" Font-Bold="true" Style="padding-top: 1px;" />
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
                    <table>
                        <tr>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                Unit Name <span style="color: Red;">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxUnitName" MaxLength="100" runat="server" TabIndex="2"></asp:TextBox>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="right">
                            </td>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                <asp:CheckBox ID="CheckBoxTCABUnit" Text="TCAB Unit" runat="server" TabIndex="3" />&nbsp&nbsp
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                Hospital Name <span style="color: Red;">*</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownListHospitalName" runat="server" AppendDataBoundItems="True"
                                    ForeColor="#06569d" Width="155px" TabIndex="1">
                                    <asp:ListItem Selected="True" Value="0">Select Hospital</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="right">
                            </td>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                <asp:LinkButton ID="LinkButtonAddHospital" Font-Bold="true" PostBackUrl="~/Users/HospitalRegistration.aspx"
                                    Font-Underline="true" runat="server">Add a New Hospital</asp:LinkButton>&nbsp&nbsp
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="LabelOwner" runat="server" Text="Owner Name" ForeColor="#06569d" Font-Bold="true"></asp:Label>
                                <span id="spanOwner" style="color: Red;" runat="server">*</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DropDownListOwner" ForeColor="#06569D" Width="155px" runat="server"
                                    AppendDataBoundItems="True" DataSourceID="ObjectDataSourceOwner" DataTextField="UserName"
                                    DataValueField="UserID">
                                    <asp:ListItem Value="0">Select Owner Name</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorOwner" runat="server" ControlToValidate="DropDownListOwner"
                                    Display="None" ErrorMessage="Must Select Owner Name." InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="ObjectDataSourceOwner" runat="server" SelectMethod="GetAllUsersExceptSuperAdmin"
                                    TypeName="RMC.BussinessService.BSUsers"></asp:ObjectDataSource>
                            </td>
                            <td align="right">
                            </td>
                            <td align="left">
                            </td>
                            <td align="right">
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div style="width: 100%; padding-top: 5px; padding-bottom: 5px;">
                                    <hr style="height: 1px; color: #d6d6d6;" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                Pharmacy Type <span style="color: Red;">*</span>
                            </td>
                            <td align="left" rowspan="3">
                                <asp:TextBox ID="TextBoxPharmacyType" MaxLength="500" runat="server" Height="74px"
                                    ForeColor="#06569d" TextMode="MultiLine" TabIndex="9"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                Beds In Unit <span style="color: Red;">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxBedsInUnit" MaxLength="5" runat="server" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                Patients Per Nurse <span style="color: Red;">*</span>
                            </td>
                            <td align="left">
                                <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;"><span>
                                    <asp:TextBox ID="TextBoxPatientsPerNurse" MaxLength="8" runat="server" TabIndex="8"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                Electronic Doc.(%) <span style="color: Red;">*</span>
                            </td>
                            <td align="left">
                                <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;"><span>
                                    <asp:TextBox ID="TextBoxElectronicDocumentation" runat="server" TabIndex="26"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                Start Date <span style="color: Red;">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxStartDate" runat="server" TabIndex="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtenderStartDate" PopupButtonID="TextBoxStartDate"
                                    TargetControlID="TextBoxStartDate" runat="server">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorStartDate" runat="server" ErrorMessage="Required Start Date."
                                    ControlToValidate="TextBoxStartDate" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                End Date <span style="color: Red;">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxEndDate" runat="server" TabIndex="11"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtenderEndDate" PopupButtonID="TextBoxEndDate"
                                    TargetControlID="TextBoxEndDate" runat="server">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEndedDate" runat="server" ErrorMessage="Required Ended Date."
                                    ControlToValidate="TextBoxEndDate" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidatorEndedDate" runat="server" ErrorMessage="Ended Date must be greater than Start Date."
                                    ControlToCompare="TextBoxStartDate" ControlToValidate="TextBoxEndDate" Display="None"
                                    Operator="GreaterThan" Type="Date" ValidationGroup="Demographic">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                Unit Type <span style="color: Red;">*</span>
                            </td>
                            <td align="left" rowspan="4" valign="top">
                                <asp:ListBox ID="ListBoxUnitType" runat="server" Width="179px"></asp:ListBox>
                            </td>
                            <td align="right">
                            </td>
                            <td align="left">
                            </td>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                <asp:LinkButton ID="LinkButtonUnitType" runat="server" Font-Bold="True" Font-Underline="True"
                                    PostBackUrl="~/Users/UnitType.aspx">Add a Unit Type</asp:LinkButton>&nbsp&nbsp
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="color: #06569d; font-weight: bold;">
                                &nbsp
                            </td>
                            <td align="left">
                                <cc11:CaptchaControl ID="CaptchaControlImage" runat="server" CaptchaBackgroundNoise="High"
                                    CaptchaLength="5" CaptchaHeight="55" CaptchaWidth="160" CaptchaLineNoise="None"
                                    CaptchaMinTimeout="5" CaptchaMaxTimeout="240" FontColor="#db4342" LineColor="192, 192, 255"
                                    NoiseColor="192, 192, 255" CaptchaFontWarping="Medium" />
                            </td>
                            <td align="right">
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                Type Code from Image <span style="color: Red;">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxCaptchaText" runat="server" MaxLength="500" TabIndex="12"></asp:TextBox>
                            </td>
                            <td align="right">
                            </td>
                            <td align="left">
                            </td>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                            </td>
                            <td align="left" rowspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left" style="color: #06569d; font-weight: bold;">
                                <asp:CheckBox ID="CheckBoxDocByException" Text="Doc By Exception" runat="server"
                                    TabIndex="13" />
                            </td>
                            <td align="right">
                            </td>
                            <td align="left">
                            </td>
                            <td align="right">
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 5px;">
                            </td>
                            <td align="left" colspan="5" style="padding-top: 5px;">
                                <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="Demographic"
                                    OnClick="ButtonSave_Click" CssClass="aspButton" Width="70px" TabIndex="14" />&nbsp
                                <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                                    CssClass="aspButton" Width="70px" TabIndex="15" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalName" runat="server"
                        ErrorMessage="Select Hospital Name." ControlToValidate="DropDownListHospitalName"
                        Display="None" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUnitName" runat="server" ErrorMessage="Required Unit Name."
                        ControlToValidate="TextBoxUnitName" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorUnitType" runat="server" ErrorMessage="Required Unit Type."
                    ControlToValidate="TextBoxUnitType" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorDemographic" runat="server"
                    ErrorMessage="Required Demographic." ControlToValidate="TextBoxDemographic" Display="None"
                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInUnit" runat="server"
                        ErrorMessage="Required 'Beds In Units'." ControlToValidate="TextBoxBedsInUnit"
                        Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInHospital" runat="server"
                    ErrorMessage="Required 'Beds In Hospital'." ControlToValidate="TextBoxBedsInHospital"
                    Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPatientsPerNurse" runat="server"
                        ErrorMessage="Required 'Budgeted Patients Per Nurse'." ControlToValidate="TextBoxPatientsPerNurse"
                        Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPharmacyType" runat="server"
                        ErrorMessage="Required Pharmacy Type." ControlToValidate="TextBoxPharmacyType"
                        Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorElectronicDoc" runat="server"
                        ControlToValidate="TextBoxElectronicDocumentation" Display="None" ErrorMessage="Required Electronic Documentation."
                        ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
