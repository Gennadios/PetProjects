using System.Reflection;
using System.Diagnostics;

namespace CustomLogger
{
    public sealed class Logger
    {
        public static long MaxFileSize { get; set; } = 30000;

        public static void Debug(string message)
        {
            MethodBase methodInfo = new StackFrame(1).GetMethod();
            LogBase logBase = new();
            if (methodInfo.ReflectedType != null)
            {
                logBase.LogDebug($"at Namespace - {methodInfo.ReflectedType.FullName}," +
                $" Method - {methodInfo.Name}." + $"\nMessage: {message}");
            }
        }

        public static void Info(string message)
        {
            MethodBase methodBase = new StackFrame(1).GetMethod();
            LogBase logWriter = new();
            if (methodBase.ReflectedType != null)
            {
                logWriter.LogInfo($"at Namespace - {methodBase.ReflectedType.FullName}," +
                $" Method: {methodBase.Name}." + $"\nMessage: {message}");
            }
        }

        public static void Error(string message)
        {
            MethodBase methodBase = new StackFrame(1).GetMethod();
            LogBase logWriter = new();
            if (methodBase.ReflectedType != null)
            {
                logWriter.LogInfo($"at Namespace - {methodBase.ReflectedType.FullName}," +
                $" Method: {methodBase.Name}." + $"\nMessage: {message}");
            }
        }
    }
}
