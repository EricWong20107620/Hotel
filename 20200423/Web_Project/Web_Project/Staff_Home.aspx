<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Staff_Home.aspx.cs" Inherits="Web_Project.Staff_Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h2><asp:Label ID="lblLogin" runat="server"></asp:Label></h2>
    </div>
    <div>
        <table border="1" style="width:24%;">
            <tr bgcolor="#000000">
                <th colspan="2"><center><asp:Label ID="lblInquiry" runat="server" Text="Inquiry" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
            </tr>
            <tr>
                <td style="width:50%;"><b><center><asp:Label ID="lblNeedReplyInquiry" runat="server" Text="Need reply inquiry"></asp:Label></center></b></td>
                <td style="width:50%;"><b><center><asp:Label ID="lblCountNeedReplyInquiry" runat="server"></asp:Label></center></b></td>
            </tr>
            <tr>
                <td><b><center><asp:Label ID="lblNotYetCloseInquirry" runat="server" Text="Not yet close inquiry (Over 10 days)"></asp:Label></center></b></td>
                <td><b><center><asp:Label ID="lblCountNotYetCloseInquirry" runat="server"></asp:Label></center></b></td>
            </tr>
        </table>
    </div>
</asp:Content>