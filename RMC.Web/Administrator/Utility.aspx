<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Utility.aspx.cs" Inherits="RMC.Web.Administrator.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table>
    <tr>
        <td><asp:Button ID="Button1" runat="server" Text="Maintain documents in folder structure " onclick="Button1_Click" /></td>
    </tr>
    <tr>
        <td><h3>
        A utility that creates the directory structure on the hard drive.<br/>
And move the existing raw data files into the directory structure.<br/>
Rename the files for their existing randomized file name to their original file name.</h3></td>
    </tr>
</table>
</asp:Content>
