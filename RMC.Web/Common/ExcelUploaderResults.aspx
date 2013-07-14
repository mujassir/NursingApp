<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ExcelUploaderResults.aspx.cs" Inherits="RMC.Web.Users.ExcelUploaderResults"
    Title="Uploaded File List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  

    <div style="width: 600px;">
        <table>
            <tr>
                <td>
                    <table id="trSuccessfully" runat="server">
                        <tr>
                            <td style="vertical-align: top; width: 600px; font-size: 14px; font-weight: bold;
                                color: #06569d;">
                                Successfully uploaded file list
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="ListBoxSuccess" Height="150px" Width="100%" runat="server"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <table id="trUnsuccessfully" runat="server">
                        <tr>
                            <td style="vertical-align: top; width: 600px; font-size: 14px; font-weight: bold;
                                color: #06569d;">
                                Unsuccessfully uploaded file list
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="ListBoxUnsuccess" Height="150px" Width="100%" runat="server"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 5px; padding-top: 10px;">
                    <asp:Button ID="ButtonBack" runat="server" Text="Back" OnClick="ButtonBack_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
