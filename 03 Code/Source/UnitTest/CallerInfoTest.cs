using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Instrumentation;
using Bluehands.Repository.Diagnostics.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class CallerInfoTest
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void When_framesIsNull_Then_ThrowsArgumentNullException()
		{
			//Given

			//When
			new CallerInfo(null, typeof(CallerInfoTest));

			//Then
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void When_framesIsEmpty_Then_ThrowsArgumentNullException()
		{
			//Given

			//When
			IEnumerable<StackFrame> frames = new StackFrame[0];
			new CallerInfo(frames, typeof(CallerInfoTest));

			//Then
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Given_CurrentStackFrames_When_messageCreatorIsNull_Then_ThrowsArgumentNullException()
		{
			//Given
			var frames = Given_CurrentStackFrames();

			//When
			new CallerInfo(frames, null);

			//Then
		}

		[TestMethod]
		[ExpectedException(typeof(InstanceNotFoundException))]
		public void Given_CurrentStackFrames_When_messageCreatorNotInStackFrames_Then_ThrowsInstanceNotFoundException()
		{
			//Given
			var frames = Given_CurrentStackFrames();

			//When
			new CallerInfo(frames, typeof(Log));

			//Then
		}

		
		[TestMethod]
		public void Given_CurrentStackFrames_When_messageCreatorIsCorrect_Then_ReturnsInitializedCallerInfo()
		{
			//Given
			var frames = Given_CurrentStackFrames();

			//When
			var info = new CallerInfo(frames, typeof(CallerInfoTest));

			//Then
			Assert.IsNotNull(info);
		}


		private static IEnumerable<StackFrame> Given_CurrentStackFrames()
		{
			var trace = (StackTrace)Activator.CreateInstance(typeof(StackTrace), new object[] { });
			var frames = trace.GetFrames();
			return frames;
		}
	}
}
