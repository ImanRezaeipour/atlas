using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
namespace Ajax
{
	public class AjaxMethodHandler : IHttpHandler, IRequiresSessionState
	{
		private Type _classType;
		private string _methodName;
		public Type ClassType
		{
			get
			{
				return this._classType;
			}
		}
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
			set
			{
				this._methodName = value;
			}
		}
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
		public AjaxMethodHandler(Type classType, string methodName)
		{
			this._classType = classType;
			this._methodName = methodName;
		}
		public void ProcessRequest(HttpContext context)
		{
			int num = 0;
			MethodInfo method = this.ClassType.GetMethod(this.MethodName);
			ParameterInfo[] parameters = method.GetParameters();
			checked
			{
				object[] array = new object[parameters.Length - 1 + 1];
				ParameterInfo[] array2 = parameters;
				for (int i = 0; i < array2.Length; i++)
				{
					ParameterInfo parameterInfo = array2[i];
					array[num] = RuntimeHelpers.GetObjectValue(parameterInfo.DefaultValue);
					num++;
				}
				num = 0;
				if (Operators.CompareString(context.Request.RequestType.ToUpper(), "POST", false) == 0)
				{
					ParameterInfo[] array3 = parameters;
					for (int j = 0; j < array3.Length; j++)
					{
						ParameterInfo parameterInfo2 = array3[j];
						if (context.Request.Form[parameterInfo2.Name] != null)
						{
							array[num] = RuntimeHelpers.GetObjectValue(this.Deserialize(context.Request.Form[parameterInfo2.Name], parameterInfo2.ParameterType));
						}
						num++;
					}
				}
				else
				{
					if (Operators.CompareString(context.Request.RequestType.ToUpper(), "GET", false) == 0)
					{
						ParameterInfo[] array4 = parameters;
						for (int k = 0; k < array4.Length; k++)
						{
							ParameterInfo parameterInfo3 = array4[k];
							if (context.Request.QueryString[parameterInfo3.Name] != null)
							{
								array[num] = RuntimeHelpers.GetObjectValue(this.Deserialize(context.Request.QueryString[parameterInfo3.Name], parameterInfo3.ParameterType));
							}
							num++;
						}
					}
				}
				object objectValue = null;
				if (method.IsStatic)
				{
					objectValue = RuntimeHelpers.GetObjectValue(this.ClassType.InvokeMember(this.MethodName, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, array));
				}
				else
				{
					object objectValue2 = RuntimeHelpers.GetObjectValue(Activator.CreateInstance(this.ClassType));
					try
					{
						objectValue = RuntimeHelpers.GetObjectValue(method.Invoke(RuntimeHelpers.GetObjectValue(objectValue2), array));
					}
					catch (Exception expr_1BA)
					{
						ProjectData.SetProjectError(expr_1BA);
						ProjectData.ClearProjectError();
					}
				}
				context.Response.Expires = 0;
				context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
				context.Response.AddHeader("Content-Type", "text/plain");
				context.Response.ContentType = "text/plain";
				try
				{
					context.Response.Write(this.Serialize(RuntimeHelpers.GetObjectValue(objectValue)).ToString());
				}
				catch (Exception expr_22A)
				{
					ProjectData.SetProjectError(expr_22A);
					Exception ex = expr_22A;
					CallBackError callBackError = new CallBackError();
					callBackError.ErrMsg = Strings.Replace(ex.Message, "'", "\\'", 1, -1, CompareMethod.Binary);
					context.Response.Status = "500 OK";
					context.Response.Write(this.Serialize(callBackError).ToString());
					ProjectData.ClearProjectError();
				}
				context.Response.End();
			}
		}
		public object Deserialize(object obj, Type paramType = null)
		{
			Type type = obj.GetType();
			Regex regex = new Regex("^(?<Value>(True|False))$", RegexOptions.IgnoreCase);
			Match match = regex.Match(obj.ToString());
			if (match.Success)
			{
				return Conversions.ToBoolean(match.Groups["Value"].Value);
			}
			regex = new Regex("^('(?<Value>[a-zA-Z0-9\\-.,!@#$%^&*()_+;:\"'|~`<>?/{}\\s]{1,20000000})'|(?<Value>[0-9.]{1,20000000}))$");
			match = regex.Match(obj.ToString());
			checked
			{
				if (match.Success)
				{
					if (paramType == typeof(string))
					{
						return match.Groups["Value"].Value;
					}
					if (paramType == typeof(Guid))
					{
						Guid guid = new Guid(match.Groups["Value"].Value);
						return guid;
					}
					if (paramType == typeof(int) || paramType == typeof(int?))
					{
						return Conversions.ToInteger(match.Groups["Value"].Value);
					}
					if (paramType == typeof(short) || paramType == typeof(short?))
					{
						return Conversions.ToShort(match.Groups["Value"].Value);
					}
					if (paramType == typeof(long) || paramType == typeof(long?))
					{
						return Conversions.ToLong(match.Groups["Value"].Value);
					}
					if (paramType == typeof(ushort) || paramType == typeof(ushort?))
					{
						return Conversions.ToUShort(match.Groups["Value"].Value);
					}
					if (paramType == typeof(uint) || paramType == typeof(uint?))
					{
						return Conversions.ToUInteger(match.Groups["Value"].Value);
					}
					if (paramType == typeof(ulong) || paramType == typeof(ulong?))
					{
						return Conversions.ToULong(match.Groups["Value"].Value);
					}
					if (paramType == typeof(double) || paramType == typeof(double?))
					{
						return Conversions.ToDouble(match.Groups["Value"].Value);
					}
					if (paramType == typeof(decimal) || paramType == typeof(decimal?))
					{
						return Conversions.ToDecimal(match.Groups["Value"].Value);
					}
					if (paramType == typeof(DateTime))
					{
						return Conversions.ToDate(match.Groups["Value"].Value);
					}
					if (paramType == typeof(bool) || paramType == typeof(bool?))
					{
						return Conversions.ToBoolean(match.Groups["Value"].Value);
					}
					return match.Groups["Value"].Value;
				}
				else
				{
					regex = new Regex("^\\[(?<Value>[a-zA-Z0-9\\-.,!@#$%^&*()_+;:\",'|~`<>?/{}\\s]*)\\]$");
					match = regex.Match(obj.ToString());
					if (match.Success)
					{
						string[] array = Strings.Split(match.Groups["Value"].Value, ",", -1, CompareMethod.Binary);
						Array result = null;
						if (array.Length > 0)
						{
							ArrayList arrayList = new ArrayList();
							string[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								string text = array2[i];
								arrayList.Add(RuntimeHelpers.GetObjectValue(this.Deserialize(text, text.GetType())));
							}
							result = arrayList.ToArray(paramType.GetElementType());
						}
						return result;
					}
					regex = new Regex("^\\{(?<Value>[\"][a-zA-Z0-9]*[\"][:][']?[a-zA-Z0-9\\-.,!@#$%^&*()_+;:\",|~`<>?/{}\\s]*[']?[,]?)+\\}$");
					match = regex.Match(obj.ToString());
					if (match.Success)
					{
						object objectValue = RuntimeHelpers.GetObjectValue(Activator.CreateInstance(paramType));
						regex = new Regex("([\"][a-zA-Z0-9]+[\"][:]['][a-zA-Z0-9\\-.,!@#$%^&*()_+;:\",|~`<>?/{}\\s]+[']|[\"][a-zA-Z0-9]+[\"][:][0-9.]+)");
						MatchCollection matchCollection = regex.Matches(obj.ToString());
						try
						{
							IEnumerator enumerator = matchCollection.GetEnumerator();
							while (enumerator.MoveNext())
							{
								match = (Match)enumerator.Current;
								regex = new Regex("[\"](?<Prop>[a-zA-Z0-9]+)[\"][:][']?(?<Value>[a-zA-Z0-9\\-.,!@#$%^&*()_+;:\",|~`<>?/{}\\s]+)[']?");
								Match match2 = regex.Match(match.Value);
								try
								{
									if (match2.Success)
									{
										PropertyInfo property = paramType.GetProperty(match2.Groups["Prop"].Value);
										property.SetValue(RuntimeHelpers.GetObjectValue(objectValue), RuntimeHelpers.GetObjectValue(this.Deserialize("'" + match2.Groups["Value"].Value + "'", property.PropertyType)), null);
									}
								}
								catch (Exception expr_49B)
								{
									ProjectData.SetProjectError(expr_49B);
									ProjectData.ClearProjectError();
								}
							}
						}
						finally
						{
							IEnumerator enumerator = null;
							if (enumerator is IDisposable)
							{
								(enumerator as IDisposable).Dispose();
							}
						}
						return objectValue;
					}
					object result2 = obj.ToString() == "''" ? string.Empty : obj;
					return result2;
				}
			}
		}
		public string Serialize(object obj)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (obj == null)
			{
				return "null";
			}
			Type type = obj.GetType();
			checked
			{
				if (type == typeof(string))
				{
					stringBuilder.Append("'" + obj.ToString() + "'");
				}
				else
				{
					if (type == typeof(int) | type == typeof(decimal) | type == typeof(short) | type == typeof(long) | type == typeof(uint) | type == typeof(ulong) | type == typeof(ushort) | type == typeof(double))
					{
						stringBuilder.Append(obj.ToString());
					}
					else
					{
						if (type == typeof(bool))
						{
							stringBuilder.Append(obj.ToString().ToLower());
						}
						else
						{
							if (type == typeof(DateTime))
							{
								stringBuilder.Append("new Date('" + Strings.Format(Conversions.ToDate(obj.ToString()), "MM/dd/yyyy hh:mm:ss") + "')");
							}
							else
							{
								if (type == typeof(Hashtable))
								{
									stringBuilder.Append("{");
									IDictionaryEnumerator enumerator = ((Hashtable)obj).GetEnumerator();
									while (enumerator.MoveNext())
									{
										object expr_15F = enumerator.Current;
										DictionaryEntry dictionaryEntry2;
										DictionaryEntry dictionaryEntry = (expr_15F != null) ? ((DictionaryEntry)expr_15F) : dictionaryEntry2;
										if (Operators.CompareString(stringBuilder.ToString(), "{", false) != 0)
										{
											stringBuilder.Append(",");
										}
										stringBuilder.Append(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("'", dictionaryEntry.Key), "':"), this.Serialize(RuntimeHelpers.GetObjectValue(dictionaryEntry.Value))));
									}
									stringBuilder.Append("}");
								}
								else
								{
									if (type.IsArray)
									{
										stringBuilder.Append("[");
										if (Operators.CompareString(type.Name, "DateTime[]", false) == 0)
										{
											DateTime[] array = (DateTime[])obj;
											DateTime[] array2 = array;
											for (int i = 0; i < array2.Length; i++)
											{
												object obj2 = array2[i];
												if (Operators.CompareString(stringBuilder.ToString(), "[", false) != 0)
												{
													stringBuilder.Append(",");
												}
												if (obj2 != null)
												{
													stringBuilder.Append(this.Serialize(RuntimeHelpers.GetObjectValue(obj2)));
												}
												else
												{
													stringBuilder.Append("''");
												}
											}
										}
										else
										{
											try
											{
												IEnumerator enumerator2 = ((IEnumerable)obj).GetEnumerator();
												while (enumerator2.MoveNext())
												{
													object objectValue = RuntimeHelpers.GetObjectValue(enumerator2.Current);
													if (Operators.CompareString(stringBuilder.ToString(), "[", false) != 0)
													{
														stringBuilder.Append(",");
													}
													if (objectValue != null)
													{
														stringBuilder.Append(this.Serialize(RuntimeHelpers.GetObjectValue(objectValue)));
													}
													else
													{
														stringBuilder.Append("''");
													}
												}
											}
											finally
											{
												IEnumerator enumerator2 = null;
												if (enumerator2 is IDisposable)
												{
													(enumerator2 as IDisposable).Dispose();
												}
											}
										}
										stringBuilder.Append("]");
									}
									else
									{
										if (type.IsAssignableFrom(typeof(DataSet)))
										{
											DataSet dataSet = (DataSet)obj;
											stringBuilder.Append("{");
											stringBuilder.Append(string.Concat(new string[]
											{
												"Type:'",
												type.Name,
												"',DataSetName:'",
												dataSet.DataSetName,
												"',TableCount:",
												Conversions.ToString(dataSet.Tables.Count),
												","
											}));
											stringBuilder.Append("Tables:[");
											int arg_3CE_0 = 0;
											int num = dataSet.Tables.Count - 1;
											for (int j = arg_3CE_0; j <= num; j++)
											{
												if (j > 0)
												{
													stringBuilder.Append(",");
												}
												stringBuilder.Append(this.Serialize(dataSet.Tables[j]));
											}
											stringBuilder.Append("]");
											stringBuilder.Append("}");
										}
										else
										{
											if (type.IsAssignableFrom(typeof(DataTable)))
											{
												DataTable dataTable = (DataTable)obj;
												stringBuilder.Append("{");
												stringBuilder.Append(string.Concat(new string[]
												{
													"Type:'",
													type.Name,
													"',TableName:'",
													dataTable.TableName,
													"',RowCount:",
													Conversions.ToString(dataTable.Rows.Count),
													","
												}));
												stringBuilder.Append("Rows:[");
												int arg_4D1_0 = 0;
												int num2 = dataTable.Rows.Count - 1;
												for (int k = arg_4D1_0; k <= num2; k++)
												{
													if (k > 0)
													{
														stringBuilder.Append(",");
													}
													stringBuilder.Append(this.Serialize(dataTable.Rows[k]));
												}
												stringBuilder.Append("]");
												stringBuilder.Append("}");
											}
											else
											{
												if (type.IsAssignableFrom(typeof(DataRow)))
												{
													DataRow dataRow = (DataRow)obj;
													stringBuilder.Append("{");
													stringBuilder.Append("Type:'" + type.Name + "',");
													stringBuilder.Append("Columns:[");
													int arg_589_0 = 0;
													int num3 = dataRow.ItemArray.Length - 1;
													for (int l = arg_589_0; l <= num3; l++)
													{
														if (l > 0)
														{
															stringBuilder.Append(",");
														}
														stringBuilder.Append("{");
														stringBuilder.Append(Operators.ConcatenateObject("Name:'" + dataRow.Table.Columns[l].ColumnName + "',Value:", Interaction.IIf(Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRow[l])), "null", this.Serialize(RuntimeHelpers.GetObjectValue(dataRow[l])))));
														stringBuilder.Append("}");
													}
													stringBuilder.Append("]");
													stringBuilder.Append("}");
												}
												else
												{
													if (type.IsClass)
													{
														int num4 = 0;
														FieldInfo[] fields = type.GetFields(BindingFlags.Public);
														PropertyInfo[] properties = type.GetProperties();
														stringBuilder.Append("{");
														stringBuilder.Append("type:'" + type.Name + "'");
														if (fields.Length > 0 | properties.Length > 0)
														{
															stringBuilder.Append(",");
														}
														FieldInfo[] array3 = fields;
														for (int m = 0; m < array3.Length; m++)
														{
															FieldInfo fieldInfo = array3[m];
															if (num4 > 0)
															{
																stringBuilder.Append(",");
															}
															stringBuilder.Append(fieldInfo.Name + ":" + this.Serialize(RuntimeHelpers.GetObjectValue(fieldInfo.GetValue(RuntimeHelpers.GetObjectValue(obj)))));
															num4++;
														}
														int num5 = 0;
														PropertyInfo[] array4 = properties;
														for (int n = 0; n < array4.Length; n++)
														{
															PropertyInfo propertyInfo = array4[n];
															MethodInfo getMethod = propertyInfo.GetGetMethod();
															if (num4 > 0)
															{
																stringBuilder.Append(",");
																num4 = 0;
															}
															if (num5 > 0)
															{
																stringBuilder.Append(",");
															}
															stringBuilder.Append(propertyInfo.Name + ":" + this.Serialize(RuntimeHelpers.GetObjectValue(getMethod.Invoke(RuntimeHelpers.GetObjectValue(obj), null))));
															num5++;
														}
														stringBuilder.Append("}");
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				return stringBuilder.ToString();
			}
		}
	}
}
