<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HospitalDemographicInfo.ascx.cs"
    Inherits="RMC.Web.UserControls.HospitalDemographicInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc11" %>
<%@ Register Src="ucDemographicMembersTreeView.ascx" TagName="ucDemographicMembersTreeView"
    TagPrefix="uc2" %>

<style type="text/css">
    .style1
    {
        height: 30px;
    }
</style>

<script type="text/javascript" language="javascript">

    jQuery(function($) {

        $(document.getElementById('<%=TextBoxBedsInUnit.ClientID%>')).numeric();
        $(document.getElementById('<%=TextBoxPatientsPerNurse.ClientID%>')).numeric({ allow: "." });
        $(document.getElementById('<%=TextBoxElectronicDocumentation.ClientID%>')).numeric({ allow: "." });

        //        $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");

        //        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });


    function confirmDelete() {

        var ans = confirm("Are you sure want to remove this Hospital Unit?");

        if (ans) {
            var otherAns = confirm("This action will permanently delete this Unit and all related links.Are you sure you want to do this?");
            if (otherAns) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    function windowPopup(page, title) {

        window.open(page, '_blank', 'height=355,width=710,top=150,left=150,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false');
        return false;
    }
</script>

<asp:Panel ID="PanelHospitalDemographicInfo" runat="server" DefaultButton="ButtonSave"
    Width="100%"> 

    <table width="100%" style="text-align: center;">
        <tr>
            <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                <%--<h3 style="font-size: 13px;">
                    <table width="100%">
                        <tr>
                            <td align="left">--%>
                <%--</td>
                            <td align="center">--%>
                <u>Hospital Unit Information <span style="padding-left: 5px;">( </span>
                    <asp:Literal ID="LiteralHospitalName" runat="server"></asp:Literal>
                    <span style="padding-left: 5px;">)</span></u>
                <%--</td>
                        </tr>
                    </table>
                </h3>--%>
            </th>
            <th align="right" style="padding-left: 10px; padding-top: 10px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    OnClick="ImageButtonBack_Click" TabIndex="21" />
            </th>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <%-- <tr>
        <td align="left" style="color: Red; padding-left: 70px; font-weight: bold;">
            * indicates a mandatory fields.
        </td>
    </tr>--%>
        <tr>
            <td style="padding-left: 120px;">
                <div style="width: 320px; float: left; background-color: Transparent; z-index: 0;">
                    <div id="divErrorMsgInner" style="width: 318px; float: left; background-color: #E8E9EA;
                        z-index: 10;">
                        <div style="text-align: left; padding-left: 25px;">
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
            <td>
                <table>
                 <tr>
                        <td style="padding-top: 5px;">
                        </td>
                        <td align="left" colspan="4" style="padding-top: 5px;">
                            <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="Demographic"
                                OnClick="ButtonSave_Click" CssClass="aspButton" Width="70px" TabIndex="19" />
                            <div id="divSaveDisable" visible="false" class="aspButtonDisable" style="width: 65px;
                                height: 25px; text-align: center; cursor: pointer;" runat="server">
                                <asp:Label ID="LabelSaveDisable" runat="server" Text="Update"></asp:Label>
                            </div>
                            &nbsp;
                            <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                                CssClass="aspButton" Width="70px" TabIndex="20" Visible="false" />&nbsp;
                            <asp:Button ID="ButtonDelete" runat="server" Visible="false" Text="Delete" CssClass="aspButton"
                                Width="70px" TabIndex="20" OnClick="ButtonDelete_Click" OnClientClick="return confirmDelete()" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Unit Name <span style="color: Red;">*</span>
                        </td>
                        <td align="left" colspan="2" valign="middle">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxUnitName" MaxLength="100" runat="server"
                                TabIndex="1" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Approved Users <span>&nbsp;&nbsp;</span>
                        </td>
                        <td align="left" rowspan="3" valign="top">
                            <div style="height: 96px; overflow: auto; border: 1px double #06569d; width: 135px;">
                                <%--<asp:ListBox CssClass="aspListBox" ID="ListBoxApprovedUsers" DataSourceID="ObjectDataSourceTreeView" SelectionMode="Multiple"
                                   DataTextField="UserName" DataValueField="UserID"  ForeColor="#06569d" runat="server" Height="175px" TabIndex="6"></asp:ListBox>--%>

                                <asp:DataList ID="DataListApprovedUsers" runat="server" DataSourceID="ObjectDataSourceTreeView"
                                    DataKeyField="UserID" CssClass="aspDataList" Width="130px" TabIndex="6">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBoxApproval" runat="server" AutoPostBack="True" Checked='<%#Eval("IsApproved") %>'
                                                        ForeColor="#06569D" OnCheckedChanged="CheckBoxApproval_CheckedChanged" TabIndex="6"/>
                                                </td>
                                                <td>
                                                 &nbsp;
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LinkButtonApprovedUser" runat="server" CommandArgument='<%#Eval("UserID") %>'
                                                        OnClick="LinkButtonApprovedUser_Click" TabIndex="6"><%#Eval("UserName") %></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButtonForApprovedUser" runat="server" OnClientClick="return false;"
                                                        Visible="false" TabIndex="6"><%#Eval("UserName") %></asp:LinkButton>
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
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                        </td>
                        <td align="left" colspan="2" valign="middle">
                            <asp:CheckBox ID="CheckBoxTCABUnit" runat="server" Font-Bold="True" Font-Size="11px"
                                ForeColor="#06569D" TabIndex="4" Text="TCAB Unit" />
                        </td>
                        <td align="left" rowspan="1">
                        </td>
                    </tr>
                    <tr id="2">
                        <td align="right" style="color: #06569d; font-weight: bold;">
<%--                            <asp:Label ID="LabelOwner" runat="server" Text="Owner Name" ForeColor="#06569d" Font-Bold="true"
                                Font-Size="11px"></asp:Label>--%>
                          <%--  <span id="spanOwner" style="color: Red;" runat="server">*</span>--%>
                        </td>
                        <td align="left" colspan="2" valign="middle">
                            <asp:DropDownList ID="DropDownListOwner" runat="server" AppendDataBoundItems="True"
                                CssClass="aspDropDownList" ForeColor="#06569D" TabIndex="5" Visible="false">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="left" colspan="2">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:LinkButton ID="LinkButtonApproveUser" runat="server" Font-Bold="True" OnClick="LinkButtonApproveUser_Click"
                                Font-Size="10px" TabIndex="7">Approve a User</asp:LinkButton><br />
                            <asp:LinkButton ID="LinkButtonRegisterNewUser" runat="server" Font-Bold="True" OnClick="LinkButtonRegisterNewUser_Click"
                                Font-Size="10px" TabIndex="8">Register a New User</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <div style="width: 100%; padding-top: 5px; padding-bottom: 5px;">
                                <hr style="height: 1px; color: #d6d6d6;" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Beds In Unit <span style="color: Red;">&nbsp;&nbsp;</span>
                        </td>
                        <td align="left">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxBedsInUnit" MaxLength="5" runat="server"
                                TabIndex="9" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Start Date <span style="color: Red;">&nbsp;&nbsp;</span>
                        </td>
                        <td align="left">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxStartDate" runat="server" TabIndex="10"
                                ToolTip="Date Format : mm/dd/yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderStartDate" PopupButtonID="TextBoxStartDate"
                                TargetControlID="TextBoxStartDate" runat="server" Format="MM/dd/yyyy">
                            </cc1:CalendarExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorStartDate" runat="server"
                                ErrorMessage="Please enter valid start date (mm/dd/yyyy)." ControlToValidate="TextBoxStartDate"
                                Display="None" ValidationExpression="(0[1-9]|1[012])[\/](0[1-9]|[12][0-9]|3[01])[\/](19|20)\d\d"
                                ValidationGroup="Demographic" ToolTip="Date Format : mm/dd/yyyy">*</asp:RegularExpressionValidator>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorStartDate" runat="server" ErrorMessage="Required Start Date."
                                ControlToValidate="TextBoxStartDate" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" 
                            class="style1">
                            Patients / Nurse <span style="color: Red;">&nbsp;&nbsp;</span>
                        </td>
                        <td align="left" class="style1">
                            <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;">
                            </span>
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxPatientsPerNurse" MaxLength="8" runat="server"
                                TabIndex="11" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
                        <td class="style1">
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" 
                            class="style1">
                            End Date <span>&nbsp;&nbsp;</span>
                        </td>
                        <td align="left" class="style1">
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxEndDate" runat="server" TabIndex="12"
                                ToolTip="Date Format : mm/dd/yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderEndDate" PopupButtonID="TextBoxEndDate"
                                TargetControlID="TextBoxEndDate" runat="server" Format="MM/dd/yyyy">
                            </cc1:CalendarExtender>
                            <asp:CompareValidator ID="CompareValidatorEndedDate" runat="server" ErrorMessage="Ended Date must be greater than Start Date."
                                SetFocusOnError="true" ControlToCompare="TextBoxStartDate" ControlToValidate="TextBoxEndDate"
                                Display="None" Operator="GreaterThan" Type="Date" ValidationGroup="Demographic">*</asp:CompareValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEndDate" runat="server"
                                ErrorMessage="Please enter valid end date (mm/dd/yyyy)." ControlToValidate="TextBoxEndDate"
                                Display="None" ValidationExpression="(0[1-9]|1[012])[\/](0[1-9]|[12][0-9]|3[01])[\/](19|20)\d\d"
                                ValidationGroup="Demographic" ToolTip="Date Format : mm/dd/yyyy">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Electronic Doc. <span style="color: Red;">&nbsp;&nbsp;</span>
                        </td>
                        <td align="left">
                            <span class="file" style="font-size: 11px; font-weight: bold; cursor: pointer;">
                            </span>
                            <asp:TextBox CssClass="aspTextBox" ID="TextBoxElectronicDocumentation" runat="server"
                                TabIndex="13" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td align="left" style="color: #06569d; font-weight: bold;" valign="top">
                            <asp:CheckBox ID="CheckBoxDocByException" Text="Doc By Exception" runat="server"
                                TabIndex="14" Font-Size="11px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Unit Type <span style="color: Red;">&nbsp;&nbsp;</span>
                        </td>
                        <td align="left" rowspan="10" valign="top">
                            <asp:ListBox CssClass="aspListBox" ID="ListBoxUnitType" DataSourceID="ObjectDataSourceUnitType"
                                ForeColor="#06569d" runat="server" SelectionMode="Multiple" DataTextField="UnitTypeName"
                                DataValueField="UnitTypeID" Height="175px" TabIndex="16"></asp:ListBox>
                        </td>
                        <td>
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                            Pharmacy Type <span style="color: Red;">&nbsp;&nbsp;</span>
                        </td>
                        <td align="left" rowspan="10" valign="top">
                            <asp:ListBox CssClass="aspListBox" ID="ListBoxPharmacyType" SelectionMode="Multiple"
                                ForeColor="#06569d" runat="server" Height="175px" Rows="12" TabIndex="18"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="color: #06569d; font-size: 11px; padding-right: 6px;">
                            Hold &#39;Ctrl&#39; key down to select/deselect multiple types.
                        </td>
                        <td>
                        </td>
                        <td align="right" style="color: #06569d; font-size: 11px; padding-right: 6px;">
                            Hold the &#39;Ctrl&#39; key down to select/deselect multiple types.
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <p>
                                <asp:LinkButton ID="LinkButtonUnitType" runat="server" PostBackUrl="" Font-Bold="True"
                                    Font-Underline="True" OnClick="LinkButtonUnitType_Click" Font-Size="10px" TabIndex="15">Add a Unit Type</asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonUnitTypeForUser" Font-Bold="True" Font-Underline="false"
                                    Font-Size="10px" OnClientClick="windowPopup('../Users/RequestForTypes.aspx?Type=Unit Type', 'Unit Type'); return false;"
                                    runat="server" TabIndex="15">Add a Unit Type</asp:LinkButton>
                                &nbsp;&nbsp;
                            </p>
                        </td>
                        <td>
                        </td>
                        <td align="right" style="padding-right: 6px;">
                            <p>
                                <asp:LinkButton ID="LinkButtonAddPharmacy" Font-Bold="true" Font-Underline="true"
                                    Font-Size="10px" runat="server" OnClick="LinkButtonAddPharmacy_Click" TabIndex="17">Add a Pharmacy Type</asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonAddPharmacyForUser" Font-Bold="True" Font-Underline="false"
                                    Font-Size="10px" OnClientClick="windowPopup('../Users/RequestForTypes.aspx?Type=Pharmacy Type', 'Unit Type'); return false;"
                                    runat="server" TabIndex="17">Add a Pharmacy Type</asp:LinkButton>
                                &nbsp;&nbsp;
                            </p>
                        </td>
                    </tr>
                    <tr>
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
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="left">
                        </td>
                        <td align="right" style="color: #06569d; font-weight: bold;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="left">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="left">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="left">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="left">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="left">
                        </td>
                        <td>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td style="padding-top: 5px;">
                        </td>
                        <td align="left" colspan="4" style="padding-top: 5px;">
                            <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="Demographic"
                                OnClick="ButtonSave_Click" CssClass="aspButton" Width="70px" TabIndex="19" />
                            <div id="divSaveDisable" visible="false" class="aspButtonDisable" style="width: 65px;
                                height: 25px; text-align: center; cursor: pointer;" runat="server">
                                <asp:Label ID="LabelSaveDisable" runat="server" Text="Update"></asp:Label>
                            </div>
                            &nbsp;
                            <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                                CssClass="aspButton" Width="70px" TabIndex="20" Visible="false" />&nbsp;
                            <asp:Button ID="ButtonDelete" runat="server" Visible="false" Text="Delete" CssClass="aspButton"
                                Width="70px" TabIndex="20" OnClick="ButtonDelete_Click" OnClientClick="return confirmDelete()" />
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="ObjectDataSourceUnitType" runat="server" SelectMethod="GetAllUnitType"
                    TypeName="RMC.BussinessService.BSUnitType"></asp:ObjectDataSource>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalName" runat="server"
                    SetFocusOnError="true" ErrorMessage="Select Hospital Name." ControlToValidate="DropDownListHospital"
                    Display="None" InitialValue="0" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUnitName" runat="server" ErrorMessage="Required Unit Name."
                    ControlToValidate="TextBoxUnitName" SetFocusOnError="true" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                <%--<asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidatorBedsInUnit"
                    runat="server" ErrorMessage="Required 'Beds In Units'." ControlToValidate="TextBoxBedsInUnit"
                    Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorPatientsPerNurse" runat="server" SetFocusOnError="true"
                        ErrorMessage="Required 'Budgeted Patients Per Nurse'." ControlToValidate="TextBoxPatientsPerNurse"
                        Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
                <%--<asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidatorPharmacyType"
                    runat="server" ErrorMessage="Required Pharmacy Type." ControlToValidate="ListBoxPharmacyType"
                    Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidatorElectronicDoc"
                    runat="server" ControlToValidate="TextBoxElectronicDocumentation" Display="None"
                    ErrorMessage="Required Electronic Documentation." ValidationGroup="Demographic">*</asp:RequiredFieldValidator>--%>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjectDataSourceTreeView" runat="server" SelectMethod="GetAllMembersOfHospitalUnit"
        TypeName="RMC.BussinessService.BSTreeView">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="hospitalDemographicId" QueryStringField="HospitalDemographicId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Panel>
