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
	internal class LogFactoryFromType : LogFactoryBase
	{
		internal LogFactoryFromType(MethodBase method, LocationInfo member)
                    : base(method, member)
                {
		}

		protected sealed override Log CreateLog(object instance, Arguments args)
		{
			return new Log(LogType);
		}
	}
}
