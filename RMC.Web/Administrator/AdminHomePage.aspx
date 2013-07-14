<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="AdminHomePage.aspx.cs" Inherits="RMC.Web.Administrator.AdminHomePage" MaintainScrollPositionOnPostback="true"
    Title="RMC :: Admin Home Page" %>

<%@ Register Src="../UserControls/ActivationOfUsers.ascx" TagName="ActivationOfUsers"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/RequestApprovalNotification.ascx" TagName="RequestApprovalNotification"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ContactUsNotification.ascx" TagName="ContactUsNotification"
    TagPrefix="uc3" %>
<%@ Register src="../UserControls/RequestList.ascx" tagname="RequestList" tagprefix="uc4" %>
<%@ Register src="../UserControls/UserNotification.ascx" tagname="UserNotification" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="padding-left: 5px; font-size: 14px; color: #06569d;">
        <center>
            <table width="100%">
                <tr>
                    <th align="left" style="font-size: 14px; padding-left:10px; padding-top:10px;">
                        <%--<h3 style="font-size: 13px;">--%>
                            <u>Admin Home Page</u>
                        <%--</h3>--%>
                    </th>
                </tr>
                <tr>
                    <td align="left">
                        <fieldset style="width: 525px;">
                            <legend>Request Owner Priviledges                                 
                            </legend>
                            <div style="padding-top: 5px;">
                                <uc1:ActivationOfUsers ID="ActivationOfUsers1" runat="server" />
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <fieldset style="width: 525px;">
                            <legend>Request Hospital Unit Access</legend>
                            <div style="padding-top: 5px;">
                                <uc2:RequestApprovalNotification ID="RequestApprovalNotification1" runat="server" />
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <fieldset style="width: 525px;">
                            <legend>Contact Us Notification</legend>
                            <div style="padding-top: 5px;">
                                <uc3:ContactUsNotification ID="ContactUsNotification1" runat="server" />
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <%--<tr>
                    <td align="left">
                        <fieldset style="width: 525px;">
                            <legend>Notification</legend>
                            <div style="padding-top: 5px;">
                                
                                <uc5:UserNotification ID="UserNotification1" runat="server" />
                                
                            </div>
                        </fieldset>
                    </td>
                </tr>--%>
                <tr>
                    <td align="left">
                        <fieldset style="width: 525px;">
                            <legend>Users Request</legend>
                            <div style="padding-top: 5px;">
                                <uc4:RequestList ID="RequestList1" runat="server" />
                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>
