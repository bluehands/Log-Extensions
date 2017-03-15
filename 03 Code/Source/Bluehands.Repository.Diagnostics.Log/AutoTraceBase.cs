using System;
using System.Diagnostics;
using System.ServiceModel.Channels;

namespace Bluehands.Repository.Diagnostics.Log
{
	public abstract class AutoTraceBase
	{
		protected readonly Log Log;
		protected readonly string Message;

		protected AutoTraceBase(Log logger, string message)
		{
			Log = logger;
			Message = message;
		}
	}
}