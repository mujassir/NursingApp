<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ValidationData.aspx.cs" Inherits="RMC.Web.Administrator.ValidationData" %>
<%@ Register src="../UserControls/Validation.ascx" tagname="Validations" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<uc1:Validations ID="Validation1" runat="server" /> 
</asp:Content>
