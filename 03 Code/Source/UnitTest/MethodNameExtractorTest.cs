using System;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    public class SampleMessageCreatorForExtractor
    {
        private readonly MethodNameExtracter m_Extractor;

        public SampleMessageCreatorForExtractor(MethodNameExtracter extractor)
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
            var sut = new MethodNameExtracter(typeof(SampleMessageCreatorForExtractor));
            var sample = new SampleMessageCreatorForExtractor(sut);
            //Act
            var callerInfo = sample.DoIt();
            //Assert
            Assert.IsNotNull(callerInfo);
            Assert.AreEqual(nameof(MethodNameExtractorTest), callerInfo.ClassNameOfGround);
            Assert.AreEqual(nameof(ExtractWithSampleCall), callerInfo.MethodNameOfMessageCreator);
            Assert.AreEqual(typeof(MethodNameExtractorTest).ToString(), callerInfo.TypeOfMessageCreator);
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
            var sut = new MethodNameExtracter(typeof(NotUsedClass));        //systemUnderTest
            var sample = new SampleMessageCreatorForExtractor(sut);
            //Act
            sample.DoIt();
        }

        //[TestMethod]
        //public void CheckCallHierarchyInStackTrace()
        //{
        //    //Arrange
        //    var sut = new Log(typeof(MethodNameExtractorTest));
        //    sut.Fatal("Test MethodNameExtractor");
            
        //    //Act
        //    var methodNameExtracter = new MethodNameExtracter(sut.GetType());
        //    var foo = methodNameExtracter.ExtractCallerInfoFromStackTrace();
        //    var bar = foo.ClassNameOfGround;
        //    //TODO fertig --> woher wissen das tatsächlich durch die Methoden und nicht durch die ctor gegangen wird
        //}
    }
}
