<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileDetail.ascx.cs"
    Inherits="RMC.Web.UserControls.ProfileDetail" %>
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
        manager.add_initializeRequest(CheckStatus);
        manager.add_endRequest(endRequest);
        manager.add_beginRequest(onBeginRequest);
    }

    function CheckStatus(sender, args) {

        if (!ValidatePaggingOnClick()) {
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

</script>

<script language="javascript" type="text/javascript">
    function mypopup(senderID) {

        mywindow = window.open("../Users/SendUserNotification.aspx?SenderID=" + senderID, "", "location=0,status=0,scrollbars=1, width=500,height=300,resizable=no ");
        return false;
    }
    function mypopupAdministrator(senderID) {

        mywindow = window.open("../Administrator/SendUserNotification.aspx?SenderID=" + senderID, "", "location=0,status=0,scrollbars=1, width=500,height=300,resizable=no ");
        return false;
    } 
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
            <u>Category Profile Detail</u>
        </th>
        <th style="padding-top: 10px;padding-right: 10px;" align="right">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="6" />
        </th>
    </tr>

    <tr>
        <td align="center" colspan="2">
            <table>
                <tr>
                    <td>
                        <%--Profile Type--%>
                    </td>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="CreateNewProfile"
                            runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Font-Size="11px"
                            Font-Bold="true" Style="padding-top: 1px;" />
                    </td>
                    <td class="style1">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" style="color: #06569d; font-size: 11px; font-weight: bold;">
                        Profile Name <span>&nbsp;&nbsp;</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxProfileType" CssClass="aspTextBox" runat="server" 
                            TabIndex="1" Width="224px"></asp:TextBox>
                    </td>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td align="right" style="color: #06569d; font-size: 11px; font-weight: bold;">
                        Share Profile <span>&nbsp;&nbsp;</span>
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="checkBoxShare" runat="server" Font-Size="11px" Font-Bold="true"
                            ForeColor="#06569d" TabIndex="4" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="color: #06569d; font-size: 11px; font-weight: bold;">
                        Author Name <span>&nbsp;&nbsp;</span>
                    </td>
                    <td valign="top" align="left">
                        <div style="border: solid 1px #06569D; height: 18px; padding-left: 5px; padding-top: 2px;">
                            <asp:LinkButton ID="LinkButtonAuthorName" Font-Size="11px" runat="server" Enabled="False"
                                TabIndex="2">LinkButton</asp:LinkButton>
                        </div>
                    </td>
                    <td align="right" valign="top" class="style1">
                        &nbsp;
                    </td>
                    <td valign="top">
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" style="color: #06569d; font-size: 11px; font-weight: bold;">
                        Description <span>&nbsp;&nbsp;</span>
                    </td>
                    <td valign="top" align="left">
                        <asp:TextBox ID="TextBoxDecription" CssClass="aspTextBox" Width="225px" Height="50px"
                            ForeColor="#06569d" TextMode="MultiLine" runat="server" TabIndex="3"></asp:TextBox>
                    </td>
                    <td valign="top" class="style1">
                        &nbsp;
                    </td>
                    <td valign="top" colspan="2">
                        <asp:Button ID="ButtonUpdate" Text="Update Name" ValidationGroup="CreateNewProfile" runat="server"
                            CssClass="aspButton" OnClick="ButtonUpdate_Click" TabIndex="5" />
                        <div id="divUpdateDisable" visible="false" class="aspButtonDisable" style="width: 65px;
                            height: 25px; text-align: center; cursor: pointer;" runat="server">
                            <asp:Label ID="LabelSaveDisable" runat="server" Text="Update"></asp:Label>
                        </div>
                    </td>                   
                </tr>
                <tr>
                  
                    <td>
                        &nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompanyName" runat="server"
                            SetFocusOnError="true" ErrorMessage="Required Profile Type." ControlToValidate="TextBoxProfileType"
                            Display="None" ValidationGroup="CreateNewProfile">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>

        <td align="right">
            <asp:Button ID="ButtonSortAll" Font-Bold="true" Font-Size="11px" runat="server" 
                Text="Sort All" CssClass="aspButton" Width="130px" 
                onclick="ButtonSortAll_Click"/>
            &nbsp;&nbsp;
            <asp:Button ID="LinkButtonUpdate" Font-Bold="true" Font-Size="11px" runat="server" Text="Update Profile Values" CssClass="aspButton" Width="130px" onclick="LinkButtonUpdate_Click" />
            <%--<asp:LinkButton ID="LinkButtonUpdate" Font-Bold="true" Font-Size="11px" 
                    runat="server" onclick="LinkButtonUpdate_Click">Update Profile Values</asp:LinkButton>--%>
            <%--<span style="padding-left:5px; padding-right:5px;"></span>--%>
            &nbsp;&nbsp;
            <asp:Button ID="LinkButtonDeleteSelected" Font-Bold="true" Font-Size="11px" runat="server" Text="Delete Selected" CssClass="aspButton" Width="125px" OnClientClick="return confirm('Are you sure you wish to delete these records?')" onclick="LinkButtonDeleteSelected_Click" />
            <%--<asp:LinkButton ID="LinkButtonDeleteSelected" Font-Bold="true" Font-Size="11px" runat="server" OnClientClick="return confirm('Are you sure you wish to delete these records?')"
                OnClick="LinkButtonDeleteSelected_Click">Delete Selected</asp:LinkButton>--%>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <asp:UpdatePanel ID="UpdatePanelGridview" runat="server">
                <ContentTemplate>
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
                                                Width="40px"></asp:TextBox><span style="font-size: 11px; font-weight: bold;"> /
                                            
                                            
                                            
                                            
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
                                            <asp:TextBox ID="TextBoxPageNo" CssClass="aspTextBox" runat="server" Width="40px"></asp:TextBox><span
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
                            <div style="height: 300px; overflow: auto; border: 0px double #06569d; width: 568px;">
                                <asp:GridView ID="GridViewCreateProfile" BorderColor="Gray" runat="server" Width="550px"
                                    CssClass="GridViewStyle" CellPadding="5" EmptyDataText="No Record to display."
                                    DataKeyNames="CategoryProfileID" GridLines="None" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" DataSourceID="ObjectDataSourceRequestList" 
                                    OnRowCommand="GridViewCreateProfile_RowCommand">
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxDelete" runat="server" EnableViewState="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton id="lnkbtnLocation" runat="server" CommandName="Sort"
                                                        CommandArgument="Location">Location</asp:LinkButton> 
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelLocation" Text='<%# Bind("Location") %>' runat="server"></asp:Label>
                                                <asp:Label ID="LabelLocationID" Visible="false" Text='<%# Eval("LocationID") %>'
                                                    runat="server"></asp:Label>
                                                <asp:Label ID="LabelValidation" Visible="false" Text='<%# Eval("ValidationID") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton id="lnkbtnActivity" runat="server" CommandName="Sort"
                                                        CommandArgument="Activity">Activity</asp:LinkButton> 
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelActivity" Text='<%# Bind("Activity") %>' runat="server"></asp:Label>
                                                <asp:Label ID="LabelActivityID" Visible="false" Text='<%# Eval("ActivityID") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton id="lnkbtnSubActivity" runat="server" CommandName="Sort"
                                                        CommandArgument="SubActivity">SubActivity</asp:LinkButton> 
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSubActivity" Text='<%# Bind("SubActivity") %>' runat="server"></asp:Label>
                                                <asp:Label ID="LabelSubActivityID" Visible="false" Text='<%# Eval("SubActivityID") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category">
                                            <HeaderTemplate>
                                                <asp:LinkButton id="lnkbtnCategory" runat="server" CommandName="Sort"
                                                        CommandArgument="Category">Category</asp:LinkButton> 
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownListCategoryAssignment" Enabled='<%#enableCategory %>' ForeColor="#06569D" runat="server"
                                                    DataSourceID="ObjectDataSourceCategoryAssignment" DataTextField="ProfileCategory"
                                                    DataValueField="ProfileCategoryID" Font-Size="11px" TabIndex="7" SelectedValue='<%# Bind("CategoryAssignmentID") %>'>
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="ObjectDataSourceCategoryAssignment" runat="server" SelectMethod="GetAllCategoryByProfileTypeID"
                                                    TypeName="RMC.BussinessService.BSCommon">
                                                    <SelectParameters>
                                                        <asp:QueryStringParameter DefaultValue="0" Name="profileTypeID" QueryStringField="ProfileTypeID"
                                                            Type="Int32" />
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
                            </div>
                                <asp:ObjectDataSource ID="ObjectDataSourceRequestList" runat="server" OnSelecting="ObjectDataSourceRequestList_Selecting"
                                    SelectMethod="GetCategoryProfileByUserID" TypeName="RMC.BussinessService.BSCategoryProfiles">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="userID" Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="profileTypeID" Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="noOfSkipRecords" Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="noOfRecords" Type="Int32" />
                                        <asp:Parameter DefaultValue=" " Name="sortExpression" Type="String" />
                                        <asp:Parameter DefaultValue=" " Name="sortOrder" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="LinkButtonDeleteSelected" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="LinkButtonUpdate" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
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
