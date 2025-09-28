using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace practiceAWDPracticals
{
    public partial class Signup : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mohammad Adil\Desktop\AWD\practiceAWDPracticals\App_Data\jobDatabase.mdf"";Integrated Security=True");

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblMessage.Text = "Passwords do not match!";
                return;
            }

            try
            {
                // 1. Check if email already exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM users WHERE Email = @Email", conn);
                checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                conn.Open();
                int emailCount = (int)checkCmd.ExecuteScalar();
                conn.Close();

                if (emailCount > 0)
                {
                    lblMessage.Text = "This email is already registered.";
                    return;
                }

                // 2. Insert new user
                string query = "INSERT INTO users (Username, Password, Email, UserRole) VALUES (@Username, @Password, @Email, @UserRole)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim()); // Use Hashing in real app!
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@UserRole", "JobSeeker");

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                // Registration successful
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Registration successful! You can now log in.'); window.location='home.aspx';", true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Registration Error: " + ex.Message;
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }
    }
}