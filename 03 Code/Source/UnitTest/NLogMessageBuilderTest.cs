using System;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class NLogMessageBuilderTest
    {
        [TestMethod]
        public void GetLogEventInfosTest()
        {
            //Arrage
            var nLogMessageBuilder = new NLogMessageBuilder(typeof(NLogMessageBuilderTest));
            Type type = typeof(NLogMessageBuilderTest);


            //Act
            var logEventInfo = nLogMessageBuilder.GetLogEventInfo(LogLevel.Fatal, "Log: bla bla bla", type, null);

            //Assert

            Assert.IsNotNull(logEventInfo);

        }


        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void CheckForNullInCtor()
        //{
        //    //Arrange

        //}
    }
}
