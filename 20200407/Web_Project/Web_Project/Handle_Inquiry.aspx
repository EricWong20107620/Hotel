<%@ Page Title="Handle Inquiry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Handle_Inquiry.aspx.cs" Inherits="Web_Project.Handle_Inquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div>
        <table border="1" style="width:90px;background-color:#e0ebeb">
            <tr>
                <td colspan="3"><center><asp:Button ID="btnAutoClose" runat="server" Text="Auto Close Inquiry" Width="100%" OnClick="btnAutoClose_Click"/></center></td>
            </tr>
            <tr>
                <th><center><b><asp:Label ID="lblFilterStatus" runat="server" Text="Status" Width="90px"></asp:Label></b></center></th>
			    <td>
				    <center>
					    <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" DataTextField="status" DataValueField="status">
						    <asp:ListItem Text="ALL" Value="%" />
						    <asp:ListItem Text="Replied" Value="R" />
						    <asp:ListItem Text="Pending" Value="P" />
						    <asp:ListItem Text="Closed" Value="C" />
					    </asp:DropDownList>
				    </center>
			    </td>
			    <td><center><asp:Button ID="btnFilter" runat="server" Text="Filter" Width="90px" OnClick="btnFilter_Click" /></center></td>
            </tr>
        </table>
    </div>
    <div>
        <table style="width:100%">
            <tr>
                <td style="width:76%">
                    <asp:Panel ID="pHeader" runat="server" ScrollBars="Vertical" Width="95%">
                    <table style="width:100%;background-color:#e0ebeb">
                        <tr>
                            <td style="width:20%"><center><b><asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label></b></center></td>
                            <td style="width:20%"><center><b><asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label></b></center></td>
                            <td style="width:20%"><center><b><asp:Label ID="lblCreateDate" runat="server" Text="Create Date"></asp:Label></b></center></td>
                            <td style="width:20%"><center><b><asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label></b></center></td>
                            <td style="width:20%"><center><b><asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label></b></center></td>
                        </tr>
                    </table>
                    </asp:Panel>
                    <asp:Panel ID="pMaster" runat="server" Height="300px" ScrollBars="Vertical" Width="95%">
                        <asp:HiddenField ID="hfIssueId2" runat="server" Value='<%# Eval("issue_ID") %>'/>
                        <asp:DataList ID="InquiryList" runat="server" Width="100%" OnItemCommand="InquiryList_ItemCommand">
                            <ItemTemplate>
                            <asp:HiddenField ID="hfIssueId" runat="server" Value='<%# Eval("issue_ID") %>'/>
                            <table style="width:100%">
                                <tr>
                                    <td style="width:20%"><center><asp:Label ID="lblSubject2" runat="server" Text='<%# Eval("issue_subject") %>'></asp:Label></center></td>
                                    <td style="width:20%"><center><asp:Label ID="lblCategory2" runat="server" Text='<%# Eval("issue_category") %>'></asp:Label></center></td>
                                    <td style="width:20%"><center><asp:Label ID="lblCreateDate2" runat="server" Text='<%# Eval("create_date") %>'></asp:Label></center></td>
                                    <td style="width:20%"><center><asp:Label ID="lblStatus2" runat="server" Text='<%# Eval("inquiry_status") %>'></asp:Label></center></td>
                                    <td style="width:20%"><center><asp:LinkButton ID="btnViewDetail" runat="server">View Detail</asp:LinkButton></center></td>
                                </tr>
                            </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </asp:Panel>
                </td>
                <td style="width:24%">
                    <table border="1" style="width:100%;background-color:#e0ebeb">
                        <tr bgcolor="#000000">
                            <th colspan="2"><center><asp:Label ID="lblInquiry" runat="server" Text="Inquiry" Font-Size="Large" ForeColor="White"></asp:Label></center></th>
                        </tr>
                        <tr>
                            <td style="width:35%;"><b><center><asp:Label ID="lblIssueSubject" runat="server" Text="Issue Subject"></asp:Label></center></b></td>
                            <td style="width:65%;"><asp:TextBox ID="txtSubject" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><b><center><asp:Label ID="lblCategory3" runat="server" Text="Category"></asp:Label></center></b></td>
                            <td><asp:TextBox ID="txtCategory" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><b><center><asp:Label ID="lblStatus3" runat="server" Text="Status"></asp:Label></center></b></td>
                            <td><asp:TextBox ID="txtStatus" runat="server" Width="100%" autocomplete="off" MaxLength="50" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pDetail" runat="server" Height="150px" ScrollBars="Vertical" Width="100%">
                                    <asp:DataList ID="InquiryDetail" runat="server" Width="100%">
                                        <ItemTemplate>
                                        <asp:HiddenField ID="hfIssueId2" runat="server" Value='<%# Eval("issue_ID") %>'/>
                                        <table style="width:100%">
                                            <tr>
                                                <td style="width:100%"><asp:Label ID="lblDetail" runat="server" Text='<%# Eval("message_content") %>'></asp:Label></td>
                                            </tr>
                                        </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <b><asp:Label ID="lblMessage" runat="server" Width="100%" Text="Message"></asp:Label></b><br />
                                <asp:TextBox ID="txtMessage" runat="server" Width="100%" Height="100px" autocomplete="off" TextMode="MultiLine" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><center><b><asp:Label ID="lblAlert" runat="server" Width="100%"></asp:Label></b></center></td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:Button ID="btnSend" runat="server" Text="Send" Width="100%" OnClick="btnSend_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>