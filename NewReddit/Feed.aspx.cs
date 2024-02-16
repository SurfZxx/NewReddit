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
                lblLoggedUser.Text = "Welcome " + Session["Username"].ToString();
                LoadPosts();
                
            }
        }

        private void LoadPosts()
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string query = "SELECT Posts.*, COUNT(Comments.CommentId) AS CommentCount " +
                                "FROM Posts LEFT JOIN Comments ON Posts.PostId = Comments.PostId " +
                                "GROUP BY Posts.PostId, Posts.Username, Posts.Content, Posts.Likes, Posts.Dislikes, Posts.PublishDate " +
                                "ORDER BY Posts.PublishDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    RepeaterPosts.DataSource = reader;
                    RepeaterPosts.DataBind();
                }
            }
        }

        protected void RepeaterPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Encontrar o rótulo para exibir o número de comentários
                Label lblCommentCount = (Label)e.Item.FindControl("lblCommentCount");

                // Obter o número de comentários do DataItem
                int commentCount = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "CommentCount"));

                // Exibir o número de comentários
                lblCommentCount.Text = commentCount.ToString();
            }
        }

        protected void btnLike_Click(object sender, EventArgs e)
        {

        }

        protected void btnDislike_Click(object sender, EventArgs e)
        {

        }

        protected void btnComment_Click(object sender, EventArgs e)
        {

        }
    }
}