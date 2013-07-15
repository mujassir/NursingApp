<%@ Page Title="RMC :: Email Sending" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SendEmail.aspx.cs" Inherits="RMC.Web.Administrator.SendEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="divcontent" runat="server">
        <div id="content">
            <h2>
                Email Sending</h2>
            <div id="firsttab">
                <p>
                    We welcome your support, involvement and comments and look forward to a continued
                    and close working relationship together.
                </p>
               
              
              
                   
            </div>
            <div id="seprater">
            </div>
            <div id="secondtab">
                <p>
                    We'd love to talk with you. You can fill out the form or contact us directly:
                </p>
                <p>
                    Fields marked (*) are required</p>
                <p>
                    Full Name:*<br>
                    <input name="Name" id="txtname" runat="server" size="35" type="text">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="email" SetFocusOnError="true" Font-Italic="true"
                        ForeColor="#cc0000" ControlToValidate="txtname" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </p>
                <p>
                    Phone:*<br>
                    <input name="Phone" id="txtphone" runat="server"  size="35" type="text">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="email" SetFocusOnError="true" Font-Italic="true"
                        ForeColor="#cc0000" ControlToValidate="txtphone" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </p>
                <p>
                    Email From:*<br>
                    <input name="EmailFrom" id="txtemail" runat="server"  size="35" type="text">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="email" SetFocusOnError="true" Font-Italic="true"
                        ForeColor="#cc0000" ControlToValidate="txtemail" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </p>
                 <p>
                    Email To:*<br>
                    <input name="EmailFrom" id="txtemailto" runat="server"  size="35" type="text">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  ValidationGroup="email" SetFocusOnError="true" Font-Italic="true"
                        ForeColor="#cc0000" ControlToValidate="txtemail" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </p>
                <p>
                    Comments:*
                    <br>
                    <textarea name="Comments" id="txtcoment" runat="server"  cols="40" rows="6"></textarea>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="email" Font-Italic="true" SetFocusOnError="true"
                        Style="vertical-align: top" ForeColor="#cc0000" ControlToValidate="txtcoment"
                        runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Button ID="btnsend" runat="server" ValidationGroup="email" Text="Send" OnClick="btnsend_Click" />&nbsp;
                    <asp:Label ID="lblmessage" runat="server" Text="" ForeColor="#CC0000"></asp:Label></p>
            </div>
        </div>
    </div>
</asp:Content>
