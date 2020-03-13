<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Web_Project.Register" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table border="1" style="width:150px;background-color:#e0ebeb; margin:0 auto;">
        <tr bgcolor="#000000">
            <th colspan="2"><center><asp:Label ID="lblRegister" runat="server" Text="Register" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblEmail" runat="server" Text="Email" Width="150px"></asp:Label></b></center></td>
            <td><center><asp:TextBox ID="txtEmail" runat="server" Width="150px" autocomplete="off"></asp:TextBox></center></td>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblFirstName" runat="server" Text="First Name" Width="150px"></asp:Label></b></center></td>
            <td><center><asp:TextBox ID="txtFirstName" runat="server" Width="150px" autocomplete="off"></asp:TextBox></center></td>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblLastName" runat="server" Text="Last Name" Width="150px"></asp:Label></b></center></td>
            <td><center><asp:TextBox ID="txtLastName" runat="server" Width="150px" autocomplete="off"></asp:TextBox></center></td>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblGender" runat="server" Text="Gender" Width="150px"></asp:Label></b></center></td>
            <td>
                <center>
                    <asp:DropDownList ID="ddlGender" runat="server" AppendDataBoundItems="true" DataTextField="gender" DataValueField="gender" Width="100%">
                        <asp:ListItem Text="M" Value="M" />
                        <asp:ListItem Text="F" Value="F" />
                    </asp:DropDownList>
                </center>
            </td>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblPhone" runat="server" Text="Phone" Width="150px"></asp:Label></b></center></td>
            <td><center><asp:TextBox ID="txtPhone" runat="server" Width="150px" autocomplete="off"></asp:TextBox></center></td>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblPassport" runat="server" Text="Passport No" Width="150px"></asp:Label></b></center></td>
            <td><center><asp:TextBox ID="txtPassport" runat="server" Width="150px" autocomplete="off"></asp:TextBox></center></td>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblPassword" runat="server" Text="Password" Width="150px"></asp:Label></b></center></td>
            <td><center><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px" autocomplete="off"></asp:TextBox></center></td>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" Width="150px"></asp:Label></b></center></td>
            <td><center><asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="150px" autocomplete="off"></asp:TextBox></center></td>
        </tr>
        <tr bgcolor="#000000">
            <th colspan="2"><center><asp:Label ID="lblSecurityQuestion" runat="server" Text="Security Question" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
        </tr>
        <tr>
            <td colspan="2"><b><asp:Label ID="lblQuestionOne" runat="server" Text="Q1. What’s your hobby ?"></asp:Label></b></td>
        </tr>
        <tr>
            <td colspan="2"><asp:TextBox ID="txtQuestionOne" runat="server" autocomplete="off" Width="100%"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2"><b><asp:Label ID="lblQuestionTwo" runat="server" Text="Q2. What’s your favorite country ?"></asp:Label></b></td>
        </tr>
        <tr>
            <td colspan="2"><asp:TextBox ID="txtQuestionTwo" runat="server" autocomplete="off" Width="100%"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2"><b><asp:Label ID="lblQuestionThree" runat="server" Text="Q3. Where are you born?"></asp:Label></b></td>
        </tr>
        <tr>
            <td colspan="2"><asp:TextBox ID="txtQuestionThree" runat="server" autocomplete="off" Width="100%"></asp:TextBox></td>
        </tr>
        <tr bgcolor="#000000">
            <th colspan="2"><center><asp:Label ID="lblVerification" runat="server" Text="Verification" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
        </tr>
        <tr>
            <td><center><b><asp:Label ID="lblVerifyCode" runat="server" Text="Verify Code" Width="150px"></asp:Label></b></center></td>
            <td><center><asp:TextBox ID="txtVerfiyCode" runat="server" Width="150px" autocomplete="off"></asp:TextBox></center></td>
        </tr>
        <tr>
            <td colspan="2"><center><b><asp:Label ID="lblMessage" runat="server" Width="300px"></asp:Label></b></center></td>
        </tr>
        <tr>
            <td><center><asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" Width="150px"/></center></td>
            <td><center><asp:Button ID="btnRegister" runat="server" Text="Send Verify Email" OnClick="btnRegister_Click" Width="150px"/></center></td>
        </tr>
    </table>
</asp:Content>
