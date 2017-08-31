using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using NLog.Targets;

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
    }

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CustomPropertiesTest
    {
        readonly Log<CustomPropertiesTest> m_Log = new Log<CustomPropertiesTest>();

        [TestMethod]
        public void When_a_custom_property_is_passed_to_log_write_Then_the_property_can_be_written_to_a_log_target()
        {
            const string customPropertyName = "myProperty";

            var target = new MemoryTarget { Layout = $"${{event-properties:item={customPropertyName}}} ${{event-properties:item=Method}}", Name = "customPropertyTarget" };
            LogManager.Configuration.AddTarget(target);
            LogManager.Configuration.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Info, target.Name);
            LogManager.Configuration.Reload();

            m_Log.Write(LogLevel.Info, () => "Hallo Logger", customProperties: new KeyValuePair<string, string>(customPropertyName, "myPropertyValue"));

            target.Logs[0].Should().Be($"myPropertyValue {MethodBase.GetCurrentMethod().Name}");
        }
    }
}
