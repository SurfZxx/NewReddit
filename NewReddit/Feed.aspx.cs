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
    public partial class Feed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    lblLoggedUser.Text = "Welcome " + Session["Username"].ToString();
                    LoadPosts();
                }
                else
                {
                    Response.Redirect("~/LoginPage.aspx");
                }
                
            }
        }

        private void LoadPosts()
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                /*string query = "SELECT Posts.*, COUNT(Comments.CommentId) AS CommentCount, COUNT(PostVotes.) " +
                               "FROM Posts LEFT JOIN Comments ON Posts.PostId = Comments.PostId " +
                                "GROUP BY Posts.PostId, Posts.Username, Posts.Content, Posts.PublishDate " +
                                "ORDER BY Posts.PublishDate DESC";*/
                string query = "SELECT Posts.*, " +
                       "(SELECT COUNT(*) FROM PostVotes WHERE PostVotes.PostID = Posts.PostID AND VoteType = 1) AS Likes, " +
                       "(SELECT COUNT(*) FROM PostVotes WHERE PostVotes.PostID = Posts.PostID AND VoteType = -1) AS Dislikes, " +
                       "(SELECT COUNT(*) FROM Comments WHERE Comments.PostID = Posts.PostID) AS CommentCount " +
                       "FROM Posts " +
                       "ORDER BY Posts.PublishDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    RepeaterPosts.DataSource = reader;
                    RepeaterPosts.DataBind();
                    conn.Close();
                }
            }
        }

        protected void RepeaterPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Encontrar o rótulo para exibir o número de comentários
                Label lblCommentCount = (Label)e.Item.FindControl("lblCommentCount");
                Label lblLikes = (Label)e.Item.FindControl("lblLikes");
                Label lblDislikes = (Label)e.Item.FindControl("lblDislikes");

                // Obter o número de comentários do DataItem
                int commentCount = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "CommentCount"));
                int likesCount = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Likes"));
                int dislikesCount = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Dislikes"));

                // Exibir o número de comentários
                lblCommentCount.Text = commentCount.ToString();
                lblLikes.Text = likesCount.ToString();
                lblDislikes.Text = dislikesCount.ToString();
            }
        }

        protected void btnLike_Command(object sender, CommandEventArgs e)
        {
            int postID = Convert.ToInt32(e.CommandArgument);
            string username = Session["Username"].ToString();
            AddLike(postID, username);
            LoadPosts();
        }

        protected void AddLike(int postID, string username)
        {
            username = Session["Username"].ToString();

            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string query = "INSERT INTO PostVotes(PostId,Username,VoteType) VALUES (@PostId, @Username, 1)";
                using (SqlCommand cmd = new SqlCommand(query,con))
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
            AddDislike(postID, username);
            LoadPosts();
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

        protected void btnComment_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Response.Redirect("~/LoginPage.aspx");
        }

        protected void btnCreatePost_Click(object sender, EventArgs e)
        {
            string posttext = txtCreatePost.Text;
            string username = Session["Username"].ToString();
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                string query = "INSERT INTO Posts(Content, Username) VALUES(@posttext,@Username)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@posttext",posttext);
                    cmd.Parameters.AddWithValue("@Username",username);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Response.Redirect("~/Feed.aspx");
        }
    }
}