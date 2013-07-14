<%@ Page Language="C#" MasterPageFile="~/Administrator/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="StandardizeLocationName.aspx.cs" Inherits="RMC.Web.Administrator.WebForm17"
    Title="RMC::Location Name" %>

<%@ Register src="../UserControls/StandardizeLocationName.ascx" tagname="StandardizeLocationName" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StandardizeLocationName ID="StandardizeLocationName1" runat="server" />
</asp:Content>
