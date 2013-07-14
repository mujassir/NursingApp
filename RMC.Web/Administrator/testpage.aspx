<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="testpage.aspx.cs" Inherits="RMC.Web.Administrator.testpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:CHART id="Chart1" runat="server" BackColor="239, 230, 247" Width="500px" 
        Height="320px" BorderDashStyle="Solid" BackGradientStyle="TopBottom" 
        BorderWidth="2px" BorderColor="#B54001">
                            <titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Nursing Time By Activity Per 12 Hour shift" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
                            </titles>
                            <legends>
                                <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="false" Name="Default"></asp:Legend>
                            </legends>
                            <borderskin SkinStyle="Emboss"></borderskin>
                            <series>
                               <asp:Series YValueType="String" XValueType="String" Name="FullName" IsValueShownAsLabel="true" IsVisibleInLegend="false" />                           
                                <asp:Series XValueType="Double" IsValueShownAsLabel="true" LegendText="Billable hours"  ChartType="StackedColumn" Color="LightGreen"
                                    Name="Billable" BorderColor="180, 26, 59, 105" CustomProperties="DrawingStyle=Cylinder" ShadowColor="DarkGreen">
                                </asp:Series>
                                <asp:Series XValueType="Double" IsValueShownAsLabel="true" Legend="non Billable hours" ChartType="StackedColumn" Color="LightCoral"
                                    Name="nonBillable" BorderColor="180, 26, 59, 105" CustomProperties="DrawingStyle=Cylinder" ShadowColor="Transparent">
                                </asp:Series>
                            </series>
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1"  BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="#EFE6F7" ShadowColor="DarkRed" BackGradientStyle="TopBottom">
                                    <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                                    <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8" Title="Minutes">
                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"  />
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisy>
                                    <axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8" Title="Members">
                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IsEndLabelVisible="False"  />
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </axisx>
                                </asp:ChartArea>
                            </chartareas>
                        </asp:CHART>
</asp:Content>
