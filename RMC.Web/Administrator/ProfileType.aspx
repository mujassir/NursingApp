<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ProfileType.aspx.cs" Inherits="RMC.Web.Administrator.WebForm1"
    Title="RMC :: Profile Type" %>
    
<%@ Register Src="../UserControls/ProfileType.ascx" TagName="ProfileType" TagPrefix="uc1"  %>    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:ProfileType ID="ProfileType1" runat="server" />
</asp:Content>
