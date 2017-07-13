using System;
using System.Diagnostics;
using System.Reflection;
using Bluehands.Repository.Diagnostics.Log.Aspects.Internal;
using PostSharp.Reflection;

namespace Bluehands.Repository.Diagnostics.Log.Aspects.LogFactory
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
		public Log GetLog(object instance)
		{
			return m_InternalLog ?? (m_InternalLog = instance == null ? new Log(LogType) : CreateLog(instance));
		}
		public Log GetLog()
		{
			return m_InternalLog;
		}

		protected abstract Log CreateLog(object instance);
	}
}
