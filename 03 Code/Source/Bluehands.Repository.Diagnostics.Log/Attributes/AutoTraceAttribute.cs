using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace Bluehands.Repository.Diagnostics.Log.Attributes
{
	[Serializable]
	[MulticastAttributeUsage(MulticastTargets.Method | MulticastTargets.InstanceConstructor | MulticastTargets.StaticConstructor, Inheritance = MulticastInheritance.None)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true)]
	public class AutoTraceAttribute : OnMethodBoundaryAspect
	{
		private LogFactoryBase m_Factory;
		private string m_Message;

		public AutoTraceAttribute()
		{
		}
		public AutoTraceAttribute(string message)
		{
			m_Message = message;
		}

		public sealed override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
		{
			base.CompileTimeInitialize(method, aspectInfo);
			m_Factory = LogFactoryBase.Create(method);
		}

		public sealed override void OnEntry(MethodExecutionArgs args)
		{
			//try
			//{
			//	Log log = GetLog(args.Instance, args.Arguments);
			//	if (log != null)
			//	{
			//		args.MethodExecutionTag = s_StopWatch.Elapsed;
			//		log.TraceAspectEnter(m_StackTrace, m_Message);
			//	}
			//}
			//catch (Exception ex)
			//{
			//	Log<AutoTraceAttribute> log = new Log<AutoTraceAttribute>();
			//	log.Error("Unexpected error. Please contact your Administrator", ex);
			//}
		}

		public sealed override void OnExit(MethodExecutionArgs args)
		{
			//try
			//{
			//	Log log = GetLog();
			//	if (log != null)
			//	{
			//		var tag = args.MethodExecutionTag;
			//		if (tag != null)
			//		{
			//			var begin = (TimeSpan)tag;
			//			var end = s_StopWatch.Elapsed - begin;
			//			log.TraceAspectExit(m_StackTrace, m_Message, end);
			//		}
			//	}
			//}
			//catch (Exception ex)
			//{
			//	Log<AutoTraceAttribute> log = new Log<AutoTraceAttribute>();
			//	log.Error("Unexpected error. Please contact your Administrator", ex);
			//}
		}

		protected virtual Log GetLog(object instance, Arguments args)
		{
			return m_Factory.GetLog(instance, args);
		}
	}
}
