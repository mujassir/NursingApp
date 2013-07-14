<%@ Page Title="RMC :: Users List" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="GetUserList.aspx.cs" Inherits="RMC.Web.Administrator.GetUserList" %>

<%@ Register Src="../UserControls/GetUsers.ascx" TagName="GetUsers" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc1:GetUsers ID="GetUsers1" runat="server" />
</asp:Content>
