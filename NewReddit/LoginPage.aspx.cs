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
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;


            if (UsernameExists(username))
            {
                string storedPassword = GetStoredPassword(username, password);
                if (storedPassword == password)
                {
                    Session["Username"] = username;
                    Response.Redirect("~/Feed.aspx");
                }
                else
                {
                    lblError.Text = "Incorrect password.";
                }
            }
            else
            {
                lblError.Text = "Username does not exist.";
            }
            
        }


        private bool UsernameExists(String username)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using(SqlConnection conn = new SqlConnection(CS)) 
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username=@Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    
                    //return true se o user existir na table Users
                    return count > 0;
                }
                 
            }
        }


        private string GetStoredPassword(String username, String password)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string query = "SELECT Password FROM Users WHERE Username=@Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    
                    //return true se o user existir na table Users
                    return (string)cmd.ExecuteScalar();
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RegisterPage.aspx");
        }
    }
}