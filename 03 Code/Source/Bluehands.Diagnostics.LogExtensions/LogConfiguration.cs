using NLog;
using NLog.Config;

namespace Bluehands.Diagnostics.LogExtensions
{
	public static class LogConfiguration
	{
		public static void ConfigureFromFile(string filePath)
		{
			var config = new XmlLoggingConfiguration(filePath);
			LogManager.Configuration = config;
		}
	}
}
