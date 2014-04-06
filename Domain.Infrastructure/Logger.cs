using Ninject.Extensions.Logging;

namespace Transverse.Implementation
{
    internal class Logger : Transverse.Api.ILogger
    {
        private readonly ILogger _logger;

        public Logger(Ninject.Extensions.Logging.ILogger logger)
        {
            _logger = logger;
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string format, params object[] args)
        {
            _logger.Debug(format, args);
        }

        public void Debug(System.Exception exception, string format, params object[] args)
        {
            _logger.Debug(exception, format, args);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string format, params object[] args)
        {
            _logger.Error(format, args);
        }

        public void Error(System.Exception exception, string format, params object[] args)
        {
            _logger.Error(exception, format, args);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(string format, params object[] args)
        {
            _logger.Fatal(format, args);
        }

        public void Fatal(System.Exception exception, string format, params object[] args)
        {
            _logger.Fatal(exception, format, args);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string format, params object[] args)
        {
            _logger.Info(format, args);
        }

        public void Info(System.Exception exception, string format, params object[] args)
        {
            _logger.Info(exception, format, args);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(string format, params object[] args)
        {
            _logger.Warn(format, args);
        }

        public void Warn(System.Exception exception, string format, params object[] args)
        {
            _logger.Warn(exception, format, args);
        }
    }
}
