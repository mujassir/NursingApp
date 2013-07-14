<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="UserProfile.aspx.cs" Inherits="RMC.Web.Users.UserProfile" Title="RMC :: My Profile" %>

<%@ Register Src="../UserControls/UserProfile.ascx" TagName="UserProfile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <uc1:UserProfile ID="UserProfile1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
