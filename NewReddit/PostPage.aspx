<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostPage.aspx.cs" Inherits="NewReddit.PostPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <div>
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Visible="False"><%# Eval("PostId") %></asp:Label><br />
    <asp:Label ID="lblUsername" runat="server" Font-Bold="True" Font-Italic="True"><%# Eval("Username") %></asp:Label><br />
    <asp:Label ID="lblPublishDate" runat="server" Font-Size="Small">(<%# Eval("PublishDate") %>)</asp:Label><br /><br />
    <asp:Label ID="lblPostContent" runat="server" BorderColor="Black" BorderStyle="Dotted" Font-Size="Large"><%# Eval("Content") %></asp:Label><br /><br />
    <asp:Button ID="btnLike" runat="server" Text="Like" CommandName="Like" OnCommand="btnLike_Command" CommandArgument='<%# Eval("PostId") %>' /><asp:Label ID="lblLikes" runat="server"></asp:Label>
    <asp:Button ID="btnDislike" runat="server" Text="Dislike" CommandName="Dislike" OnCommand="btnDislike_Command" CommandArgument='<%# Eval("PostId") %>' /><asp:Label ID="lblDislikes" runat="server" ></asp:Label>
    
    <br />
    <hr />
    
</div>
        </div>
    </form>
</body>
</html>
