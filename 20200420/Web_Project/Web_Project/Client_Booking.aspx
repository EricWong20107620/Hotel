<%@ Page Title="Client Booking" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client_Booking.aspx.cs" Inherits="Web_Project.Client_Booking" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div>
        <table border="1" style="width:100%;background-color:#e0ebeb; margin:0 auto;">
            <tr bgcolor="#000000">
                <th><center><asp:Label ID="lblStandardRoom" runat="server" Text="Standard Room" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
                <th><center><asp:Label ID="lblDeluxeRoom" runat="server" Text="Deluxe Room" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
                <th><center><asp:Label ID="lblStudioRoom" runat="server" Text="Studio Room" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
            </tr>
            <tr>
                <th><center><asp:Label ID="lblStandardRoomAmount" runat="server" Text="Choose check in date first" Font-Size="Large"></asp:Label></center></th>
                <th><center><asp:Label ID="lblDeluxeRoomAmount" runat="server" Text="Choose check in date first" Font-Size="Large"></asp:Label></center></th>
                <th><center><asp:Label ID="lblStudioRoomAmount" runat="server" Text="Choose check in date first" Font-Size="Large"></asp:Label></center></th>
            </tr>
        </table>
    </div>
    <br />
    <div>
        <table border="1" style="width:100%;background-color:#e0ebeb; margin:0 auto;">
            <tr bgcolor="#000000">
                <th colspan="4"><center><asp:Label ID="lblBooking" runat="server" Text="Booking Room" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
            </tr>
            <tr>
                <td rowspan="21" style="width:50%">asddas</td>
                <td style="width:26%"><center><b><asp:Label ID="lblCheckInDate" runat="server" Text="Check-in Date" Width="150px"></asp:Label></b></center></td>
                <td style="width:24%"><center><asp:TextBox ID="txtCheckInDate" runat="server" Width="100%" autocomplete="off" MaxLength="50" TextMode="Date" OnTextChanged="txtCheckInDate_TextChanged" AutoPostBack="True"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblCheckOutDate" runat="server" Text="Check-out Date" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtCheckOutDate" runat="server" Width="100%" autocomplete="off" MaxLength="50" TextMode="Date" OnTextChanged="txtCheckOutDate_TextChanged" AutoPostBack="True"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblRoomType" runat="server" Text="Room Type" Width="150px"></asp:Label></b></center></td>
                <td>
                    <center>
                        <asp:DropDownList ID="ddlRoomType" runat="server" AutoPostBack="True" AppendDataBoundItems="true" DataTextField="roomType" DataValueField="roomType" Width="100%" OnSelectedIndexChanged="ddlRoomType_SelectedIndexChanged">
                            <asp:ListItem Text="--SELECT--" Value="0" />
                            <asp:ListItem Text="Standard" Value="1" />
                            <asp:ListItem Text="Deluxe" Value="2" />
                            <asp:ListItem Text="Studio" Value="3" />
                        </asp:DropDownList>
                    </center>
                </td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblBedType" runat="server" Text="Bed Type" Width="100%"></asp:Label></b></center></td>
                <td>
                    <center>
                        <asp:DropDownList ID="ddlBedType" runat="server" AppendDataBoundItems="true" DataTextField="bedType" DataValueField="bedType" Width="100%">
                            <asp:ListItem Text="King Bed" Value="King Bed" />
                            <asp:ListItem Text="Twin Bed" Value="Twin Bed" />
                        </asp:DropDownList>
                    </center>
                </td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblMaxOccupancy" runat="server" Text="Max Occupancy" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtMaxOccupancy" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblRoomSize" runat="server" Text="Room Size" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtRoomSize" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblBathTub" runat="server" Text="Bath Tub" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtBathTub" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblDesignerChair" runat="server" Text="Designer Chair" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtDesignerChair" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblInternet" runat="server" Text="Internet" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtInternet" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false" TextMode="MultiLine" Height="47px"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblSmartTV" runat="server" Text="Smart TV" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtSmartTV" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblHairDeyer" runat="server" Text="Hair Deyer" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtHairDeyer" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblInRoomSafe" runat="server" Text="In Room Safe" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtInRoomSafe" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblMiniFridge" runat="server" Text="Mini Fridge" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtMiniFridge" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false" TextMode="MultiLine" Height="47px"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblReplenished" runat="server" Text="Replenished" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtReplenished" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblHouseKeeping" runat="server" Text="House Keeping" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtHouseKeeping" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblBedSofa" runat="server" Text="Bed Sofa" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtBedSofa" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false" TextMode="MultiLine" Height="47px"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblOptionalBreakfast" runat="server" Text="Optional Breakfast" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtOptionalBreakfast" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblPricePerDay" runat="server" Text="Price per day" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtPricePerDay" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
             <tr>
                <td><center><b><asp:Label ID="lblTotalPrice" runat="server" Text="Total Price" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtTotalPrice" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></center></td>
            </tr>
             <tr>
                <td><center><b><asp:Label ID="lblCreditCard" runat="server" Text="Credit Card" Width="150px"></asp:Label></b></center></td>
                <td><center><asp:TextBox ID="txtCreditCard" runat="server" Width="100%" autocomplete="off" MaxLength="50"></asp:TextBox></center></td>
            </tr>
            <tr>
                <td><center><b><asp:Label ID="lblRemark" runat="server" Width="100%" Text="Remark"></asp:Label></b></center></td>
                <td><asp:TextBox ID="txtRemark" runat="server" Width="100%" Height="150px" autocomplete="off" TextMode="MultiLine" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="3"><center><b><asp:Label ID="lblMessage" runat="server" Width="100%"></asp:Label></b></center></td>
            </tr>
        </table>
    </div>
    <br />
    <div>
    <table style="width:100%;margin:0 auto">
        <tr>
            <td><center><asp:Button ID="btnBooking" runat="server" Text="Booking" Width="100%" OnClick="btnBooking_Click"/></center></td>
        </tr>
    </table>
    </div>
</asp:Content>
