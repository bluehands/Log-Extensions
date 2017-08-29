using System;
using System.Diagnostics;
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
		private static readonly Func<string> TestMessage = () => "Test message.";

		[TestMethod]
		public void Given_ArgumentNullExceptionArgument_When_WriteLogEntryWithLogLevelErrorAndCallerName_Then_LogStringContainsErrorAndTestMessageAndArgumentNullException()
		{
			var writer = new StringWriter();
			Console.SetOut(writer);

			//When
			var expectedException = new ArgumentNullException();
			m_LogMessageWriter.WriteLogEntry(LogLevel.Error, TestMessage, System.Reflection.MethodBase.GetCurrentMethod().Name, expectedException);
			var logString = writer.ToString();
			Debug.WriteLine(logString);

			//Then
			var logColumns = logString.Split('|');
			Assert.AreEqual(LogLevel.Error.ToString().ToUpper() + ":", logColumns[2].Trim());
			Assert.AreEqual(TestMessage(), logColumns[6].Trim());
			Assert.IsTrue(logColumns[7].Contains(expectedException.ToString()));

		}

        [TestMethod]
        public void Given_TestLoggerNameWithGenericClassArguments_Then_LogClassNameShouldContainGenericTypes()
        {
            var writer = new StringWriter();
            Console.SetOut(writer);

            //When
            var messageWriter = new LogMessageWriter(typeof(MyGenericClass<string>));
            messageWriter.WriteLogEntry(LogLevel.Debug, () => "test log", "TestLoggerNameWithGenericClassArguments");
            var logString = writer.ToString();
            Debug.WriteLine(logString);

            //Then
            Assert.IsTrue(logString.Contains("MyGenericClass<string>|TestLoggerNameWithGenericClassArguments|test log"));
        }

        private static class MyGenericClass<T> { }
    }
}
