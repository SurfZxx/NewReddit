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
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    lblLoggedUser.Text = "Welcome " + Session["Username"].ToString();
                    lblUserProfile.Text = Session["Username"].ToString();
                    LoadPosts(Session["Username"].ToString());
                }
                else
                {
                    Response.Redirect("~/LoginPage.aspx");
                }

            }
        }


        private void LoadPosts(string username)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string query = "SELECT *" +
                                "FROM Posts " +
                                "WHERE Username=@Username " +
                                "ORDER BY Posts.PublishDate DESC";
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    RepeaterPosts.DataSource = reader;
                    RepeaterPosts.DataBind();
                }
            }
        }

        protected void btnFeed_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Feed.aspx");
        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Response.Redirect("~/LoginPage.aspx");
        }
    }
}