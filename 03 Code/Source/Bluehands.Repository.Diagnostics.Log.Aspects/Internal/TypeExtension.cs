using System;
using System.Linq;
using System.Reflection;
using PostSharp.Reflection;

namespace Bluehands.Repository.Diagnostics.Log.Aspects.Internal
{
	internal static class TypeExtension	//todo revisit this.
	{
		public static FieldInfo GetFieldFromType<TField>(this Type typeToGetFrom)
		{
			var memberInfos = typeToGetFrom.FindMembers(MemberTypes.Field, BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public,
														(mi, obj) =>
														{
															var memberType = mi.GetMemberType();
															if (memberType != null)
															{
																return ((Type)obj).IsAssignableFrom(memberType);
															}
															return false;
														}
														, typeof(TField));

			return (FieldInfo)memberInfos.FirstOrDefault();
		}
		public static LocationInfo GetLocationFromType<TField>(this Type typeToGetFrom)
		{
			FieldInfo member = typeToGetFrom.GetFieldFromType<TField>();
			if (member != null)
			{
				return new LocationInfo(member);
			}
			return null;
		}
	}
}
