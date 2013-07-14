<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateNewProfile.ascx.cs"
    Inherits="RMC.Web.UserControls.CreateNewProfile" %>
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
<%--<script src="../jquery/jquery.lightbox.min.js" type="text/javascript"></script>
<script src="../jquery/jquery.popupWindow.js" type="text/javascript"></script>--%>
<script type="text/javascript">
    //    $('#HyperLinkCopyProfileFromTemplate').popupWindow({centerScreen:1 });
//    $('#HyperLinkCopyProfileFromTemplate').lightbox();
    function pageLoad() {

        var manager = Sys.WebForms.PageRequestManager.getInstance();
        manager.add_initializeRequest(CheckStatus);
        manager.add_endRequest(endRequest);
        manager.add_beginRequest(onBeginRequest);
    }

    function CheckStatus(sender, args) {
        
        if (!ValidatePaggingOnClick())
        {
            args.set_cancel(true);
        }
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

    function ValidateNoOfRecords() {

        var noOfRecords = document.getElementById('<%=TextBoxNoOfRecordsPerPage.ClientID%>');
        var totalRecords = document.getElementById('<%=LabelTotalNoOfRecords.ClientID%>');

        if (noOfRecords.value != '') {

            if (parseInt(noOfRecords.value) > parseInt(totalRecords.innerText)) {

                noOfRecords.value = 50;
                alert('Please enter Valid No. Of Records Per Page.');
                return false;
            }
            else if (parseInt(noOfRecords.value) < 1) {

                noOfRecords.value = 50;
                alert('No. Of Records Per Page must be greater than Zero.');
                return false;
            }
            else {

                return true;
            }
        }
        else {

            noOfRecords.value = 50;
            alert('Empty field is not allowed.');
            return false;
        }
    }

    function ValidateNoOfPages() {

        var currentPage = document.getElementById('<%=TextBoxPageNo.ClientID%>');
        var totalPages = document.getElementById('<%=LabelTotalPages.ClientID%>');
        var currentPaging = document.getElementById('<%=LabelCurrentPageNo.ClientID%>');

        if (currentPage.value != '') {
            if (parseInt(currentPage.value) > parseInt(totalPages.innerText)) {

                currentPage.value = 1;
                currentPaging.value = 1;
                alert('Please enter Valid Page Number.');
                return false;
            }
            else if (parseInt(currentPage.value) < 1) {

                currentPage.value = 1
                currentPaging.value = 1;
                alert('Page Number must be greater than Zero.');
                return false;
            }
            else {

                return true;
            }
        }
        else {

            currentPage.value = 1
            currentPaging.value = 1;
            alert('Empty field is not allowed.');
            return false;
        }
    }

    function ValidatePaggingOnClick() {

        if (ValidateNoOfRecords() == false) {

            return false;
        }

        if (ValidateNoOfPages() == false) {

            return false;
        }

        return true;
    }
    function myPopup(PType) {
        var l, t;
        l = (screen.width - 100)/2-5;      
        t = (screen.height - 150)/2-20;
        mywindow = window.open("../Common/CopyProfileFromTemplate.aspx?PType=" + PType, "", "modal=true,status=no,menubar=no,scrollbars=no,title=no,toolbar=no,resizable=no,width=100,height=150,left="+l+",top="+t);
        mywindow.focus();
        return false;
    }
</script>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server">
</asp:ScriptManager>
<table width="99%">
    <tr>
        <th align="left" style="font-size: 14px; color: #06569d; padding-left: 10px; padding-top: 10px;">
            <u>Create Profile Detail</u>
        </th>
        <th align="right" style="padding-top: 10px;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="6" />
        </th>
    </tr>
    <tr>
        <td colspan="2">
            <table width="100%" style="padding-left: 5px;">
                <tr>
                    <td colspan="4">
                        <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="CreateNewProfile"
                            runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Font-Size="11px"
                            Font-Bold="true" Style="padding-top: 1px;" />
                    </td>
                </tr>
                <tr>
                    <td style="color: #06569d; font-weight: bold; font-size: 11px;" align="right">
                        Profile Name <span>&nbsp;&nbsp;</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxProfileType" CssClass="aspTextBox" runat="server" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompanyName" runat="server"
                            SetFocusOnError="true" ErrorMessage="Required Profile Type." ControlToValidate="TextBoxProfileType"
                            Display="None" ValidationGroup="CreateNewProfile">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="font-size: 11px; color: #06569d; font-weight: bold; width: 210px;" align="right"
                        colspan="1">
                        Share Profile
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="checkBoxShare" runat="server" Font-Bold="true" Font-Size="11px"
                            ForeColor="#06569D" TabIndex="3" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="color: #06569d; font-size: 11px; font-weight: bold;" align="right">
                        Description <span>&nbsp;&nbsp;</span>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="TextBoxDecription" CssClass="aspTextBox" Width="200" Height="100px"
                            TextMode="MultiLine" runat="server" ForeColor="#06569D" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td valign="top" style="color: #06569d; font-size: 11px; font-weight: bold;" align="right">
                        <span>&nbsp;&nbsp;</span>
                    </td>
                    <td colspan="3" style="height: 2px;">
                    <asp:DropDownList ID="DropDownListProfileType" CssClass="aspDropDownList"
                                    runat="server" ForeColor="#06569D" TabIndex="4" 
             
                Height="22px" Width="200px" >
         </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ButtonCopyProfileFromTemplate" 
                            Text="Copy Profile from Profile Template" runat="server"
                            CssClass="aspButton" style="width:auto;" TabIndex="6" 
                            onclick="ButtonCopyProfileFromTemplate_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 30px">
                    </td>
                    <td style="color: #06569d; font-size: 11px;" align="left" colspan="2" valign="middle">
                        <asp:Button ID="ButtonSave" Text="Save" ValidationGroup="CreateNewProfile" runat="server"
                            CssClass="aspButton" OnClick="ButtonSave_Click" TabIndex="5" />
                        <div style="margin-left: 10px; float: right; color: #06569d; font-size: 11px; font-weight: bold;">
                            <%--<asp:HyperLink ID="HyperLinkCopyProfileFromTemplate" NavigateUrl="~/Common/CopyProfileFromTemplate.aspx"  Text="Copy Profile from Profile Template" runat="server"
                            TabIndex="6"></asp:HyperLink>--%>
                            <%--<asp:LinkButton ID="LinkButtonCopyProfileFromTemplate" Font-Bold="true" Font-Size="11px" runat="server">Copy Profile from Profile Template</asp:LinkButton>--%>
                         </div>
                    </td>
                    <td align="left">
                       <%-- <asp:DropDownList ID="DropDownListProfileType" CssClass="aspDropDownList" AutoPostBack="true"
                            runat="server" ForeColor="#06569D" TabIndex="4" OnSelectedIndexChanged="DropDownListProfileType_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: #06569d; font-size: 12px; font-weight: bold; text-decoration: underline;"
                        colspan="4">
                        List of Profile Values
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <asp:LinkButton ID="LinkButtonDeleteSelected" Font-Bold="true" Font-Size="11px" runat="server"
                            OnClick="LinkButtonDeleteSelected_Click" OnClientClick="return confirm('Are you sure you wish to delete these records?')">Delete Selected</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:UpdatePanel ID="UpdatePanelGridview" runat="server">
                            <contenttemplate>
                                <table width="100%" cellpadding="4" cellspacing="0">
                                    <tr class="RowStyle">
                                        <td class="StartColumnStyle" align="left">
                                            <table width="100%">
                                                <tr>
                                                    <td style="font-size: 11px; font-weight: bold;" align="right">
                                                        No. Of Records Per Page&nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxNoOfRecordsPerPage" CssClass="aspTextBox" runat="server"
                                                            Width="40px"  ></asp:TextBox><span style="font-size: 11px; font-weight: bold;"> /
                                                        </span>
                                                        <asp:Label ID="LabelTotalNoOfRecords" Font-Size="11px" ForeColor="White" Font-Bold="true"
                                                            runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td style="width: 15px;">
                                                    </td>
                                                    <td style="font-size: 11px; font-weight: bold;" align="right">
                                                        Page No.&nbsp;&nbsp
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxPageNo" CssClass="aspTextBox" runat="server" Width="40px" ></asp:TextBox><span
                                                            style="font-size: 11px; font-weight: bold;"> / </span>
                                                        <asp:Label ID="LabelTotalPages" Font-Size="11px" ForeColor="White" Font-Bold="true"
                                                            runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td style="width: 15px;">
                                                    </td>
                                                    <td align="left" style="color: #06569d; font-size: 12px; font-weight: bold;">
                                                        <asp:LinkButton ID="LinkButtonShow" Font-Bold="true" runat="server" ForeColor="White"
                                                            OnClick="LinkButtonShow_Click">Show</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7">
                                                        <hr style="color: White; width: 100%;" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" align="center">
                                                        <table width="60%">
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:ImageButton ID="ImageButtonFirst" runat="server" ImageUrl="~/Images/media_beginning.png"
                                                                        OnClick="ImageButtonFirst_Click" />
                                                                </td>
                                                                <td align="right">
                                                                    <asp:ImageButton ID="ImageButtonStepBackward" runat="server" ImageUrl="~/Images/stepBackward.png"
                                                                        OnClick="ImageButtonStepBackward_Click" />
                                                                </td>
                                                                <td style="font-size: 11px; font-weight: bold;" align="center">
                                                                    <asp:Label ID="LabelCurrentPageNo" runat="server" Text="Label" Font-Size="11px" ForeColor="White"
                                                                        Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:ImageButton ID="ImageButtonStepForward" runat="server" ImageUrl="~/Images/stepForward.png"
                                                                        OnClick="ImageButtonStepForward_Click" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:ImageButton ID="ImageButtonLast" runat="server" OnClientClick="return ValidatePaggingOnClick();"
                                                                        ImageUrl="~/Images/media_beginning1.png" OnClick="ImageButtonLast_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridViewCreateProfile" BorderColor="Gray" runat="server" Width="550px"
                                                CssClass="GridViewStyle" CellPadding="5" EmptyDataText="No Record to display."
                                                EnableViewState="true" DataKeyNames="ValidationID" GridLines="None" AutoGenerateColumns="False"
                                                HorizontalAlign="Center" AllowPaging="false" TabIndex="6">
                                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBoxDelete" runat="server" EnableViewState="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelLocation" Text='<%# Bind("Location") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="LabelLocationID" Visible="false" Text='<%# Eval("LocationID") %>'
                                                                runat="server"></asp:Label>
                                                            <asp:Label ID="LabelValidation" Visible="false" Text='<%# Eval("ValidationID") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Activity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelActivity" Text='<%# Bind("Activity") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="LabelActivityID" Visible="false" Text='<%# Eval("ActivityID") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SubActivity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSubActivity" Text='<%# Bind("SubActivity") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="LabelSubActivityID" Visible="false" Text='<%# Eval("SubActivityID") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="DropDownListCategoryAssignment" ForeColor="#06569D" runat="server"
                                                                DataSourceID="ObjectDataSourceCategoryAssignment" DataTextField="ProfileCategory" AppendDataBoundItems="true"
a
                                                                DataValueField="ProfileCategoryID" Font-Size="11px" TabIndex="7" SelectedValue='<%# Eval("CategoryAssignmentID")%>'> 
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                           <%-- <asp:DropDownList ID="DropDownListCategoryAssignment"  ForeColor="#06569D" runat="server"
                                                    DataSourceID="ObjectDataSourceCategoryAssignment" DataTextField="ProfileCategory"
                                                    DataValueField="ProfileCategoryID" Font-Size="11px" TabIndex="7" SelectedValue='<%# Bind("CategoryAssignmentID") %>'>
                                                </asp:DropDownList>--%>
                                                            <asp:ObjectDataSource ID="ObjectDataSourceCategoryAssignment" runat="server" SelectMethod="GetAllValueAdded_CategoryGroup"
                                                                TypeName="RMC.BussinessService.BSCommon">
                                                                <SelectParameters>
                                                                    <asp:QueryStringParameter DefaultValue="0" Name="valuetype" QueryStringField="valuetype"
                                                                        Type="String" />
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="GridViewFooterStyle" />
                                                <PagerStyle CssClass="GridViewPagerStyle" />
                                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                <RowStyle CssClass="GridViewRowStyle" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </contenttemplate>
                            <triggers>
                               <%-- <asp:AsyncPostBackTrigger ControlID="DropDownListProfileType" EventName="SelectedIndexChanged" />--%>
                                <asp:AsyncPostBackTrigger ControlID="LinkButtonDeleteSelected" EventName="Click" />
                            </triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>
