<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DeleteHospital.aspx.cs" Inherits="RMC.Web.Administrator.WebForm3" Title="Untitled Page" %>
<%@ Register Src="~/UserControls/DeleteHospital.ascx" TagName="DeleteHospital" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <uc1:DeleteHospital ID="DeleteHospital1" runat="server" />
</asp:Content>
