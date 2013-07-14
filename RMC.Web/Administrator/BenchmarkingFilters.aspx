<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
 CodeBehind="BenchmarkingFilters.aspx.cs" Inherits="RMC.Web.Administrator.WebForm11" Title="BenchMarking Filters" %>
<%@ Register src="../UserControls/BenchmarkingFilters.ascx" tagname="BenchmarkingFilters" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:BenchmarkingFilters ID="BenchmarkingFilters1" runat="server" />
</asp:Content>
