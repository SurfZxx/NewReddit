<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostPage.aspx.cs" Inherits="NewReddit.PostPage" %>

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
            <asp:Button ID="btnFeed" runat="server" Text="Feed" OnClick="btnFeed_Click" />
            <br /><br />
        </div>
        
        
        <asp:Repeater ID="RepeaterPost" runat="server" OnItemDataBound="RepeaterPosts_ItemDataBound">
            <ItemTemplate><div>
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Visible="False"><%# Eval("PostId") %></asp:Label><br />
    <asp:Label ID="lblUsername" runat="server" Font-Bold="True" Font-Italic="True"><%# Eval("Username") %></asp:Label><br />
    <asp:Label ID="lblPublishDate" runat="server" Font-Size="Small">(<%# Eval("PublishDate") %>)</asp:Label><br /><br />
    <asp:Label ID="lblPostContent" runat="server" BorderColor="Black" BorderStyle="Dotted" Font-Size="Large"><%# Eval("Content") %></asp:Label><br /><br />
    <asp:Button ID="btnLike" runat="server" Text="Like" CommandName="Like" OnCommand="btnLike_Command" CommandArgument='<%# Eval("PostId") %>' /><asp:Label ID="lblLikes" runat="server"></asp:Label>
    <asp:Button ID="btnDislike" runat="server" Text="Dislike" CommandName="Dislike" OnCommand="btnDislike_Command" CommandArgument='<%# Eval("PostId") %>' /><asp:Label ID="lblDislikes" runat="server" ></asp:Label>
    <br />
    <hr />
                </div>
     </ItemTemplate>
    </asp:Repeater><br /><br /><br />
        <h3>Comment</h3>
<asp:TextBox ID="txtCreateComment" runat="server" TextMode="MultiLine"></asp:TextBox><br />
<asp:Button ID="btnCreateComment" runat="server" Text="Comment" OnClick="btnCreateComment_Click" />
        <br />
        <br /><br />
        <br />
        <asp:Repeater ID="RepeaterComments" runat="server" >
            <ItemTemplate>
                <div>
                    <asp:Label ID="lblCommentUsername" runat="server" Font-Bold="True" Font-Italic="True"><%# Eval("Username") %></asp:Label><br />
                    <asp:Label ID="lblCommentPublishDate" runat="server" Font-Size="Small">(<%# Eval("PublishDate") %>)</asp:Label><br /><br />
                    <asp:Label ID="lblCommentContent" runat="server" BorderColor="Black" BorderStyle="Dotted" Font-Size="Large"><%# Eval("Content") %></asp:Label>
                     <br /><br /><hr />
                </div>
            </ItemTemplate>
        </asp:Repeater>



    </form>
</body>
</html>
