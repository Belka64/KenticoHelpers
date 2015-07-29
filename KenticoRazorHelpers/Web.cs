using CMS.Controls;
using CMS.DocumentEngine;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using KenticoHelpers;

namespace KenticoRazorHelpers
{
    public static class Web
    {
        /// <summary> 
        /// Get 'img' tag with image url from given node</summary>
        /// <remarks> 
        /// Use only for attacment with image in field</remarks> 
        public static MvcHtmlString GetImage(this TreeNode node, string imgFieldName, object htmlAttributes)
        {
            if (node != null && !string.IsNullOrEmpty(imgFieldName))
            {
                var th = new TransformationHelper();
                var outputHtml = new TagBuilder("img");
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                var mediaItemUrl = th.GetFileUrlFromAlias(node.GetValue(imgFieldName), node.NodeAlias);
                outputHtml.MergeAttribute("src", mediaItemUrl);
                outputHtml.MergeAttributes(attributes, true);

                return MvcHtmlString.Create(outputHtml.ToString());
            }

            return MvcHtmlString.Empty;
        }

        /// <summary> 
        /// Get 'a' tag with attached file url from given node</summary>
        /// <remarks> 
        /// For any attachment types</remarks> 
        public static MvcHtmlString GetFileLink(this TreeNode node, string fileFieldName, string linkText, object htmlAttributes)
        {
            if (node != null && !string.IsNullOrEmpty(fileFieldName))
            {
                var th = new TransformationHelper();
                var outputHtml = new TagBuilder("a")
                {
                    InnerHtml =
                        (!String.IsNullOrEmpty(linkText))
                            ? HttpUtility.HtmlEncode(linkText)
                            : String.Empty
                };
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                var mediaItemUrl = th.GetFileUrlFromAlias(node.GetValue(fileFieldName), node.NodeAlias);
                outputHtml.MergeAttribute("href", mediaItemUrl);
                outputHtml.MergeAttributes(attributes, true);

                return MvcHtmlString.Create(outputHtml.ToString());
            }

            return MvcHtmlString.Empty;
        }

        /// <summary> 
        /// Get 'a' tag with url to given node</summary> 
        public static MvcHtmlString GetDocumentLink(
            this TreeNode node,
            string linkText,
            string siteName,
            object htmlAttributes)
        {
            if (node != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                var builder = new TagBuilder("a")
                {
                    InnerHtml =
                        (!String.IsNullOrEmpty(linkText))
                            ? HttpUtility.HtmlEncode(linkText)
                            : String.Empty
                };
                builder.MergeAttribute("href", node.RelativeURL);
                builder.MergeAttributes(attributes, true);

                return MvcHtmlString.Create(builder.ToString());
            }

            return MvcHtmlString.Empty;
        }

        /// <summary> 
        /// Get 'a' tag with url to node by given guid</summary> 
        public static MvcHtmlString GetDocumentLink(
            Guid nodeGuid,
            string linkText,
            string siteName,
            object htmlAttributes)
        {
            if (nodeGuid != Guid.Empty)
            {
                var tn = Extensions.GetNode(nodeGuid, siteName);
                return GetDocumentLink(tn, linkText, siteName, htmlAttributes);
            }
            return MvcHtmlString.Empty;
        }
    }
}
