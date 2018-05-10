using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bluehands.Repository.Diagnostics.Log.Test
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class LogConfigurationTest
	{
		//Asserts that LogConfiguration is successfully set and used. But distracts other Tests running in parallel (all have the same target because of NLog).

		//private readonly Log<LogConfigurationTest> m_Log = new Log<LogConfigurationTest>();

		//[TestMethod]
		//public void Given_logFilePathAndLogFileMissingAndNewConfigFile_When_SetNewConfigFileAndLogTestMessage_Then_LogFileExists()
		//{
		//	//Given
		//	const string newConfigFile = "../../../assets/NLog.config";
		//	const string logFile = "./Logs/test.log";

		//	Assert.IsTrue(File.Exists(newConfigFile));
		//	if (File.Exists(logFile))
		//	{
		//		File.Delete(logFile);
		//	}

		//	//When
		//	LogConfiguration.ConfigureFromFile(newConfigFile);
		//	m_Log.Info("TestMessage");

		//	//Then
		//	Assert.IsTrue(File.Exists(logFile));

		//	LogConfiguration.ConfigureFromFile(logFile);

		//}
	}
}
