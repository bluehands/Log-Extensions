using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bluehands.Repository.Diagnostics.Log.Internal
{
	[DebuggerNonUserCode]
	public static class MemberInfoExtension
	{
		public static Type GetMemberType(this MemberInfo memberInfo)
		{
			Type memberType = null;

			switch (memberInfo.MemberType) //todo revisit this.
			{
				case MemberTypes.Field:
					var fi = memberInfo as FieldInfo;
					if (fi != null)
					{
						memberType = fi.FieldType;
					}
					break;
				case MemberTypes.Property:
					var pi = memberInfo as PropertyInfo;
					if (pi != null)
					{
						memberType = pi.PropertyType;
					}
					break;
			}

			return memberType;
		}
	}
}
