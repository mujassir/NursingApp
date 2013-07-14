<%@ Page Title="RMC :: Hospital Details" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="HospitalDetail.aspx.cs" Inherits="RMC.Web.Administrator.HospitalDetail" %>

<%@ Register Src="../UserControls/HospitalInfo.ascx" TagName="HospitalInfo" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucMembersTreeView.ascx" TagName="ucMembersTreeView"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/HospitalEditRegistration.ascx" TagName="HospitalEditRegistration"
    TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <%-- <uc1:HospitalInfo ID="HospitalInfo1" runat="server" /> --%>
                <uc3:HospitalEditRegistration ID="HospitalEditRegistration1" runat="server" />
            </td>
        </tr>
        <%-- <tr>
            <td>
                <uc2:ucMembersTreeView ID="ucMembersTreeView1" runat="server" />
            </td>
        </tr>--%>
    </table>
</asp:Content>
