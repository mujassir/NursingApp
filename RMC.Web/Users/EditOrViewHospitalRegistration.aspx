<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditOrViewHospitalRegistration.aspx.cs" Inherits="RMC.Web.Users.EditOrViewHospitalRegistration"
    Title="RMC :: Hospital Infomation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        jQuery(function($) {

            $(document.getElementById('<%=TextBoxCNOPhone.ClientID %>')).mask("999-999-9999");
            $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
            $('#accordion').accordion();
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
            background-color: Transparent;
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

    </script>

    <asp:ScriptManager ID="ScriptManagerEditOrViewHospitalRegistration" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="PanelEditOrViewHospitalRegistration" runat="server" DefaultButton="ButtonUpdate">
        <table width="100%" style="text-align: center;">
            <tr>
                <th align="left">
                    <h3 style="font-size: 13px;">
                        <asp:ImageButton ID="ImageButton1" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                            PostBackUrl="~/Users/DataManagement.aspx" /><span style="padding-left: 290px;">Hospital
                                Detail</span>
                    </h3>
                </th>
            </tr>
            <tr>
                <td>
                    <div id="accordion" style="width: 90%;">
                        <h3>
                            <a style="font-weight: bold;" href="#">Hospital Infomation</a></h3>
                        <div>
                            <p>
                                <asp:MultiView ID="MultiViewForEditOrViewHospitalRegistration" runat="server">
                                    <asp:View ID="ViewHospitalRegistration" runat="server" OnLoad="ViewHospitalRegistration_Load">
                                        <div style="width: 100%;">
                                            <table width="100%" style="text-align: center;">
                                                <tr>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td align="center">
                                                                    <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                                                                        <div id="div1" style="width: 418px; float: left; background-color: #E8E9EA; z-index: 10;">
                                                                            <div style="padding-left: 50px; text-align: left;">
                                                                                <asp:Panel ID="PanelViewHospitalRegistrationError" runat="server" Style="padding-top: 1px;"
                                                                                    Visible="false">
                                                                                    <ul>
                                                                                        <li>
                                                                                            <asp:Label ID="LabelViewHospitalRegistrationError" runat="server" Font-Bold="true"
                                                                                                Font-Size="11px"></asp:Label>
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
                                                                    <table width="370px">
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                Hospital Name &nbsp&nbsp
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="LabelHospitalName" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                C.N.O. First Name &nbsp&nbsp
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="LabelCNOFirstName" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                C.N.O. Last Name &nbsp&nbsp
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="LabelCNOLastName" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                C.N.O. Phone &nbsp&nbsp
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="LabelCNOPhone" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                Address&nbsp&nbsp
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="LabelAddress" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                City&nbsp&nbsp
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="LabelCity" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                Country&nbsp&nbsp
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="LabelCountry" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                State&nbsp&nbsp
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="LabelState" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                Zip&nbsp&nbsp
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="LabelZip" runat="server" Text=""></asp:Label>
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
                                    </asp:View>
                                    <asp:View ID="ViewEditHospitalRegistration" runat="server" OnLoad="ViewEditHospitalRegistration_Load">
                                        <div style="width: 100%;">
                                            <table width="100%" style="text-align: center;">
                                                <%--  <tr>
                                                <td align="left" style="color: Red; padding-left: 150px; font-weight: bold;">
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
                                                                    <table width="370px">
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                Hospital Name <span style="color: Red;">*</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="TextBoxHospitalName" MaxLength="200" runat="server" Width="181px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalInfo" runat="server"
                                                                                    ControlToValidate="TextBoxHospitalName" Display="None" ErrorMessage="Required Hospital Name."
                                                                                    ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorHospitalName" runat="server"
                                                                                    ControlToValidate="TextBoxHospitalName" Display="None" ErrorMessage="Allow only alphanumeric characters in Hospital Name."
                                                                                    ValidationExpression="^[a-zA-Z0-9''-'\s]{1,200}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                C.N.O. First Name <span style="color: Red;">*</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="TextBoxCNOFirstName" MaxLength="100" runat="server" Width="181px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCNOFirstName" runat="server"
                                                                                    ControlToValidate="TextBoxCNOFirstName" Display="None" ErrorMessage="Required Chief Nursing Officer First Name."
                                                                                    ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCNOFirstName" runat="server"
                                                                                    ControlToValidate="TextBoxCNOFirstName" Display="None" ErrorMessage="Allow only alphabet characters in 'Chief Nursing Officer First Name'."
                                                                                    ValidationExpression="^[a-zA-Z''-'\s]{1,100}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                C.N.O. Last Name &nbsp&nbsp
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="TextBoxCNOLastName" MaxLength="100" runat="server" Width="181px"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCNOLastName" runat="server"
                                                                                    ControlToValidate="TextBoxCNOLastName" Display="None" ErrorMessage="Allow only alphabet characters in 'Chief Nursing Officer Last Name'."
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
                                                                                    Display="None" ErrorMessage="Required Chief Nursing Officer Phone." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCNOPhone" runat="server"
                                                                                    ControlToValidate="TextBoxCNOPhone" Display="None" ErrorMessage="Invalid Phone Number of Chief Nursing Officer."
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
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddress" runat="server"
                                                                                    Display="None" ErrorMessage="Allow only alphanumeric characters in Address."
                                                                                    ValidationExpression="^[a-zA-Z0-9''-'\s]{1,500}$" ValidationGroup="HospitalInfo"
                                                                                    ControlToValidate="TextBoxAddress">*</asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                City <span style="color: Red;">*</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="TextBoxCity" MaxLength="50" runat="server" Width="181px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCity" runat="server" ControlToValidate="TextBoxCity"
                                                                                    Display="None" ErrorMessage="Required City Name." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorCity" runat="server"
                                                                                    ControlToValidate="TextBoxCity" Display="None" ErrorMessage="Allow only alphabetical characters in City."
                                                                                    ValidationExpression="^[a-zA-Z''-'\s]{1,50}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                Country <span style="color: Red;">*</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="DropDownListCountry" runat="server" Width="186px" AppendDataBoundItems="True"
                                                                                    AutoPostBack="True" DataSourceID="ObjectDataSourceCountry" DataTextField="CountryName"
                                                                                    ForeColor="#06569d" DataValueField="CountryID" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged">
                                                                                    <asp:ListItem Selected="True" Value="0">Select Country</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCountry" runat="server" ControlToValidate="DropDownListCountry"
                                                                                    InitialValue="0" Display="None" ErrorMessage="Must Select Country." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                                                                <asp:ObjectDataSource ID="ObjectDataSourceCountry" runat="server" SelectMethod="GetAllCountries"
                                                                                    TypeName="RMC.BussinessService.BSCommon"></asp:ObjectDataSource>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                State <span style="color: Red;">*</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:UpdatePanel ID="UpdatePanelState" runat="server" UpdateMode="Conditional">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="DropDownListState" runat="server" Width="186px" AppendDataBoundItems="True"
                                                                                            DataSourceID="ObjectDataSourceState" DataTextField="StateName" DataValueField="StateID"
                                                                                            ForeColor="#06569d">
                                                                                            <asp:ListItem Selected="True" Value="0">Select State</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorState" runat="server" ControlToValidate="DropDownListState"
                                                                                            InitialValue="0" Display="None" ErrorMessage="Must Select State." ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
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
                                                                                <div id="ParentDiv" class="Background">
                                                                                    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 55%;
                                                                                        top: 55%; visibility: visible; vertical-align: middle; border-style: none; border-color: black;
                                                                                        z-index: 40;">
                                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
                                                                                            AlternateText="Loading"></asp:Image>
                                                                                    </div>
                                                                                </div>
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
                                                                                            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" ValidationGroup="HospitalInfo"
                                                                                                OnClick="ButtonUpdate_Click" CssClass="aspButton" Width="55px" />
                                                                                            <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                                                                                                CssClass="aspButton" Width="55px" Visible="false" />
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
                                    </asp:View>
                                </asp:MultiView>
                            </p>
                        </div>
                        <h3>
                            <a style="font-weight: bold;" href="#">User List</a></h3>
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridHospital" runat="server" Width="430px" BackColor="#F79220"
                                            BorderColor="#F79220" BorderStyle="None" BorderWidth="1px" AllowPaging="True"
                                            AllowSorting="True" PageSize="8" CellPadding="3" CellSpacing="2" HorizontalAlign="Center"
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
