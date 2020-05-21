<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client_Home.aspx.cs" Inherits="Web_Project.Client_Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        body { 
            background-image: url('Images/client_bg.jpg');
            background-repeat:no-repeat;
            background-attachment:fixed;
            background-size: 100% 100%
        }
    </style>
    <body>
        <div class="jumbotron">
            <h2><asp:Label ID="lblLogin" runat="server"></asp:Label></h2>
        </div>
        <div>
            <table border="1" style="width:24%;background-color:#e0ebeb">
                <tr bgcolor="#000000">
                    <th colspan="2"><center><asp:Label ID="lblInquiry" runat="server" Text="Inquiry" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
                </tr>
                <tr>
                    <td style="width:35%;"><b><center><asp:Label ID="lblIssueSubject" runat="server" Text="Issue Subject"></asp:Label></center></b></td>
                    <td style="width:65%;"><asp:TextBox ID="txtSubject" runat="server" Width="100%" autocomplete="off" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><b><center><asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label></center></b></td>
                    <td>
                        <center>
                            <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" DataTextField="category" DataValueField="category" Width="100%">
                                <asp:ListItem Text="Login" Value="Login" />
                                <asp:ListItem Text="Booking" Value="Booking" />
                                <asp:ListItem Text="Price" Value="Price" />
                                <asp:ListItem Text="Package" Value="Package" />
                                <asp:ListItem Text="Other" Value="Other" />
                            </asp:DropDownList>
                        </center>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <b><asp:Label ID="lblMessage" runat="server" Width="100%" Text="Message"></asp:Label></b><br />
                        <asp:TextBox ID="txtMessage" runat="server" Width="100%" Height="150px" autocomplete="off" TextMode="MultiLine" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"><center><b><asp:Label ID="lblAlert" runat="server" Width="100%"></asp:Label></b></center></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="btnSend" runat="server" Text="Send" Width="100%" OnClick="btnSend_Click"/></td>
                </tr>
            </table>
        </div>
    </body>
</asp:Content>