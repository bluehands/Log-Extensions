using System;
using System.IO;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static NLog.LogLevel;

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
            Assert(splitLogTextArray, Fatal, exceptionString);
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
            Assert(splitedLogTextArray, Fatal, exceptionString);
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
            Assert(splitedLogTextArray, Error, exceptionString);
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
            Assert(splitedLogTextArray, Error, exceptionString);
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
            Assert(splitedLogTextArray, Warn, exceptionString);
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
            Assert(splitedLogTextArray, Warn, exceptionString);
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
            Assert(splitedLogTextArray, Info, exceptionString);
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
            Assert(splitedLogTextArray, Info, exceptionString);
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
            Assert(splitedLogTextArray, Debug, exceptionString);
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
            Assert(splitedLogTextArray, Debug, exceptionString);
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
            Assert(splitedLogTextArray, Trace, exceptionString);
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
            Assert(splitedLogTextArray, Trace, exceptionString);
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

        private static void Assert(string[] splitedLogTextArray, NLog.LogLevel logLevel, string exception)
        {
            AreEqual(logLevel.ToString(), splitedLogTextArray[0]);
            AreEqual("UnitTest.LogTest", splitedLogTextArray[1]);
            AreEqual("LogTest", splitedLogTextArray[2]);
            AreEqual(exception, splitedLogTextArray[4]);
            AreEqual("logg e mol", splitedLogTextArray[5]);
            AreEqual("UnitTest.LogTest", splitedLogTextArray[6]);
            AreEqual(splitedLogTextArray[1], splitedLogTextArray[6]);
        }
    }
}
