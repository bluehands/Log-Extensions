using System;
using System.Diagnostics;
using System.Reflection;
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

		protected sealed override Log CreateLog(object instance)
		{
			var member = Member.GetValue(instance) as Log;
			return member;
		}
	}
}
