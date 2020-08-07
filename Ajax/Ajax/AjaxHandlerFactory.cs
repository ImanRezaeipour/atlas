using Microsoft.VisualBasic.CompilerServices;
using System;
using System.IO;
using System.Web;
namespace Ajax
{
	public class AjaxHandlerFactory : IHttpHandlerFactory
	{
		public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			string text = Path.GetFileNameWithoutExtension(context.Request.Path);
			string text2 = string.Empty;
			checked
			{
				if (!(Operators.CompareString(text.Substring(text.Length - 6), "__ajax", false) == 0 & (Operators.CompareString(requestType.ToUpper(), "POST", false) == 0 | Operators.CompareString(requestType.ToUpper(), "GET", false) == 0)))
				{
					return null;
				}
				try
				{
					text = text.Substring(0, text.Length - 6);
					text2 = context.Request.Headers["AJAX-METHOD"];
				}
				catch (Exception expr_8A)
				{
					ProjectData.SetProjectError(expr_8A);
					text2 = string.Empty;
					ProjectData.ClearProjectError();
				}
				Type type = Type.GetType(text);
				if (Operators.CompareString(text2, string.Empty, false) != 0)
				{
					return new AjaxMethodHandler(type, text2);
				}
				if (Operators.CompareString(text, "common", false) == 0)
				{
					return new AjaxEmbededJavaScriptHandler();
				}
				type = Type.GetType(text);
				if (type != null)
				{
					return new AjaxJavaScriptHandler(type);
				}
				return null;
			}
		}
		public void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
