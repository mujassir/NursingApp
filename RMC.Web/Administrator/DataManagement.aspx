<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="DataManagement.aspx.cs" Inherits="RMC.Web.Administrator.DataManagement"
    Title="RMC :: Data Management" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../UserControls/DataManagement.ascx" TagName="DataManagement" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc1:DataManagement ID="DataManagement1" runat="server" />
</asp:Content>
