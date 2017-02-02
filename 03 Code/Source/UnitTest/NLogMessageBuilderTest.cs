using System;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using LogLevel = Bluehands.Repository.Diagnostics.Log.LogLevel;

namespace UnitTest
{
    [TestClass]
    public class NLogMessageBuilderTest
    {
        [TestMethod]
        public void GetLogEventInfosTestWithoutExeption()
        {
            //Arrage
            var callerInfo = new CallerInfo("Aydin", "Laura", "Liwen");
            var sut = new NLogMessageBuilder("Marcel");
            //Act
            var logEventInfo = sut.BuildNLogEventInfo(LogLevel.Fatal, "Log: bla bla bla"/*, callerOfGround*/, null, callerInfo);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Fatal, logEventInfo.Level);
            Assert.AreEqual("Log: bla bla bla", logEventInfo.Message);
            Assert.IsNull(logEventInfo.Exception);
            Assert.AreEqual("Aydin", logEventInfo.Properties["typeOfCallerOfGround"]);
            Assert.AreEqual("Laura", logEventInfo.Properties["classOfCallerOfGround"]);
            Assert.AreEqual("Liwen", logEventInfo.Properties["methodOfCallerOfGround"]);
            Assert.AreEqual("Marcel", logEventInfo.LoggerName);
        }

        [TestMethod]
        public void GetLogEventInfosTestWithExeption()
        {
            //Arrage
            var callerInfo = new CallerInfo("Aydin", "Laura", "Liwen");
            var sut = new NLogMessageBuilder("Marcel");
            var exeption = new NotImplementedException();

            //Act
            var logEventInfo = sut.BuildNLogEventInfo(LogLevel.Fatal, "Log: bla bla bla"/*, callerOfGround*/, exeption, callerInfo);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Fatal, logEventInfo.Level);
            Assert.AreEqual("Log: bla bla bla", logEventInfo.Message);
            Assert.AreEqual("Aydin", logEventInfo.Properties["typeOfCallerOfGround"]);
            Assert.AreEqual("Laura", logEventInfo.Properties["classOfCallerOfGround"]);
            Assert.AreEqual("Liwen", logEventInfo.Properties["methodOfCallerOfGround"]);
            Assert.AreEqual("Marcel", logEventInfo.LoggerName);
            Assert.AreEqual(exeption, logEventInfo.Exception);
        }

        [TestMethod]
        public void CheckLogEventInfoIsNotNull()
        {
            //Arrage
            var callerInfo = new CallerInfo("Aydin", "Laura", "Liwen");
            var sut = new NLogMessageBuilder("Marcel");
            //var callerOfGround = typeof(NLogMessageBuilderTest);

            //Act
            var logEventInfo = sut.BuildNLogEventInfo(LogLevel.Fatal, "Log: bla bla bla"/*, callerOfGround*/, null, callerInfo);

            //Assert
            Assert.IsNotNull(logEventInfo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckCtorForString()
        {
            //Arrange
            var nLogMessageBuilder = new NLogMessageBuilder(null);
        }

        [TestMethod]
        public void CheckLogLevelFatal()
        {
            //Arrage
            var callerInfo = new CallerInfo("Aydin", "Laura", "Liwen");
            var sut = new NLogMessageBuilder("Marcel");

            //Act
            var logEventInfo = sut.BuildNLogEventInfo(LogLevel.Fatal, "Log: bla bla bla" /*, callerOfGround*/, null, callerInfo);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Fatal.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelError()
        {
            //Arrage
            var callerInfo = new CallerInfo("Aydin", "Laura", "Liwen");
            var sut = new NLogMessageBuilder("Marcel");

            //Act
            var logEventInfo = sut.BuildNLogEventInfo(LogLevel.Error, "Log: bla bla bla", /*callerOfGround,*/ null, callerInfo);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Error.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelWarning()
        {
            //Arrage
            var callerInfo = new CallerInfo("Aydin", "Laura", "Liwen");
            var sut = new NLogMessageBuilder("Marcel");

            //Act
            var logEventInfo = sut.BuildNLogEventInfo(LogLevel.Warning, "Log: bla bla bla", /*callerOfGround,*/ null, callerInfo);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Warn.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelInfo()
        {
            //Arrage
            var callerInfo = new CallerInfo("Aydin", "Laura", "Liwen");
            var sut = new NLogMessageBuilder("Marcel");

            //Act
            var logEventInfo = sut.BuildNLogEventInfo(LogLevel.Info, "Log: bla bla bla"/*, callerOfGround*/, null, callerInfo);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Info.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelDebug()
        {
            //Arrage
            var callerInfo = new CallerInfo("Aydin", "Laura", "Liwen");
            var sut = new NLogMessageBuilder("Marcel");

            //Act
            var logEventInfo = sut.BuildNLogEventInfo(LogLevel.Debug, "Log: bla bla bla"/*, callerOfGround*/, null, callerInfo);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Debug.ToString(), logEventInfo.Level.ToString());
        }

        [TestMethod]
        public void CheckLogLevelTrace()
        {
            //Arrage
            var callerInfo = new CallerInfo("Aydin", "Laura", "Liwen");
            var sut = new NLogMessageBuilder("Marcel");

            //Act
            var logEventInfo = sut.BuildNLogEventInfo(LogLevel.Trace, "Log: bla bla bla"/*, callerOfGround*/, null, callerInfo);

            //Assert
            Assert.AreEqual(NLog.LogLevel.Trace.ToString(), logEventInfo.Level.ToString());
        }


        [TestMethod]
        public void CheckLogLevelDefault()
        {
            //Arrage
            var callerInfo = new CallerInfo("Aydin", "Laura", "Liwen");
            var sut = new NLogMessageBuilder("Marcel");

            //Act
            var logEventInfo = sut.BuildNLogEventInfo((LogLevel)7, "Log: bla bla bla", null, callerInfo);

            //Assert
            Assert.AreEqual(LogLevel.Trace.ToString(), logEventInfo.Level.ToString());
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
