using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bluehands.Repository.Diagnostics.Log.Aspects.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bluehands.Repository.Diagnostics.Log.Aspects.Test
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class AutoTraceAttributeTest
	{
		private readonly Log m_Log = new Log<AutoTraceAttributeTest>();
		private const string TestMessage = "Test message.";

		[TestMethod]
		[AutoTrace(TestMessage)]
		public void Given_TestMessageAndLogger_When_AutoTraceCreated_Then_LogFileContainsStartEndTraceEntries()
		{
			//Given
			var writer = new StringWriter();
			Console.SetOut(writer);

			//When
			m_Log.Warning(() => "Warning test.");
			m_Log.Info(() => "Info test.");
			m_Log.Fatal(() => "Fatal test.");
			m_Log.Debug(() => "Debug test.");

			var logString = writer.ToString();

			//Then
			var logColumns = logString.Split('|');
			var traceEntries = logColumns.Where(c => !c.Contains("TRACE:"));
			Assert.IsTrue(traceEntries.Count() >= 2);
		}

	}

	[TestClass]
	[ExcludeFromCodeCoverage]
	public class AutoTraceAttributeWithAsyncTest
	{
		private static readonly Log s_Log = new Log<AutoTraceAttributeWithAsyncTest>();

		[AutoTrace("FirstLevelMessage")]
		public async Task FirstLevelAsyncMethod()
		{
			s_Log.Info($"In {nameof(FirstLevelAsyncMethod)}");

			await SecondLevelAsyncMethod();
			s_Log.Info("Hallo in auto traced section");
			await ThirdLevelAsyncMethod();

			s_Log.Info("Hallo after traced section");
		}

		[AutoTrace("SecondLevelMessage")]
		private async Task SecondLevelAsyncMethod()
		{
			s_Log.Info("Hallo in auto traced section");
			await ThirdLevelAsyncMethod();
			await Task.Delay(200).ConfigureAwait(false);
		}

		[AutoTrace("ThirdLevelMessage")]
		private async Task ThirdLevelAsyncMethod()
		{
			s_Log.Info("Hallo in auto traced section");
			await Task.Delay(200).ConfigureAwait(false);
		}

		[TestMethod]
		[ExcludeFromCodeCoverage]
		public async Task Given_AsyncMethodWithAutoTrace_When_RunAsyncMethod_Then_LogStringMatchesExpectations()
		{
			//Given
			var writer = new StringWriter();
			Console.SetOut(writer);

			const int expectedEnterNum = 4;
			const int expectedLeaveNum = 4;
			const int expectedMaxIndent = 6;

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

				var rowIndent = row.ToCharArray().Count(chr => chr == ' ');
				rowIndent /= 2;
				maxIndent = Math.Max(rowIndent, maxIndent);
			}

			Assert.AreEqual(expectedEnterNum, enterCounter);
			Assert.AreEqual(expectedLeaveNum, leaveCounter);
			Assert.AreEqual(expectedMaxIndent, maxIndent);

		}
	}
}
