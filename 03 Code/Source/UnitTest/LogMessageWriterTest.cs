using System;
using System.IO;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using LogLevel = Bluehands.Repository.Diagnostics.Log.LogLevel;

namespace UnitTest
{
    [TestClass]
    public class LogMessageWriterTest
    {

        private class SampleGroundForLogMessageWriterTest
        {
            private LogMessageWriter m_LogMessageWriter;

            public SampleGroundForLogMessageWriterTest()
            {
                m_LogMessageWriter = new LogMessageWriter(typeof(LogMessageWriterTest),
                    typeof(SampleGroundForLogMessageWriterTest));
            }

            public void WriteLogEntryWithoutException(LogLevel level, string message)
            {
                m_LogMessageWriter.WriteLogEntry(level, message);
            }
            public void WriteLogEntryWithException(LogLevel level, string message)
            {
                var exception = new NotImplementedException();
                m_LogMessageWriter.WriteLogEntry(level, message, exception);
            }
        }

        private static SampleGroundForLogMessageWriterTest s_Sut = new SampleGroundForLogMessageWriterTest();

        [TestMethod]
        public void PossitivTestWriteLogEntryWithoutException()
        {
            //Arange
            File.Delete("logTest.txt");
            s_Sut.WriteLogEntryWithoutException(LogLevel.Fatal, "logg emol ebbes anres uewer WriteLogEntryWithoutException");

            //Act
            var completeLogText = File.ReadAllText("logTest.txt");

            string[] stringSeparator = { "," };
            var splitedLogTextArray = completeLogText.Split(stringSeparator, StringSplitOptions.None);


            //Assert TODO
            Assert.AreEqual(LogLevel.Fatal.ToString(), splitedLogTextArray[0]);
            Assert.AreEqual("UnitTest.LogMessageWriterTest", splitedLogTextArray[1]);
            Assert.AreEqual("LogMessageWriterTest", splitedLogTextArray[2]);
            Assert.AreEqual("PossitivTestWriteLogEntryWithoutException", splitedLogTextArray[3]);
            Assert.AreEqual("", splitedLogTextArray[4]);
            Assert.AreEqual("logg emol ebbes anres uewer WriteLogEntryWithoutException", splitedLogTextArray[5]);
            Assert.AreEqual("UnitTest.LogMessageWriterTest", splitedLogTextArray[6]);
        }

        [TestMethod]
        public void PossitivTestWriteLogEntryWithException()
        {
            //Arange
            File.Delete("logTest.txt");
            s_Sut.WriteLogEntryWithException(LogLevel.Fatal, "logg emol ebbes anres uewer WriteLogEntryWithException");

            //Act
            var completeLogText = File.ReadAllText("logTest.txt");

            string[] stringSeparator = { "," };
            var splitedLogTextArray = completeLogText.Split(stringSeparator, StringSplitOptions.None);


            //Assert TODO
            Assert.AreEqual(LogLevel.Fatal.ToString(), splitedLogTextArray[0]);
            Assert.AreEqual("UnitTest.LogMessageWriterTest", splitedLogTextArray[1]);
            Assert.AreEqual("LogMessageWriterTest", splitedLogTextArray[2]);
            Assert.AreEqual("PossitivTestWriteLogEntryWithException", splitedLogTextArray[3]);
            Assert.AreEqual("Die Methode oder der Vorgang ist nicht implementiert.", splitedLogTextArray[4]);
            Assert.AreEqual("logg emol ebbes anres uewer WriteLogEntryWithException", splitedLogTextArray[5]);
            Assert.AreEqual("UnitTest.LogMessageWriterTest", splitedLogTextArray[6]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckCtor()
        {
            //Arrange
            var logMessageWriter = new LogMessageWriter(null, null);
        }

        //[TestMethod]
        //[ExpectedException(typeof(NotImplementedException))]
        //public void OnlyLogLevel()
        //{
        //    //Arrange
        //    var sampleGroundForLogMessageWriterTest = new SampleGroundForLogMessageWriterTest();

        //    //Act
        //    sampleGroundForLogMessageWriterTest.DoItSampleGround(LogLevel.Fatal, null);
        //}
    }
}
