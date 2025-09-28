using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web;

namespace practiceAWDPracticals
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mohammad Adil\Desktop\AWD\practiceAWDPracticals\App_Data\jobDatabase.mdf"";Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindJobs();
            }

            if (User.Identity.IsAuthenticated)
            {
                user_detail.Visible = false;
            }
            else
            {
                user_detail.Visible = true;
            }
        }

        private void BindJobs(string searchTerm = null)
        {
            try
            {
                string query = "SELECT * FROM jobs";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += " WHERE jobTitle LIKE @SearchTerm OR jobDes LIKE @SearchTerm";
                }

                SqlCommand cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                }

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                rptJobs.DataSource = dr;
                rptJobs.DataBind();

                dr.Close();
            }
            catch (Exception ex)
            {
                // Optionally log error
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            string email = loginEmail.Text.Trim();
            string password = loginPassword.Text.Trim();

            try
            {
                string query = "SELECT COUNT(1) FROM users WHERE Email=@Email AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();

                if (count == 1)
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid credentials!');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Login Error: " + ex.Message.Replace("'", "") + "');", true);
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            BindJobs(searchId.Text.Trim());
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string jobId = btn.CommandArgument;

            if (!User.Identity.IsAuthenticated)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please log in to apply for a job.');", true);
                return;
            }

            string userEmail = User.Identity.Name;
            int userId = 0;

            try
            {
                conn.Open();

                SqlCommand getUserCmd = new SqlCommand("SELECT Id FROM users WHERE Email = @Email", conn);
                getUserCmd.Parameters.AddWithValue("@Email", userEmail);
                object result = getUserCmd.ExecuteScalar();

                if (result != null)
                {
                    userId = Convert.ToInt32(result);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User not found in database.');", true);
                    return;
                }

                string insertQuery = "INSERT INTO applications (UserId, JobId) VALUES (@UserId, @JobId)";
                SqlCommand applyCmd = new SqlCommand(insertQuery, conn);
                applyCmd.Parameters.AddWithValue("@UserId", userId);
                applyCmd.Parameters.AddWithValue("@JobId", jobId);

                applyCmd.ExecuteNonQuery();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Application submitted successfully! Check Applied Job\\'s page.');", true);
            }
            catch (SqlException sqlex) when (sqlex.Number == 2627) 
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You have already applied for this job.');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Application Error: " + ex.Message.Replace("'", "") + "');", true);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
    }
}