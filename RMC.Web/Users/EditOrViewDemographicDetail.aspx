<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditOrViewDemographicDetail.aspx.cs" Inherits="RMC.Web.Users.EditOrViewDemographicDetail"
    Title="RMC :: Demographic Detail" %>

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

            $('#accordion').accordion();
            $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
            $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
        });
   
    </script>

    <asp:Panel ID="PanelEditOrViewDemographicDetail" runat="server" DefaultButton="ButtonUpda">
        <table width="100%" style="text-align: center;">
            <tr>
                <th align="left">
                    <h3 style="font-size: 13px;">
                        <asp:ImageButton ID="ImageButton1" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                            PostBackUrl="~/Users/DataManagement.aspx" /><span style="padding-left: 250px;">Hospital
                                DemoGraphic Detail</span>
                    </h3>
                </th>
            </tr>
            <tr>
                <td>
                    <div id="accordion" style="width: 90%;">
                        <%-- <h3>
                        <a style="font-weight: bold;" href="#">Hospital Demographic Detail</a></h3>--%>
                        <div>
                            <%-- <asp:MultiView ID="MultiViewEditOrViewDemographic" runat="server">
                            <asp:View ID="ViewReadOnly" runat="server" OnLoad="ViewReadOnly_Load">--%>
                            <%--  <table width="100%" style="text-align: center;">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Hospital Name <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelHospitalName" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Unit Name <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelUnitName" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="left" colspan="5" style="color: #06569d; font-weight: bold;">
                                                        <asp:CheckBox ID="CheckBoxTCUnit" Enabled="false" Text="TCAB Unit" runat="server"
                                                            TabIndex="3" />
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
                                                        Unit Type <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelUnitType" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Demographic <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelDemographic" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Beds In Unit <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelBedInUnit" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Beds In Hospital <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelBedsInHospital" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Budgeted Patients Per Nurse <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelBudgetedPatientsPerNurse" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Pharmacy Type <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left" rowspan="2">
                                                        <asp:Label ID="LabelPharmacyType" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Electronic Documentation(%) <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelElectronicDocumentation" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Start Date <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelStartDate" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        End Date <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelEndDate" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-top: 5px;">
                                                    </td>
                                                    <td align="left" colspan="5" style="padding-top: 5px;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>--%>
                            <%-- </asp:View>--%>
                            <%-- <asp:View ID="ViewEdit" runat="server" OnLoad="ViewEdit_Load">--%>
                            <table width="100%" style="text-align: center;">
                                <tr>
                                    <th>
                                        <h3 style="font-size: 13px;">
                                            Hospital Unit Information
                                        </h3>
                                    </th>
                                </tr>
                                <%-- <tr>
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
                                                <%-- <td align="right" style="color: #06569d; font-weight: bold;">
                        <asp:LinkButton ID="LinkButtonAddHospital" Font-Bold="true" PostBackUrl="~/Users/HospitalRegistration.aspx"
                            Font-Underline="true" runat="server">Add a New Hospital</asp:LinkButton>&nbsp&nbsp
                    </td>--%>
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
                                                    <%-- <asp:LinkButton ID="LinkButtonUnitType" runat="server" Font-Bold="True" Font-Underline="True"
                            PostBackUrl="~/Users/UnitType.aspx">Add a Unit Type</asp:LinkButton>&nbsp&nbsp--%>
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
                                            <%-- <tr>
                                            <td style="padding-top: 5px;">
                                            </td>
                                            <td align="left" colspan="5" style="padding-top: 5px;">
                                                <asp:Button ID="ButtonUpdate" runat="server" Text="Update" ValidationGroup="Demographic"
                                                    CssClass="aspButton" Width="70px" TabIndex="14" OnClick="ButtonUpdate_Click" />&nbsp
                                                <asp:Button ID="ButtonReset" runat="server" Text="Reset" CssClass="aspButton" Width="70px"
                                                    TabIndex="15" OnClick="ButtonReset_Click" />
                                            </td>
                                        </tr>--%>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInUnit" runat="server"
                                            ErrorMessage="Required 'Beds In Units'." ControlToValidate="TextBoxBedsInUnit"
                                            Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
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
                            <%--<table width="100%" style="text-align: center;">
                                    <tr>
                                        <td align="left" style="color: Red; padding-left: 70px; font-weight: bold;">
                                            * indicates a mandatory fields.
                                        </td>
                                    </tr>
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
                                                    <td colspan="6" align="right" style="padding-right: 30px;">
                                                        <asp:LinkButton ID="LinkButtonAddHospital" Font-Bold="true" PostBackUrl="~/Users/HospitalRegistration.aspx"
                                                            runat="server">Add a New Hospital</asp:LinkButton>
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
                                                        Unit Name <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxUnitName" MaxLength="100" runat="server" TabIndex="2"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="left" colspan="5" style="color: #06569d; font-weight: bold;">
                                                        <asp:CheckBox ID="CheckBoxTCABUnit" Text="TCAB Unit" runat="server" TabIndex="3" />
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
                                                        Unit Type <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxUnitType" MaxLength="50" runat="server" TabIndex="4"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Demographic <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxDemographic" MaxLength="50" runat="server" TabIndex="5"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Beds In Unit <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxBedsInUnit" MaxLength="5" runat="server" TabIndex="6"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Beds In Hospital <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxBedsInHospital" MaxLength="8" runat="server" TabIndex="7"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Budgeted Patients Per Nurse <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxPatientsPerNurse" MaxLength="8" runat="server" TabIndex="8"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Pharmacy Type <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left" rowspan="2">
                                                        <asp:TextBox ID="TextBoxPharmacyType" MaxLength="500" runat="server" Height="47px"
                                                            ForeColor="#06569d" TextMode="MultiLine" TabIndex="9"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Electronic Documentation(%) <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxElectronicDocumentation" runat="server" TabIndex="26"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
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
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
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
                                                        <asp:Button ID="ButtonUpdate" runat="server" Text="Update" ValidationGroup="Demographic"
                                                            OnClick="ButtonUpdate_Click" CssClass="aspButton" Width="70px" TabIndex="14" />&nbsp
                                                        <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                                                            CssClass="aspButton" Width="70px" TabIndex="15" />
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUnitType" runat="server" ErrorMessage="Required Unit Type."
                                                ControlToValidate="TextBoxUnitType" Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDemographic" runat="server"
                                                ErrorMessage="Required Demographic." ControlToValidate="TextBoxDemographic" Display="None"
                                                ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInUnit" runat="server"
                                                ErrorMessage="Required 'Beds In Units'." ControlToValidate="TextBoxBedsInUnit"
                                                Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInHospital" runat="server"
                                                ErrorMessage="Required 'Beds In Hospital'." ControlToValidate="TextBoxBedsInHospital"
                                                Display="None" ValidationGroup="Demographic">*</asp:RequiredFieldValidator>
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
                                </table>--%>
                            <%--  </asp:View>
                        </asp:MultiView>--%>
                        </div>
                        <div style="width: 90%; margin-left: 50px">
                            <div style="float: left; width: 95px">
                                <asp:Button ID="ButtonUpda" Width="70px" TabIndex="14" CssClass="aspButton" runat="server"
                                    Text="Update" OnClick="Button1_Click" ValidationGroup="Demographic" />
                            </div>
                            <div style="float: left; width: 100px">
                                <asp:Button ID="ButtonReset1" runat="server" TabIndex="15" Text="Reset" CssClass="aspButton"
                                    Width="70px" OnClick="ButtonReset1_Click" Visible="false" />
                            </div>
                        </div>
                        <h3>
                            <a style="font-weight: bold;" href="#">User List</a></h3>
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridViewDemographicDetail" runat="server" Width="430px" BackColor="#F79220"
                                            BorderColor="#F79220" BorderStyle="None" BorderWidth="1px" AllowPaging="True"
                                            AllowSorting="True" PageSize="2" CellPadding="3" CellSpacing="2" HorizontalAlign="Center"
                                            DataSourceID="ObjectDataSourceHospitalInfo">
                                            <HeaderStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" />
                                            <EmptyDataRowStyle BackColor="#A50D0C" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#FFF7E7" ForeColor="#06569D" />
                                            <FooterStyle BackColor="#A50D0C" ForeColor="White" />
                                            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#A50D0C" />
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="ObjectDataSourceHospitalInfo" runat="server" SelectMethod="GetUserInfomationByHospitalInfoID"
                                            TypeName="RMC.BussinessService.BSMultiUserHospital">
                                            <SelectParameters>
                                                <asp:QueryStringParameter DefaultValue="0" Name="hospitalInfoID" QueryStringField="HospitalInfoID"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
