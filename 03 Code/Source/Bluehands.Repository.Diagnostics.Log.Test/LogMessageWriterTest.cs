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
		private const string LogFilePath = "./Logs/test.log";
		private const string TestMessage = "Test message.";


		[TestMethod]
		public void Given_LogFileMissing_When_LogLevelDebugAndNullMessageToWriteLogEntry_Then_LogFileMissing()
		{
			Given_LogFileMissing();

			//When
			m_LogMessageWriter.WriteLogEntry(LogLevel.Debug, null);

			//Then
			Assert.IsFalse(File.Exists(LogFilePath));
		}

		[TestMethod]
		public void Given_LogFileMissing_When_MessageAndLogLevelErrorAndArgumentNullExceptionToWriteLogEntry_Then_LogFileExistsAndLogLevelIsErrorAndMessageAsExpectedAndExceptionIsArgumentNullException()
		{
			Given_LogFileMissing();

			//When

			var expectedException = new ArgumentNullException();
			m_LogMessageWriter.WriteLogEntry(LogLevel.Error, TestMessage,expectedException);

			//Then
			var logColumns = Then_FileExistsExtractLogText();
			Assert.AreEqual(LogLevel.Error.ToString().ToUpper() + ":", logColumns[9]);
			Assert.AreEqual(TestMessage, logColumns[13]);
			Assert.AreEqual(expectedException.ToString(), logColumns[14].TrimEnd());

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
