<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ExcelUploader.aspx.cs" Inherits="RMC.Web.Users.ExcelUploader" Title="Advanced Files Uploader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManagerUploader" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <table width="100%">
                <tr>
                    <th colspan="2" style="height: 5px;">
                        <h3 style="font-size: 13px;">
                            Advanced Excel Files Uploader
                        </h3>
                    </th>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="LabelErrorMsg" runat="server" Visible="False" Font-Size="12px"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummaryDemoGraphic" ValidationGroup="Demographic"
                            runat="server" DisplayMode="List" Font-Size="12px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; color: #06569d; font-weight: bold;" align="right">
                        Hospital Name
                    </td>
                    <td style="width: 75%;" align="left">
                        <asp:DropDownList ID="DropDownListHospitalName" runat="server" AutoPostBack="True"
                            ForeColor="#06569d" Width="300px" DataSourceID="ObjectDataSourceHospitalName"
                            DataTextField="HospitalName" DataValueField="HospitalInfoID" OnSelectedIndexChanged="DropDownListHospitalName_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td style="width: 20%; color: #06569d; font-weight: bold;" align="right">
                                    Hospital Unit
                                </td>
                                <td style="width: 30%;" align="left">
                                    <asp:UpdatePanel ID="UpdatePanelUnitName" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownListUnitName" runat="server" ForeColor="#06569d" Width="200px"
                                                AutoPostBack="True" DataSourceID="ObjectDataSourceUnitName" DataTextField="HospitalUnitName"
                                                DataValueField="HospitalDemographicID" OnSelectedIndexChanged="DropDownListUnitName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="DropDownListHospitalName" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="DropDownListUnitName" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; color: #06569d; font-weight: bold;" align="right">
                                    Date
                                </td>
                                <td style="width: 40%; vertical-align: middle;" align="left">
                                    <asp:UpdatePanel ID="UpdatePanelDate" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TextBoxDate" runat="server" AutoPostBack="True" OnTextChanged="TextBoxDate_TextChanged"
                                                Width="140px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtenderDate" Format="MM/dd/yyyy" TargetControlID="TextBoxDate"
                                                PopupButtonID="TextBoxDate" runat="server">
                                            </cc1:CalendarExtender>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="TextBoxDate" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 10px; color: #06569d; font-weight: bold;" colspan="2" align="left">
                        <div style="padding-left: 35px; color: #06569d;">
                            Please click in the below area to activate flash control.
                        </div>
                    </td>
                </tr>
            </table>
            <table width="96%">
                <tr>
                    <td>
                        <div style="padding-top: 5px; border: solid 1px #06569d;">
                            <%--
        
        Adding the flash uploader to the page.  <param name="wmode" value="transparent"> makes
        the movie transparent so any page styling can shine through.
        FlashVars is set to pass in the upload page and a javascript function if desired.
        The javascript function fires when the upload is completed.  This allows us to 
        call a codebehind function to refresh a gridView or anything else.
        A link button is used to perform this function.  The javascript is added in the pageLoad 
        in the code behin.
        To edit the flash file, Adobe/Macromedia has a 30-day trial of Macromedia Flash
        for download.  After that, you're on your own.
        --%>
                            <div style="padding-left: 35px; color: #06569d;">
                                Please click on Add Files Button to upload the .sda files.
                            </div>
                            <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0"
                                width="100%" height="320" id="fileUpload" align="middle">
                                <param name="allowScriptAccess" value="sameDomain" />
                                <param name="movie" value="FlashFileUpload.swf" />
                                <param name="quality" value="high" />
                                <param name="wmode" value="transparent" />
                                <param name="FlashVars" value='uploadPage=Upload.axd<%=GetFlashVars()%>&completeFunction=UploadComplete()' />
                                <embed src="FlashFileUpload.swf" flashvars='uploadPage=Upload.axd<%=GetFlashVars()%>&completeFunction=UploadComplete()'
                                    quality="high" wmode="transparent" width="700" height="400" name="fileUpload"
                                    align="middle" allowscriptaccess="sameDomain" type="application/x-shockwave-flash"
                                    pluginspage="http://www.macromedia.com/go/getflashplayer" />
                            </object>
                            <%-- <script src="../JavaScript/fixit.js" type="text/javascript"></script>
--%>
                            <%--This link button is here so we can call the LinkButton1_Click even from javascript.
            Make the text empty so the link doesn't show up on the page.--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ObjectDataSource ID="ObjectDataSourceHospitalName" runat="server" OnSelecting="ObjectDataSourceHospitalName_Selecting"
                            SelectMethod="GetHospitalNamesByUserID" TypeName="RMC.BussinessService.BSHospitalInfo">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="userID" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="ObjectDataSourceUnitName" runat="server" SelectMethod="GetHospitalDemographicDetailByHospitalInfoID"
                            TypeName="RMC.BussinessService.BSHospitalDemographicDetail">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListHospitalName" DefaultValue="0" Name="hospitalInfoID"
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>
