using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Reflection;
using System.Text;
using System.Web;
namespace Ajax
{
	public class AjaxJavaScriptHandler : IHttpHandler
	{
		private Type _ClassType;
		public Type ClassType
		{
			get
			{
				return this._ClassType;
			}
		}
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
		public AjaxJavaScriptHandler(Type classType)
		{
			this._ClassType = classType;
		}
		public void ProcessRequest(HttpContext context)
		{
			MethodInfo[] methods = this.ClassType.GetMethods();
			DateTime lastModified = new DateTime(DateAndTime.Now.Year, DateAndTime.Now.Month, DateAndTime.Now.Day, DateAndTime.Now.Hour, DateAndTime.Now.Minute, DateAndTime.Now.Second);
			StringBuilder stringBuilder = new StringBuilder();
			string text = this.ClassType.FullName + "," + this.ClassType.Assembly.FullName.Substring(0, this.ClassType.Assembly.FullName.IndexOf(",")) + "__ajax";
			context.Response.AddHeader("Content-Type", "applicatoin/x-javascript");
			context.Response.ContentEncoding = Encoding.UTF8;
			context.Response.Cache.SetLastModified(lastModified);
			stringBuilder.Append(this.ClassType.Name + "_Class = new AjaxMethod();\r\n");
			string str;
			if (Operators.CompareString(HttpContext.Current.Request.ApplicationPath, "/", false) != 0)
			{
				str = string.Concat(new string[]
				{
					"'",
					HttpContext.Current.Request.ApplicationPath,
					"/",
					text,
					Utility.HandlerExtention,
					"'"
				});
			}
			else
			{
				str = "'" + text + Utility.HandlerExtention + "'";
			}
			stringBuilder.Append(this.ClassType.Name + "_Class.initialize(" + str + ");\r\n");
			MethodInfo[] array = methods;
			checked
			{
				for (int i = 0; i < array.Length; i++)
				{
					MethodInfo methodInfo = array[i];
					if (methodInfo.IsPublic)
					{
						AjaxMethodAttribute[] array2 = (AjaxMethodAttribute[])methodInfo.GetCustomAttributes(typeof(AjaxMethodAttribute), true);
						if (array2.Length > 0)
						{
							ParameterInfo[] parameters = methodInfo.GetParameters();
							string str2;
							if (array2[0].MethodName == null)
							{
								str2 = methodInfo.Name;
							}
							else
							{
								str2 = array2[0].MethodName;
							}
							string text2 = string.Empty;
							if (array2[0].IsCallBackRequired)
							{
								if (array2[0].CallBackMethodName == null)
								{
									text2 = "CallBack_" + methodInfo.Name;
								}
								else
								{
									text2 = array2[0].CallBackMethodName;
								}
							}
							string text3 = string.Empty;
							if (array2[0].IsErrorRequired)
							{
								if (array2[0].ErrorMethodName == null)
								{
									text3 = "Error_" + methodInfo.Name;
								}
								else
								{
									text3 = array2[0].ErrorMethodName;
								}
							}
							string loadingMessage = array2[0].LoadingMessage;
							bool isAsync = array2[0].IsAsync;
							stringBuilder.Append("function " + str2 + "(");
							int num = 0;
							ParameterInfo[] array3 = parameters;
							for (int j = 0; j < array3.Length; j++)
							{
								ParameterInfo parameterInfo = array3[j];
								stringBuilder.Append(parameterInfo.Name);
								if (num < parameters.Length - 1)
								{
									stringBuilder.Append(",");
								}
								num++;
							}
							stringBuilder.Append("){\r\n");
							num = 0;
							stringBuilder.Append(string.Concat(new string[]
							{
								"\treturn ",
								this.ClassType.Name,
								"_Class.invokeAjax('",
								methodInfo.Name,
								"',{"
							}));
							ParameterInfo[] array4 = parameters;
							for (int k = 0; k < array4.Length; k++)
							{
								ParameterInfo parameterInfo2 = array4[k];
								stringBuilder.Append("'" + parameterInfo2.Name + "':" + parameterInfo2.Name);
								if (num < parameters.Length - 1)
								{
									stringBuilder.Append(",");
								}
								num++;
							}
							stringBuilder.Append("},");
							stringBuilder.Append("{");
							if (isAsync)
							{
								stringBuilder.Append(string.Concat(new string[]
								{
									"'CallBack':'",
									text2,
									"','Error':'",
									text3,
									"',"
								}));
							}
							if (loadingMessage != null && Operators.CompareString(loadingMessage.Trim(), string.Empty, false) != 0)
							{
								stringBuilder.Append("'LoadingMsg':'" + loadingMessage + "',");
							}
							stringBuilder.Append("'IsAsync':" + isAsync.ToString().ToLower() + "});\r\n");
							stringBuilder.Append("}");
						}
					}
				}
				context.Response.Write(stringBuilder.ToString());
			}
		}
	}
}
