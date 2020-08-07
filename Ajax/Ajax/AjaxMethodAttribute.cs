using System;
namespace Ajax
{
	public class AjaxMethodAttribute : Attribute
	{
		private string _MethodName;
		private string _CallBackMethodName;
		private string _ErrorMethodName;
		private string _LoadingMsg;
		private bool _IsCallBackRequired;
		private bool _IsErrorRequired;
		private bool _IsAsync;
		public bool IsAsync
		{
			get
			{
				return this._IsAsync;
			}
			set
			{
				this._IsAsync = value;
			}
		}
		public bool IsCallBackRequired
		{
			get
			{
				return this._IsCallBackRequired;
			}
			set
			{
				this._IsCallBackRequired = value;
			}
		}
		public bool IsErrorRequired
		{
			get
			{
				return this._IsErrorRequired;
			}
			set
			{
				this._IsErrorRequired = value;
			}
		}
		public string LoadingMessage
		{
			get
			{
				return this._LoadingMsg;
			}
			set
			{
				this._LoadingMsg = value;
			}
		}
		public string MethodName
		{
			get
			{
				return this._MethodName;
			}
			set
			{
				this._MethodName = value;
			}
		}
		public string ErrorMethodName
		{
			get
			{
				return this._ErrorMethodName;
			}
			set
			{
				this._ErrorMethodName = value;
			}
		}
		public string CallBackMethodName
		{
			get
			{
				return this._CallBackMethodName;
			}
			set
			{
				this._CallBackMethodName = value;
			}
		}
		public AjaxMethodAttribute(string methodName, bool isCallBackRequired, bool isErrorRequired, string LoadingMessage = "", bool isAsync = true)
		{
			this._IsCallBackRequired = true;
			this._IsErrorRequired = true;
			this._IsAsync = true;
			this._MethodName = methodName;
			this._IsCallBackRequired = isCallBackRequired;
			this._IsErrorRequired = isErrorRequired;
			this._LoadingMsg = LoadingMessage;
			this._IsAsync = isAsync;
		}
		public AjaxMethodAttribute(string methodName, string CallBackMethodName, string ErrorMethodName, string LoadingMessage, bool isAsync = true)
		{
			this._IsCallBackRequired = true;
			this._IsErrorRequired = true;
			this._IsAsync = true;
			this._MethodName = methodName;
			if (CallBackMethodName != null)
			{
				this._CallBackMethodName = CallBackMethodName;
			}
			if (ErrorMethodName != null)
			{
				this._ErrorMethodName = ErrorMethodName;
			}
			this._LoadingMsg = LoadingMessage;
			this._IsAsync = isAsync;
		}
		public AjaxMethodAttribute(string methodName, bool isAsync = true)
		{
			this._IsCallBackRequired = true;
			this._IsErrorRequired = true;
			this._IsAsync = true;
			this._MethodName = methodName;
			this._IsAsync = isAsync;
		}
		public AjaxMethodAttribute(bool isAsync = true)
		{
			this._IsCallBackRequired = true;
			this._IsErrorRequired = true;
			this._IsAsync = true;
			this._IsAsync = isAsync;
		}
	}
}
