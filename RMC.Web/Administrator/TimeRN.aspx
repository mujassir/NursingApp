<%@ Page Language="C#" MasterPageFile="~/Administrator/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="TimeRN.aspx.cs" Inherits="RMC.Web.Administrator.WebForm5" Title="Reports :: Time Study RN" %>

<%@ Register Src="~/UserControls/TimeRN.ascx" TagName="TimeRM" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:TimeRM ID="TimeRM1" runat="server" />
</asp:Content>
