using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;

namespace practiceAWDPracticals
{
    public partial class AdminPanel : System.Web.UI.Page
    {
        private const string ADMIN_EMAIL = "mohammadadil@gmail.com"; 

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mohammad Adil\Desktop\AWD\practiceAWDPracticals\App_Data\jobDatabase.mdf"";Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // CRITICAL SECURITY CHECK
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                    return;
                }

                string loggedInUserEmail = User.Identity.Name;

                if (loggedInUserEmail.Equals(ADMIN_EMAIL, StringComparison.OrdinalIgnoreCase))
                {
                    BindGridView();
                }
                else
                {
                    Response.Redirect("~/html code/home.aspx?error=unauthorized");
                    return;
                }
            }
        }

        private void BindGridView()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Id, jobTitle, jobDes FROM jobs", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conn.Open();
                da.Fill(dt);

               
                GridViewJobs.DataSource = dt;
                GridViewJobs.DataBind();
                lblMessage.Text = ""; 
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading jobs: " + ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        protected void GridViewJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "InsertNew")
            {
                TextBox txtTitle = (TextBox)GridViewJobs.FooterRow.FindControl("txtNewJobTitle");
                TextBox txtDes = (TextBox)GridViewJobs.FooterRow.FindControl("txtNewJobDes");

                if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtDes.Text))
                {
                    lblMessage.Text = "Job Title and Description are required for insertion.";
                    return;
                }

                try
                {
                    string insertQuery = "INSERT INTO jobs (jobTitle, jobDes) VALUES (@Title, @Des)";
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Des", txtDes.Text.Trim());

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "New job added successfully!";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error adding job: " + ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }

                BindGridView();
            }
        }
        protected void GridViewJobs_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewJobs.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridViewJobs_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewJobs.EditIndex = -1;
            BindGridView();
        }

        protected void GridViewJobs_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int jobId = Convert.ToInt32(GridViewJobs.DataKeys[e.RowIndex].Value);

            TextBox txtTitle = (TextBox)GridViewJobs.Rows[e.RowIndex].FindControl("txtEditJobTitle");
            TextBox txtDes = (TextBox)GridViewJobs.Rows[e.RowIndex].FindControl("txtEditJobDes");

            try
            {
                string updateQuery = "UPDATE jobs SET jobTitle = @Title, jobDes = @Des WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@Des", txtDes.Text.Trim());
                cmd.Parameters.AddWithValue("@Id", jobId);

                conn.Open();
                cmd.ExecuteNonQuery();
                lblMessage.Text = $"Job ID {jobId} updated successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating job: " + ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            GridViewJobs.EditIndex = -1;
            BindGridView();
        }

        protected void GridViewJobs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int jobId = Convert.ToInt32(GridViewJobs.DataKeys[e.RowIndex].Value);

            try
            {
                conn.Open();

                SqlCommand deleteAppsCmd = new SqlCommand("DELETE FROM applications WHERE JobId = @Id", conn);
                deleteAppsCmd.Parameters.AddWithValue("@Id", jobId);
                deleteAppsCmd.ExecuteNonQuery();

                SqlCommand deleteJobCmd = new SqlCommand("DELETE FROM jobs WHERE Id = @Id", conn);
                deleteJobCmd.Parameters.AddWithValue("@Id", jobId);
                deleteJobCmd.ExecuteNonQuery();

                lblMessage.Text = $"Job ID {jobId} and its applications deleted successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting job: " + ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            BindGridView();
        }
    }
}