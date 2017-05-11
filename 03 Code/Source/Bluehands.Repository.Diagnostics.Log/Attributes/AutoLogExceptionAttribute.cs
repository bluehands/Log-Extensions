using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Extensibility;
using PostSharp.Reflection;

namespace Bluehands.Repository.Diagnostics.Log.Attributes
{
	[Serializable]
	[DebuggerNonUserCode]
	[MulticastAttributeUsage(MulticastTargets.Method | MulticastTargets.InstanceConstructor | MulticastTargets.StaticConstructor, Inheritance = MulticastInheritance.None)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property, AllowMultiple = true)]
	public class AutoLogExceptionAttribute : OnExceptionAspect
	{
		private LogFactoryBase m_Factory;
		//protected Type m_TypeOnAspect;

		public sealed override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
		{
			base.CompileTimeInitialize(method, aspectInfo);
			CreateLogOnCompileTime(method);
		}

		public override bool CompileTimeValidate(MethodBase method)
		{
			//m_TypeOnAspect = method.DeclaringType;
			return base.CompileTimeValidate(method);
		}

		public sealed override void OnException(MethodExecutionArgs args)
		{
			try
			{
				var log = GetLog(args.Instance, args.Arguments);
				if (log == null)
				{
					throw new ArgumentNullException(nameof(log));
				}
				log.Error("Unhandled exception.", args.Exception);
			}
			catch (Exception ex)
			{
				var log = new Log<AutoLogExceptionAttribute>();
				log.Error("Unexpected error. Please contact your Administrator", ex);
			}
			args.FlowBehavior = FlowBehavior.RethrowException;
		}

		protected virtual Log GetLog(object instance, Arguments args)
		{
			return m_Factory.GetLog(instance, args);
		}

		protected virtual void CreateLogOnCompileTime(MethodBase method)
		{
			m_Factory = LogFactoryBase.Create(method);
		}
	}
}
