<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HospitalEditRegistration.ascx.cs"
    Inherits="RMC.Web.UserControls.HospitalEditRegistration" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc11" %>

<script type="text/javascript" language="javascript">
    jQuery(function($) {

        $(document.getElementById('<%=TextBoxBedsInHospital.ClientID%>')).numeric();
        $(document.getElementById('<%=TextBoxCNOPhone.ClientID %>')).mask("999-999-9999");
        //$("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });
</script>

<style type="text/css">
    .Background
    {
        position: fixed;
        left: 0;
        top: 0;
        z-index: 10;
        width: 100%;
        height: 100%;
        filter: alpha(opacity=60);
        opacity: 0.6;
        background-color: Black;
        visibility: hidden;
    }
</style>

<script type="text/JavaScript" language="JavaScript">

    function pageLoad() {

        var manager = Sys.WebForms.PageRequestManager.getInstance();

        manager.add_endRequest(endRequest);

        manager.add_beginRequest(onBeginRequest);

    }

    function onBeginRequest(sender, args) {

        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = 'Background';
        divParent.style.visibility = 'visible';
        divImage.style.visibility = 'visible';

    }

    function endRequest(sender, args) {

        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = '';
        divParent.style.visibility = 'hidden';
        divImage.style.visibility = 'hidden';

    }

    function confirmDelete() {

        var ans = confirm("Are you sure want to remove this Hospital Inforamtion?");

        if (ans) {
            var otherAns = confirm("This action will permanently delete this Hospital Inforamtion and all related links.Are you sure you want to do this?");
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

<asp:Panel ID="PanelHospitalEditRegistration" runat="server" DefaultButton="ButtonSave"
    Width="100%">
    <table width="100%;" style="text-align: center;">
        <tr>
            <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">
                <%--<h3 style="font-size: 13px;">
                        <table width="100%">
                            <tr>
                                <td align="left">--%>
                <%--</td>
                                <td align="center">--%>
                <u>Hospital Information</u>
                <%--</td>
                            </tr>
                        </table>
                    </h3>--%>
            </th>
            <th align="right" style="padding-left: 10px; padding-top: 10px;">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    TabIndex="21" OnClick="ImageButtonBack_Click" />
            </th>
        </tr>
        <tr>
            <td style="height: 10px;">
            </td>
        </tr>
        <%--<tr>
            <td align="left" style="color: Red; padding-left: 150px; font-weight: bold;" colspan="2">
                * indicates a mandatory fields
            </td>
        </tr>--%>
        <tr>
            <td align="center" colspan="2">
                <table width="100%">
                    <tr>
                        <td style="height: 2px;">
                            <div style="float: left; padding-left: 100px;">
                                <div style="width: 320px; float: left; background-color: Transparent; z-index: 0;">
                                    <div id="divErrorMsgInner" style="width: 318px; float: left; background-color: #E8E9EA;
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
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                            
                             <tr>
                                    <td align="right" style="padding-top: 5px;">
                                    </td>
                                    <td align="left" style="padding-top: 5px;" colspan="2">
                                        <table>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    <asp:Button ID="ButtonSave" runat="server" Text="Update" ValidationGroup="HospitalInfo"
                                                        OnClick="ButtonSave_Click" CssClass="aspButton" Width="55px" TabIndex="18" />&nbsp
                                                    <%--<asp:Button ID="ButtonSaveDisable" runat="server" Text="Update" OnClientClick="return false;"
                                                            Visible="false" CssClass="aspButtonDisable" Width="55px" />--%>
                                                    <div id="divSaveDisable" visible="false" class="aspButtonDisable" style="width: 65px;
                                                        height: 25px; text-align: center; cursor: pointer;" runat="server">
                                                        <asp:Label ID="LabelSaveDisable" runat="server" Text="Update"></asp:Label>
                                                    </div>
                                                    <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                                                        CssClass="aspButton" Width="55px" Visible="false" TabIndex="19" />&nbsp
                                                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="aspButton" Width="55px"
                                                        OnClick="ButtonDelete_Click" Visible="false" OnClientClick="return confirmDelete()"
                                                        TabIndex="20" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trHospitalIndex" runat="server">
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        Hospital Index <span style="color: Red;">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="aspTextBox" ID="TextBoxHospitalIndex" MaxLength="200" runat="server"
                                            TabIndex="1" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalIndex" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxHospitalIndex" Display="None"
                                            ErrorMessage="Required Hospital Index." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorHospitalIndex" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxHospitalIndex" Display="None"
                                            ErrorMessage="Allow only Numeric characters in a Hospital Index." ValidationExpression="^[0-9''-'\s]{1,20}$"
                                            ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right">
                                    </td>
                                    <td align="left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        Hospital Name <span style="color: Red;">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="aspTextBox" ID="TextBoxHospitalName" MaxLength="200" runat="server"
                                            TabIndex="1" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalInfo" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxHospitalName" Display="None"
                                            ErrorMessage="Required Hospital Name." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorHospitalName" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxHospitalName" Display="None"
                                            ErrorMessage="Allow only alphanumeric characters in Hospital Name." ValidationExpression="^[a-zA-Z0-9''-'\s]{1,200}$"
                                            ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right">
                                        <asp:HyperLink ID="HyperLinkOwnerName" runat="server" Font-Bold="True" Font-Size="11px"
                                            NavigateUrl="~/Administrator/UserRegistration.aspx" Visible="false">Owner Name</asp:HyperLink>
                                        <span id="spanOwner" style="color: Red;" runat="server"></span>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListOwner" ForeColor="#06569D"
                                            runat="server" AppendDataBoundItems="True" DataSourceID="LinqDataSourceOwner"
                                            DataTextField="Name" DataValueField="UserID" TabIndex="2" Visible="false">
                                            <asp:ListItem Value="0">Select Owner Name</asp:ListItem>
                                        </asp:DropDownList>
                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidatorOwner" runat="server" ControlToValidate="DropDownListOwner"
                                            SetFocusOnError="true" Display="None" ErrorMessage="Must Select Owner Name."
                                            InitialValue="0" ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <%--<asp:LinqDataSource ID="LinqDataSourceOwner" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                            Select="new (UserID, FirstName + ' ' + LastName + '(' + Email + ')' As Name)"
                                            TableName="UserInfos" Where="Email != @Email">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="SuperAdmin" Name="Email" Type="String" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>--%>
                                        <asp:LinqDataSource ID="LinqDataSourceOwner" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                            Select="new (UserID, FirstName + ' ' + LastName + '(' + Email + ')' As Name)"
                                            TableName="UserInfos" Where="Email != @Email &amp;&amp; IsDeleted == @IsDeleted"
                                            OrderBy="FirstName">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="SuperAdmin" Name="Email" Type="String" />
                                                <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        C.N.O. First Name <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="aspTextBox" ID="TextBoxCNOFirstName" MaxLength="50" runat="server"
                                            TabIndex="3" AutoCompleteType="Disabled"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorCNOFirstName" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxCNOFirstName" Display="None"
                                            ErrorMessage="Required Chief Nursing Officer First Name." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorCNOFirstName" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxCNOFirstName" Display="None"
                                            ErrorMessage="Allow only alphabet characters in 'Chief Nursing Officer First Name'."
                                            ValidationExpression="^[a-zA-Z''-'\s]{1,100}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        C.N.O. Last Name <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="aspTextBox" ID="TextBoxCNOLastName" MaxLength="50" runat="server"
                                            TabIndex="4" AutoCompleteType="Disabled"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorCNOLastName" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxCNOLastName" Display="None"
                                            ErrorMessage="Required Chief Nursing Officer Last Name." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorCNOLastName" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxCNOLastName" Display="None"
                                            ErrorMessage="Allow only alphabet characters in 'Chief Nursing Officer Last Name'."
                                            ValidationExpression="^[a-zA-Z''-'\s]{1,100}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        C.N.O. Phone <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="aspTextBox" ID="TextBoxCNOPhone" MaxLength="20" runat="server"
                                            TabIndex="5" AutoCompleteType="Disabled"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorCNOPhone" runat="server" ControlToValidate="TextBoxCNOPhone"
                                            SetFocusOnError="true" Display="None" ErrorMessage="Required Chief Nursing Officer Phone."
                                            ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorCNOPhone" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxCNOPhone" Display="None" ErrorMessage="Invalid Phone Number of Chief Nursing Officer."
                                            ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$"
                                            ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        C.N.O. Email <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="aspTextBox" ID="TextBoxCNOEmail" MaxLength="50" runat="server"
                                            TabIndex="6" AutoCompleteType="Disabled"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="TextBoxCNOEmail"
                                            SetFocusOnError="true" Display="None" ErrorMessage="Required Chief Nursing Officer Email."
                                            ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorCNOEmail" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxCNOEmail" Display="None" ErrorMessage="Invalid Email of Chief Nursing Officer."
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        Beds In Hospital <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="aspTextBox" ID="TextBoxBedsInHospital" MaxLength="8" runat="server"
                                            TabIndex="7" AutoCompleteType="Disabled"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBedsInHospital" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxBedsInHospital" Display="None"
                                            ErrorMessage="Required Beds In Hospital." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorBedsInHospital" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxBedsInHospital" Display="None"
                                            ErrorMessage="Only Numeric allow in 'Beds In Hospital'." ValidationExpression="^\d+$"
                                            ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        Ownership Type <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListOwnershipType" ForeColor="#06569D"
                                            runat="server" AppendDataBoundItems="True" DataSourceID="LinqDataSourceOwnerShipType"
                                            DataTextField="OwnerShipTypeName" DataValueField="OwnerShipTypeID" TabIndex="9">
                                            <asp:ListItem Value="0">Select Ownership Type</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorOwnerShipType" runat="server"
                                            SetFocusOnError="true" ControlToValidate="DropDownListOwnershipType" Display="None"
                                            ErrorMessage="Must Select Ownership Type." InitialValue="0" ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <asp:LinqDataSource ID="LinqDataSourceOwnerShipType" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                            OrderBy="OwnerShipTypeName" TableName="OwnerShipTypes">
                                        </asp:LinqDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                    <td align="right">
                                        <p>
                                            <asp:LinkButton ID="LinkButtonOwnershipType" Font-Size="10px" runat="server" TabIndex="8">Add a New Ownership Type</asp:LinkButton>
                                            <%--<asp:HyperLink ID="HyperLinkOwnershipType" runat="server" Font-Bold="True" ForeColor="#22B7FF"
                                                    NavigateUrl="~/Users/RequestForTypes.aspx?Type=Ownership Type" Target="_blank">Add a New Ownership Type</asp:HyperLink>--%>
                                            <asp:LinkButton ID="LinkButtonOwnershipTypeForUsers" runat="server" TabIndex="8"
                                                Font-Size="10px" ForeColor="#22B7FF" OnClientClick="windowPopup('../Users/RequestForTypes.aspx?Type=Ownership Type', 'Ownership Type'); return false;">Add a New Ownership Type</asp:LinkButton>
                                            &nbsp;&nbsp;
                                        </p>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                        <br />
                                        Hospital Type <span style="color: Red;">&nbsp;&nbsp;</span>
                                        <br />
                                        <p>
                                            <asp:LinkButton ID="LinkButtonHospitalType" Font-Size="10px" PostBackUrl="~/Administrator/HospitalType.aspx"
                                                runat="server" TabIndex="10">Add a New Hospital Type</asp:LinkButton>
                                            <%--<asp:HyperLink ID="HyperLinkHospitalType" runat="server" Font-Bold="True" ForeColor="#22B7FF"
                                                    NavigateUrl="~/Users/RequestForTypes.aspx?Type=Hospital Type" Target="_blank">Add a New Hospital Type</asp:HyperLink>--%>
                                            <asp:LinkButton ID="LinkButtonHospitalTypeForUser" runat="server" TabIndex="10" Font-Size="10px"
                                                ForeColor="#22B7FF" OnClientClick="windowPopup('../Users/RequestForTypes.aspx?Type=Hospital Type', 'Hospital Type'); return false;">Add a New Hospital Type</asp:LinkButton>
                                            &nbsp&nbsp
                                        </p>
                                    </td>
                                    <td align="left">
                                        <asp:ListBox CssClass="aspListBox" ID="ListBoxHospitalType" runat="server" Height="88px"
                                            ForeColor="#06569D" SelectionMode="Multiple" DataSourceID="LinqDataSourceHospitalType"
                                            DataTextField="HospitalTypeName" DataValueField="HospitalTypeID" TabIndex="11">
                                        </asp:ListBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalType" runat="server"
                                            SetFocusOnError="true" ControlToValidate="ListBoxHospitalType" Display="None"
                                            ErrorMessage="Select Hospital Type." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <asp:LinqDataSource ID="LinqDataSourceHospitalType" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                                            OrderBy="HospitalTypeName" TableName="HospitalTypes">
                                        </asp:LinqDataSource>
                                    </td>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                        <br />
                                        Address <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="aspTextBox" ID="TextBoxAddress" MaxLength="500" Height="75px"
                                            runat="server" TextMode="MultiLine" ForeColor="#06569d" TabIndex="12"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress" runat="server" ControlToValidate="TextBoxAddress"
                                            SetFocusOnError="true" Display="None" ErrorMessage="Required Address." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidatorAddress" runat="server"
                                                SetFocusOnError="true" Display="None" ErrorMessage="Allow only alphanumeric characters in Address."
                                                ValidationExpression="^[a-zA-Z0-9''-'\s]{1,500}$" ValidationGroup="HospitalInfo"
                                                ControlToValidate="TextBoxAddress">*</asp:RegularExpressionValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        City <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="aspTextBox" ID="TextBoxCity" MaxLength="50" runat="server"
                                            TabIndex="13" AutoCompleteType="Disabled"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorCity" runat="server" ControlToValidate="TextBoxCity"
                                            SetFocusOnError="true" Display="None" ErrorMessage="Required City Name." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorCity" runat="server"
                                            SetFocusOnError="true" ControlToValidate="TextBoxCity" Display="None" ErrorMessage="Allow only alphabetical characters in City."
                                            ValidationExpression="^[a-zA-Z''-'\s]{1,50}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        Approved Users <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td rowspan="4" valign="top" align="left">
                                        <%--<asp:ListBox CssClass="aspListBox" ID="ListBoxApprovedUsers" SelectionMode="Single"
                                            ForeColor="#06569d" runat="server" Height="104px" Rows="6" TabIndex="17"></asp:ListBox>--%>
                                        <div style="height: 96px; overflow: auto; border: 1px double #06569d; width: 135px;">
                                            <asp:DataList ID="DataListApprovedUsers" runat="server" DataSourceID="ObjectDataSourceHospitalInfo"
                                                DataKeyField="UserID" CssClass="aspDataList" Width="130px" TabIndex="6">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBoxApproval" runat="server" AutoPostBack="True" Checked='<%#Eval("IsApproved") %>'
                                                                    ForeColor="#06569D" OnCheckedChanged="CheckBoxApproval_CheckedChanged" />
                                                            </td>
                                                            <td>
                                                             &nbsp;
                                                            </td>
                                                            <td>
                                                                <%--<asp:LinkButton ID="LinkButtonApprovedUser" runat="server" CommandArgument='<%#Eval("UserID") %>'
                                                                    TabIndex="6"><%#Eval("UserName") %></asp:LinkButton>--%>
                                                                <asp:LinkButton ID="LinkButtonForApprovedUser" runat="server" OnClientClick="return false;"
                                                                    TabIndex="6"><%#Eval("UserName") %></asp:LinkButton>
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
                                        State <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="UpdatePanelState" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListState" runat="server"
                                                    AppendDataBoundItems="True" DataSourceID="ObjectDataSourceState" DataTextField="StateName"
                                                    DataValueField="StateID" ForeColor="#06569d" TabIndex="14">
                                                    <asp:ListItem Selected="True" Value="0">Select State</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorState" runat="server" ControlToValidate="DropDownListState"
                                                    SetFocusOnError="true" InitialValue="0" Display="None" ErrorMessage="Must Select State."
                                                    ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
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
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        Country <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList CssClass="aspDropDownList" ID="DropDownListCountry" runat="server"
                                            AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="ObjectDataSourceCountry"
                                            DataTextField="CountryName" ForeColor="#06569d" DataValueField="CountryID" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged"
                                            TabIndex="15">
                                            <asp:ListItem Selected="True" Value="0">Select Country</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorCountry" runat="server" ControlToValidate="DropDownListCountry"
                                            SetFocusOnError="true" InitialValue="0" Display="None" ErrorMessage="Must Select Country."
                                            ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
                                        <asp:ObjectDataSource ID="ObjectDataSourceCountry" runat="server" SelectMethod="GetAllCountries"
                                            TypeName="RMC.BussinessService.BSCommon"></asp:ObjectDataSource>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                        Zip <span style="color: Red;">&nbsp;&nbsp;</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="aspTextBox" ID="TextBoxZip" MaxLength="15" runat="server"
                                            TabIndex="16" AutoCompleteType="Disabled"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorZip" runat="server" ControlToValidate="TextBoxZip"
                                            SetFocusOnError="true" Display="None" ErrorMessage="Must Enter Zip Code." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>--%>
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
                                        <div style="float: left;">
                                            <asp:LinkButton ID="LinkButtonApproveUser" runat="server" Font-Bold="True" OnClick="LinkButtonApproveUser_Click"
                                                Font-Size="10px">Approve a User</asp:LinkButton>
                                        </div>
                                        <div style="float: left;">
                                            <asp:LinkButton ID="LinkButtonRegisterNewUser" runat="server" Font-Bold="True" OnClick="LinkButtonRegisterNewUser_Click"
                                                Font-Size="10px">Register a New User</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td align="right" style="padding-top: 5px;">
                                    </td>
                                    <td align="left" style="padding-top: 5px;" colspan="2">
                                        <table>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    <asp:Button ID="ButtonSave" runat="server" Text="Update" ValidationGroup="HospitalInfo"
                                                        OnClick="ButtonSave_Click" CssClass="aspButton" Width="55px" TabIndex="18" />&nbsp
                                                    <%--<asp:Button ID="ButtonSaveDisable" runat="server" Text="Update" OnClientClick="return false;"
                                                            Visible="false" CssClass="aspButtonDisable" Width="55px" />--%>
                                                    <%--<div id="divSaveDisable" visible="false" class="aspButtonDisable" style="width: 65px;
                                                        height: 25px; text-align: center; cursor: pointer;" runat="server">
                                                        <asp:Label ID="LabelSaveDisable" runat="server" Text="Update"></asp:Label>
                                                    </div>
                                                    <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                                                        CssClass="aspButton" Width="55px" Visible="false" TabIndex="19" />&nbsp
                                                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="aspButton" Width="55px"
                                                        OnClick="ButtonDelete_Click" Visible="false" OnClientClick="return confirmDelete()"
                                                        TabIndex="20" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjectDataSourceHospitalInfo" runat="server" SelectMethod="GetAllMembersByHospitalID"
        TypeName="RMC.BussinessService.BSTreeView">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="hospitalInfoId" QueryStringField="HospitalInfoId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Panel>
