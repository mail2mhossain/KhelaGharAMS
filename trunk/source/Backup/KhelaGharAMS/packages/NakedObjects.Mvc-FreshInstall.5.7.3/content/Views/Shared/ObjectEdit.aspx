<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.WithServices.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="NakedObjects.Web.Mvc.Html"%>
<%@ Import Namespace="NakedObjects.Web.Mvc.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Html.ObjectTitle(Model)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.TabbedHistory(Model)%>   
    <div class="<%: Html.ObjectEditClass(Model) + " " +  Html.TransientFlag(Model is FindViewModel ? ((FindViewModel) Model).TargetObject : Model) %>" id="<%: Html.ObjectTypeAsCssId(Model) %>">
        <% Html.RenderPartial("ObjectEditControl", Model); %>           
     </div>
</asp:Content>
