<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="EditHospitalInfomation.aspx.cs" Inherits="RMC.Web.Administrator.EditHospitalInfomation"
    Title="RMC: Edit Hospital Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            background-color: Silver;
            visibility: hidden;
        }
    </style>

    <script type="text/javascript" language="javascript">
        jQuery(function($) {

            $("#TextBoxCNOPhone").mask("999-999-9999");
        });
    </script>

    <script type="text/JavaScript" language="JavaScript">

        function pageLoad() {

            var manager = Sys.WebForms.PageRequestManager.getInstance();

            manager.add_endRequest(endRequest);

            manager.add_beginRequest(onBeginRequest);

        }

        function onBeginRequest(sender, args) {

            var divParent = $get('ParentDiv');
            var divImage = document.getElementById('IMGDIV');

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

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="PanelEditHospitalInformation" runat="server" DefaultButton="ButtonUpdate">
        <center>
            <div style="width: 630px;">
                <div id="divOutterHospitalInfo" style="background-color: #06569d; z-index: 0;">
                    <div id="divHospitalInfo" style="background-color: White; z-index: 10;">
                        <div style="padding-top: 10px; padding-bottom: 10px;">
                            <center>
                                <table>
                                    <tr>
                                        <th>
                                            <h3 style="font-size: 13px;">
                                                <span>Hospital Infomation</span>
                                            </h3>
                                        </th>
                                    </tr>
                                    <%-- <tr>
                                    <td align="left" style="color: Red; padding-left: 30px; font-weight:bold;" colspan="2">
                                        * indicates a mandatory fields.
                                    </td>
                                </tr>--%>
                                    <tr>
                                        <td style="height: 2px;">
                                            <asp:Label ID="LabelErrorMsg" runat="server" Visible="False" Font-Size="12px"></asp:Label>
                                            <asp:ValidationSummary ID="ValidationSummaryHospitalInfo" runat="server" DisplayMode="List"
                                                Font-Size="12px" ValidationGroup="HospitalInfo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="630px">
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
                                                        C. N.O. Name <span style="color: Red;">*</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxChiefNursingOfficer" MaxLength="100" runat="server" Width="181px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorChiefNursingOfficer" runat="server"
                                                            ControlToValidate="TextBoxChiefNursingOfficer" Display="None" ErrorMessage="Required Chief Nursing Officer Name."
                                                            ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorChiefNursingOfficer"
                                                            runat="server" ControlToValidate="TextBoxChiefNursingOfficer" Display="None"
                                                            ErrorMessage="Allow only alphabet characters in 'Chief Nursing Officer Name'."
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
                                                            ControlToValidate="TextBoxCNOPhone" Display="None" ErrorMessage="Allow only Numeric Characher in Chief 'Nursing Officer Phone'."
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
                                                            Width="182px" ForeColor="#06569d"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddress" runat="server"
                                                            Display="None" ErrorMessage="Allow only alphanumeric characters in Address."
                                                            ValidationExpression="^[a-zA-Z0-9''-'\s]{1,500}$" ValidationGroup="HospitalInfo"
                                                            ControlToValidate="TextBoxAddress">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        City&nbsp&nbsp
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxCity" MaxLength="50" runat="server" Width="181px"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorCity" runat="server"
                                                            ControlToValidate="TextBoxCity" Display="None" ErrorMessage="Allow only alphabetical characters in City."
                                                            ValidationExpression="^[a-zA-Z''-'\s]{1,50}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="color: #06569d; font-weight: bold;">
                                                        Country&nbsp&nbsp
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="DropDownListCountry" runat="server" Width="186px" AppendDataBoundItems="True"
                                                            AutoPostBack="True" DataSourceID="ObjectDataSourceCountry" DataTextField="CountryName"
                                                            DataValueField="CountryID" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged"
                                                            ForeColor="#06569d">
                                                            <asp:ListItem Selected="True" Value="0">Select Country</asp:ListItem>
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
                                                        <asp:UpdatePanel ID="UpdatePanelState" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="DropDownListState" runat="server" Width="186px" AppendDataBoundItems="True"
                                                                    DataSourceID="ObjectDataSourceState" DataTextField="StateName" DataValueField="StateID"
                                                                    ForeColor="#06569d">
                                                                    <asp:ListItem Selected="True" Value="0">Select State</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="DropDownListCountry" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                        <asp:ObjectDataSource ID="ObjectDataSourceState" runat="server" SelectMethod="GetAllStateNamesByCountryID"
                                                            TypeName="RMC.BussinessService.BSCommon" OnSelecting="ObjectDataSourceState_Selecting">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="DropDownListCountry" DefaultValue="0" Name="CountryID"
                                                                    PropertyName="SelectedValue" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                        <div id="ParentDiv" class="Background">
                                                            <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 57%;
                                                                top: 57%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
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
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidatorZip" runat="server"
                                                        Display="None" ErrorMessage="Enter Valid Zip Code." ValidationExpression="^(\d{5}-\d{4}|\d{5}|\d{9})$|^([a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$"
                                                        ValidationGroup="HospitalInfo" ControlToValidate="TextBoxZip">*</asp:RegularExpressionValidator>--%>
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
                                                                    <asp:Button ID="ButtonReset" runat="server" Visible="false" Text="Reset" OnClick="ButtonReset_Click"
                                                                        CssClass="aspButton" Width="55px" />
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Button ID="ButtonBack" runat="server" Text="Cancel" OnClick="ButtonBack_Click"
                                                                        CssClass="aspButton" Width="55px" ToolTip="Back To Hospital List Page." />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
        </center>
    </asp:Panel>
</asp:Content>
