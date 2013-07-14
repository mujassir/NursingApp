<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="UserHomePage.aspx.cs" Inherits="RMC.Web.Users.UserHomePage" Title="RMC :: User Home Page" %>

<%@ Register Src="../UserControls/ActivationOfUsers.ascx" TagName="ActivationOfUsers"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/RequestApprovalNotification.ascx" TagName="RequestApprovalNotification"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/UserNotification.ascx" TagName="UserNotification"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding-left: 5px; font-size: 14px; color: #06569d;">
        <center>
            <table width="100%">
                <tr>
                        <%--<h3 style="font-size: 13px;">
                            User Home Page
                        </h3>--%>
                        <th align="left" style="font-size: 14px; padding-left:10px; padding-top:10px;">
                            <u>User Home Page</u>
                         </th>
                </tr>
                <%--<tr>
                    <td align="left">
                        <fieldset style="width: 93%;">
                            <legend>Activation Request From Users </legend>
                            <div style="padding-top: 5px;">
                                <uc1:ActivationOfUsers ID="ActivationOfUsers1" runat="server" />
                            </div>
                        </fieldset>
                    </td>
                </tr>--%>
                <tr>
                    <td align="left">
                        <fieldset style="width: 93%;">
                            <legend>Request Hospital Unit Access</legend>
                            <div style="padding-top: 5px;"> 
                                <uc2:RequestApprovalNotification ID="RequestApprovalNotification1" runat="server" />
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <fieldset style="width: 93%;">
                            <legend>Notification</legend>
                            <div style="padding-top: 5px;">
                                <uc3:UserNotification ID="UserNotification1" runat="server" />
                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>
