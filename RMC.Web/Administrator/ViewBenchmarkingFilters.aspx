<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ViewBenchmarkingFilters.aspx.cs" Inherits="RMC.Web.Administrator.WebForm13"
    Title="RMC :: Filter Detail" %>

<%@ Register src="../UserControls/ViewBenchmarkingFilters.ascx" tagname="ViewBenchmarkingFilters" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ViewBenchmarkingFilters ID="ViewBenchmarkingFilters1" runat="server" />
</asp:Content>
