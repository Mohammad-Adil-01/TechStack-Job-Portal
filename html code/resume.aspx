<%@ Page Title="" Language="C#" MasterPageFile="~/Master page/HomePage.Master" AutoEventWireup="true" CodeBehind="resume.aspx.cs" Inherits="practiceAWDPracticals.Resume" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .resume-container {
            width: 80%;
            max-width: 900px;
            margin: 50px auto;
            padding: 40px;
            background-color: var(--text-light); /* White background */
            border-radius: 10px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
            border-top: 5px solid var(--primary-color);
        }

        .resume-container h2 {
            color: var(--secondary-color);
            font-size: 2rem;
            font-weight: 700;
            margin-bottom: 30px;
            text-align: center;
        }

        .form-section {
            display: flex;
            flex-direction: column;
            gap: 20px;
        }

        .form-group {
            display: flex;
            flex-direction: column;
        }

        .form-group label {
            font-weight: 600;
            margin-bottom: 5px;
            color: var(--text-dark);
            font-size: 1rem;
        }

        .input-text,
        .input-textarea {
            padding: 10px;
            border: 1px solid var(--border-color);
            border-radius: 5px;
            font-size: 1rem;
            background-color: var(--background-light); /* Light gray input background */
            transition: border-color 0.3s;
        }

        .input-text:focus,
        .input-textarea:focus {
            border-color: var(--primary-color);
            outline: none;
        }

        .input-textarea {
            resize: vertical;
            min-height: 100px;
        }
        
        /* File Upload Styling */
        .file-upload-section {
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 20px;
            border: 2px dashed var(--border-color);
            border-radius: 8px;
            margin-top: 15px;
            background-color: #fcfcfc;
        }
        .file-upload-section input[type="file"] {
            margin-top: 10px;
        }

        /* Submit Button */
        .submit-btn {
            padding: 15px;
            background-color: var(--primary-color);
            color: var(--text-light);
            border: none;
            border-radius: 5px;
            font-size: 1.1rem;
            cursor: pointer;
            font-weight: 600;
            margin-top: 30px;
            transition: background-color 0.3s;
        }

        .submit-btn:hover {
            background-color: #006666;
        }
        
        .message-label {
            margin-top: 15px;
            text-align: center;
            font-size: 1rem;
            font-weight: 600;
        }
        
        /* Responsive Adjustments */
        @media (max-width: 768px) {
            .resume-container {
                width: 95%;
                padding: 20px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="resume-container">
        <h2>Your Professional Resume</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="message-label"></asp:Label>
        
        <div class="form-section">
            <div class="form-group">
                <label for="<%= txtFullName.ClientID %>">Full Name:</label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="input-text" placeholder="e.g., Jane Doe"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="<%= txtContact.ClientID %>">Contact Number:</label>
                <asp:TextBox ID="txtContact" runat="server" CssClass="input-text" TextMode="Phone" placeholder="e.g., 9876543210"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="<%= txtSkills.ClientID %>">Key Skills (Comma Separated):</label>
                <asp:TextBox ID="txtSkills" runat="server" CssClass="input-textarea" TextMode="MultiLine" placeholder="e.g., C#, ASP.NET, SQL, JavaScript, HTML5"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="<%= txtExperience.ClientID %>">Work Experience/Summary:</label>
                <asp:TextBox ID="txtExperience" runat="server" CssClass="input-textarea" TextMode="MultiLine" placeholder="Summarize your professional experience..."></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Upload Existing CV/Resume (PDF/DOCX):</label>
                <div class="file-upload-section">
                    <asp:FileUpload ID="fileUploadResume" runat="server" />
                </div>
            </div>
            
            <asp:Button ID="btnSaveResume" runat="server" Text="Save & Upload Resume" CssClass="submit-btn" OnClick="btnSaveResume_Click" />
        </div>
    </div>
</asp:Content>
