using System;
using System.Diagnostics;
using System.Reflection;
using PostSharp.Reflection;

namespace Bluehands.Repository.Diagnostics.Log.Aspects.LogFactory
{
	[Serializable]
	[DebuggerNonUserCode]
	internal class LogFactoryFromType : LogFactoryBase
	{
		internal LogFactoryFromType(MethodBase method, LocationInfo member)
                    : base(method, member)
                {
		}

		protected sealed override Bluehands.Diagnostics.LogExtensions.Log CreateLog(object instance)
		{
			return new Bluehands.Diagnostics.LogExtensions.Log(LogType);
		}
	}
}
