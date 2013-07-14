<%@ Page Title="RMC :: Category Profile List" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="CategoryProfiles.aspx.cs" Inherits="RMC.Web.Administrator.CategoryProfiles" %>

<%@ Register Src="../UserControls/ProfileTreeView.ascx" TagName="ProfileTreeView"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="99%">
        <tr>
            <th align="left" style="font-size:14px; padding-left:10px; padding-top:10px; color:#06569d;">
                <%--<h3 style="font-size: 13px; text-align:center;">--%>
                    <u>Category Profiles</u>
                <%--</h3>--%>
            </th>
        </tr>
        <tr>
            <td style="height:5px;"></td>
        </tr>
        <tr>
            <td>
                <uc1:ProfileTreeView ID="ProfileTreeView1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
