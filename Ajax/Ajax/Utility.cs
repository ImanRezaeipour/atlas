using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
namespace Ajax
{
	public class Utility
	{
		public static string HandlerExtention = ".ashx";
		private static bool configAtClientSide = false;
		public static bool ConfigureAtClientSide
		{
			get
			{
				return Utility.configAtClientSide;
			}
			set
			{
				Utility.configAtClientSide = value;
			}
		}
		public static void GenerateMethodScripts(object oType)
		{
			Utility.GenerateMethodScripts(RuntimeHelpers.GetObjectValue(oType), false);
		}
		public static void GenerateMethodScripts(object oType, bool configureAtClientSide)
		{
			Type type = oType.GetType();
			string text = type.FullName + "," + type.Assembly.FullName.Substring(0, type.Assembly.FullName.IndexOf(",")) + "__ajax";
			Page page = ((Control)oType).Page;
			if (Operators.CompareString(HttpContext.Current.Request.ApplicationPath, "/", false) != 0)
			{
				if (!page.ClientScript.IsClientScriptBlockRegistered(page.GetType(), "common"))
				{
                    page.ClientScript.RegisterClientScriptBlock(page.GetType(), "common", "<script type='text/javascript' src='" + HttpContext.Current.Request.ApplicationPath + "DesktopModules/Atlas/common__ajax.ashx'></script>");
				}
				page.ClientScript.RegisterClientScriptBlock(type, text, string.Concat(new string[]
				{
					"<script type='text/javascript' src='",
					HttpContext.Current.Request.ApplicationPath,
					"DesktopModules/Atlas/",
					text,
					".ashx'></script>"
				}));
			}
			else
			{
				if (!page.ClientScript.IsClientScriptBlockRegistered(page.GetType(), "common"))
				{
                    page.ClientScript.RegisterClientScriptBlock(page.GetType(), "common", "<script type='text/javascript' src='/DesktopModules/Atlas/common__ajax.ashx'></script>");
				}
                page.ClientScript.RegisterClientScriptBlock(type, text, "<script type='text/javascript' src='/DesktopModules/Atlas/" + text + ".ashx'></script>");
			}
			Utility.configAtClientSide = configureAtClientSide;
		}
	}
}
