using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace NewReddit
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string name = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string password = txtPassword.Text;

            if (IsUsernameAvailable(username))
            {
                String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(CS))
                {
                    string query = "INSERT INTO Users (Username, FirstName, LastName, Password) VALUES (@Username, @FirstName, @LastName, @Password)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@FirstName", name);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Password", password);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                Response.Redirect("~/LoginPage.aspx");
            }
            else
            {
                LabelError.Text = "Username already in use. Please choose another.";
            }
            
        }

        private bool IsUsernameAvailable(string username)
        {
            String CS = ConfigurationManager.ConnectionStrings["desktop-torregtx.Reddit.dbo"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(CS))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username=@Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //abrir conexao e executar
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    
                    //return true se nao existe o username na table
                    return count == 0;
                }
            }
        }

        

        

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LoginPage.aspx");
        }
    }
}