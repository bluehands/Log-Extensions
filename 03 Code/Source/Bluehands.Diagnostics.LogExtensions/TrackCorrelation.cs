using System.Threading;

namespace Bluehands.Diagnostics.LogExtensions
{
    public static class TrackCorrelation
    {
        static readonly AsyncLocal<string> s_AsyncLocalString = new AsyncLocal<string>();

        public static string Correlation
        {
            get => s_AsyncLocalString.Value ?? "";
            set => s_AsyncLocalString.Value = value;
        }
    }
}