<%@ Page Title="" Language="C#" MasterPageFile="~/Master page/HomePage.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="practiceAWDPracticals.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .signup-container {
            width: 400px;
            margin: 50px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #f9f9f9;
            text-align: center;
        }
        .signup-container h2 { margin-bottom: 20px; color: darkslategray; }
        .signup-container .input-field { 
            width: 90%; 
            padding: 10px; 
            margin: 10px 0; 
            border-radius: 5px; 
            border: 1px solid #ccc; 
        }
        .signup-container .register-btn {
            width: 90%;
            padding: 10px;
            margin-top: 20px;
            background-color: darkslategray;
            color: ghostwhite;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .message-label { color: red; margin-top: 10px; display: block; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="signup-container">
        <h2>Create a New Account</h2>
        <asp:TextBox ID="txtUsername" runat="server" CssClass="input-field" placeholder="Enter Username"></asp:TextBox>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="input-field" placeholder="Enter Email"></asp:TextBox>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="input-field" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="input-field" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="register-btn" OnClick="btnRegister_Click" />
        <asp:Label ID="lblMessage" runat="server" CssClass="message-label" EnableViewState="false"></asp:Label>
    </div>
</asp:Content>
