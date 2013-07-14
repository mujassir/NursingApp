<%@ Page Title="RMC :: Validation Table" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="ValidationTable.aspx.cs" Inherits="RMC.Web.Administrator.ValidationTable" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../UserControls/ValidationData.ascx" TagName="ValidationData" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <th align="left" style="color:#06569d; font-size:14px; padding-left:10px; padding-top:10px;">
                <%--<h3 style="font-size: 13px;">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back To Data Management" runat="server"
                                    ImageUrl="~/Images/back.gif" PostBackUrl="~/Users/DataManagement.aspx" OnClick="ImageButtonBack_Click" />
                            </td>
                            <td align="center">
                                <span>--%>
                                <u>Validation Table</u><%--</span>
                            </td>
                        </tr>
                    </table>
                </h3>--%>
            </th>
        </tr>
        <tr>
            <td align="center">
                <uc1:ValidationData ID="ValidationData1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
