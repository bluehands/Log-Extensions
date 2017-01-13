namespace Bluehands.Diagnostics.Log
{
    public class LogContextInfo
    {
        public string ApplicationContextId { get; set; }
        public string DatabaseContextId { get; set; }
        public string DatabaseUserContextId { get; set; }
        public string UserContextId { get; set; }
        public string ApplicationName { get; set; }
        public string DatabaseId { get; set; }
        public string UserId { get; set; }
        public string ObfuscatedSessionToken { get; set; }
    }
}