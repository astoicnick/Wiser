<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateWisdom.aspx.cs" Inherits="Wiser.WF.CreateWisdom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
    <div class="CreateForm">
        <div class="row">

    <asp:Label ID="ContentLabel" runat="server" Text="Content"></asp:Label>
    <asp:TextBox ID="Content" runat="server" placeholder="Content"></asp:TextBox>
        </div>
        <div class="row">
    <asp:Label ID="SourceLabel" runat="server" Text="Source"></asp:Label>
    <asp:TextBox ID="Source" runat="server" placeholder="Source"></asp:TextBox>
            </div>
        <div class="row">
    <asp:Label ID="AuthorLabel" runat="server" Text="Select Author"></asp:Label>
    <asp:DropDownList ID="Author" runat="server" DataSourceID="AuthorDropDownList" DataTextField="FullName" DataValueField="AuthorId"></asp:DropDownList>
    <asp:LinqDataSource runat="server" EntityTypeName="" ID="AuthorDropDownList" ContextTypeName="Wiser.Models.ApplicationDbContext" Select="new (AuthorId, FullName)" TableName="AuthorTable"></asp:LinqDataSource>
            </div>
    </div>
    </div>
    <div style="text-align: center;">

    <asp:Button Text="Create" runat="server" OnClick="SubmitWisdom"/>
    </div>
</asp:Content>
