<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DeleteNursePDAInfo.aspx.cs" Inherits="RMC.Web.Administrator.DeleteNursePDAInfo" %>
<%@ Register src="../UserControls/DeleteNursePDAInfo.ascx" tagname="DeleteNursePDAInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <p>
        <uc1:DeleteNursePDAInfo ID="DeleteNursePDAInfo1" runat="server" />
    </p>
    
</asp:Content>
