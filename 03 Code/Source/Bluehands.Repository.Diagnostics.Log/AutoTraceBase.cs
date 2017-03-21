using System;
using System.Diagnostics;
using System.ServiceModel.Channels;

namespace Bluehands.Repository.Diagnostics.Log
{
	public abstract class AutoTraceBase
	{
		protected readonly LogMessageWriter LogWriter;
		protected readonly string Message;

		protected AutoTraceBase(LogMessageWriter logWriter, string message)
		{
			LogWriter = logWriter;
			Message = message;
		}
	}
}