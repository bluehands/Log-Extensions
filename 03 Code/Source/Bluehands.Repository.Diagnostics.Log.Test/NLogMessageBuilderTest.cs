using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bluehands.Repository.Diagnostics.Log.Test
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class NLogMessageBuilderTest
	{

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void When_loggerNameIsNull_Then_ThrowsArgumentNullException()
		{
			//Given

			//When
			new NLogMessageBuilder(null);

			//Then
		}
	}
}
