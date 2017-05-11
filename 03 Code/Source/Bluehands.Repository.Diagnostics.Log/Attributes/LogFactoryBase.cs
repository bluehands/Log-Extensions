using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bluehands.Repository.Diagnostics.Log.Internal;
using NLog;
using PostSharp.Aspects;
using PostSharp.Aspects.Configuration;
using PostSharp.Extensibility;
using PostSharp.Reflection;

namespace Bluehands.Repository.Diagnostics.Log.Attributes
{
	[Serializable]
	[DebuggerNonUserCode]
	internal abstract class LogFactoryBase
	{
	
		protected Type LogType { get; }
		protected LocationInfo Member { get; }
		[NonSerialized] private Log m_InternalLog;

		protected LogFactoryBase(MethodBase method, LocationInfo member)
		{
			Member = member;
			LogType = method.DeclaringType;
		}
		public static LogFactoryBase Create(MethodBase method)
		{
			var typeOnAspect = method.DeclaringType;
			LocationInfo member = typeOnAspect.GetLocationFromType<Log>();
			if (member != null && !member.LocationType.ContainsGenericParameters)
			{
				return new LogFactoryFromLog(method, member);
			}
			return new LogFactoryFromType(method, null);
		}
		public Log GetLog(object instance, Arguments args)
		{
			return m_InternalLog ?? (m_InternalLog = instance == null ? new Log(LogType) : CreateLog(instance, args));
		}
		public Log GetLog()
		{
			return m_InternalLog;
		}

		protected abstract Log CreateLog(object instance, Arguments args);	//todo args is not used
	}
}
