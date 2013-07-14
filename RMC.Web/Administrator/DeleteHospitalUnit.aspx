<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DeleteHospitalUnit.aspx.cs" Inherits="RMC.Web.Administrator.DeleteHospitalUnit" Title="Delete Hospital Units" %>
<%@ Register src="../UserControls/HospitalUnitInformationDeletion.ascx" tagname="HospitalUnitInformationDeletion" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:HospitalUnitInformationDeletion ID="HospitalUnitInformationDeletion1" 
        runat="server" />

</asp:Content>
