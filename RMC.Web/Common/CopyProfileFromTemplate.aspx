<%@ Page Title="RMC :: Category Profiles" Language="C#" AutoEventWireup="true"
    CodeBehind="CopyProfileFromTemplate.aspx.cs" Inherits="RMC.Web.Users.CopyProfileFromTemplate" %>

<%@ Register src="../UserControls/CopyProfileFromTemplate.ascx" tagname="CopyProfileFromTemplate" tagprefix="uc2" %>
<div>
    <table width="99%">
        <tr>
            <th style="font-size: 14px; color:#06569d; padding-left:10px; padding-top:10px;" align="left">
                    <u>Copy Profiles</u>
            </th>            
        </tr>
        <tr>
            <td>
                
                <uc2:CopyProfileFromTemplate ID="CopyProfileFromTemplate1" runat="server" />
                
            </td>
        </tr>
    </table>
</div>
