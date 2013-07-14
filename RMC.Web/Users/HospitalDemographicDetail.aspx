<%@ Page Title="RMC :: Hospital Unit Informatiion" Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="HospitalDemographicDetail.aspx.cs" Inherits="RMC.Web.Users.HospitalDemographicDetail" %>

<%@ Register Src="../UserControls/ucDemographicMembersTreeView.ascx" TagName="ucDemographicMembersTreeView"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/RequestApproval.ascx" TagName="RequestApproval"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControls/HospitalDemographicInfo.ascx" TagName="HospitalDemographicInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td>
                <uc1:HospitalDemographicInfo ID="HospitalDemographicInfo1" runat="server" />
            </td>
        </tr>
        <%--<tr>
            <td>
                <uc2:ucDemographicMembersTreeView ID="ucDemographicMembersTreeView1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <uc3:RequestApproval ID="RequestApproval1" runat="server" />
            </td>
        </tr>--%>
    </table>
</asp:Content>
