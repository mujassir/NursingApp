<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="SendNotification.aspx.cs" Inherits="RMC.Web.Administrator.SendNotification"
    Title="RMC :: Notification" %>

<%@ Register Src="../UserControls/NewsLetter.ascx" TagName="NewsLetter" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:NewsLetter ID="NewsLetter1" runat="server" />
</asp:Content>
