using System;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{

    [TestClass]
    public class CallerInfoExtractorTest
    {
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void When_MessageCreatorIsNull_Then_ThrowsArgumentNullException()
		{
			//Given

			//When
			new CallerInfoExtractor(null);

			//Then
		}
    }
}
