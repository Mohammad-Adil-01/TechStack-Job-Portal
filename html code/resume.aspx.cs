using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace practiceAWDPracticals
{
    public partial class Resume : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mohammad Adil\Desktop\AWD\practiceAWDPracticals\App_Data\jobDatabase.mdf"";Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }

        protected void btnSaveResume_Click(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }

            string userEmail = User.Identity.Name;
            int userId = 0;
            string filePath = null;

            if (fileUploadResume.HasFile)
            {
                string extension = Path.GetExtension(fileUploadResume.FileName).ToLower();
                if (extension != ".pdf" && extension != ".docx")
                {
                    lblMessage.Text = "<span style='color:red;'>Error: Only PDF and DOCX files are allowed.</span>";
                    return;
                }

                try
                {
                    string folderPath = Server.MapPath("~/Resumes/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + extension;
                    filePath = "~/Resumes/" + uniqueFileName;

                    fileUploadResume.SaveAs(Server.MapPath(filePath));
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"<span style='color:red;'>File upload failed: {ex.Message}</span>";
                    return;
                }
            }

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
                    lblMessage.Text = "<span style='color:red;'>User identity error. Please re-login.</span>";
                    return;
                }

                string query = @"
                    IF EXISTS (SELECT 1 FROM resumes WHERE UserId = @UserId)
                        UPDATE resumes SET FullName=@FN, Contact=@Contact, Skills=@Skills, Experience=@Exp, FilePath=@FP 
                        WHERE UserId = @UserId
                    ELSE
                        INSERT INTO resumes (UserId, FullName, Contact, Skills, Experience, FilePath) 
                        VALUES (@UserId, @FN, @Contact, @Skills, @Exp, @FP)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@FN", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim());
                cmd.Parameters.AddWithValue("@Skills", txtSkills.Text.Trim());
                cmd.Parameters.AddWithValue("@Exp", txtExperience.Text.Trim());
                cmd.Parameters.AddWithValue("@FP", filePath ?? (object)DBNull.Value); // Saves NULL if no file uploaded

                cmd.ExecuteNonQuery();

                lblMessage.Text = "<span style='color:green;'>Resume details and file uploaded successfully!</span>";
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"<span style='color:red;'>Database Error: {ex.Message}</span>";
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
    }
}