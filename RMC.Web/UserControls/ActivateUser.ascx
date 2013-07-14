<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivateUser.ascx.cs"
    Inherits="RMC.Web.UserControls.ActivateUser" %>
<style type="text/css">
    .style1
    {
        height: 97px;
    }
</style>

<script type="text/javascript" language="javascript">
    jQuery(function($) {
        $("#" + '<%=TextBoxPhone.ClientID%>').mask("999-999-9999");
        $("#" + '<%=TextBoxFax.ClientID%>').mask("999-999-9999");

        //        $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
        //        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });
</script>

<%--<asp:Panel ID="PanelActivateUser" runat="server" DefaultButton="ButtonSave">--%>
<table width="99%" style="border: solid 0px black;">
    <tr>
        <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">
            <%--<h3 style="font-size: 13px;">--%>
            <u>User Profile</u>
            <%--</h3>--%>
        </th>
        <th align="right" style="padding-left: 10px; padding-top: 10px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                PostBackUrl="~/Administrator/GetUserList.aspx" TabIndex="14" />
        </th>
    </tr>
    <tr>
        <td style="height: 10px;">
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div style="width: 420px; background-color: Transparent; z-index: 0; margin-left: 50px">
                <div id="divErrorMsgInner" style="width: 418px; background-color: #E8E9EA; z-index: 10;">
                    <div style="margin-left: 90px;">
                        <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="UserRegistration"
                            runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Font-Size="11px"
                            Font-Bold="true" Style="padding-top: 1px;" />
                        <asp:Panel ID="PanelErrorMsg" runat="server" Style="padding-top: 1px;" Visible="false">
                            <ul>
                                <li>
                                    <asp:Label ID="LabelErrorMsg" ForeColor="Red" runat="server" Font-Bold="true" Font-Size="13px"></asp:Label>
                                </li>
                            </ul>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Company Name <span style="color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxCompanyName" MaxLength="50" runat="server" TabIndex="1" Width="160px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompanyName" runat="server"
                SetFocusOnError="true" ErrorMessage="Required Company Name." ControlToValidate="TextBoxCompanyName"
                Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            First Name <span style="color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxFirstName" MaxLength="50" runat="server" TabIndex="1" Width="160px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName" runat="server" ErrorMessage="Required First Name."
                SetFocusOnError="true" ControlToValidate="TextBoxFirstName" Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Last Name <span style="color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxLastName" MaxLength="50" runat="server" TabIndex="2" Width="160px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName" runat="server" ErrorMessage="Required Last Name."
                SetFocusOnError="true" ControlToValidate="TextBoxLastName" Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Phone <span style="color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxPhone" MaxLength="15" runat="server" TabIndex="3" Width="160px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server" ErrorMessage="Required Phone Number."
                SetFocusOnError="true" ControlToValidate="TextBoxPhone" Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Fax&nbsp;&nbsp;
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxFax" MaxLength="15" runat="server" TabIndex="4" Width="160px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Email <span style="color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxEmail" runat="server" AutoCompleteType="Email" TabIndex="5"
                Width="160px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="Required Email Address."
                SetFocusOnError="true" ControlToValidate="TextBoxEmail" Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server"
                SetFocusOnError="true" ErrorMessage="Please Enter Valid Email Address." ControlToValidate="TextBoxEmail"
                Display="None" ValidationGroup="UserRegistration" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Password <span style="color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxPassword" runat="server" MaxLength="50" TabIndex="6" Width="160px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword"
                SetFocusOnError="true" Display="None" ErrorMessage="Required Password." ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server"
                SetFocusOnError="true" ControlToValidate="TextBoxPassword" Display="None" ErrorMessage="Must use at least one digit and 8 to 50 Characters in Password."
                ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,50})$" ValidationGroup="UserRegistration">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Confirm - Password <span style="color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxConfirmPassword" runat="server" MaxLength="50" TabIndex="7"
                Width="160px"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidatorConfirmPassword" runat="server" ControlToCompare="TextBoxConfirmPassword"
                SetFocusOnError="true" ControlToValidate="TextBoxPassword" Display="None" ErrorMessage="Password and Confirm - Password do not match."
                ValidationGroup="UserRegistration">*</asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Security Question <span style="color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxSecurityQuestion" runat="server" MaxLength="500" TabIndex="8"
                Width="160px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecurityQuestion" runat="server"
                SetFocusOnError="true" ErrorMessage="Required Security Question." ControlToValidate="TextBoxSecurityQuestion"
                Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Security Answer <span style="color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxSecurityAnswer" runat="server" MaxLength="500" TabIndex="9"
                Width="160px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecurityAnswer" runat="server"
                SetFocusOnError="true" ErrorMessage="Required Security Answer." ControlToValidate="TextBoxSecurityAnswer"
                Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
        </td>
    </tr>
     <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Activate User
        </td>
        <td align="left">
            <asp:CheckBox ID="CheckBoxActivateUsers" ForeColor="#06569d" Font-Size="11px" runat="server" TabIndex=10 />
        </td>
    </tr>
    <%--  <tr>
            <td align="right" style="color: #06569d; font-weight: bold;">
                Activate User <span style="color: Red;">*</span>
            </td>
            <td align="left">
                <asp:CheckBox ID="CheckBoxActivate" Visible="false" runat="server" />
            </td>
        </tr>--%>
    <tr>
        <td style="padding-top: 5px;" width="190px">
        </td>
        <td align="left" style="padding-top: 5px;">
            <asp:Button ID="ButtonSave" runat="server" Text="Update" ValidationGroup="UserRegistration"
                OnClick="ButtonSave_Click" CssClass="aspButton" Width="70px" TabIndex="11" />
            &nbsp;
            <asp:Button ID="ButtonReset" runat="server" Visible="false" Text="Reset" OnClick="ButtonReset_Click"
                CssClass="aspButton" Width="70px" TabIndex="12" />&nbsp
            <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="aspButton" Width="70px"
                TabIndex="13" OnClick="ButtonDelete_Click" />
        </td>
    </tr>
    <tr>
        <td style="height: 10px;">
        </td>
    </tr>
    <tr>
        <%-- <td width="190px">
                &nbsp;
            </td>--%>
        <td colspan="2" style="padding-left: 50px">
            <div style="height: 150px; overflow: auto; border: 1px double #06569d; width: 430px;
                text-align: center;" id="divDataListRequestHospitalUnitAccess" runat="server">
                <asp:GridView ID="GridViewList" runat="server" AutoGenerateColumns="false" DataKeyField="MultiUserDemographicID"
                    CssClass="GridViewStyle" CellPadding="3" EmptyDataText="No Record to display."
                    GridLines="None" HorizontalAlign="Center" TabIndex="14">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Hospital Name">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelHospitalName" Text='<%#Eval("HospitalName") %>' runat="server"></asp:Label>
                                            <asp:Label ID="LabelMultiUserDemographicID" runat="server" Text='<%#Eval("MultiUserDemographicID") %>'
                                                Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelCity" Text='<%#Eval("HospitalCity") %>' runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelState" Text='<%#Eval("HospitalState") %>' runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Name">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelUnitName" Text='<%#Eval("UnitName") %>' runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Permission">
                            <%--<ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelPermission" Text='<%#Eval("Permission") %>' runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>--%>
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownListPermission" Width="90px" CssClass="aspDropDownList"
                                    AutoPostBack="true" runat="server" DataSourceID="LinqDataSourcePermission" DataTextField="Permission1"
                                    DataValueField="PermissionID" SelectedValue='<%#Bind("PermissionID") %>' OnSelectedIndexChanged="DropDownListPermission_SelectedIndexChanged" TabIndex="13">
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSourcePermission" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                    Select="new (PermissionID, Permission1)" TableName="Permissions" Where="PermissionID != @PermissionID">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="PermissionID" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImageButtonDelete" runat="server" OnClientClick="return confirm('Are you sure you wish to delete this entry?');"
                                                ImageUrl="~/Images/delete.png" OnClick="ImageButtonDelete_Click" TabIndex="13" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListCategoryAssignment" ForeColor="#06569D" runat="server"
                                    Font-Size="11px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                </asp:GridView>
            </div>
        </td>
    </tr>
</table>
<%--</asp:Panel>
--%>