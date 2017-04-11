using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			using (new AutoTrace(m_Log, TestMessage))
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
	public class AutoTraceWithAsyncTest
	{
		private static readonly Log m_Log = new Log<AutoTraceWithAsyncTest>();

		public async Task FirstLevelAsyncMethod()
		{
			m_Log.Info($"Entered {nameof(FirstLevelAsyncMethod)}");

			using (m_Log.AutoTrace("FirstLevelMessage"))
			{
				await SecondLevelAsyncMethod();
				m_Log.Info("Hallo in auto traced section");
				await SecondLevelAsyncMethod();
			}

			m_Log.Info("Hallo after traced section");
		}

		private static ConfiguredTaskAwaitable SecondLevelAsyncMethod()
		{
			using (m_Log.AutoTrace("SecondLevelMessage"))
			{
				return Task.Delay(200).ConfigureAwait(false);
			}
			
		}

		[TestMethod]
		public async Task AsynAutoTraceTest()
		{
			var writer = new StringWriter();
			Console.SetOut(writer);

			await FirstLevelAsyncMethod();

			var logString = writer.ToString();
		}
	}

}
