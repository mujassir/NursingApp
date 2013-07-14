<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsLetter.ascx.cs"
    Inherits="RMC.Web.UserControls.NewsLetter" %>
<%--<script type="text/javascript" language="javascript">
    jQuery(function($) {

//        $("#divInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
//        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });
</script>--%>
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

<script type="text/javascript">

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
<asp:Panel ID="PanelHospitalInfo" runat="server" DefaultButton="ButtonSend">
    <div style="width: 99%;">
        <table width="100%" style="text-align: center;">
            <tr>
                <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                    <%--<h3 style="font-size: 13px;">
                        <table width="100%" style="text-align: left;">
                            <tr>
                                <td align="left" width="10%">--%>
                    <%--</td>
                                <td align="center">--%>
                    <%--<span>--%><u>Notification</u><%--</span>--%>
                    <%--</td>
                            </tr>
                        </table>
                    </h3>--%>
                </th>
                <th align="right" style="padding-top: 10px;">
                    <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                        PostBackUrl="~/Administrator/AdminHomePage.aspx" TabIndex="6"/>
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
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Subject &nbsp&nbsp
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxSubject" MaxLength="200" runat="server" Width="400px" TabIndex="1"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorHospitalInfo" runat="server"
                                                ControlToValidate="TextBoxSubject" Display="None" ErrorMessage="Required Subject."
                                                ValidationGroup="HospitalInfo">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorHospitalName" runat="server"
                                                ControlToValidate="TextBoxHospitalName" Display="None" ErrorMessage="Allow only alphanumeric characters in Hospital Name."
                                                ValidationExpression="^[a-zA-Z0-9''-'\s]{1,200}$" ValidationGroup="HospitalInfo">*</asp:RegularExpressionValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Filter &nbsp;&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListFilter" runat="server" Width="404px" AutoPostBack="true"
                                                ForeColor="#06569D" TabIndex="2">
                                                <asp:ListItem Value="0">All Users</asp:ListItem>
                                                <asp:ListItem Value="1">All Owner</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                            Users <span style="color: Red;">*</span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:UpdatePanel ID="UpdatePanelNewsLetter" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListBox ID="ListBoxUsers" runat="server" Height="135px" Width="404px" DataSourceID="ObjectDataSourceNewsLetter"
                                                        DataTextField="UserName" DataValueField="UserID" SelectionMode="Multiple" ForeColor="#06569D" TabIndex="3">
                                                    </asp:ListBox>
                                                    <asp:ObjectDataSource ID="ObjectDataSourceNewsLetter" runat="server" SelectMethod="FilterUserInformation"
                                                        TypeName="RMC.BussinessService.BSUsers">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="DropDownListFilter" DefaultValue="0" Name="filter"
                                                                PropertyName="SelectedValue" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="DropDownListFilter" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                            Message &nbsp;&nbsp;
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="TextBoxMessage" runat="server" Height="182px" MaxLength="100" TextMode="MultiLine"
                                                Width="400px" ForeColor="#06569D" TabIndex="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-top: 5px;">
                                        </td>
                                        <td align="left" style="padding-top: 5px;">
                                            <table width="190px">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="ButtonSend" runat="server" Text="Send" CssClass="aspButton" Width="55px"
                                                            OnClick="ButtonSend_Click" TabIndex="5" />
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
<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>
