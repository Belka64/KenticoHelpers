using CMS.Controls;
using CMS.DocumentEngine;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KenticoHelpers
{
    public static class Extensions
    {
        /// <summary>
        /// Get nodes from multiple choice dual list field</summary>
        /// <remarks>
        /// Helper for "Multiple Choice Dual List (With Sort)". Download Page:
        /// devnet.kentico.com/marketplace/inline-controls/dual-list-multi-select-form-(with-sort) </remarks>
        public static IEnumerable<TreeNode> GetNodesFromMultiList(this TreeNode node, string columnName)
        {
            var value = node.GetValue(columnName, "");
            if (!string.IsNullOrEmpty(value))
            {
                var guidVals = value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var guids = new List<TreeNode>();
                foreach (var v in guidVals)
                {
                    Guid g;
                    var val = Guid.TryParse(v, out g) ? g : Guid.Empty;
                    var contentNode = GetNode(val, SiteContext.CurrentSite.SiteName);
                    if (contentNode != null) guids.Add(GetNode(val, SiteContext.CurrentSite.SiteName));
                };
                return guids;
            }
            return Enumerable.Empty<TreeNode>();
        }

        /// <summary>
        /// Get node by Guid</summary>
        /// <remarks> 
        /// Return null if nodeGuid is empty or node is not exist </remarks> 
        public static TreeNode GetNode(Guid nodeGuid, string siteName)
        {
            if (nodeGuid != Guid.Empty)
            {
                var tp = new TreeProvider();

                return tp.SelectSingleNode(TreePathUtils.GetNodeIdByNodeGUID(nodeGuid, siteName));
            }

            return null;
        }

        /// <summary>
        /// Get url by Guid</summary>
        public static string GetDocumentLink(Guid nodeGuid, string siteName)
        {
            var tp = new TreeProvider();
            TreeNode tn = tp.SelectSingleNode(TreePathUtils.GetNodeIdByNodeGUID(nodeGuid, siteName));
            if (tn != null) return tn.RelativeURL;
            
            return string.Empty;
        }

        /// <summary>
        /// Get node by Guid</summary>
        /// /// <remarks> 
        /// Get url by guid for current site </remarks>
        public static string GetDocumentLink(Guid nodeGuid)
        {
            return GetDocumentLink(nodeGuid, SiteContext.CurrentSiteName);
        }
    }
}
