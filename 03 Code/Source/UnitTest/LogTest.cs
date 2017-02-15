using System;
using System.IO;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using LogLevel = Bluehands.Repository.Diagnostics.Log.LogLevel;

namespace UnitTest
{
    [TestClass]
    public class LogTest
    {
        private Log m_Log;

        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void LogFatalWithoutException()
        {
            //Arrange
            Arrange();

            var exceptionString = "";

            //Act
            m_Log.Fatal("logg e mol");

            var splitLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitLogTextArray, LogLevel.Fatal, exceptionString, "LogFatalWithoutException");
        }

        [TestMethod]
        public void LogFatalWithException()
        {
            //Arrange
            Arrange();
            var exception = new NotImplementedException();
            var exceptionString = exception.Message;

            //Act
            m_Log.Fatal(exception, "logg e mol");
            var splitedLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitedLogTextArray, LogLevel.Fatal, exceptionString, "LogFatalWithException");
        }

        [TestMethod]
        public void LogErrorWithoutException()
        {
            //Arrange
            Arrange();

            var exceptionString = "";

            //Act
            m_Log.Error("logg e mol");

            var splitedLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitedLogTextArray, LogLevel.Error, exceptionString, "LogErrorWithoutException");
        }

        [TestMethod]
        public void LogErrorWithException()
        {
            //Arrange
            Arrange();
            var exception = new NotImplementedException();
            var exceptionString = exception.Message;

            //Act
            m_Log.Error(exception, "logg e mol");
            var splitedLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitedLogTextArray, LogLevel.Error, exceptionString, "LogErrorWithException");
        }

        [TestMethod]
        public void LogWarningWithoutException()
        {
            //Arrange
            Arrange();

            var exceptionString = "";

            //Act
            m_Log.Warning("logg e mol");

            var splitedLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitedLogTextArray, LogLevel.Warning, exceptionString, "LogWarningWithoutException");
        }

        [TestMethod]
        public void LogWarningWithException()
        {
            //Arrange
            Arrange();
            var exception = new NotImplementedException();
            var exceptionString = exception.Message;

            //Act
            m_Log.Warning(exception, "logg e mol");

            //Assert
            var splitedLogTextArray = GetSplitLogTextArray();
            Assert(splitedLogTextArray, LogLevel.Warning, exceptionString, "LogWarningWithException");
        }

        [TestMethod]
        public void LogInfoWithoutException()
        {
            //Arrange
            Arrange();

            var exceptionString = "";

            //Act
            m_Log.Info("logg e mol");

            var splitedLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitedLogTextArray, LogLevel.Info, exceptionString, "LogInfoWithoutException");
        }

        [TestMethod]
        public void LogInfoWithException()
        {
            //Arrange
            Arrange();
            var exception = new NotImplementedException();
            var exceptionString = exception.Message;

            //Act
            m_Log.Info(exception, "logg e mol");
            var splitedLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitedLogTextArray, LogLevel.Info, exceptionString, "LogInfoWithException");
        }

        [TestMethod]
        public void LogDebugWithoutException()
        {
            //Arrange
            Arrange();

            var exceptionString = "";

            //Act
            m_Log.Debug("logg e mol");

            var splitedLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitedLogTextArray, LogLevel.Debug, exceptionString, "LogDebugWithoutException");
        }

        [TestMethod]
        public void LogDebugWithException()
        {
            //Arrange
            Arrange();
            var exception = new NotImplementedException();
            var exceptionString = exception.Message;

            //Act
            m_Log.Debug(exception, "logg e mol");
            var splitedLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitedLogTextArray, LogLevel.Debug, exceptionString, "LogDebugWithException");
        }

        [TestMethod]
        public void LogTraceWithoutException()
        {
            //Arrange
            Arrange();

            var exceptionString = "";

            //Act
            m_Log.Trace("logg e mol");

            var splitedLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitedLogTextArray, LogLevel.Trace, exceptionString, "LogTraceWithoutException");
        }

        [TestMethod]
        public void LogTraceWithException()
        {
            //Arrange
            Arrange();
            var exception = new NotImplementedException();
            var exceptionString = exception.Message;

            //Act
            m_Log.Trace(exception, "logg e mol");
            var splitedLogTextArray = GetSplitLogTextArray();

            //Assert
            Assert(splitedLogTextArray, LogLevel.Trace, exceptionString, "LogTraceWithException");
            //AreEqual("LogTraceWithException", splitedLogTextArray[3]);
        }

        private void Arrange()
        {
            File.Delete("logTest.txt");
            m_Log = new Log(typeof(LogTest));
        }

        private static string[] GetSplitLogTextArray()
        {
            var completeLogText = File.ReadAllText("logTest.txt");

            string[] stringSeparator = { "," };
            var splitedLogTextArray = completeLogText.Split(stringSeparator, StringSplitOptions.None);
            return splitedLogTextArray;
        }

        private static void Assert(string[] splitedLogTextArray, LogLevel logLevel, string exception, string method)
        {
            var nLogLevel = GetNLogLevel(logLevel);

            AreEqual(nLogLevel.ToString(), splitedLogTextArray[0]);
            AreEqual("UnitTest.LogTest", splitedLogTextArray[1]);
            AreEqual("LogTest", splitedLogTextArray[2]);
            AreEqual(method, splitedLogTextArray[3]);
            AreEqual(exception, splitedLogTextArray[4]);
            AreEqual("", splitedLogTextArray[5]);
            AreEqual("logg e mol", splitedLogTextArray[6]);
            AreEqual("UnitTest.LogTest", splitedLogTextArray[7]);
            AreEqual(splitedLogTextArray[1], splitedLogTextArray[7]);
        }

        private static NLog.LogLevel GetNLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Warning:
                    return NLog.LogLevel.Warn;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                default:
                    return NLog.LogLevel.Trace;
            }
        }

    }
}
