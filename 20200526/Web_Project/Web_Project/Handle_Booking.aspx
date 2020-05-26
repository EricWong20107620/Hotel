<%@ Page Title="Handle Booking" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Handle_Booking.aspx.cs" Inherits="Web_Project.Handle_Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        body { 
            background-image: url('Images/staff_bg.jpg');
            background-repeat:no-repeat;
            background-attachment:fixed;
            background-size: 100% 100%
        }
    </style>
    <body>
        <br />
        <div>
            <table border="1" style="width:100%;background-color:#e0ebeb; margin:0 auto;">
                <tr bgcolor="#000000">
                    <th><center><asp:Label ID="lblStandardRoom" runat="server" Text="Standard Room" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
                    <th><center><asp:Label ID="lblDeluxeRoom" runat="server" Text="Deluxe Room" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
                    <th><center><asp:Label ID="lblStudioRoom" runat="server" Text="Studio Room" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
                </tr>
                <tr>
                    <th><center><asp:Label ID="lblStandardRoomAmount" runat="server" Text="Choose date range first" Font-Size="Large"></asp:Label></center></th>
                    <th><center><asp:Label ID="lblDeluxeRoomAmount" runat="server" Text="Choose date range first" Font-Size="Large"></asp:Label></center></th>
                    <th><center><asp:Label ID="lblStudioRoomAmount" runat="server" Text="Choose date range first" Font-Size="Large"></asp:Label></center></th>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <asp:Button ID="btnFilter" runat="server" Text="Show all booking" Width="100%" OnClick="btnFilter_Click"/>
        </div>
        <br />
        <div>
            <table style="width:100%">
                <tr>
                    <td style="width:50%">
                        <asp:Panel ID="pHeader" runat="server" ScrollBars="Vertical" Width="95%">
                        <table style="width:100%;background-color:#e0ebeb">
                            <tr>
                                <td style="width:25%"><center><b><asp:Label ID="lblTotalRoom" runat="server" Text="Total Room"></asp:Label></b></center></td>
                                <td style="width:25%"><center><b><asp:Label ID="lblCheckInDate" runat="server" Text="Check In Date"></asp:Label></b></center></td>
                                <td style="width:25%"><center><b><asp:Label ID="lblCheckOutDate" runat="server" Text="Check Out Date"></asp:Label></b></center></td>
                                <td style="width:25%"><center><b><asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label></b></center></td>
                            </tr>
                        </table>
                        </asp:Panel>
                        <asp:Panel ID="pMaster" runat="server" Height="300px" ScrollBars="Vertical" Width="95%" BackColor="White">
                            <asp:HiddenField ID="hfBookingId2" runat="server" Value='<%# Eval("booking_id") %>'/>
                            <asp:DataList ID="BookingList" runat="server" Width="100%" OnItemCommand="BookingList_ItemCommand">
                                <ItemTemplate>
                                <asp:HiddenField ID="hfBookingId" runat="server" Value='<%# Eval("booking_id") %>'/>
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:25%"><center><asp:Label ID="lblTotalRoom2" runat="server" Text='<%# Eval("total_room") %>'></asp:Label></center></td>
                                        <td style="width:25%"><center><asp:Label ID="lblCheckInDate2" runat="server" Text='<%# Eval("check_in_date") %>'></asp:Label></center></td>
                                        <td style="width:25%"><center><asp:Label ID="lblCheckOutDate3" runat="server" Text='<%# Eval("check_out_date") %>'></asp:Label></center></td>
                                        <td style="width:25%"><center><asp:LinkButton ID="btnViewDetail" runat="server">View Detail</asp:LinkButton></center></td>
                                    </tr>
                                </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </asp:Panel>
                    </td>
                    <td style="width:50%">
                        <div>
                            <table border="1" style="width:400px;background-color:#e0ebeb; margin:0 auto">
                                    <tr bgcolor="#000000">
                                        <th colspan="2"><center><asp:Label ID="lblBooking" runat="server" Text="Room Booking" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
                                    </tr>
                                    <tr>
                                        <td style="width:50%"><center><b><asp:Label ID="lblCheckInDate3" runat="server" Text="Check-in Date" Width="150px"></asp:Label></b></center></td>
                                        <td style="width:50%"><center><asp:TextBox ID="txtCheckInDate" runat="server" Width="100%" autocomplete="off" MaxLength="50" TextMode="Date" OnTextChanged="txtCheckInDate_TextChanged" AutoPostBack="True"></asp:TextBox></center></td>
                                    </tr>
                                    <tr>
                                        <td><center><b><asp:Label ID="lblCheckOutDate3" runat="server" Text="Check-out Date" Width="150px"></asp:Label></b></center></td>
                                        <td><center><asp:TextBox ID="txtCheckOutDate" runat="server" Width="100%" autocomplete="off" MaxLength="50" TextMode="Date" OnTextChanged="txtCheckOutDate_TextChanged" AutoPostBack="True"></asp:TextBox></center></td>
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
                                        <td><center><b><asp:Label ID="lblStandardRoomAmount2" runat="server" Text="Standard Room Amount" Width="200px"></asp:Label></b></center></td>
                                        <td>
                                            <center>
                                                <asp:DropDownList ID="ddlStandardRoomAmount" runat="server" AutoPostBack="True" AppendDataBoundItems="true" DataTextField="StandardRoomAmount" DataValueField="StandardRoomAmount" Width="100%" OnSelectedIndexChanged="ddlStandardRoomAmount_SelectedIndexChanged">
                                                    <asp:ListItem Text="0" Value="0" />
                                                    <asp:ListItem Text="1" Value="1" />
                                                    <asp:ListItem Text="2" Value="2" />
                                                    <asp:ListItem Text="3" Value="3" />
                                                </asp:DropDownList>
                                            </center>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><center><b><asp:Label ID="lblDeluxeRoomAmount2" runat="server" Text="Deluxe Room Amount" Width="200px"></asp:Label></b></center></td>
                                        <td>
                                            <center>
                                                <asp:DropDownList ID="ddlDeluxeRoomAmount" runat="server" AutoPostBack="True" AppendDataBoundItems="true" DataTextField="DeluxeRoomAmount" DataValueField="DeluxeRoomAmount" Width="100%" OnSelectedIndexChanged="ddlDeluxeRoomAmount_SelectedIndexChanged">
                                                    <asp:ListItem Text="0" Value="0" />
                                                    <asp:ListItem Text="1" Value="1" />
                                                    <asp:ListItem Text="2" Value="2" />
                                                    <asp:ListItem Text="3" Value="3" />
                                                </asp:DropDownList>
                                            </center>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><center><b><asp:Label ID="lblStudioRoomAmount2" runat="server" Text="Studio Room Amount" Width="200px"></asp:Label></b></center></td>
                                        <td>
                                            <center>
                                                <asp:DropDownList ID="ddlStudioRoomAmount" runat="server" AutoPostBack="True" AppendDataBoundItems="true" DataTextField="StudioRoomAmount" DataValueField="StudioRoomAmount" Width="100%" OnSelectedIndexChanged="ddlStudioRoomAmount_SelectedIndexChanged">
                                                    <asp:ListItem Text="0" Value="0" />
                                                    <asp:ListItem Text="1" Value="1" />
                                                    <asp:ListItem Text="2" Value="2" />
                                                    <asp:ListItem Text="3" Value="3" />
                                                </asp:DropDownList>
                                            </center>
                                        </td>
                                    </tr>
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
                                        <td><asp:TextBox ID="txtRemark" runat="server" Width="100%" Height="80px" autocomplete="off" TextMode="MultiLine" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"><center><b><asp:Label ID="lblMessage" runat="server" Width="100%"></asp:Label></b></center></td>
                                    </tr>
                            </table>
                        </div>
                        <br />
                        <div>
                        <table style="width:100%;margin:0 auto">
                            <tr>
                                <td><center><asp:Button ID="btnUpdate" runat="server" Text="Update" Width="100%" OnClick="btnUpdate_Click" Visible="false"/></center></td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </body>
</asp:Content>