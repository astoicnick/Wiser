<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WisdomIndex.aspx.cs" Inherits="Wiser.WF.WisdomIndex" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 style="text-align:center;">Infinity Scroll</h1>
    <div style="margin:auto;width:10%;">
    <asp:Button Text="Create Wisdom" OnClick="Create_Wisdom" runat="server" />
    </div>
    <% foreach (var wisdom in _wisdom) { %>
    <div class="scrollContainer">
    <h1><%:wisdom.Content %></h1>
    <h3><%:wisdom.Source %></h3>
    <h4><%:wisdom.ScrollAuthor.AuthorName %></h4>
    </div>
      <%  }  %>
</asp:Content>
