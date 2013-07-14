<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ValidateData.aspx.cs" Inherits="RMC.Web.Users.ValidateData" Title="RMC :: Data Validation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <th>
                <h3>
                    Data Validation
                </h3>
            </th>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridViewDataValidation" runat="server" BackColor="#F79220" BorderColor="#F79220"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" EmptyDataText="No Record."
                    AutoGenerateColumns="False" HorizontalAlign="Center">
                    <RowStyle BackColor="#FFF7E7" ForeColor="#06569D" HorizontalAlign="Left" />
                    <EmptyDataRowStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                
            </td>
        </tr>
    </table>
</asp:Content>
