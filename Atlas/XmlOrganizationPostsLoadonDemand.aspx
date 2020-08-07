﻿<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="ComponentArt.Web.UI" %>
<%@ Import Namespace="GTS.Clock.Business.Charts" %>
<%@ Import Namespace="GTS.Clock.Model.Charts" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<%@ Import Namespace="GTS.Clock.Infrastructure.Exceptions.UI" %>
<%@ Import Namespace="GTS.Clock.Presentaion.Forms.App_Code" %>
<% Response.ContentType = "text/xml";%>
<script language="C#" runat="server">
    internal class OrganizationPostNodeValue
    {
        public string CustomCode { get; set; }
        public string ParentPath { get; set; }
        public string PersonnelName { get; set; }
        public string PersonnelCode { get; set; }
        public string PersonnelID { get; set; }
        public string ChildCount { get; set; }
        public string ParentID { get; set; }                
    }

    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    void Page_Load(Object sender, EventArgs e)
    {
        string[] retMessage = new string[3];
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string requestedPageUrl = HttpContext.Current.Request.UrlReferrer.Segments[HttpContext.Current.Request.UrlReferrer.Segments.Length - 1];
        ComponentArt.Web.UI.TreeView trvOrgPosts = new ComponentArt.Web.UI.TreeView();
        string LangID = Request.QueryString["LangID"];

        try
        {
            BOrganizationUnit OrganizationPostBusiness = BusinessHelper.GetBusinessInstance<BOrganizationUnit>();
            IList<OrganizationUnit> orgUnitList = OrganizationPostBusiness.GetChilds(decimal.Parse(Request.QueryString["ParentOrgPostID"]));
            foreach (OrganizationUnit childOrgPost in orgUnitList)
            {
                string imageUrl = PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif";
                string imagePath = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";
                TreeViewNode childOrgPostNode = new TreeViewNode();
                childOrgPostNode.ID = childOrgPost.ID.ToString();
                childOrgPostNode.Text = childOrgPost.Name;
                OrganizationPostNodeValue childOrgPostNodeValue = new OrganizationPostNodeValue();
                childOrgPostNodeValue.CustomCode = childOrgPost.CustomCode;
                childOrgPostNodeValue.ParentPath = childOrgPost.ParentPath;
                childOrgPostNodeValue.PersonnelName = childOrgPost.Person != null ? childOrgPost.Person.Name : string.Empty;
                childOrgPostNodeValue.PersonnelCode = childOrgPost.Person != null ? childOrgPost.Person.PersonCode : string.Empty;
                childOrgPostNodeValue.PersonnelID = childOrgPost.Person != null ? childOrgPost.Person.ID.ToString() : "0";
                childOrgPostNodeValue.ChildCount = childOrgPost.ChildCount.ToString();
                childOrgPostNodeValue.ParentID = childOrgPost.Parent != null ? childOrgPost.Parent.ID.ToString() : "0";
                childOrgPostNode.Value = this.JsSerializer.Serialize(childOrgPostNodeValue);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl))
                    childOrgPostNode.ImageUrl = imagePath;
                childOrgPostNode.ContentCallbackUrl = "XmlOrganizationPostsLoadonDemand.aspx?ParentOrgPostID=" + childOrgPost.ID + "&LangID=" + LangID;
                if (childOrgPost.ChildCount > 0)
                    childOrgPostNode.Nodes.Add(new TreeViewNode());
                trvOrgPosts.Nodes.Add(childOrgPostNode);
            }
            Response.Write(trvOrgPosts.GetXml());
        }
        catch (UIValidationExceptions ex)
        {
            this.ParentPageRedirect(requestedPageUrl, LangID, ExceptionTypes.UIValidationExceptions, ex, retMessage);
        }
        catch (UIBaseException ex)
        {
            this.ParentPageRedirect(requestedPageUrl, LangID, ExceptionTypes.UIBaseException, ex, retMessage);
        }
        catch (Exception ex)
        {
            this.ParentPageRedirect(requestedPageUrl, LangID, ExceptionTypes.Exception, ex, retMessage);
        }
    }

    private void ParentPageRedirect(string RequestPageUrl, string CurrentCulture, ExceptionTypes exceptionType, Exception ex, string[] retMessage)
    {
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        exceptionHandler.CurrentPage = RequestPageUrl;
        exceptionHandler.CurrentCulture = CurrentCulture;
        retMessage = exceptionHandler.HandleException(null, exceptionType, ex, retMessage);
        Response.Redirect(RequestPageUrl + "?ErrorType=" + retMessage[0] + "&ErrorBody=" + retMessage[1] + "&error=error" + "&OrgPostsErrorSender=" + this.Page.ToString().Replace("ASP.", "").Replace("_aspx", ".aspx"));
    }
</script>
