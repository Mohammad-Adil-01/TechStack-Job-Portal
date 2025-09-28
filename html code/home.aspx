<%@ Page Title="" Language="C#" MasterPageFile="~/Master page/HomePage.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="practiceAWDPracticals.WebForm1"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/home.css" type="text/css" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="job-container" runat="server">
        <div runat="server" class="input-div">
            <asp:TextBox ID="searchId" runat="server" placeholder="Enter Your Specfic Role to Search" class="search-textbox"></asp:TextBox>
            <asp:Button ID="searchBtn" runat="server" Text="Search" class="search-btn" OnClick="searchBtn_Click" />
        </div>
        <h1 runat="server" class="heading">Welcome to Job's Section</h1>
        <hr class="hr-line" />
        <div runat="server" class="job-user-container">
            <div runat="server" class="job-selection-section">
                <div runat="server" class="job-category">
                    <asp:Repeater ID="rptJobs" runat="server">
                        <ItemTemplate>
                            <div class="job-type">
                                <h1 class="job-title"><%# Eval("jobTitle") %></h1>
                               <p class="job-description"><%# Eval("jobDes") %></p>
                                <asp:Button ID="btnApply" runat="server" Text="Apply Now" class="apply-btn"
                                          OnClick="btnApply_Click" CommandArgument='<%# Eval("Id") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                
            </div>
            <div runat="server" class="user-detail" id="user_detail">
                <h1 runat="server" class="login-title">Welcome Back</h1>
                <hr class="divide" />
                <div runat="server" class="login">
                        <asp:TextBox ID="loginEmail" class="input-login-text" runat="server" placeholder="Enter Your Mail"></asp:TextBox>
                        <asp:TextBox ID="loginPassword" class="input-login-text" runat="server" placeholder="Enter Your Password" TextMode="Password"></asp:TextBox>
                    
                        <asp:Button ID="LoginBtn" class="input-login-text" runat="server" Text="Login" OnClick="LoginBtn_Click" />
                      
                        <asp:HyperLink NavigateUrl  ="~/html code/signup.aspx" Text="New User? Register Here" runat="server" CssClass="signup-link" />
                </div>
            </div>
        </div>

    </div>
</asp:Content>