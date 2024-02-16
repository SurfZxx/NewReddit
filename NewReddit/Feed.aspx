<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feed.aspx.cs" Inherits="NewReddit.Feed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblLoggedUser" runat="server" Text="" Font-Bold="True" Font-Italic="True"></asp:Label><br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Profile.aspx">Profile</asp:HyperLink>
            <br />
            <h1>Feed</h1><br />
        </div>
        <div>
            <asp:Repeater ID="RepeaterPosts" runat="server" OnItemDataBound="RepeaterPosts_ItemDataBound">
                <ItemTemplate>
                    <div>
                        <asp:Label ID="lblUsername" runat="server" Font-Bold="True" Font-Italic="True"><%# Eval("Username") %></asp:Label><br />
                        <asp:Label ID="lblPublishDate" runat="server" Font-Size="Small">(<%# Eval("PublishDate") %>)</asp:Label><br /><br />
                        <asp:Label ID="lblPostContent" runat="server" BorderColor="Black" BorderStyle="Dotted" Font-Size="Large"><%# Eval("Content") %></asp:Label><br /><br />
                        <asp:Button ID="btnLike" runat="server" Text="Like" OnClick="btnLike_Click" /><asp:Label ID="lblLikeCounter" runat="server"><%# Eval("Likes") %></asp:Label>
                        <asp:Button ID="btnDislike" runat="server" Text="Dislike" OnClick="btnDislike_Click" /><asp:Label ID="lblDislikeCounter" runat="server" ><%# Eval("Dislikes") %></asp:Label>
                        <asp:Button ID="btnComment" runat="server" Text="Comments" OnClick="btnComment_Click" /><asp:Label ID="lblCommentCount" runat="server" Text=""></asp:Label>
                        <br />
                        <hr />

                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
