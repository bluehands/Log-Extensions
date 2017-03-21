using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bluehands.Repository.Diagnostics.Log.Test
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
