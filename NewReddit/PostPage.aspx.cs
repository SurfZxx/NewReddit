using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewReddit
{
    public partial class PostPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
                if (Session["Username"] != null)
                {
                    if (Request.QueryString["postId"] != null)
                    {
                        lblLoggedUser.Text = "Welcome " + Session["Username"].ToString();
                        int postId = Convert.ToInt32(Request.QueryString["postId"]);
                        LoadPost(postId);
                        LoadComments(postId);
                    }
                    
                }
                else
                {
                    Response.Redirect("~/LoginPage.aspx");
                }

            }
        }


        protected void LoadPost(int postId)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS)) 
            {
                /*string query = "SELECT Posts.*, 
                 * (SELECT COUNT(*) FROM PostVotes WHERE PostVotes.PostID = Posts.PostID AND VoteType = 1) AS Likes, 
                 * (SELECT COUNT(*) FROM PostVotes WHERE PostVotes.PostID = Posts.PostID AND VoteType = -1) AS Dislikes, " +
                                "FROM Posts " +
                                "WHERE PostId = @PostId";*/
                string query = "SELECT Posts.*, " +
                       "(SELECT COUNT(*) FROM PostVotes WHERE PostVotes.PostID = Posts.PostID AND VoteType = 1) AS Likes, " +
                       "(SELECT COUNT(*) FROM PostVotes WHERE PostVotes.PostID = Posts.PostID AND VoteType = -1) AS Dislikes, " +
                       "(SELECT COUNT(*) FROM Comments WHERE Comments.PostID = Posts.PostID) AS CommentCount " +
                       "FROM Posts " +
                       "WHERE PostId = @PostId";
                using (SqlCommand cmd = new SqlCommand(query,conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    SqlDataReader r = cmd.ExecuteReader();
                    RepeaterPost.DataSource = r;
                    RepeaterPost.DataBind();
                    conn.Close();
                }
            }
        }

        protected void RepeaterPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Encontrar o rótulo para exibir o número de comentários
                Label lblLikes = (Label)e.Item.FindControl("lblLikes");
                Label lblDislikes = (Label)e.Item.FindControl("lblDislikes");

                // Obter o número de comentários do DataItem
                
                int likesCount = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Likes"));
                int dislikesCount = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Dislikes"));

                // Exibir o número de comentários
                lblLikes.Text = likesCount.ToString();
                lblDislikes.Text = dislikesCount.ToString();
            }
        }

        protected void LoadComments(int postId)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string query = "SELECT Username, Content, PublishDate " +                                
                                "FROM Comments " +
                                "WHERE PostId = @PostId";
                using (SqlCommand cmd = new SqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    RepeaterComments.DataSource = reader;
                    RepeaterComments.DataBind();
                    conn.Close();
                }
            }
        }

        protected void btnLike_Command(object sender, CommandEventArgs e)
        {
            int postID = Convert.ToInt32(e.CommandArgument);
            string username = Session["Username"].ToString();
            if (!isITLiked(postID, username) && !isITDisliked(postID, username))
            {
                AddLike(postID, username);
                LoadPost(postID);
            }
            else if (!isITLiked(postID, username) && isITDisliked(postID, username))
            {
                RemoveLike(postID, username);
                AddLike(postID, username);
                LoadPost(postID);
            }
            else if (isITLiked(postID, username))
            {
                RemoveLike(postID, username);
                LoadPost(postID);
            }

        }

        protected bool isITLiked(int postId, string username)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string query = "SELECT COUNT(*) FROM PostVotes WHERE PostId = @PostId AND Username = @Username AND VoteType=1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    return count > 0;
                }
            }
        }

        protected void AddLike(int postID, string username)
        {
            username = Session["Username"].ToString();

            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string query = "INSERT INTO PostVotes(PostId,Username,VoteType) VALUES (@PostId, @Username, 1)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PostId", postID);
                    cmd.Parameters.AddWithValue("@Username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        protected void btnDislike_Command(object sender, CommandEventArgs e)
        {
            int postID = Convert.ToInt32(e.CommandArgument);
            string username = Session["Username"].ToString();
            if (!isITDisliked(postID, username) && !isITLiked(postID, username))
            {
                AddDislike(postID, username);
                LoadPost(postID);
            }
            else if (!isITDisliked(postID, username) && isITLiked(postID, username))
            {
                RemoveLike(postID, username);
                AddDislike(postID, username);
                LoadPost(postID);
            }
            else if (isITDisliked(postID, username))
            {
                RemoveLike(postID, username);
                LoadPost(postID);
            }

        }

        protected bool isITDisliked(int postId, string username)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string query = "SELECT COUNT(Username) FROM PostVotes WHERE PostId = @PostId AND Username = @Username AND VoteType=-1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    return count > 0;
                }
            }
        }

        protected void AddDislike(int postID, string username)
        {
            username = Session["Username"].ToString();

            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string query = "INSERT INTO PostVotes(PostId,Username,VoteType) VALUES (@PostId, @Username, -1)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PostId", postID);
                    cmd.Parameters.AddWithValue("@Username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


        protected void RemoveLike(int postId, string username)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string query = "DELETE FROM PostVotes WHERE Username=@Username AND PostId=@PostId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    cmd.Parameters.AddWithValue("@Username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }



        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Response.Redirect("~/LoginPage.aspx");
        }

        protected void btnCreateComment_Click(object sender, EventArgs e)
        {
            string commenttext = txtCreateComment.Text;
            string username = Session["Username"].ToString();
            int postId = Convert.ToInt32(Request.QueryString["postId"]);
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                string query = "INSERT INTO Comments(Content, Username, PostId) VALUES(@commenttext,@Username, @PostId)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@commenttext", commenttext);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //Page_Load(sender, e);
            //LoadComments(postId);
            Response.Redirect($"PostPage.aspx?postId={postId}");
        }

        protected void btnFeed_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Feed.aspx");
        }


        /*protected void RepeaterComments_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblCommentLikes = (Label)e.Item.FindControl("lblCommentLikes");
                Label lblCommentDislikes = (Label)e.Item.FindControl("lblCommentDislikes");

                int likesCount = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Likes"));
                int dislikesCount = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Dislikes"));

                lblCommentLikes.Text = likesCount.ToString();
                lblCommentDislikes.Text = dislikesCount.ToString();
            }
        }*/

        /*
        protected void btnCommentLike_Command(object sender, CommandEventArgs e)
        {
            int commentID = Convert.ToInt32(e.CommandArgument);
            string username = Session["Username"].ToString();
            if (!CommentisITLiked(commentID, username) && !CommentisITDisliked(commentID, username))
            {
                CommentAddLike(commentID, username);
                LoadPost(commentID);
            }
            else if (!CommentisITLiked(commentID, username) && CommentisITDisliked(commentID, username))
            {
                CommentRemoveLike(commentID, username);
                CommentAddLike(commentID, username);
                LoadPost(commentID);
            }
            else if (CommentisITLiked(commentID, username))
            {
                CommentRemoveLike(commentID, username);
                LoadPost(commentID);
            }

        }

        protected void btnCommentDislike_Command(object sender, CommandEventArgs e)
        {
            int commentId = Convert.ToInt32(e.CommandArgument);
            string username = Session["Username"].ToString();
            if (!CommentisITDisliked(commentId, username) && !CommentisITLiked(commentId, username))
            {
                AddCommentDislike(commentId, username);
                LoadPost(commentId);
            }
            else if (!CommentisITDisliked(commentId, username) && CommentisITLiked(commentId, username))
            {
                CommentRemoveLike(commentId, username);
                AddCommentDislike(commentId, username);
                LoadPost(commentId);
            }
            else if (CommentisITDisliked(commentId, username))
            {
                CommentRemoveLike(commentId, username);
                LoadPost(commentId);
            }

        }

        protected bool CommentisITDisliked(int commentId, string username)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string query = "SELECT COUNT(Username) FROM CommentVotes WHERE CommentId = @CommentId AND Username = @Username AND VoteType=-1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CommentId", commentId);
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    return count > 0;
                }
            }
        }

        protected void AddCommentDislike(int commentId, string username)
        {
            username = Session["Username"].ToString();

            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string query = "INSERT INTO CommentVotes(CommentId,Username,VoteType) VALUES (@CommentId, @Username, -1)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CommentId", commentId);
                    cmd.Parameters.AddWithValue("@Username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


        protected void CommentRemoveLike(int commentId, string username)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string query = "DELETE FROM CommentVotes WHERE Username=@Username AND CommentId=@CommentId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CommentId", commentId);
                    cmd.Parameters.AddWithValue("@Username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


        protected bool CommentisITLiked(int commentId, string username)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string query = "SELECT COUNT(*) FROM CommentVotes WHERE CommentId = @CommentId AND Username = @Username AND VoteType=1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CommentId", commentId);
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    return count > 0;
                }
            }
        }

        protected void CommentAddLike(int commentId, string username)
        {
            username = Session["Username"].ToString();

            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string query = "INSERT INTO CommentVotes(CommentId,Username,VoteType) VALUES (@CommentId, @Username, 1)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CommentId", commentId);
                    cmd.Parameters.AddWithValue("@Username", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }*/



    }
}