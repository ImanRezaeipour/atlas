﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using ComponentArt.Web.UI;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RuleDesigner;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Concepts;
using System.Web;

public class NodeValueExpressions
{
    string imageUrl_Yellow = PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder.gif";
    string imagePath_Yellow = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder.gif";

    string imageUrl_Blue = PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder_blue.gif";
    string imagePath_Blue = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder_blue.gif";

    string imageUrl_silver = PathHelper.GetModulePath_Nuke() + "Images\\TreeView\\folder_silver.gif";
    string imagePath_silver = PathHelper.GetModuleUrl_Nuke() + "Images/TreeView/folder_silver.gif";

    private BConceptExpression BConceptExpression;

    public NodeValueExpressions()
    {
        if (BConceptExpression == null) BConceptExpression = new BConceptExpression();
    }

    public decimal Id { get; set; }

    public string ImageUrl { get; set; }
    public ConceptExpression Value { get; set; }

    private NodeValueExpressions MapConceptExpressionToNodeValueExpressions(ConceptExpression ConceptExpression)
    {
        var nodExpressions = new NodeValueExpressions() { Id = ConceptExpression.ID };

        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl_Yellow)) nodExpressions.ImageUrl = imagePath_Yellow;

        if (ConceptExpression.CanEditInFinal) nodExpressions.ImageUrl = imageUrl_Blue;
        if (!ConceptExpression.CanAddToFinal) nodExpressions.ImageUrl = imageUrl_silver;

        nodExpressions.Value = ConceptExpression;

        return nodExpressions;
    }

    public string MakeJsonObjectListString(ConceptExpression childCncptExprsn)
    {
        var tt = Serialize(new List<NodeValueExpressions>() { MapConceptExpressionToNodeValueExpressions(childCncptExprsn) });
        return tt;
    }

    public string MakeJsonObjectListString(List<ConceptExpression> organizationUnitChlidList)
    {
        var NodeValueExpressionsList =
             organizationUnitChlidList
             //.OrderBy(x => x.SortOrder)
             .Select(MapConceptExpressionToNodeValueExpressions)
             .ToList();

        var serializeNodeValueExpressionsList = Serialize(NodeValueExpressionsList);
        return serializeNodeValueExpressionsList;
    }
    public void MakeJsonObjectListString(List<ConceptExpression> organizationUnitChlidList, ComponentArt.Web.UI.TreeView rootCncptExprsnNode, string languageId, bool editable)
    {
        foreach (ConceptExpression childCncptExprsn in organizationUnitChlidList//.OrderBy(x => x.SortOrder)
            )
        {

            var conceptExpressionNode = new TreeViewNode
            {
                ID = childCncptExprsn.ID.ToString(CultureInfo.InvariantCulture),
                Text = BLanguage.CurrentLocalLanguage == LanguagesName.Parsi ? childCncptExprsn.ScriptBeginFa : childCncptExprsn.ScriptBeginEn,
                EditingEnabled = editable && childCncptExprsn.CanEditInFinal,
                Value = MakeJsonObjectListString(childCncptExprsn)
            };

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + imageUrl_Yellow)) conceptExpressionNode.ImageUrl = imagePath_Yellow;

            if (childCncptExprsn.CanEditInFinal)
            {
                conceptExpressionNode.ImageUrl = imageUrl_Blue;
            }

            if (!childCncptExprsn.CanAddToFinal)
            {
                conceptExpressionNode.ImageUrl = imageUrl_silver;
            }

            conceptExpressionNode.ContentCallbackUrl = "XmlConceptExpressionLoadOnDemand.aspx?CncptExprsnId=" + childCncptExprsn.ID + "&LangID=" + languageId + "&Editable=" + editable;


            if (BConceptExpression.GetByParentId(childCncptExprsn.ID).Count > 0)
            {
                conceptExpressionNode.Nodes.Add(new TreeViewNode());
            }
            else
            {
                conceptExpressionNode.Nodes.Clear();
            }
            rootCncptExprsnNode.Nodes.Add(conceptExpressionNode);
        }
    }

    public static string Serialize(object obj)
    {
        return new JavaScriptSerializer().Serialize(obj);
    }
}
