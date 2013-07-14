<%@ Page Title="RMC :: Hospital Unit Information" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="HospitalDemographicDetail.aspx.cs" Inherits="RMC.Web.Administrator.HospitalDemographicDetail" %>

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
    </table>
</asp:Content>
