using System;
using System.Diagnostics;
using System.Reflection;
using Bluehands.Repository.Diagnostics.Log.Aspects.Internal;
using PostSharp.Aspects;
using PostSharp.Reflection;

namespace Bluehands.Repository.Diagnostics.Log.Aspects.LogFactory
{
	[Serializable]
	[DebuggerNonUserCode]
	internal abstract class LogFactoryBase
	{
	
		protected Type LogType { get; }
		protected LocationInfo Member { get; }
		[NonSerialized] private Repository.Diagnostics.Log.Log m_InternalLog;

		protected LogFactoryBase(MethodBase method, LocationInfo member)
		{
			Member = member;
			LogType = method.DeclaringType;
		}
		public static LogFactoryBase Create(MethodBase method)
		{
			var typeOnAspect = method.DeclaringType;
			LocationInfo member = typeOnAspect.GetLocationFromType<Repository.Diagnostics.Log.Log>();
			if (member != null && !member.LocationType.ContainsGenericParameters)
			{
				return new LogFactoryFromLog(method, member);
			}
			return new LogFactoryFromType(method, null);
		}
		public Repository.Diagnostics.Log.Log GetLog(object instance, Arguments args)
		{
			return m_InternalLog ?? (m_InternalLog = instance == null ? new Repository.Diagnostics.Log.Log(LogType) : CreateLog(instance, args));
		}
		public Repository.Diagnostics.Log.Log GetLog()
		{
			return m_InternalLog;
		}

		protected abstract Repository.Diagnostics.Log.Log CreateLog(object instance, Arguments args);	//todo args is not used
	}
}
