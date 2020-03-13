<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web_Project.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table border="1" style="width:150px;background-color:#e0ebeb; margin:0 auto">
        <tr bgcolor="#000000">
            <th colspan="2"><center><asp:Label ID="lblLogin" runat="server" Text="Login" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblEmail" runat="server" Text="Email" Width="150px"></asp:Label></b></center></td>
            <td><center><asp:TextBox ID="txtEmail" runat="server" Width="150px" autocomplete="off"></asp:TextBox></center></td>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblPassword" runat="server" Text="Password" Width="150px"></asp:Label></b></center></td>
            <td><center><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px" autocomplete="off"></asp:TextBox></center></td>
        </tr>
        <tr bgcolor="#000000">
            <th colspan="2"><center><asp:Label ID="lblSecurityQuestion" runat="server" Text="Security Question" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
        </tr>
        <tr>
            <td colspan="2"><b><asp:Label ID="lblQuestion" runat="server"></asp:Label></b></td>
        </tr>
        <tr>
            <td colspan="2"><asp:TextBox ID="txtQuestion" runat="server" autocomplete="off" Width="100%"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2"><center><b><asp:Label ID="lblMessage" runat="server" Width="300px"></asp:Label></b></center></td>
        </tr>
        <tr>
            <td><center><asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" Width="150px"/></center></td>
            <td><center><asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" Width="150px"/></center></td>
        </tr>
    </table>
</asp:Content>
