<%@ Page Title="Admin Job Management" Language="C#" MasterPageFile="~/Master page/HomePage.Master" AutoEventWireup="true" CodeFile="adminPanel.aspx.cs" Inherits="practiceAWDPracticals.AdminPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .admin-container {
            width: 90%;
            margin: 30px auto;
            padding: 30px;
            background-color: var(--text-light);
            border-radius: 10px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
        }
        .admin-container h2 {
            color: var(--secondary-color);
            font-size: 2rem;
            margin-bottom: 20px;
            border-bottom: 2px solid var(--primary-color);
            padding-bottom: 10px;
        }
        .message-label { 
            display: block; 
            margin-bottom: 15px;
            font-weight: 600;
        }
        
        .admin-grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 1rem;
            margin-top: 20px;
        }
        .admin-grid th {
            background-color: var(--primary-color);
            color: var(--text-light);
            padding: 12px;
            text-align: left;
        }
        .admin-grid td {
            background-color: #fff;
            padding: 10px;
            border-bottom: 1px solid var(--border-color);
        }
        .admin-grid .alternatingRowStyle td {
            background-color: var(--background-light);
        }
        
        .admin-grid a {
            color: var(--primary-color);
            text-decoration: none;
            margin-right: 10px;
            font-weight: 500;
        }
        .admin-grid a:hover {
            text-decoration: underline;
        }

        .admin-grid .footerStyle td {
            background-color: #e6f7ff;
        }
        .admin-grid .footerStyle input[type="text"] {
            padding: 5px;
            border: 1px solid var(--border-color);
            border-radius: 3px;
            width: 90%;
        }
        .admin-grid .footerStyle a {
            color: var(--accent-color);
            font-weight: 600;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-container">
        <h2>Job Management Panel (Admin)</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="message-label" ForeColor="Red"></asp:Label>
        
        <asp:GridView ID="GridViewJobs" runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="Id" 
            OnRowEditing="GridViewJobs_RowEditing"
            OnRowCancelingEdit="GridViewJobs_RowCancelingEdit"
            OnRowUpdating="GridViewJobs_RowUpdating"
            OnRowDeleting="GridViewJobs_RowDeleting"
            OnRowCommand="GridViewJobs_RowCommand"
            CssClass="admin-grid"
            HeaderStyle-CssClass="headerStyle"
            RowStyle-CssClass="rowStyle"
            AlternatingRowStyle-CssClass="alternatingRowStyle"
            FooterStyle-CssClass="footerStyle"
            ShowFooter="True">

            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" SortExpression="Id" />
                
                <asp:TemplateField HeaderText="Job Title">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditJobTitle" runat="server" Text='<%# Bind("jobTitle") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblJobTitle" runat="server" Text='<%# Bind("jobTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNewJobTitle" runat="server" placeholder="Enter Title"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditJobDes" runat="server" Text='<%# Bind("jobDes") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblJobDes" runat="server" Text='<%# Bind("jobDes") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNewJobDes" runat="server" placeholder="Enter Description"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Actions">
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this job?');"></asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="btnInsert" runat="server" CommandName="InsertNew" Text="Add Job"></asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
