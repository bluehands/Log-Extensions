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
		private const string LogFilePath = "./Logs/test.log";
		private const string TestMessage = "Test message.";

		[TestMethod]
		public void Given_LogFileMissingAndLogMessageWriterAndTestMessage_When_AutoTraceCreated_Then_LogFileContainsStartEndTraceEntries()
		{
			//Given
			//Given_LogFileMissing();

			//When
			using (var trace = new AutoTrace(m_LogMessageWriter, TestMessage))
			{
				m_LogMessageWriter.WriteLogEntry(LogLevel.Warning, "Warning test.");
				m_LogMessageWriter.WriteLogEntry(LogLevel.Info, "Info test.");
				m_LogMessageWriter.WriteLogEntry(LogLevel.Fatal, "Fatal test.");
				m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, "Debug test.");
			}

			//Then
			var logColumns = Then_FileExistsExtractLogText();
			var traceEntries = logColumns.Where(c => !c.Contains("TRACE:"));
			Assert.IsTrue(traceEntries.Any());
		}

		private static void Given_LogFileMissing()
		{
			if (File.Exists(LogFilePath))
			{
				File.Delete(LogFilePath);
			}
		}

		private static string[] Then_FileExistsExtractLogText()
		{
			Assert.IsTrue(File.Exists(LogFilePath));
			var logText = File.ReadAllText(LogFilePath);
			char[] separator = { '\t' };
			return logText.Split(separator, StringSplitOptions.None);
		}

	}

}
