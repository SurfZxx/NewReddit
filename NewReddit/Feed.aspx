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
            <br /><br /><br /><br />
            <asp:Button ID="ButtonLogout" runat="server" Text="Logout" OnClick="ButtonLogout_Click" />
            <br /><br /><br /><br />
            <h3>Create a Post</h3>
            <asp:TextBox ID="txtCreatePost" runat="server" TextMode="MultiLine"></asp:TextBox><br />
            <asp:Button ID="btnCreatePost" runat="server" Text="Post" OnClick="btnCreatePost_Click" />
            <h1>Feed</h1><br />
        </div>
        <div>
            <asp:Repeater ID="RepeaterPosts" runat="server" OnItemDataBound="RepeaterPosts_ItemDataBound">
                <ItemTemplate>
                    <div>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Visible="False"><%# Eval("PostId") %></asp:Label><br />
                        <asp:Label ID="lblUsername" runat="server" Font-Bold="True" Font-Italic="True"><%# Eval("Username") %></asp:Label><br />
                        <asp:Label ID="lblPublishDate" runat="server" Font-Size="Small">(<%# Eval("PublishDate") %>)</asp:Label><br /><br />
                        <asp:Label ID="lblPostContent" runat="server" BorderColor="Black" BorderStyle="Dotted" Font-Size="Large"><%# Eval("Content") %></asp:Label><br /><br />
                        <asp:Button ID="btnLike" runat="server" Text="Like" CommandName="Like" OnCommand="btnLike_Command" CommandArgument='<%# Eval("PostId") %>' /><asp:Label ID="lblLikes" runat="server"></asp:Label>
                        <asp:Button ID="btnDislike" runat="server" Text="Dislike" CommandName="Dislike" OnCommand="btnDislike_Command" CommandArgument='<%# Eval("PostId") %>' /><asp:Label ID="lblDislikes" runat="server" ></asp:Label>
                        <asp:Button ID="btnComment" runat="server" Text="Comments" CommandName="Comment" CommandArgument='<%# Eval("PostID") %>' OnCommand="btnComment_Command" /><asp:Label ID="lblCommentCount" runat="server" Text=""></asp:Label>
                        <br />
                        <hr />
                        
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
       
    </form>
    
</body>
</html>
