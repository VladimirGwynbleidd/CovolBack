using System;

namespace SYE.Covol
{
    public static class Log
    {
        private static readonly log4net.ILog _Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Info(string mensaje)
        {
            _Log.Info(mensaje);
        }

        public static void Warn(string mensaje)
        {
            _Log.Info(mensaje);
        }

        public static void Error(string mensaje)
        {
            _Log.Error(mensaje);
        }
        public static void Error(string mensaje, Exception exception)
        {
            _Log.Error(mensaje, exception);
        }
    }
}
