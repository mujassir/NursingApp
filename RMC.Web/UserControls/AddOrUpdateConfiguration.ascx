<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddOrUpdateConfiguration.ascx.cs"
    Inherits="RMC.Web.UserControls.AddOrUpdateConfiguration" %>
<table width="100%">
    <tr>
        <th colspan="4" align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px;
            color: #06569d;">
            <u>SDA File Configuration</u>
        </th>
    </tr>
    <tr>
        <td style="height: 10px;" colspan="4">
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Edit Configuration Name <span style="padding-left: 5px">&nbsp;&nbsp;</span>
        </td>
        <td align="left" colspan="3">
            <asp:DropDownList ID="DropDownListConfigName" CssClass="aspDropDownList" runat="server"
                Width="105px" AppendDataBoundItems="True" 
                DataSourceID="ObjectDataSourceConfigName" DataTextField="Key" 
                DataValueField="Value" AutoPostBack="True" ForeColor="#06569D" 
                onselectedindexchanged="DropDownListConfigName_SelectedIndexChanged">
                <asp:ListItem Value="0">Select Config Name</asp:ListItem>
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSourceConfigName" runat="server" 
                SelectMethod="GetDataImportConfigLocation" 
                TypeName="RMC.BussinessService.BSDataImportConfigLocation">
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Configuration Name <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxConfigName" CssClass="aspTextBox" runat="server" MaxLength="100"
                Width="100px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfigName" runat="server"
                ErrorMessage="Must enter the Configuration Name." ControlToValidate="TextBoxConfigName"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
        </td>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Configuration Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxConfigLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfigLocation" runat="server"
                ErrorMessage="Must enter the Configuration Location." ControlToValidate="TextBoxConfigLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorConfigLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in Configuration Location." ControlToValidate="TextBoxConfigLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            PDA Name Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxPDANameLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPDANameLocation" runat="server"
                ErrorMessage="Must enter the PDA Name Location." ControlToValidate="TextBoxPDANameLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPDANameLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in PDA Name Location." ControlToValidate="TextBoxPDANameLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Software Version Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxSoftwareVersionLocation" CssClass="aspTextBox" runat="server"
                MaxLength="2" Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSoftwareVersionLocation" runat="server"
                ErrorMessage="Must enter the Software Version Location." ControlToValidate="TextBoxSoftwareVersionLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorSoftwareVersionLocation"
                runat="server" ErrorMessage="Enter the alphabetical characters in Software Version Location."
                ControlToValidate="TextBoxSoftwareVersionLocation" Display="None" ValidationGroup="Config"
                ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Key Data Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxKeyDataLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorKeyDataLocation" runat="server"
                ErrorMessage="Must enter the Key Data Location." ControlToValidate="TextBoxKeyDataLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorKeyDataLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in Key Data Location." ControlToValidate="TextBoxKeyDataLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Key Data Sequence Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxKeyDataSeqLocation" CssClass="aspTextBox" runat="server"
                MaxLength="2" Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorKeyDataSeqLocation" runat="server"
                ErrorMessage="Must enter the Key Data Sequence Location." ControlToValidate="TextBoxKeyDataSeqLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorKeyDataSeqLocation"
                runat="server" ErrorMessage="Enter the alphabetical characters in Key Data Seq Location."
                ControlToValidate="TextBoxKeyDataSeqLocation" Display="None" ValidationGroup="Config"
                ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Info Sequence Location <%--<span style="padding-left: 5px; color: Red;">*</span>--%>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxInfoSeqLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorInfoSeqLocation" runat="server"
                ErrorMessage="Must enter the Info Sequence Location." ControlToValidate="TextBoxInfoSeqLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorInfoSeqLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in Info Sequence Location." ControlToValidate="TextBoxInfoSeqLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Header Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxHeaderLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorHeaderLocation" runat="server"
                ErrorMessage="Must enter the Header Location." ControlToValidate="TextBoxHeaderLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorHeaderLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in Header Location." ControlToValidate="TextBoxHeaderLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Date Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxDateLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDateLocation" runat="server"
                ErrorMessage="Must enter the Date Location." ControlToValidate="TextBoxDateLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorDateLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in Date Location." ControlToValidate="TextBoxDateLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Hour Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxHourLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorHourLocation" runat="server"
                ErrorMessage="Must enter the Hour Location." ControlToValidate="TextBoxHourLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorHourLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in Hour Location." ControlToValidate="TextBoxHourLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Month Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxMonthLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMonthLocation" runat="server"
                ErrorMessage="Must enter the Month Location." ControlToValidate="TextBoxMonthLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMonthLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in Month Location." ControlToValidate="TextBoxMonthLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Minute Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxMinuteLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMinuteLocation" runat="server"
                ErrorMessage="Must enter the Minute Location." ControlToValidate="TextBoxMinuteLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMinuteLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in Minute Location." ControlToValidate="TextBoxMinuteLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
    </tr>
    <tr>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Year Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxYearLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorYearLocation" runat="server"
                ErrorMessage="Must enter the Year Location." ControlToValidate="TextBoxYearLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorYearLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in Year Location." ControlToValidate="TextBoxYearLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
        <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
            Second Location <span style="padding-left: 5px; color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxSecondLocation" CssClass="aspTextBox" runat="server" MaxLength="2"
                Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecondLocation" runat="server"
                ErrorMessage="Must enter the Second Location." ControlToValidate="TextBoxSecondLocation"
                Display="None" ValidationGroup="Config">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorSecondLocation" runat="server"
                ErrorMessage="Enter the alphabetical characters in Second location." ControlToValidate="TextBoxSecondLocation"
                Display="None" ValidationGroup="Config" ValidationExpression="[A-Za-z]{1,2}">*</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td style="height: 5px;" colspan="4">
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td align="left" colspan="3">
            <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="Config" CssClass="aspButton"
                Width="70px" onclick="ButtonSave_Click" />
            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" 
                ValidationGroup="Config" CssClass="aspButton" Width="70px" 
                onclick="ButtonUpdate_Click" />
            <asp:Button ID="ButtonDelete" runat="server" Text="Delete" 
                ValidationGroup="Config" CssClass="aspButton" Width="70px" 
                onclick="ButtonDelete_Click" />
        </td>
    </tr>
</table>
<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Config" ShowMessageBox="true"
    ShowSummary="false" runat="server" />
