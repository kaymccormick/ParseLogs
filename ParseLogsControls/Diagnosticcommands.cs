using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ParseLogsControls.Properties;

namespace ParseLogsControls
{
    public static class DiagnosticCommands
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static readonly RoutedCommand ShowLogEventInfoCommand =
            new RoutedUICommand("Show Log Event Info", nameof(ShowLogEventInfoCommand), typeof(DiagnosticCommands));

    }
}