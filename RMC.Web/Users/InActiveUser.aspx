<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="InActiveUser.aspx.cs" Inherits="RMC.Web.Users.InActiveUser" Title="RMC :: In Active Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="float: left; width: 500px;margin:10px">
        <span style="color: #06569d; font-weight: bold; font-size: medium;">
            Your Account is not Active. You don't have Permission to Add Hospital Unit. Please
            Contact Administrator. </span>
    </div>
</asp:Content>
