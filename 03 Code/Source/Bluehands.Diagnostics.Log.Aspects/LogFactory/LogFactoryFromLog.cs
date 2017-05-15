using System;
using System.Diagnostics;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Reflection;

namespace Bluehands.Repository.Diagnostics.Log.Aspects.LogFactory
{
	[Serializable]
	[DebuggerNonUserCode]
	internal class LogFactoryFromLog : LogFactoryBase
	{
		internal LogFactoryFromLog(MethodBase method, LocationInfo member)
				   : base(method, member)
		{
		}

		protected sealed override Repository.Diagnostics.Log.Log CreateLog(object instance, Arguments args)
		{
			return Member.GetValue(instance) as Repository.Diagnostics.Log.Log;
		}
	}
}
