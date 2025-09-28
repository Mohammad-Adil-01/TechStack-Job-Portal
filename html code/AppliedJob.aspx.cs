using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security; 

namespace practiceAWDPracticals
{
    public partial class AppliedJobs : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mohammad Adil\Desktop\AWD\practiceAWDPracticals\App_Data\jobDatabase.mdf"";Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    litLoginPrompt.Visible = false; 
                    BindAppliedJobs();              
                }
                else
                {
                    litLoginPrompt.Visible = true; 
                    rptAppliedJobs.Visible = false;
                    litNoRecords.Visible = false; 
                }
            }
        }

        private void BindAppliedJobs()
        {
           
            string userEmail = User.Identity.Name;
            SqlCommand cmd = null;

            try
            {
                
                string query = @"
                    SELECT
                        j.jobTitle AS JobTitle,
                        a.ApplicationDate AS ApplicationDate,
                        a.Status AS Status
                    FROM
                        applications a
                    INNER JOIN
                        jobs j ON a.JobId = j.Id
                    INNER JOIN
                        users u ON a.UserId = u.Id
                    WHERE
                        u.Email = @Email
                    ORDER BY 
                        a.ApplicationDate DESC";

                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", userEmail);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                
                rptAppliedJobs.DataSource = dr;
                rptAppliedJobs.DataBind();

                dr.Close();

                
                if (rptAppliedJobs.Items.Count == 0)
                {
                    rptAppliedJobs.Visible = false;
                    litNoRecords.Visible = true;
                }
                else
                {
                    rptAppliedJobs.Visible = true;
                    litNoRecords.Visible = false;
                }
            }
            catch (Exception ex)
            {
              
                litLoginPrompt.Text = $"<p style='color:red;'>Error loading applications: {ex.Message}</p>";
                litLoginPrompt.Visible = true;
                rptAppliedJobs.Visible = false;
                litNoRecords.Visible = false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}