<%@ Page Language="C#" MasterPageFile="~/Administrator/MasterPageValidData.Master" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="RMC.Web.Administrator.WebForm4" Title="Untitled Page" %>
<%@ Register src="../UserControls/ShowSpecialTypeData.ascx" tagname="ShowSpecialTypeData" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ShowSpecialTypeData ID="ShowSpecialTypeData1" runat="server" />
</asp:Content>
