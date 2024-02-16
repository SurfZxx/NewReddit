<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="NewReddit.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblLoggedUser" runat="server" Text=""></asp:Label><br /><br />
<br />
        </div>
        <div title="Profile">
            <br />
            <asp:Label ID="lblUserProfile" runat="server" Text="" Font-Bold="True" Font-Italic="True" Font-Size="XX-Large"></asp:Label><br /><br />
        </div>
        <div>
            <asp:Repeater ID="RepeaterPosts" runat="server" >
                <ItemTemplate>
                <div>
                    <asp:Label ID="lblUsername" runat="server" Font-Bold="True" Font-Italic="True"><%# Eval("Username") %></asp:Label><br />
                    <asp:Label ID="lblPublishDate" runat="server" Font-Size="Small">(<%# Eval("PublishDate") %>)</asp:Label><br /><br />
                    <asp:Label ID="lblPostContent" runat="server" BorderColor="Black" BorderStyle="Dotted" Font-Size="Large"><%# Eval("Content") %></asp:Label><br /><br />
                    <br />
                    <hr />
                 </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
    </form>
</body>
</html>
