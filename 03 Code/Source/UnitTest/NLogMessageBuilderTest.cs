using System;
using System.Threading;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLevel = Bluehands.Repository.Diagnostics.Log.LogLevel;

namespace UnitTest
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
