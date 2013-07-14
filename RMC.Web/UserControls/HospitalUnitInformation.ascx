<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HospitalUnitInformation.ascx.cs"
    Inherits="RMC.Web.UserControls.HospitalUnitInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc11" %>
<asp:ScriptManager ID="ScriptManagerDemographicDetail" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    jQuery(function($) {

        $(document.getElementById('<%=TextBoxBedsInUnit.ClientID%>')).numeric();
        $(document.getElementById('<%=TextBoxPatientsPerNurse.ClientID%>')).numeric({ allow: "." });
        $(document.getElementById('<%=TextBoxElectronicDocumentation.ClientID%>')).numeric({ allow: "." });

        //        $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
        //        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });

    function windowPopup(page, title) {

        window.open(page, '_blank', 'height=355,width=710,top=150,left=150,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false');
        return false;
    }
</script>

<asp:Panel ID="PanelHospitalUnitInformation" runat="server" DefaultButton="ButtonSave">
    <table width="99%">
        <tr>
            <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                <u>Hospital Unit Information <span style="padding-left: 5px;">( </span>
                    <asp:Literal ID="LiteralHospitalName" runat="server"></asp:Literal>
                    <span style="padding-left: 5px;">)</span></u>
            </th>
            <th align="right" style="padding-top: 10px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    TabIndex="16" OnClick="ImageButtonBack_Click" Visible="false" />
            </th>
        </tr>
        <tr>
            <td style="padding-left: 20px;" colspan="2">
                <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                    <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
                        z-index: 10;">
                        <div style="text-align: left; padding-left: 5px;">
                            <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="Demographic"
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
            <td style="height: 10px;" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td style="padding-top: 5px;">
                        </td>
                        <td align="left" colspan="3" style="padding-top: 5px;">
                            <asp:Button ID="Button1" runat="server" Text="Save" ValidationGroup="Demographic"
                                OnClick="ButtonSave_Click" CssClass="aspButton" Width="70px" TabIndex="14" />&nbsp
                            <asp:Button ID="Button2" runat="server" Text="Reset" Visible="false" OnClick="ButtonReset_Click"
                                CssClass="aspButton" Width="70px" TabIndex="15" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; width: 30%; font-size: 11px;">
                            Unit Name <span style="color: Red;">*</span>
                        </td>
                        <td align="left" valign="middle" style="width: 70%;" colspan="3">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxUnitName" MaxLength="100" runat="server"
                                Width="300px" TabIndex="1" AutoCompleteType="Disabled"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; width: 30%;">
                            &nbsp;
                        </td>
                        <td align="left" valign="middle" style="width: 70%;" colspan="3">
                            <asp:CheckBox ID="CheckBoxTCABUnit" runat="server" Font-Bold="True" Font-Size="11px"
                                ForeColor="#06569D" TabIndex="2" Text="TCAB Unit" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">
                            <asp:Label ID="LabelOwner" runat="server" Text="Owner Name" ForeColor="#06569d" Font-Bold="true"
                                Font-Size="11px"></asp:Label>
                            <span id="spanOwner" style="color: Red;" runat="server">*</span>
                        </td>
                        <td align="left" style="width: 70%;" colspan="3">
                            <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListOwner" ForeColor="#06569D"
                                Width="305px" runat="server" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceOwner"
                                DataTextField="UserName" DataValueField="UserID" TabIndex="3">
                                <asp:ListItem Value="0">Select Owner Name</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorOwner" runat="server" ControlToValidate="DropDownListOwner"
                                SetFocusOnError="true" Display="None" ErrorMessage="Must Select Owner Name."
                                InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="ObjectDataSourceOwner" runat="server" SelectMethod="GetUsersByHospitalInfoID"
                                TypeName="RMC.BussinessService.BSUsers">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="0" Name="hospitalInfoID" QueryStringField="HospitalInfoID"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="width: 100%; padding-top: 5px; padding-bottom: 5px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Beds In Unit &nbsp&nbsp<%--<span style="color: Red;">*</span>--%>
                        </td>
                        <td align="left">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxBedsInUnit" MaxLength="5" runat="server"
                                TabIndex="4" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Start Date &nbsp&nbsp<%--<span style="color: Red;">*</span>--%>
                        </td>
                        <td align="left">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxStartDate" runat="server" TabIndex="5"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderStartDate" PopupButtonID="TextBoxStartDate"
                                Format="MM/dd/yyyy" TargetControlID="TextBoxStartDate" runat="server">
                            </cc1:CalendarExtender>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorStartDate" runat="server" ErrorMessage="Required Start Date."
                                SetFocusOnError="true" ControlToValidate="TextBoxStartDate" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorStartDate" runat="server"
                                ControlToValidate="TextBoxStartDate" Display="None" ErrorMessage="Please enter valid start date (mm/dd/yyyy)."
                                ToolTip="Date Format : mm/dd/yyyy" ValidationExpression="(0[1-9]|1[012])[\/](0[1-9]|[12][0-9]|3[01])[\/](19|20)\d\d"
                                ValidationGroup="Demographic">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Patients Per Nurse &nbsp&nbsp<%--<span style="color: Red;">*</span>--%>
                        </td>
                        <td align="left">
                            <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;"><span>
                                <asp:TextBox CssClass="aspTextBox" ID="TextBoxPatientsPerNurse" MaxLength="8" runat="server"
                                    TabIndex="6" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            End Date &nbsp&nbsp
                        </td>
                        <td align="left">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxEndDate" runat="server" TabIndex="7"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderEndDate" PopupButtonID="TextBoxEndDate"
                                Format="MM/dd/yyyy" TargetControlID="TextBoxEndDate" runat="server">
                            </cc1:CalendarExtender>
                            <asp:CompareValidator ID="CompareValidatorEndedDate" runat="server" ErrorMessage="Ended Date must be greater than Start Date."
                                ControlToCompare="TextBoxStartDate" ControlToValidate="TextBoxEndDate" Display="None"
                                Operator="GreaterThan" Type="Date" ValidationGroup="Demographic">*</asp:CompareValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEndDate" runat="server"
                                ControlToValidate="TextBoxEndDate" Display="None" ErrorMessage="Please enter valid end date (mm/dd/yyyy)."
                                ToolTip="Date Format : mm/dd/yyyy" ValidationExpression="(0[1-9]|1[012])[\/](0[1-9]|[12][0-9]|3[01])[\/](19|20)\d\d"
                                ValidationGroup="Demographic">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Electronic Doc.(%) &nbsp&nbsp<%--<span style="color: Red;">*</span>--%>
                        </td>
                        <td align="left">
                            <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;"><span>
                                <asp:TextBox CssClass="aspTextBox" ID="TextBoxElectronicDocumentation" runat="server"
                                    TabIndex="8" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td align="left" style="color: #06569d; font-weight: bold;" valign="top">
                            <asp:CheckBox ID="CheckBoxDocByException" Text="Doc By Exception" runat="server"
                                Font-Size="11px" TabIndex="9" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Unit Type &nbsp&nbsp<%--<span style="color: Red;">*</span>--%>
                        </td>
                        <td align="left" rowspan="10" valign="top">
                            <asp:ListBox CssClass="aspListBox" ID="ListBoxUnitType" runat="server" DataSourceID="ObjectDataSourceUnitType"
                                DataTextField="UnitTypeName" DataValueField="UnitTypeID" ForeColor="#06569D"
                                SelectionMode="Multiple" Height="175px" Rows="12" TabIndex="11"></asp:ListBox>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Pharmacy Type &nbsp&nbsp<%--<span style="color: Red;">*</span>--%>
                        </td>
                        <td align="left" rowspan="10" valign="top">
                            <asp:ListBox CssClass="aspListBox" ID="ListBoxPharmacyType" runat="server" ForeColor="#06569D"
                                DataSourceID="ObjectDataSourcePharmacyType" DataTextField="PharmacyName" DataValueField="PharmacyTypeID"
                                SelectionMode="Multiple" Height="175px" Rows="12" TabIndex="13"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-size: 11px; padding-right: 6px;">
                            Hold &#39;Ctrl&#39; key down to select/deselect multiple types.
                        </td>
                        <td align="right" style="color: #06569d; font-size: 11px; padding-right: 6px;">
                            Hold &#39;Ctrl&#39; key down to select/deselect multiple types.
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold;">
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:LinkButton ID="LinkButtonAddUnitType" Font-Bold="True" Font-Underline="True"
                                Font-Size="10px" runat="server" TabIndex="10">Add a Unit Type</asp:LinkButton>
                            <asp:LinkButton ID="LinkButtonUnitTypeForUser" Font-Bold="True" Font-Underline="false"
                                Font-Size="10px" OnClientClick="windowPopup('../Users/RequestForTypes.aspx?Type=Unit Type', 'Unit Type'); return false;"
                                runat="server" TabIndex="10">Add a Unit Type</asp:LinkButton>
                            &nbsp&nbsp
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="LinkButtonPharmacyType" runat="server" Font-Bold="True" Font-Underline="True"
                                Font-Size="10px" TabIndex="12">Add a Pharmacy Type</asp:LinkButton>&nbsp&nbsp
                            <asp:LinkButton ID="LinkButtonAddPharmacyForUser" Font-Bold="True" Font-Underline="false"
                                Font-Size="10px" OnClientClick="windowPopup('../Users/RequestForTypes.aspx?Type=Pharmacy Type', 'Unit Type'); return false;"
                                runat="server" TabIndex="12">Add a Pharmacy Type</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold;">
                            &nbsp&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="width: 100%; padding-top: 5px; padding-bottom: 5px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="right" style="color: #06569d; font-weight: bold;">
                        </td>
                        <td align="left" style="color: #06569d; font-weight: bold;">
                            <cc11:CaptchaControl ID="CaptchaControlImage" runat="server" CaptchaBackgroundNoise="High"
                                CaptchaLength="5" CaptchaHeight="55" CaptchaWidth="160" CaptchaLineNoise="None"
                                CaptchaMinTimeout="5" CaptchaMaxTimeout="240" FontColor="#db4342" LineColor="192, 192, 255"
                                NoiseColor="192, 192, 255" CaptchaFontWarping="Medium" />
                        </td>
                        <td align="left">
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Type Code <span style="color: Red;">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxCaptchaText" runat="server" MaxLength="500"
                                TabIndex="16" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
                        <td align="left">
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold;">
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="padding-top: 5px;">
                        </td>
                        <td align="left" colspan="3" style="padding-top: 5px;">
                            <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="Demographic"
                                OnClick="ButtonSave_Click" CssClass="aspButton" Width="70px" TabIndex="14" Visible="false" />&nbsp
                            <asp:Button ID="ButtonReset" runat="server" Text="Reset" Visible="false" OnClick="ButtonReset_Click"
                                CssClass="aspButton" Width="70px" TabIndex="15" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ObjectDataSource ID="ObjectDataSourcePharmacyType" runat="server" SelectMethod="GetAllPharmacyType"
                    TypeName="RMC.BussinessService.BSPharmacyType"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceUnitType" runat="server" SelectMethod="GetAllUnitType"
                    TypeName="RMC.BussinessService.BSUnitType"></asp:ObjectDataSource>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalName" runat="server"
                    ErrorMessage="Select Hospital Name." ControlToValidate="DropDownListHospitalName"
                    Display="None" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUnitName" runat="server" ErrorMessage="Required Unit Name."
                    ControlToValidate="TextBoxUnitName" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorUnitType" runat="server" ErrorMessage="Required Unit Type."
                    ControlToValidate="TextBoxUnitType" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorDemographic" runat="server"
                    ErrorMessage="Required Demographic." ControlToValidate="TextBoxDemographic" Display="None"
                    ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInUnit" runat="server"
                    SetFocusOnError="true" ErrorMessage="Required 'Beds In Units'." ControlToValidate="TextBoxBedsInUnit"
                    Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInHospital" runat="server"
                    ErrorMessage="Required 'Beds In Hospital'." ControlToValidate="TextBoxBedsInHospital"
                    Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorPatientsPerNurse" runat="server"
                    SetFocusOnError="true" ErrorMessage="Required 'Budgeted Patients Per Nurse'."
                    ControlToValidate="TextBoxPatientsPerNurse" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorPharmacyType" runat="server"
                ErrorMessage="Required Pharmacy Type." ControlToValidate="TextBoxPharmacyType"
                Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorElectronicDoc" runat="server"
                    SetFocusOnError="true" ControlToValidate="TextBoxElectronicDocumentation" Display="None"
                    ErrorMessage="Required Electronic Documentation." ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorPharmacyType" runat="server"
                    SetFocusOnError="true" ControlToValidate="ListBoxPharmacyType" Display="None"
                    ErrorMessage="Select Pharmacy Type." ValidationGroup="Demographic" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorUserType" runat="server" ControlToValidate="ListBoxUnitType"
                    SetFocusOnError="true" Display="None" ErrorMessage="Select Unit Type." ValidationGroup="Demographic"
                    InitialValue="0">*</asp:RequiredFieldValidator>--%>
            </td>
        </tr>
    </table>
</asp:Panel>
