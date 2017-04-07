using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bluehands.Repository.Diagnostics.Log.Test
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class AutoTraceTest
	{
		private readonly LogMessageWriter m_LogMessageWriter = new LogMessageWriter(typeof(AutoTraceTest));
		//private const string LogFilePath = "./Logs/test.log";
		private const string TestMessage = "Test message.";

		[TestMethod]
		public void Given_LogFileMissingAndLogMessageWriterAndTestMessage_When_AutoTraceCreated_Then_LogFileContainsStartEndTraceEntries()
		{
			//Given
			var writer = new StringWriter();
			Console.SetOut(writer);

			//When
			using (var trace = new AutoTrace(m_LogMessageWriter, TestMessage))
			{
				m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, "Warning test.");
				m_LogMessageWriter.WriteLogEntry(LogLevel.Info, "Info test.");
				m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, "Fatal test.");
				m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, "Debug test.");
			}
			var logString = writer.ToString();

			//Then
			var logColumns = logString.Split('|');
			var traceEntries = logColumns.Where(c => !c.Contains("TRACE:"));
			Assert.IsTrue(traceEntries.Any());
		}

	}

}
