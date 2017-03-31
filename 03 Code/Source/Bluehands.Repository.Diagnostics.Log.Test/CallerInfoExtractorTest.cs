using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bluehands.Repository.Diagnostics.Log.Test
{

    [TestClass]
	[ExcludeFromCodeCoverage]
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
