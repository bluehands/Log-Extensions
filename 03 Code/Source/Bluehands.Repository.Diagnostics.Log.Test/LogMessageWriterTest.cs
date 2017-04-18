using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bluehands.Repository.Diagnostics.Log.Test
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class LogMessageWriterTest
	{
		private readonly LogMessageWriter m_LogMessageWriter = new LogMessageWriter(typeof(LogMessageWriterTest));
		private const string TestMessage = "Test message.";

		[TestMethod]
		public void Given_NullMessage_When_WriteLogEntryWithLogLevelDebugAndCallerMethodName_Then_LogStringEmpty()
		{
			var writer = new StringWriter();
			Console.SetOut(writer);

			//When
			m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, System.Reflection.MethodBase.GetCurrentMethod().Name, null);
			var logString = writer.ToString();

			//Then
			Assert.IsTrue(string.IsNullOrEmpty(logString));
		}

		[TestMethod]
		public void Given_ArgumentNullExceptionArgument_When_WriteLogEntryWithLogLevelErrorAndCallerName_Then_LogStringContainsErrorAndTestMessageAndArgumentNullException()
		{
			var writer = new StringWriter();
			Console.SetOut(writer);

			//When
			var expectedException = new ArgumentNullException();
			m_LogMessageWriter.WriteLogEntry(LogLevel.Error, System.Reflection.MethodBase.GetCurrentMethod().Name, TestMessage, expectedException);
			var logString = writer.ToString();

			//Then
			var logColumns = logString.Split('|');
			Assert.AreEqual(LogLevel.Error.ToString().ToUpper() + ":", logColumns[2].Trim());
			Assert.AreEqual(TestMessage, logColumns[6].Trim());
			Assert.AreEqual(expectedException.ToString(), logColumns[7].TrimEnd());

		}
	}
}
