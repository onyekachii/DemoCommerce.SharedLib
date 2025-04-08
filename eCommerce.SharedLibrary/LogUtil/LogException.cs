using Serilog;

namespace eCommerce.SharedLibrary.Logs
{
    public static class LogException
    {
        public static void LogExceptions(Exception e)
        {
            LogToFile(e.ToString());
            LogToDebugger(e.ToString());
            LogToConsole(e.ToString());
        }
        public static void LogToFile(string msg) => Log.Information(msg);
        public static void LogToDebugger(string msg) => Log.Debug(msg);
        public static void LogToConsole(string msg) => Log.Warning(msg);

    }
}
