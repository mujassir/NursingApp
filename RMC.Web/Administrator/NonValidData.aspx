<%@ Page Title="RMC :: Non-Valid Data" Language="C#" MasterPageFile="~/Administrator/Admin.Master" AutoEventWireup="true"
    CodeBehind="NonValidData.aspx.cs" Inherits="RMC.Web.Administrator.NonValidData" %>

<%@ Register Src="../UserControls/ViewNonValid.ascx" TagName="ViewNonValid" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ViewNonValid ID="ViewNonValid1" runat="server" />
</asp:Content>
