using System;
using NLog;

namespace ParkingOut.Utils.Services
{
    public class AppLogger : IAppLogger
    {
        private readonly Logger _logger;

        public AppLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string message, Exception ex)
        {
            _logger.Debug(ex, message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string message, Exception ex)
        {
            _logger.Info(ex, message);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Warning(string message, Exception ex)
        {
            _logger.Warn(ex, message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            _logger.Error(ex, message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(string message, Exception ex)
        {
            _logger.Fatal(ex, message);
        }

        public void LogError(string message)
        {
            Error(message);
        }

        public void LogError(string message, Exception ex)
        {
            Error(message, ex);
        }

        public void Warn(string message)
        {
            Warning(message);
        }

        public void Warn(string message, Exception ex)
        {
            Warning(message, ex);
        }

        public void LogInfo(string message)
        {
            Info(message);
        }

        public void LogInfo(string message, Exception ex)
        {
            Info(message, ex);
        }
    }
}
