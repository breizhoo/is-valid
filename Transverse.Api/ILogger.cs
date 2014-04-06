using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transverse.Api
{
    public interface ILogger
    {
        // Résumé :
        //     Logs the specified message with Debug severity.
        //
        // Paramètres :
        //   message:
        //     The message.
        void Debug(string message);
        //
        // Résumé :
        //     Logs the specified message with Debug severity.
        //
        // Paramètres :
        //   format:
        //     The message or format template.
        //
        //   args:
        //     Any arguments required for the format template.
        void Debug(string format, params object[] args);
        //
        // Résumé :
        //     Logs the specified exception with Debug severity.
        //
        // Paramètres :
        //   exception:
        //     The exception to log.
        //
        //   format:
        //     The message or format template.
        //
        //   args:
        //     Any arguments required for the format template.
        void Debug(Exception exception, string format, params object[] args);

        //
        // Résumé :
        //     Logs the specified message with Error severity.
        //
        // Paramètres :
        //   message:
        //     The message.
        void Error(string message);
        //
        // Résumé :
        //     Logs the specified message with Error severity.
        //
        // Paramètres :
        //   format:
        //     The message or format template.
        //
        //   args:
        //     Any arguments required for the format template.
        void Error(string format, params object[] args);
        //
        // Résumé :
        //     Logs the specified exception with Error severity.
        //
        // Paramètres :
        //   exception:
        //     The exception to log.
        //
        //   format:
        //     The message or format template.
        //
        //   args:
        //     Any arguments required for the format template.
        void Error(Exception exception, string format, params object[] args);

        //
        // Résumé :
        //     Logs the specified message with Fatal severity.
        //
        // Paramètres :
        //   message:
        //     The message.
        void Fatal(string message);
        //
        // Résumé :
        //     Logs the specified message with Fatal severity.
        //
        // Paramètres :
        //   format:
        //     The message or format template.
        //
        //   args:
        //     Any arguments required for the format template.
        void Fatal(string format, params object[] args);
        //
        // Résumé :
        //     Logs the specified exception with Fatal severity.
        //
        // Paramètres :
        //   exception:
        //     The exception to log.
        //
        //   format:
        //     The message or format template.
        //
        //   args:
        //     Any arguments required for the format template.
        void Fatal(Exception exception, string format, params object[] args);

        //
        // Résumé :
        //     Logs the specified message with Info severity.
        //
        // Paramètres :
        //   message:
        //     The message.
        void Info(string message);
        //
        // Résumé :
        //     Logs the specified message with Info severity.
        //
        // Paramètres :
        //   format:
        //     The message or format template.
        //
        //   args:
        //     Any arguments required for the format template.
        void Info(string format, params object[] args);
        //
        // Résumé :
        //     Logs the specified exception with Info severity.
        //
        // Paramètres :
        //   exception:
        //     The exception to log.
        //
        //   format:
        //     The message or format template.
        //
        //   args:
        //     Any arguments required for the format template.
        void Info(Exception exception, string format, params object[] args);

        //
        // Résumé :
        //     Logs the specified message with Warn severity.
        //
        // Paramètres :
        //   message:
        //     The message.
        void Warn(string message);
        //
        // Résumé :
        //     Logs the specified message with Warn severity.
        //
        // Paramètres :
        //   format:
        //     The message or format template.
        //
        //   args:
        //     Any arguments required for the format template.
        void Warn(string format, params object[] args);
        //
        // Résumé :
        //     Logs the specified exception with Warn severity.
        //
        // Paramètres :
        //   exception:
        //     The exception to log.
        //
        //   format:
        //     The message or format template.
        //
        //   args:
        //     Any arguments required for the format template.
        void Warn(Exception exception, string format, params object[] args);
    }
}
