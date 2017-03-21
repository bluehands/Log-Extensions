using System;
using System.IO;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLevel = Bluehands.Repository.Diagnostics.Log.LogLevel;

namespace UnitTest
{
	[TestClass]
	public class LogMessageWriterTest
	{
		private readonly LogMessageWriter m_Sut = new LogMessageWriter(typeof(LogMessageWriterTest));
		private const string LogFilePath = "./Logs/test.log";


		[TestMethod]
		public void Given_LogFileMissing_When_LogLevelIsDebugAndMessageIsNull_Then_LogFileMissing()
		{
			Given_LogFileMissing();

			//When
			m_Sut.WriteLogEntry(LogLevel.Debug, null);

			//Then
			Assert.IsFalse(File.Exists(LogFilePath));
		}

		[TestMethod]
		public void Given_LogFileMissing_When_MessageIsGivenAndLogLevelDebugEnabled_Then_LogFileIsWritten()
		{
			Given_LogFileMissing();

			//When
			const string expectedMessage = "Test message.";
			m_Sut.WriteLogEntry(LogLevel.Debug, expectedMessage);

			//Then
			Assert.IsTrue(File.Exists(LogFilePath));
			var logText = File.ReadAllText(LogFilePath);
			char[] separator = {'\t'};
			var logColumns = logText.Split(separator, StringSplitOptions.None);
			Assert.AreEqual(LogLevel.Debug.ToString().ToUpper() + ":", logColumns[9]);
			Assert.AreEqual(expectedMessage, logColumns[13]);

		}

		private static void Given_LogFileMissing()
		{
			if (File.Exists(LogFilePath))
			{
				File.Delete(LogFilePath);
			}
		}
	}
}
