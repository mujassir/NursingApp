<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="SendRequest.aspx.cs" Inherits="RMC.Web.Users.SendRequest" Title="RMC :: Request Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        jQuery(function($) {

            $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
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

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="PanelSendRequest" runat="server" DefaultButton="ButtonSendRequest">
        <table width="100%" style="text-align: center;">
            <tr>
                <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                    <%-- <h3 style="font-size: 13px;">--%>
                    <%--<span style="padding-left: 250px;">--%><u>Request Form</u><%--</span>--%><%--</h3>--%></th>
                <th align="right" style="padding-left:10px; padding-top:10px;">
                    <asp:ImageButton ID="ImageButtonBack" runat="server" ImageUrl="~/Images/back.gif"
                        ToolTip="Back" TabIndex="4" Visible="false"/>
                </th>
            </tr>
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
            <%--<tr>
            <td align="left" style="color: Red; padding-left: 70px; font-weight: bold;" colspan="2">
                * indicates a mandatory fields.
            </td>
        </tr>--%>
            <tr>
                <td style="padding-left: 70px;" colspan="2">
                    <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                        <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
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
                <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;">
                    Hospital <span style="color: Red;">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropDownListHospital" runat="server" AppendDataBoundItems="True"
                        AutoPostBack="True" DataSourceID="ObjectDataSourceHospital" DataTextField="HospitalExtendedName"
                        DataValueField="HospitalInfoID" Width="200px" OnSelectedIndexChanged="DropDownListHospital_SelectedIndexChanged" TabIndex="1">
                        <asp:ListItem Value="0">Select Hospital</asp:ListItem>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSourceHospital" runat="server" OnSelecting="ObjectDataSourceHospital_Selecting"
                        SelectMethod="GetHospitalNamesForRequestByUserID" TypeName="RMC.BussinessService.BSHospitalInfo">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0"  Name="userID" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorHospital" runat="server" ErrorMessage="Select Hospital Name."
                        ControlToValidate="DropDownListHospital" Display="None" InitialValue="0" ValidationGroup="Request">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="color: #06569d; font-weight: bold; font-size:11px;" valign="top">
                    Unit <span style="color: Red;">*</span>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="UpdatePanelUnit" runat="server">
                        <ContentTemplate>
                            <asp:ListBox ID="ListBoxUnit" runat="server" DataSourceID="ObjectDataSourceUnit"
                                DataTextField="HospitalUnitName" DataValueField="HospitalDemographicID" Width="200px"
                                Height="119px" SelectionMode="Multiple" TabIndex="2"></asp:ListBox>
                            <asp:ObjectDataSource ID="ObjectDataSourceUnit" runat="server" SelectMethod="GetHospitalDemographicDetailByHospitalID"
                                TypeName="RMC.BussinessService.BSHospitalDemographicDetail" OnSelecting="ObjectDataSourceUnit_Selecting">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownListHospital" DefaultValue="0" Name="hospitalID"
                                        PropertyName="SelectedValue" Type="Int32" />
                                    <asp:Parameter DefaultValue="0" Name="userID" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DropDownListHospital" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorListHospitalUnit" runat="server"
                        ControlToValidate="ListBoxUnit" Display="None" SetFocusOnError="true" ErrorMessage="Select Hospital Unit Name."
                        InitialValue="0" ValidationGroup="Request">*</asp:RequiredFieldValidator>
                    <div id="ParentDiv" class="Background" style="display:none;">
                        <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
                            top: 55%; visibility: visible; vertical-align: middle; border-style: none; border-color: black;
                            z-index: 40;">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
                                AlternateText="Loading"></asp:Image>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="ButtonSendRequest" runat="server" Text="Send Request" ValidationGroup="Request"
                        CssClass="aspButton" OnClick="ButtonSendRequest_Click" TabIndex="3" />
                    <asp:Button ID="ButtonReset" runat="server" Text="Reset" CssClass="aspButton" OnClick="ButtonReset_Click"
                        Visible="false" TabIndex="3" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
