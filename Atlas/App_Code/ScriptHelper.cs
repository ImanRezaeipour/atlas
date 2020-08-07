using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Reflection;
using System.IO;
using DotNetNuke.Entities.Modules;

/// <summary>
/// Summary description for ScriptHelper
/// </summary>
public class ScriptHelper
{
    private const string ScriptsBasePath = "JS/API/";
    private const string ScriptsBasePathNuke = "DesktopModules/Atlas/JS/API/";
    private const string DefiantScriptsBasePath = "JS/Defiant/";
    private const string DefiantScriptsBasePathNuke = "DesktopModules/Atlas/JS/Defiant/";
    private const string nukePath = "DesktopModules/Atlas/";

	public ScriptHelper()
	{

	}
    public static void InitializeScriptsNuke(Page page,PortalModuleBase module, Type Scripts)
    {
        foreach (MemberInfo memberInfo in Scripts.GetMembers())
        {
            if (memberInfo.DeclaringType == Scripts && memberInfo.Name != "value__")
            {
                string Key = memberInfo.Name + ".js";
                string Script = "<script type=\"text/javascript\" src=\"" +  ScriptsBasePathNuke + Key + "?" + DateTime.Now.Ticks.ToString() + "\"></script>";
                if (!page.ClientScript.IsClientScriptBlockRegistered(Key))
                    page.ClientScript.RegisterClientScriptBlock(page.GetType(), Key, Script);
            }
        }
    }
    public static void InitializeScripts(Page page, Type Scripts)
    {
        foreach (MemberInfo memberInfo in Scripts.GetMembers())
        {
            if (memberInfo.DeclaringType == Scripts && memberInfo.Name != "value__")
            {
                string Key = memberInfo.Name + ".js";
                string Script = "<script type=\"text/javascript\" src=\""  + ScriptsBasePath + Key + "?" + DateTime.Now.Ticks.ToString() + "\"></script>";
                if (!page.ClientScript.IsClientScriptBlockRegistered(Key))
                    page.ClientScript.RegisterClientScriptBlock(page.GetType(), Key, Script);
            }
        }
    }

    public static void InitializeDefiantScripts(Page page)
    {
        string DefiantScriptsPath = DefiantScriptsBasePath.Replace("/", "\\");
        foreach (string scriptPath in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + DefiantScriptsBasePathNuke))
        {
                string[] scriptPathParts = scriptPath.Split(new char[]{'\\'});
                string Key = scriptPathParts[scriptPathParts.Length - 1].Replace(nukePath, "");
                string Script = "<script type=\"text/javascript\" src=\"" + Key + "\"></script>";
                if (!page.ClientScript.IsClientScriptBlockRegistered(Key))
                    page.ClientScript.RegisterClientScriptBlock(page.GetType(), Key, Script);
        }
    }

}