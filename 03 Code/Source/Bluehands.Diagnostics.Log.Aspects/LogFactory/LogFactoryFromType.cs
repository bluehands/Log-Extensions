using System;
using System.Diagnostics;
using System.Reflection;
using PostSharp.Aspects;
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

		protected sealed override Repository.Diagnostics.Log.Log CreateLog(object instance, Arguments args)
		{
			return new Repository.Diagnostics.Log.Log(LogType);
		}
	}
}
