using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
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
		public void Given_LogFileMissing_When_LogLevelDebugAndNullMessageToWriteLogEntry_Then_LogFileMissing()
		{
			var writer = new StringWriter();
			Console.SetOut(writer);

			//When
			m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, null);
			var logString = writer.ToString();

			//Then
			Assert.IsTrue(string.IsNullOrEmpty(logString));
		}

		[TestMethod]
		public void Given_LogFileMissing_When_MessageAndLogLevelErrorAndArgumentNullExceptionToWriteLogEntry_Then_LogFileExistsAndLogLevelIsErrorAndMessageAsExpectedAndExceptionIsArgumentNullException()
		{
			var writer = new StringWriter();
			Console.SetOut(writer);

			//When
			var expectedException = new ArgumentNullException();
			m_LogMessageWriter.WriteLogEntry(LogLevel.Error, TestMessage,expectedException);
			var logString = writer.ToString();

			//Then
			var logColumns = logString.Split('|');
			Assert.AreEqual(LogLevel.Error.ToString().ToUpper() + ":", logColumns[2]);
			Assert.AreEqual(TestMessage, logColumns[6]);
			Assert.AreEqual(expectedException.ToString(), logColumns[7].TrimEnd());

		}
	}
}
