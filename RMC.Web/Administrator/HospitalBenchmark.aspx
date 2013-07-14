<%@ Page Language="C#" MasterPageFile="~/Administrator/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="HospitalBenchmark.aspx.cs" Inherits="RMC.Web.Administrator.WebForm2"
    Title="Reports :: Collaboration Report" %>

<%@ Register Src="~/UserControls/HospitalBenchmark.ascx" TagName="HospitalBenchmark"
    TagPrefix="uc1" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:HospitalBenchmark ID="HospitalBenchmark1" runat="server" />
</asp:Content>
