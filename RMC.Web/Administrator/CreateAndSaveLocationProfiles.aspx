<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="CreateAndSaveLocationProfiles.aspx.cs" Inherits="RMC.Web.Administrator.WebForm15"
    Title="RMC :: Create & Save Location Profile" %>

<%@ Register Src="../UserControls/CreateSaveLocationProfiles.ascx" TagName="CreateSaveLocationProfiles"
    TagPrefix="uc1" %>
<%@ Register src="../UserControls/CreateAndSaveLocationProfiles.ascx" tagname="CreateAndSaveLocationProfiles" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:CreateAndSaveLocationProfiles ID="CreateAndSaveLocationProfiles1" 
        runat="server" />
</asp:Content>
