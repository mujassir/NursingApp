<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HospitalInfo.ascx.cs"
    Inherits="RMC.Web.HospitalInfo" %>

<script type="text/javascript" language="javascript">
    jQuery(function($) {
        $("#" + '<%=TextBoxCNOPhone.ClientID%>').mask("999-999-9999");
        //$("#UserProfile1_TextBoxPhone").mask("999-999-9999");
        //$("#TextBoxFax").mask("999-999-9999");
    });
</script>

<asp:ScriptManagerProxy ID="ScriptManagerHospitalInfo" runat="server">
</asp:ScriptManagerProxy>
<asp:Panel ID="PanelHospitalInfo" runat="server" DefaultButton="ButtonSave">
    <div style="width: 100%;">
        <table width="100%" style="text-align: center;">
            <tr>
                <th>
                    <h3 style="font-size: 13px;">
                        <table width="100%" style="text-align: center;">
                            <tr>
                                <td align="left" width="10%">
                                    <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                                        OnClick="ImageButtonBack_Click" />
                                </td>
                                <td>
                                    <span>Hospital Infomation</span>
                                </td>
                            </tr>
                        </table>
                    </h3>
                </th>
            </tr>
            <%--<tr>
            <td align="left" style="color: Red; padding-left: 150px; font-weight: bold;" colspan="2">
                * indicates a mandatory fields
            </td>
        </tr>--%>
            <tr>
                <td align="center">
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
                                <table style="width: 417px">
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                            Hospital Name <span style="color: Red;">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxHospitalName" MaxLength="200" runat="server" Width="181px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalInfo" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxHospitalName" Display="None"
                                                ErrorMessage="Required Hospital Name." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorHospitalName" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxHospitalName" Display="None"
                                                ErrorMessage="Allow only alphanumeric characters in Hospital Name." ValidationExpression="^[a-zA-Z0-9''-'\s]{1,200}$"
                                                ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                            Admin <span style="color: Red;">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListAdmin" CssClass="dropdown" runat="server">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="RequiredFieldValidatorAdmin" runat="server" ControlToValidate="DropDownListAdmin"
                                                SetFocusOnError="true" Operator="NotEqual" ValueToCompare="0" Display="None"
                                                ErrorMessage="Select Admin." ValidationGroup="HospitalInfo">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                            C.N.O. First Name <span style="color: Red;">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxCNOFirstName" MaxLength="50" runat="server" Width="181px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCNOFirstName" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxCNOFirstName" Display="None"
                                                ErrorMessage="Required Chief Nursing Officer First Name." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorCNOFirstName" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxCNOFirstName" Display="None"
                                                ErrorMessage="Allow only alphabet characters in 'Chief Nursing Officer First Name'."
                                                ValidationExpression="^[a-zA-Z''-'\s]{1,100}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                            C.N.O. Last Name &nbsp&nbsp
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxCNOLastName" MaxLength="50" runat="server" Width="181px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorCNOLastName" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxCNOLastName" Display="None"
                                                ErrorMessage="Allow only alphabet characters in 'Chief Nursing Officer Last Name'."
                                                ValidationExpression="^[a-zA-Z''-'\s]{1,100}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                            C.N.O. Phone <span style="color: Red;">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxCNOPhone" MaxLength="20" runat="server" Width="181px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCNOPhone" runat="server" ControlToValidate="TextBoxCNOPhone"
                                                SetFocusOnError="true" Display="None" ErrorMessage="Required Chief Nursing Officer Phone."
                                                ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorCNOPhone" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxCNOPhone" Display="None" ErrorMessage="Invalid Phone Number of Chief Nursing Officer."
                                                ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$"
                                                ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                            Address&nbsp&nbsp
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxAddress" MaxLength="500" runat="server" Height="52px" TextMode="MultiLine"
                                                ForeColor="#06569d" Width="182px"></asp:TextBox>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidatorAddress" runat="server"
                                                SetFocusOnError="true" Display="None" ErrorMessage="Allow only alphanumeric characters in Address."
                                                ValidationExpression="^[a-zA-Z0-9''-'\s]{1,500}$" ValidationGroup="HospitalInfo"
                                                ControlToValidate="TextBoxAddress">*</asp:RegularExpressionValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                            City&nbsp&nbsp
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxCity" MaxLength="50" runat="server" Width="181px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorCity" runat="server"
                                                SetFocusOnError="true" ControlToValidate="TextBoxCity" Display="None" ErrorMessage="Allow only alphabetical characters in City."
                                                ValidationExpression="^[a-zA-Z''-'\s]{1,50}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                            Country&nbsp&nbsp
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListCountry" CssClass="dropdown" runat="server" Width="186px"
                                                AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="ObjectDataSourceCountry"
                                                DataTextField="CountryName" ForeColor="#06569d" DataValueField="CountryID" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjectDataSourceCountry" runat="server" SelectMethod="GetAllCountries"
                                                TypeName="RMC.BussinessService.BSCommon"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                            State&nbsp&nbsp
                                        </td>
                                        <td align="left">
                                            <asp:UpdatePanel ID="UpdatePanelState" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownListState" runat="server" Width="186px" AppendDataBoundItems="True"
                                                        DataSourceID="ObjectDataSourceState" CssClass="dropdown" DataTextField="StateName"
                                                        DataValueField="StateID" ForeColor="#06569d">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:ObjectDataSource ID="ObjectDataSourceState" runat="server" SelectMethod="GetAllStateNamesByCountryID"
                                                        TypeName="RMC.BussinessService.BSCommon">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="DropDownListCountry" DefaultValue="0" Name="CountryID"
                                                                PropertyName="SelectedValue" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="DropDownListCountry" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%-- <div id="ParentDiv" class="Background">
                                            <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 55%;
                                                top: 55%; visibility: visible; vertical-align: middle; border-style: none; border-color: black;
                                                z-index: 40;">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
                                                    AlternateText="Loading"></asp:Image>
                                            </div>
                                        </div>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                            Zip&nbsp&nbsp
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxZip" MaxLength="15" runat="server" Width="181px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-top: 5px;">
                                        </td>
                                        <td align="left" style="padding-top: 5px;">
                                            <table width="190px">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="HospitalInfo"
                                                            OnClick="ButtonSave_Click" CssClass="aspButton" Width="55px" />
                                                        <asp:Button ID="ButtonReset" runat="server" Visible="false" Text="Reset" OnClick="ButtonReset_Click"
                                                            CssClass="aspButton" Width="55px" />
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
