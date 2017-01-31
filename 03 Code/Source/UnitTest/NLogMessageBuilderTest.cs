using System;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using LogLevel = Bluehands.Repository.Diagnostics.Log.LogLevel;

namespace UnitTest
{
    public class SampelCallerForNLogMessageBuilderTest
    {
        private readonly NLogMessageBuilder m_NLogMessageBuilder;

        public SampelCallerForNLogMessageBuilderTest()
        {
            m_NLogMessageBuilder = new NLogMessageBuilder(typeof(SampelCallerForNLogMessageBuilderTest));
        }

        public LogEventInfo DoIt(LogLevel level, string message, Type callerOfGround, Exception exception)
        {
            var logEnventInfo = m_NLogMessageBuilder.GetLogEventInfo(level, message, callerOfGround, exception);
            return logEnventInfo;
        }
    }

    [TestClass]
    public class NLogMessageBuilderTest
    {
        [TestMethod]
        public void GetLogEventInfosTestWithoutExeption()
        {
            //Arrage
            var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = sampelCallerForNLogMessageBuilderTest.DoIt(LogLevel.Fatal, "Log: bla bla bla", callerOfGround, null);


            //Assert
            Assert.AreEqual(NLog.LogLevel.Fatal.ToString(), logEventInfo.Level.ToString());
            Assert.AreEqual("Log: bla bla bla", logEventInfo.Message);
            Assert.AreEqual(callerOfGround.ToString(), logEventInfo.LoggerName);
        }

        [TestMethod]
        public void GetLogEventInfosTestWithExeption()
        {
            //Arrage
            var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();
            var callerOfGround = typeof(NLogMessageBuilderTest);
            var exeption = new NotImplementedException();

            //Act
            var logEventInfo = sampelCallerForNLogMessageBuilderTest.DoIt(LogLevel.Fatal, "Log: bla bla bla", callerOfGround, exeption);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Fatal.ToString(), logEventInfo.Level.ToString());
            Assert.AreEqual("Log: bla bla bla", logEventInfo.Message);
            Assert.AreEqual(callerOfGround.ToString(), logEventInfo.LoggerName);
            Assert.AreEqual(exeption, logEventInfo.Exception);
        }

        [TestMethod]
        public void CheckLogEventInfoForNull()
        {
            //Arrage
            var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = sampelCallerForNLogMessageBuilderTest.DoIt(LogLevel.Fatal, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.IsNotNull(logEventInfo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckCtor()
        {
            //Arrange
            var nLogMessageBuilder = new NLogMessageBuilder(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckParams()
        {
            //Arrage
            var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();

            //Act
            sampelCallerForNLogMessageBuilderTest.DoIt(LogLevel.Debug, null, null, null);
        }

        [TestMethod]
        public void CheckLogLevelFatal()
        {
            //Arrage
            var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = sampelCallerForNLogMessageBuilderTest.DoIt(LogLevel.Fatal, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Fatal.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelError()
        {
            //Arrage
            var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = sampelCallerForNLogMessageBuilderTest.DoIt(LogLevel.Error, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Error.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelWarning()
        {
            //Arrage
            var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = sampelCallerForNLogMessageBuilderTest.DoIt(LogLevel.Warning, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Warn.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelInfo()
        {
            //Arrage
            var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = sampelCallerForNLogMessageBuilderTest.DoIt(LogLevel.Info, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Info.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelDebug()
        {
            //Arrage
            var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = sampelCallerForNLogMessageBuilderTest.DoIt(LogLevel.Debug, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Debug.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelTrace()
        {
            //Arrage
            var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = sampelCallerForNLogMessageBuilderTest.DoIt(LogLevel.Trace, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Trace.ToString(), logEventInfo.Level.ToString());
        }

        //[TestMethod]
        //public void CheckLogLevel()
        //{
        //    //Arrage
        //var sampelCallerForNLogMessageBuilderTest = new SampelCallerForNLogMessageBuilderTest();
        //    var callerOfGround = typeof(NLogMessageBuilderTest);

        //    //Act

        //    foreach (var level in Enum.GetValues(typeof(LogLevel)))
        //    {
        //        LogLevel foo = (LogLevel)level;
        //        var logEventInfo = sampelCallerForNLogMessageBuilderTest.DoIt(foo, "Log: bla bla bla", callerOfGround, null);
        //    }
        //}
    }
}
