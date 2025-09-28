<%@ Page Title="Applied Jobs" Language="C#" MasterPageFile="~/Master page/HomePage.Master" AutoEventWireup="true" CodeBehind="AppliedJobs.aspx.cs" Inherits="practiceAWDPracticals.AppliedJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link rel="stylesheet" href="../CSS/applied.css" type="text/css" runat="server" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="applied-jobs-container">
        <h2>Your Job Applications</h2>
        
        <asp:Literal ID="litLoginPrompt" runat="server" Text="<p>Please log in to view your applied jobs.</p>" Visible="false"></asp:Literal>

        <asp:Repeater ID="rptAppliedJobs" runat="server">
            <ItemTemplate>
                <div class="applied-job-item">
                    <h3>Job Title: <%# Eval("JobTitle") %></h3>
                    <p><strong>Application Date:</strong> <%# Eval("ApplicationDate", "{0:d}") %></p>
                    <p><strong>Status:</strong> <asp:Label runat="server" Text='<%# Eval("Status") %>' /></p>
                    <hr />
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Literal ID="litNoRecords" runat="server" Text="<p>You have not applied for any jobs yet.</p>" Visible="false"></asp:Literal>
        
    </div>
</asp:Content>