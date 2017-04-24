﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bluehands.Repository.Diagnostics.Log.Test
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class AutoTraceTest
	{
		private readonly Log m_Log = new Log<AutoTraceTest>();
		//private const string LogFilePath = "./Logs/test.log";
		private const string TestMessage = "Test message.";

		[TestMethod]
		public void Given_TestMessageAndLogger_When_AutoTraceCreated_Then_LogFileContainsStartEndTraceEntries()
		{
			//Given
			var writer = new StringWriter();
			Console.SetOut(writer);

			//When
			using (new AutoTrace(m_Log, () => TestMessage))
			{
				m_Log.Warning("Warning test.");
				m_Log.Info("Info test.");
				m_Log.Fatal("Fatal test.");
				m_Log.Debug("Debug test.");
			}
			var logString = writer.ToString();

			//Then
			var logColumns = logString.Split('|');
			var traceEntries = logColumns.Where(c => !c.Contains("TRACE:"));
			Assert.IsTrue(traceEntries.Count() >= 2);
		}

	}

	[TestClass]
	[ExcludeFromCodeCoverage]
	public class AutoTraceWithAsyncTest
	{
		private static readonly Log s_Log = new Log<AutoTraceWithAsyncTest>();

		public async Task FirstLevelAsyncMethod()
		{
			s_Log.Info($"In {nameof(FirstLevelAsyncMethod)}");

			using (s_Log.AutoTrace("FirstLevelMessage"))
			{
				await SecondLevelAsyncMethod();
				s_Log.Info("Hallo in auto traced section");
				await ThirdLevelAsyncMethod();
			}

			s_Log.Info("Hallo after traced section");
		}

		private static async Task SecondLevelAsyncMethod()
		{
			using (s_Log.AutoTrace("SecondLevelMessage"))
			{
				s_Log.Info("Hallo in auto traced section");
				await ThirdLevelAsyncMethod();
				await Task.Delay(200).ConfigureAwait(false);
			}
			
		}

		private static async Task ThirdLevelAsyncMethod()
		{
			using (s_Log.AutoTrace("ThirdLevelMessage"))
			{
				s_Log.Info("Hallo in auto traced section");
				await Task.Delay(200).ConfigureAwait(false);
			}
		}

		[TestMethod]
		public async Task Given_AsyncMethodWithAutoTrace_When_RunAsyncMethod_Then_LogStringMatchesExpectations()
		{
			//Given
			var writer = new StringWriter();
			Console.SetOut(writer);

			const int expectedEnterNum = 4;
			const int expectedLeaveNum = 4;
			const int expectedMaxIndent = 3;

			//When
			await FirstLevelAsyncMethod();


			//Then
			var logString = writer.ToString();
			var logRows = logString.Split('\r');

			var enterCounter = 0;
			var leaveCounter = 0;
			var maxIndent = 0;

			foreach (var row in logRows)
			{
				if (row.Contains("Enter")) { enterCounter++; }
				if (row.Contains("Leave")) { leaveCounter++; }

				var rowIndent = row.ToCharArray().Count(chr => chr == '\t');
				maxIndent = Math.Max(rowIndent, maxIndent);
			}

			Assert.AreEqual(expectedEnterNum, enterCounter);
			Assert.AreEqual(expectedLeaveNum, leaveCounter);
			Assert.AreEqual(expectedMaxIndent, maxIndent);

		}
	}

}
