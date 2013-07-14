<%@ Page Language="C#" MasterPageFile="~/Administrator/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="LocationProfile.aspx.cs" Inherits="RMC.Web.Administrator.WebForm15"
    Title="Reports::From/To Trips Report" %>

<%@ Register Src="../UserControls/LocationProfile.ascx" TagName="LocationProfile"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:LocationProfile ID="LocationProfile1" runat="server" />
</asp:Content>
