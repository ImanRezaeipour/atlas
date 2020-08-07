using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
namespace Ajax
{
	public class AjaxEmbededJavaScriptHandler : IHttpHandler
	{
		public bool IsReusable
		{
			get
			{
				bool result = false;
				return result;
			}
		}
		public void ProcessRequest(HttpContext context)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string str = executingAssembly.FullName.Split(new char[]
			{
				','
			})[0];
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(str + ".Ajax.js");
			StreamReader streamReader = new StreamReader(manifestResourceStream);
			DateTime lastModified = new DateTime(DateAndTime.Now.Year, DateAndTime.Now.Month, DateAndTime.Now.Day, DateAndTime.Now.Hour, DateAndTime.Now.Minute, DateAndTime.Now.Second);
			context.Response.AddHeader("Content-Type", "applicatoin/x-javascript");
			context.Response.ContentEncoding = Encoding.UTF8;
			context.Response.Cache.SetLastModified(lastModified);
			context.Response.Write(streamReader.ReadToEnd());
		}
	}
}
