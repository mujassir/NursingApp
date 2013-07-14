<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestForTypes.ascx.cs"
    Inherits="RMC.Web.UserControls.RequestForTypes" %>

<script type="text/javascript" language="javascript">
    jQuery(function($) {

        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });
</script>

<asp:Panel ID="PanelPharmacyType" runat="server" DefaultButton="ButtonSendRequest">
    <div style="width: 100%;">
        <table width="100%" style="text-align: center;">
            <tr>
                <th align="center">
                    <h3 style="font-size: 13px;">
                        Add New
                        <asp:Literal ID="LiteralHead" runat="server"></asp:Literal>
                        Request Form
                        <asp:Literal ID="LiteralHospitalName" runat="server"></asp:Literal>
                        <asp:Literal ID="LiteralUnitName" runat="server"></asp:Literal>
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
                    <table width="430px">
                        <%--<tr>
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                <asp:Label ID="LabelHospitalName" ForeColor="#06569d" runat="server" Text="Hospital Name : "></asp:Label>
                                
                            </td>
                            <td align="left">
                                <asp:Label ID="LabelHospitalNameValue" ForeColor="#06569d" runat="server" Text=""></asp:Label>
                            </td>                        
                            <td align="right" style="color: #06569d; font-weight: bold;">
                                <asp:Label ID="LabelUnitName" ForeColor="#06569d" runat="server" Text="Unit Name : "></asp:Label>
                                &nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:Label ID="LabelUnitNameValue" ForeColor="#06569d" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="height: 2px;" colspan="4">
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
                            <td colspan="4">
                                <table width="500px">
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                            Enter the NEW
                                            <asp:Literal ID="LiteralType" runat="server"></asp:Literal>
                                            <span style="color: Red;">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxType" runat="server" Width="300px" ForeColor="#06569D"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" valign="top">
                                            Message &nbsp;&nbsp;
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="TextBoxMessage" ForeColor="#06569D" runat="server" Height="88px"
                                                TextMode="MultiLine" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-top: 5px;">
                                        </td>
                                        <td align="left" style="padding-top: 5px;">
                                            <table>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <asp:Button ID="ButtonSendRequest" runat="server" Text="Send Request" ValidationGroup="HospitalInfo"
                                                            CssClass="aspButton" Width="105px" OnClick="ButtonSendRequest_Click" />
                                                        <input id="ButtonClose" class="aspButton" type="button" value="Close" style="width: 90px;"
                                                            onclick="window.close();" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="font-size: 11px; font-weight: bold; padding-top: 5px;
                                            color: #06569d;">
                                            This request will be sent to the database manager who will make a final determination.
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
<asp:RequiredFieldValidator ID="RequiredFieldValidatorType" runat="server" ControlToValidate="TextBoxType"
    ValidationGroup="HospitalInfo" Display="None" SetFocusOnError="true" ErrorMessage="Must enter the type.">*</asp:RequiredFieldValidator>
