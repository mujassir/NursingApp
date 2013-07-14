<%@ Page Language="C#" MasterPageFile="~/Users/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="TimeRN.aspx.cs" Inherits="RMC.Web.Users.WebForm3" Title="Reports : Time Study RN" %>

<%@ Register Src="~/UserControls/TimeRN.ascx" TagName="TimeRN" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:TimeRN ID="TimeRN1" runat="server" />
</asp:Content>
