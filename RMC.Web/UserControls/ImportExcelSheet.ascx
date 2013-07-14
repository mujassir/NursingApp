<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportExcelSheet.ascx.cs"
    Inherits="RMC.Web.UserControls.ImportExcelSheet" %>
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
   
    function onBeginRequest() {

        var divParent = document.getElementById('ParentDiv');
        var divImage = document.getElementById('IMGDIV');

        divParent.className = 'Background';
        divParent.style.visibility = 'visible';
        divImage.style.visibility = 'visible';
        return true;
    }

    function endRequest(sender, args) {

        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = '';
        divParent.style.visibility = 'hidden';
        divImage.style.visibility = 'hidden';
    }
   
    </script>
<table width="99%">
    <tr>
        <th align="left" style="font-size: 14px; color: #06569d; padding-left: 10px; padding-top: 10px;">
            <%--<h3 style="font-size: 13px;">
                <table width="100%">
                    <tr>
                        <td align="left">--%>
            <%--</td>
                        <td align="center">--%>
            <u>Import Excel Sheet</u>
            <%--</td>
                    </tr>
                </table>
            </h3>--%>
        </th>
        <th align="right" style="padding-top: 10px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                PostBackUrl="~/Administrator/ValidationTable.aspx" TabIndex="4" />
        </th>
    </tr>
    <tr>
        <td style="height: 15px;">
        </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: bold; font-size: 11px; color: #06569d;">
            Sheet Name <span style="color: Red;">*</span>
        </td>
        <td align="left">
            <asp:TextBox ID="TextBoxSheetName" runat="server" TabIndex="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSheetName" runat="server" ErrorMessage="Sheet Name is required."
                ControlToValidate="TextBoxSheetName" Display="None" ValidationGroup="Import">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="font-weight: bold; font-size: 11px; color: #06569d;">
            Upload Excel Sheet<span>&nbsp;&nbsp;</span>
        </td>
        <td align="left">
            <asp:FileUpload ID="FileUploadExcelSheet" runat="server" TabIndex="2" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td align="left">
            <asp:Button ID="ButtonImportExcelSheet" CssClass="aspButton" ValidationGroup="Import" OnClientClick="return onBeginRequest();"
                runat="server" Text="Import Excel Sheet" Width="150px" OnClick="ButtonImportExcelSheet_Click" TabIndex="3"/>
        </td>
    </tr>
    <tr>
        <td style="height: 10px;">
        </td>
    </tr>
    <%--<tr>
        <td align="center" colspan="2" style="color: Red; font-size: 11px; font-weight: bold;">
            Warning : Please wait for Message or Completion of Import Process.after clicking 'Import Excel Sheet' Button.
        </td>
    </tr>--%>
</table>
<asp:RegularExpressionValidator ID="FileUpLoadValidator" runat="server" ErrorMessage="Upload Excel Sheet only."
    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$"
    ControlToValidate="FileUploadExcelSheet" ValidationGroup="Import" Display="None"></asp:RegularExpressionValidator>
<asp:ValidationSummary ID="ValidationSummaryImport" ShowMessageBox="true" ShowSummary="false"
    ValidationGroup="Import" runat="server" />
<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>