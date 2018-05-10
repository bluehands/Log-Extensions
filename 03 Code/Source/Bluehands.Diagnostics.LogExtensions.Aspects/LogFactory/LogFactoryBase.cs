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
		[NonSerialized] private Bluehands.Diagnostics.LogExtensions.Log m_InternalLog;

		protected LogFactoryBase(MethodBase method, LocationInfo member)
		{
			Member = member;
			LogType = method.DeclaringType;
		}
		public static LogFactoryBase Create(MethodBase method)
		{
			var typeOnAspect = method.DeclaringType;
			LocationInfo member = typeOnAspect.GetLocationFromType<Bluehands.Diagnostics.LogExtensions.Log>();
			if (member != null && !member.LocationType.ContainsGenericParameters)
			{
				return new LogFactoryFromLog(method, member);
			}
			return new LogFactoryFromType(method, null);
		}
		public Bluehands.Diagnostics.LogExtensions.Log GetLog(object instance)
		{
			return m_InternalLog ?? (m_InternalLog = instance == null ? new Bluehands.Diagnostics.LogExtensions.Log(LogType) : CreateLog(instance));
		}
		public Bluehands.Diagnostics.LogExtensions.Log GetLog()
		{
			return m_InternalLog;
		}

		protected abstract Bluehands.Diagnostics.LogExtensions.Log CreateLog(object instance);
	}
}
