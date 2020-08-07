using System;
namespace Ajax
{
	internal class CallBackError
	{
		private int _ErrNum;
		private string _ErrMsg;
		public int ErrNum
		{
			get
			{
				return this._ErrNum;
			}
			set
			{
				this._ErrNum = value;
			}
		}
		public string ErrMsg
		{
			get
			{
				return this._ErrMsg;
			}
			set
			{
				this._ErrMsg = value;
			}
		}
	}
}
