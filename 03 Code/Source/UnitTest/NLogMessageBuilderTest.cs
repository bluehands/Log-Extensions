using System;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class NLogMessageBuilderTest
    {
        [TestMethod]
        public void GetLogEventInfosTestWithoutExeption()
        {
            //Arrage
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = nLogMessageBuilder.GetLogEventInfo(LogLevel.Fatal, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Fatal.ToString(), logEventInfo.Level.ToString());
            Assert.AreEqual("Log: bla bla bla", logEventInfo.Message);
            Assert.AreEqual(callerOfGround.ToString(), logEventInfo.LoggerName);
        }

        [TestMethod]
        public void GetLogEventInfosTestWithExeption()
        {
            //Arrage
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
            var callerOfGround = typeof(NLogMessageBuilderTest);
            var exeption = new NotImplementedException();

            //Act
            var logEventInfo = nLogMessageBuilder.GetLogEventInfo(LogLevel.Fatal, "Log: bla bla bla", callerOfGround, exeption);

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
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = nLogMessageBuilder.GetLogEventInfo(LogLevel.Fatal, "Log: bla bla bla", callerOfGround, null);

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
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));

            //Act
            nLogMessageBuilder.GetLogEventInfo(LogLevel.Debug, null, null, null);
        }

        [TestMethod]
        public void CheckLogLevelFatal()
        {
            //Arrage
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = nLogMessageBuilder.GetLogEventInfo(LogLevel.Fatal, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Fatal.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelError()
        {
            //Arrage
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = nLogMessageBuilder.GetLogEventInfo(LogLevel.Error, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Error.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelWarning()
        {
            //Arrage
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = nLogMessageBuilder.GetLogEventInfo(LogLevel.Warning, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Warn.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelInfo()
        {
            //Arrage
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = nLogMessageBuilder.GetLogEventInfo(LogLevel.Info, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Info.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelDebug()
        {
            //Arrage
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = nLogMessageBuilder.GetLogEventInfo(LogLevel.Debug, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Debug.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelTrace()
        {
            //Arrage
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
            var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = nLogMessageBuilder.GetLogEventInfo(LogLevel.Trace, "Log: bla bla bla", callerOfGround, null);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Trace.ToString(), logEventInfo.Level.ToString());
        }

        //[TestMethod]
        //public void CheckLogLevel()
        //{
        //    //Arrage
        //    var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
        //    var callerOfGround = typeof(NLogMessageBuilderTest);

        //    //Act

        //    foreach (var level in Enum.GetValues(typeof(LogLevel)))
        //    {
        //        LogLevel foo = (LogLevel)level;
        //        var logEventInfo = nLogMessageBuilder.GetLogEventInfo(foo, "Log: bla bla bla", callerOfGround, null);
        //    }
        //}
    }
}
