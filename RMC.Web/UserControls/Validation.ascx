
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Validation.ascx.cs" Inherits="RMC.Web.UserControls.Validation" %>
<link href="../CSS/ControlStyles.css" rel="stylesheet" type="text/css" />


<form>
<table>
<tr>
    <td>
        <asp:LinkButton ID="LinkButton3" Font-Size="11px" runat="server" TabIndex="2" 
                    Font-Bold="true" onclick="LinkButton3_Click" >Export To Excel</asp:LinkButton>
    </td>
</tr>
<tr>
<td>
            <h3>Valid Data</h3>
        </td>
</tr>
    <tr>
        
        <td>
        <div id="batchList" style="height:200px;overflow: auto;" >

            <asp:GridView ID="GridValidData" runat="server" Width="450px"
            CellPadding="1" EmptyDataText="No Record to display." CssClass="GridViewStyle"
                HorizontalAlign="Center" 
                EnableModelValidation="True">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>
            </div>
        </td>
    </tr>
    <tr>
        <td style="height:30px;"></td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="LinkButton1" Font-Size="11px" runat="server" TabIndex="2" 
                        Font-Bold="true" onclick="LinkButton1_Click" >Export To Excel</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <h3>Invalid Data</h3>
        </td>
    </tr>
    <tr>
        
        <td>
        <div id="Div1" style="height:200px; width:500px; overflow: auto;" >
            <asp:GridView ID="GridViewInValiedData" runat="server" Width="450px"
            CellPadding="1" EmptyDataText="No Record to display." CssClass="GridViewStyle"
                HorizontalAlign="Center" 
                EnableModelValidation="True">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataRowStyle CssClass="GridViewEmptyRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <RowStyle CssClass="GridViewRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>
            
            </div>
            <div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
    </div>
        </td>
    </tr>
</table>
</form>
