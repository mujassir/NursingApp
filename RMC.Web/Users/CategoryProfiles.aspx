<%@ Page Title="RMC :: Category Profiles" Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="CategoryProfiles.aspx.cs" Inherits="RMC.Web.Users.CategoryProfiles" %>

<%@ Register Src="../UserControls/ProfileTreeView.ascx" TagName="ProfileTreeView"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="99%">
        <tr>
            <th style="font-size: 14px; color:#06569d; padding-left:10px; padding-top:10px;" align="left">
                <%--<h3 style="font-size: 13px; text-align: center;">--%>
                    <u>Category Profiles</u>
                <%--</h3>--%>
            </th>            
        </tr>
        <tr>
            <td>
                <uc1:profiletreeview id="ProfileTreeView1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
