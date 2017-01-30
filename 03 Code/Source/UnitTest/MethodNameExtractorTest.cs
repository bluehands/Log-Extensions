using System;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    public class SampleCallerOfGroundForExtractor
    {
        private readonly MethodNameExtracter m_Extractor;

        public SampleCallerOfGroundForExtractor(MethodNameExtracter extractor)
        {
            m_Extractor = extractor;
        }
        public CallerInfo DoIt()
        {
            return m_Extractor.ExtractCallerInfoFromStackTrace();
        }
    }

    [TestClass]
    public class MethodNameExtractorTest
    {
        [TestMethod]
        public void ExtractWithSampleCall()
        {
            //Arrange
            var sut = new MethodNameExtracter(typeof(SampleCallerOfGroundForExtractor));
            var sample = new SampleCallerOfGroundForExtractor(sut);
            //Act
            var callerInfo = sample.DoIt();
            //Assert
            Assert.IsNotNull(callerInfo);
            Assert.AreEqual(nameof(MethodNameExtractorTest), callerInfo.ClassNameOfGround);
            Assert.AreEqual(nameof(ExtractWithSampleCall), callerInfo.MethodNameOfCallerOfGround);
            Assert.AreEqual(typeof(MethodNameExtractorTest).ToString(), callerInfo.TypeOfCallerOfGround);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckForNullInCtor()
        {
            //Arrange
            var methodNameExtracter = new MethodNameExtracter(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CheckFrameIsNotInStackTrace()
        {
            //Arrange
            var sut = new MethodNameExtracter(typeof(NotUsedClass));
            var sample = new SampleCallerOfGroundForExtractor(sut);
            //Act
            sample.DoIt();
        }
    }
}
