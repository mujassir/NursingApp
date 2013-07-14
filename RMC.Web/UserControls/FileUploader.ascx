<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUploader.ascx.cs"
    Inherits="RMC.Web.UserControls.FileUploader" %>

<script type="text/javascript" language="javascript">
    jQuery(function($) {

        $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
    });     
        
</script>

<table width="99%">
    <tr>
        <th style="font-size: 13px;" align="left">
            <h3>
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif" /><span
                    style="padding-left: 200px;">Upload File</span></h3>
        </th>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td colspan="4" align="left">
                        <div style="width: 320px; float: left; background-color: Transparent; z-index: 0;">
                            <div id="divErrorMsgInner" style="width: 318px; float: left; background-color: #E8E9EA;
                                z-index: 10;">
                                <div style="padding-left: 90px;">
                                    <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="UploadFile"
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
                    <td align="right" style="color: #06569d; font-weight: bold;">
                        Year <span style="color: Red;">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DropDownListYear" ForeColor="#06569D" runat="server" 
                            AutoPostBack="True" 
                            onselectedindexchanged="DropDownListYear_SelectedIndexChanged" 
                            style="height: 22px">
                            <asp:ListItem>2009</asp:ListItem>
                            <asp:ListItem>2010</asp:ListItem>
                            <asp:ListItem>2011</asp:ListItem>
                            <asp:ListItem>2012</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="color: #06569d; font-weight: bold;">
                        Month <span style="color: Red;">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DropDownListMonth" ForeColor="#06569D" runat="server" 
                            AutoPostBack="True" 
                            onselectedindexchanged="DropDownListMonth_SelectedIndexChanged">
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">Feburary</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="color: #06569d; font-weight: bold;">
                        Upload File <span style="color: Red;">*</span>
                    </td>
                    <td align="left" colspan="3">
                        <asp:FileUpload ID="FileUploadSDA" ForeColor="#06569D" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="color: #06569d; font-weight: bold;">
                    </td>
                    <td align="left" colspan="3">
                        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" CssClass="aspButton" OnClick="ButtonSubmit_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:RequiredFieldValidator ID="RequiredFieldValidatorUploadFile" runat="server"
    ControlToValidate="FileUploadSDA" Display="None" ErrorMessage="Must select a SDA File."
    ValidationGroup="UploadFile">*</asp:RequiredFieldValidator>
