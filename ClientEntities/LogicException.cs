using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientEntities
{
	
	public class LogicDataQueryException: Exception
	{
		public LogicDataQueryException(Exception ex, int statusCode) : base(ex.Message, ex.InnerException)
		{
			HelpLink = ex.HelpLink;
			HResult = ex.HResult;
			Source = ex.Source;

			StatusCode = statusCode;
		}
		public int StatusCode { get; set; }
	}

	/*
	public class LogicDataUpdateException : Exception
	{
		public LogicDataUpdateException(Exception ex, int statusCode) : base(ex.Message, ex.InnerException)
		{
			HelpLink = ex.HelpLink;
			HResult = ex.HResult;
			Source = ex.Source;

			StatusCode = statusCode;
		}
		int StatusCode { get; set; }
	}*/
}
