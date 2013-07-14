<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ErrorMessage.aspx.cs" Inherits="RMC.Web.Users.ErrorMessage" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" style="text-align: center">
        <tr>
            <th>
                <h3>
                    Error Message
                </h3>
            </th>
        </tr>
        <tr>
            <td style="font-size:13px;">
                <asp:Literal ID="LiteralErrorMessage" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
