using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bluehands.Repository.Diagnostics.Log.Test
{
	[TestClass]
	public class NLogMessageBuilderTest
	{
		//int m_Indent = 0;

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
