<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="FiltersList.aspx.cs" Inherits="RMC.Web.Administrator.WebForm12" Title="RMC :: Filters List" %>

<%@ Register Src="../UserControls/FiltersList.ascx" TagName="FiltersList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc1:FiltersList ID="FiltersList1" runat="server" />
</asp:Content>
