using System;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class LogMessageWriterTest
    {
        private class SampleGroundForLogMessageWriterTest
        {
            private LogMessageWriter m_LogMessageWriter;

            public SampleGroundForLogMessageWriterTest()       //Log.cs
            {
                m_LogMessageWriter = new LogMessageWriter(typeof(LogMessageWriterTest),
                    typeof(SampleGroundForLogMessageWriterTest));
            }

            public void DoItSampleGround(LogLevel level, string message)
            {
                var exception = new NotImplementedException();
                m_LogMessageWriter.WriteLogEntry(level, message);
                m_LogMessageWriter.WriteLogEntry(level, message, exception);
            }
        }

        [TestMethod]
        public void PossitivTestWriteLogEntry()
        {
            //Arange
            var sampleGroundForLogMessageWriterTest = new SampleGroundForLogMessageWriterTest();

            //Act
            sampleGroundForLogMessageWriterTest.DoItSampleGround(LogLevel.Fatal, "TestLog");

            //Assert
            //Assert.AreEqual(LogLevel.Fatal, )
        }
    }
}
