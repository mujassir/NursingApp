<%@ Page Language="C#" MasterPageFile="~/Administrator/PopupMasterPage.Master" AutoEventWireup="true"
 CodeBehind="ReportTimeRNChartsPie.aspx.cs" Inherits="RMC.Web.Administrator.WebForm9" Title="Untitled Page" %>

<%--<%@ Register src="../UserControls/ReportTimeRNChartsPie.ascx" tagname="ReportTimeRNChartsPie" tagprefix="uc1" %>--%>


<%@ Register src="../UserControls/ReportTimeRNChartsPie.ascx" tagname="ReportTimeRNChartsPie" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div style="display:inline-block; width:100%; text-align:center; border:1px green solid; margin:0 auto;">


    <uc1:ReportTimeRNChartsPie ID="ReportTimeRNChartsPie" runat="server" />

    </div>
</asp:Content>
