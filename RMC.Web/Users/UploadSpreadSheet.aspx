<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="UploadSpreadSheet.aspx.cs" Inherits="RMC.Web.Users.UploadSpreadSheet"
    Title="RMC :: Upload Spread Sheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table width="100%">
            <tr>
                <th>
                    <h3 style="font-size: 13px;">
                        Upload Spreed Sheet
                    </h3>
                </th>
            </tr>
            <tr>
                <td>
                    <div style="text-align: left; width: 100%; float: left;">
                        <asp:FileUpload ID="FileUploadSpreadSheet" runat="server"/>&nbsp
                        <span style="vertical-align: top; width:100%;">Please upload files with the extension .sda only.</span>
                        <div style="text-align: left; width: 100%; float: left; padding-top: 20px;">
                            <asp:Button ID="ButtonUpload" runat="server" Text="Upload File" 
                                OnClick="ButtonUpload_Click" CssClass="aspButton" />
                        </div>
                        <div style="text-align: left; width: 100%; float: left;">
                            <asp:Label ID="LabelErrorMsg" runat="server" Font-Size="12px" Visible="False"></asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
