namespace TicketStore.Infra.CrossCutting.Logging
{
    public class Logger
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        public static void Debug(string format, params object[] args)
        {
            logger.Debug(format, args);
        }

        public static void Info(string format, params object[] args)
        {
            logger.Info(format, args);
        }

        public static void Warn(string format, params object[] args)
        {
            logger.Warn(format, args);
        }

        public static void Error(string format, params object[] args)
        {
            logger.Error(format, args);
        }
    }
}
