using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Reflection;

namespace Bluehands.Repository.Diagnostics.Log.Attributes
{
	[Serializable]
	[DebuggerNonUserCode]
	internal class LogFactoryFromLog : LogFactoryBase
	{
		internal LogFactoryFromLog(MethodBase method, LocationInfo member)
				   : base(method, member)
		{
		}

		protected sealed override Log CreateLog(object instance, Arguments args)
		{
			return Member.GetValue(instance) as Log;
		}
	}
}
