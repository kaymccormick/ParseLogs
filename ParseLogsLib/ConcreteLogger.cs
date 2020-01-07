﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NLog;

namespace ParseLogsLib
{
    public static class Commands
    {
        public static void DumpLogConfig()
        {
            var loggingConfiguration = NLog.LogManager.Configuration;
            //ParseLogLib.Actions.DumpLogConfig(loggingConfiguration);
            foreach (var rule in loggingConfiguration.LoggingRules)
            {
                Tuple<string, object> t;
                ICollection<Tuple<string, object>> o = new List<Tuple<string, object>>();
                IDictionary<string, object> d = new Dictionary<string, object>();
                IEnumerator<Tuple<string, object>> ie;

                d["RuleName"] = rule.RuleName;
                d["Type"] = rule.GetType().FullName;

                IObservable<KeyValuePair<string, object>> observable = d.ToObservable();
                observable.Do(pair =>
                    Console.WriteLine($"${pair}"));
            }
        }
    }

    public class LogConfigDump : IStartupDebugCommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Commands.DumpLogConfig();
        }

        public event EventHandler CanExecuteChanged;
    }

    public interface IStartupDebugCommand : ICommand
    {
    }

    public class AppLogEventInfo : LogEventInfo
    {
    }

    public class ConcreteLogger : ILogInterface, ILogger
    {
        public ConcreteLogger()
        {
            Console.Error.WriteLine("constructing concretelogger");
        }

        public ConcreteLogger(ILogger logger)
        {
            _logger = logger;
        }

        private ILogger _logger;

        public void Log(LogLevel level, object value)
        {
            _logger.Log(level, value);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, object value)
        {
            _logger.Log(level, formatProvider, value);
        }

        public void Log(LogLevel level, string message, object arg1, object arg2)
        {
            _logger.Log(level, message, arg1, arg2);
        }

        public void Log(LogLevel level, string message, object arg1, object arg2, object arg3)
        {
            _logger.Log(level, message, arg1, arg2, arg3);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, bool argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, char argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, byte argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, string argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, int argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, long argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, float argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, double argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, decimal argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, object argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, sbyte argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, uint argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, ulong argument)
        {
            _logger.Log(level, message, argument);
        }

        public bool IsEnabled(LogLevel level)
        {
            return _logger.IsEnabled(level);
        }

        public void Log(LogEventInfo logEvent)
        {
            _logger.Log(logEvent);
        }

        public void Log(Type wrapperType, LogEventInfo logEvent)
        {
            _logger.Log(wrapperType, logEvent);
        }

        public void Log<T>(LogLevel level, T value)
        {
            _logger.Log(level, value);
        }

        public void Log<T>(LogLevel level, IFormatProvider formatProvider, T value)
        {
            _logger.Log(level, formatProvider, value);
        }

        public void Log(LogLevel level, LogMessageGenerator messageFunc)
        {
            _logger.Log(level, messageFunc);
        }

        public void Log(LogLevel level, Exception exception, string message, params object[] args)
        {
            _logger.Log(level, exception, message, args);
        }

        public void Log(LogLevel level, Exception exception, IFormatProvider formatProvider, string message,
            params object[] args)
        {
            _logger.Log(level, exception, formatProvider, message, args);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Log(level, formatProvider, message, args);
        }

        public void Log(LogLevel level, string message)
        {
            _logger.Log(level, message);
        }

        public void Log(LogLevel level, string message, params object[] args)
        {
            _logger.Log(level, message, args);
        }

        public void Log(LogLevel level, string message, Exception exception)
        {
            _logger.Log(level, message, exception);
        }

        public void Log<TArgument>(LogLevel level, IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Log(level, formatProvider, message, argument);
        }

        public void Log<TArgument>(LogLevel level, string message, TArgument argument)
        {
            _logger.Log(level, message, argument);
        }

        public void Log<TArgument1, TArgument2>(LogLevel level, IFormatProvider formatProvider, string message,
            TArgument1 argument1,
            TArgument2 argument2)
        {
            _logger.Log(level, formatProvider, message, argument1, argument2);
        }

        public void Log<TArgument1, TArgument2>(LogLevel level, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            _logger.Log(level, message, argument1, argument2);
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, IFormatProvider formatProvider,
            string message,
            TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Log(level, formatProvider, message, argument1, argument2, argument3);
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, string message, TArgument1 argument1,
            TArgument2 argument2,
            TArgument3 argument3)
        {
            _logger.Log(level, message, argument1, argument2, argument3);
        }

        public string Name => _logger.Name;

        public LogFactory Factory => _logger.Factory;

        public event EventHandler<EventArgs> LoggerReconfigured
        {
            add => _logger.LoggerReconfigured += value;
            remove => _logger.LoggerReconfigured -= value;
        }

        public void Swallow(Action action)
        {
            _logger.Swallow(action);
        }

        public T Swallow<T>(Func<T> func)
        {
            return _logger.Swallow(func);
        }

        public T Swallow<T>(Func<T> func, T fallback)
        {
            return _logger.Swallow(func, fallback);
        }

        public void Swallow(Task task)
        {
            _logger.Swallow(task);
        }

        public Task SwallowAsync(Task task)
        {
            return _logger.SwallowAsync(task);
        }

        public Task SwallowAsync(Func<Task> asyncAction)
        {
            return _logger.SwallowAsync(asyncAction);
        }

        public Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc)
        {
            return _logger.SwallowAsync(asyncFunc);
        }

        public Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc, TResult fallback)
        {
            return _logger.SwallowAsync(asyncFunc, fallback);
        }

        public void Trace(object value)
        {
            _logger.Trace(value);
        }

        public void Trace(IFormatProvider formatProvider, object value)
        {
            _logger.Trace(formatProvider, value);
        }

        public void Trace(string message, object arg1, object arg2)
        {
            _logger.Trace(message, arg1, arg2);
        }

        public void Trace(string message, object arg1, object arg2, object arg3)
        {
            _logger.Trace(message, arg1, arg2, arg3);
        }

        public void Trace(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, bool argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, char argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, byte argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, string argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, int argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, long argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, float argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, double argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, decimal argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, object argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, sbyte argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, uint argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, ulong argument)
        {
            _logger.Trace(message, argument);
        }

        public void Debug(object value)
        {
            _logger.Debug(value);
        }

        public void Debug(IFormatProvider formatProvider, object value)
        {
            _logger.Debug(formatProvider, value);
        }

        public void Debug(string message, object arg1, object arg2)
        {
            _logger.Debug(message, arg1, arg2);
        }

        public void Debug(string message, object arg1, object arg2, object arg3)
        {
            _logger.Debug(message, arg1, arg2, arg3);
        }

        public void Debug(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, bool argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, char argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, byte argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, string argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, int argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, long argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, float argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, double argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, decimal argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, object argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, sbyte argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, uint argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, ulong argument)
        {
            _logger.Debug(message, argument);
        }

        public void Info(object value)
        {
            _logger.Info(value);
        }

        public void Info(IFormatProvider formatProvider, object value)
        {
            _logger.Info(formatProvider, value);
        }

        public void Info(string message, object arg1, object arg2)
        {
            _logger.Info(message, arg1, arg2);
        }

        public void Info(string message, object arg1, object arg2, object arg3)
        {
            _logger.Info(message, arg1, arg2, arg3);
        }

        public void Info(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, bool argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, char argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, byte argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, string argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, int argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, long argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, float argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, double argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, decimal argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, object argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, sbyte argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, uint argument)
        {
            _logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, ulong argument)
        {
            _logger.Info(message, argument);
        }

        public void Warn(object value)
        {
            _logger.Warn(value);
        }

        public void Warn(IFormatProvider formatProvider, object value)
        {
            _logger.Warn(formatProvider, value);
        }

        public void Warn(string message, object arg1, object arg2)
        {
            _logger.Warn(message, arg1, arg2);
        }

        public void Warn(string message, object arg1, object arg2, object arg3)
        {
            _logger.Warn(message, arg1, arg2, arg3);
        }

        public void Warn(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, bool argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, char argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, byte argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, string argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, int argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, long argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, float argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, double argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, decimal argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, object argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, sbyte argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, uint argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, ulong argument)
        {
            _logger.Warn(message, argument);
        }

        public void Error(object value)
        {
            _logger.Error(value);
        }

        public void Error(IFormatProvider formatProvider, object value)
        {
            _logger.Error(formatProvider, value);
        }

        public void Error(string message, object arg1, object arg2)
        {
            _logger.Error(message, arg1, arg2);
        }

        public void Error(string message, object arg1, object arg2, object arg3)
        {
            _logger.Error(message, arg1, arg2, arg3);
        }

        public void Error(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, bool argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, char argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, byte argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, string argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, int argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, long argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, float argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, double argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, decimal argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, object argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, sbyte argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, uint argument)
        {
            _logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, ulong argument)
        {
            _logger.Error(message, argument);
        }

        public void Fatal(object value)
        {
            _logger.Fatal(value);
        }

        public void Fatal(IFormatProvider formatProvider, object value)
        {
            _logger.Fatal(formatProvider, value);
        }

        public void Fatal(string message, object arg1, object arg2)
        {
            _logger.Fatal(message, arg1, arg2);
        }

        public void Fatal(string message, object arg1, object arg2, object arg3)
        {
            _logger.Fatal(message, arg1, arg2, arg3);
        }

        public void Fatal(IFormatProvider formatProvider, string message, bool argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, bool argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, char argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, char argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, byte argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, byte argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, string argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, string argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, int argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, int argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, long argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, long argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, float argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, float argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, double argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, double argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, decimal argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, decimal argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, object argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, object argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, sbyte argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, sbyte argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, uint argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, uint argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, ulong argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, ulong argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Trace<T>(T value)
        {
            _logger.Trace(value);
        }

        public void Trace<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Trace(formatProvider, value);
        }

        public void Trace(LogMessageGenerator messageFunc)
        {
            _logger.Trace(messageFunc);
        }


        public void Trace(Exception exception, string message)
        {
            _logger.Trace(exception, message);
        }

        public void Trace(Exception exception, string message, params object[] args)
        {
            _logger.Trace(exception, message, args);
        }

        public void Trace(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Trace(exception, formatProvider, message, args);
        }

        public void Trace(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Trace(formatProvider, message, args);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        public void Trace(string message, Exception exception)
        {
            _logger.Trace(message, exception);
        }

        public void Trace<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Trace(formatProvider, message, argument);
        }

        public void Trace<TArgument>(string message, TArgument argument)
        {
            _logger.Trace(message, argument);
        }

        public void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            _logger.Trace(formatProvider, message, argument1, argument2);
        }

        public void Trace<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Trace(message, argument1, argument2);
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message,
            TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Trace(formatProvider, message, argument1, argument2, argument3);
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1,
            TArgument2 argument2,
            TArgument3 argument3)
        {
            _logger.Trace(message, argument1, argument2, argument3);
        }

        public void Debug<T>(T value)
        {
            _logger.Debug(value);
        }

        public void Debug<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Debug(formatProvider, value);
        }

        public void Debug(LogMessageGenerator messageFunc)
        {
            _logger.Debug(messageFunc);
        }


        public void Debug(Exception exception, string message)
        {
            _logger.Debug(exception, message);
        }

        public void Debug(Exception exception, string message, params object[] args)
        {
            _logger.Debug(exception, message, args);
        }

        public void Debug(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Debug(exception, formatProvider, message, args);
        }

        public void Debug(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Debug(formatProvider, message, args);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void Debug(string message, Exception exception)
        {
            _logger.Debug(message, exception);
        }

        public void Debug<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Debug(formatProvider, message, argument);
        }

        public void Debug<TArgument>(string message, TArgument argument)
        {
            _logger.Debug(message, argument);
        }

        public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            _logger.Debug(formatProvider, message, argument1, argument2);
        }

        public void Debug<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Debug(message, argument1, argument2);
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message,
            TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Debug(formatProvider, message, argument1, argument2, argument3);
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1,
            TArgument2 argument2,
            TArgument3 argument3)
        {
            _logger.Debug(message, argument1, argument2, argument3);
        }

        public void Info<T>(T value)
        {
            _logger.Info(value);
        }

        public void Info<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Info(formatProvider, value);
        }

        public void Info(LogMessageGenerator messageFunc)
        {
            _logger.Info(messageFunc);
        }


        public void Info(Exception exception, string message)
        {
            _logger.Info(exception, message);
        }

        public void Info(Exception exception, string message, params object[] args)
        {
            _logger.Info(exception, message, args);
        }

        public void Info(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Info(exception, formatProvider, message, args);
        }

        public void Info(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Info(formatProvider, message, args);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void Info(string message, Exception exception)
        {
            _logger.Info(message, exception);
        }

        public void Info<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Info(formatProvider, message, argument);
        }

        public void Info<TArgument>(string message, TArgument argument)
        {
            _logger.Info(message, argument);
        }

        public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            _logger.Info(formatProvider, message, argument1, argument2);
        }

        public void Info<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Info(message, argument1, argument2);
        }

        public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message,
            TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Info(formatProvider, message, argument1, argument2, argument3);
        }

        public void Info<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
            TArgument3 argument3)
        {
            _logger.Info(message, argument1, argument2, argument3);
        }

        public void Warn<T>(T value)
        {
            _logger.Warn(value);
        }

        public void Warn<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Warn(formatProvider, value);
        }

        public void Warn(LogMessageGenerator messageFunc)
        {
            _logger.Warn(messageFunc);
        }

        public void Warn(Exception exception, string message)
        {
            _logger.Warn(exception, message);
        }

        public void Warn(Exception exception, string message, params object[] args)
        {
            _logger.Warn(exception, message, args);
        }

        public void Warn(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Warn(exception, formatProvider, message, args);
        }

        public void Warn(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Warn(formatProvider, message, args);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public void Warn(string message, Exception exception)
        {
            _logger.Warn(message, exception);
        }

        public void Warn<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Warn(formatProvider, message, argument);
        }

        public void Warn<TArgument>(string message, TArgument argument)
        {
            _logger.Warn(message, argument);
        }

        public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            _logger.Warn(formatProvider, message, argument1, argument2);
        }

        public void Warn<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Warn(message, argument1, argument2);
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message,
            TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Warn(formatProvider, message, argument1, argument2, argument3);
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
            TArgument3 argument3)
        {
            _logger.Warn(message, argument1, argument2, argument3);
        }

        public void Error<T>(T value)
        {
            _logger.Error(value);
        }

        public void Error<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Error(formatProvider, value);
        }

        public void Error(LogMessageGenerator messageFunc)
        {
            _logger.Error(messageFunc);
        }


        public void Error(Exception exception, string message)
        {
            _logger.Error(exception, message);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            _logger.Error(exception, message, args);
        }

        public void Error(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Error(exception, formatProvider, message, args);
        }

        public void Error(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Error(formatProvider, message, args);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Error<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Error(formatProvider, message, argument);
        }

        public void Error<TArgument>(string message, TArgument argument)
        {
            _logger.Error(message, argument);
        }

        public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            _logger.Error(formatProvider, message, argument1, argument2);
        }

        public void Error<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Error(message, argument1, argument2);
        }

        public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message,
            TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Error(formatProvider, message, argument1, argument2, argument3);
        }

        public void Error<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1,
            TArgument2 argument2,
            TArgument3 argument3)
        {
            _logger.Error(message, argument1, argument2, argument3);
        }

        public void Fatal<T>(T value)
        {
            _logger.Fatal(value);
        }

        public void Fatal<T>(IFormatProvider formatProvider, T value)
        {
            _logger.Fatal(formatProvider, value);
        }

        public void Fatal(LogMessageGenerator messageFunc)
        {
            _logger.Fatal(messageFunc);
        }


        public void Fatal(Exception exception, string message)
        {
            _logger.Fatal(exception, message);
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            _logger.Fatal(exception, message, args);
        }

        public void Fatal(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Fatal(exception, formatProvider, message, args);
        }

        public void Fatal(IFormatProvider formatProvider, string message, params object[] args)
        {
            _logger.Fatal(formatProvider, message, args);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public void Fatal(string message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }

        public void Fatal<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            _logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal<TArgument>(string message, TArgument argument)
        {
            _logger.Fatal(message, argument);
        }

        public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            _logger.Fatal(formatProvider, message, argument1, argument2);
        }

        public void Fatal<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            _logger.Fatal(message, argument1, argument2);
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message,
            TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            _logger.Fatal(formatProvider, message, argument1, argument2, argument3);
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1,
            TArgument2 argument2,
            TArgument3 argument3)
        {
            _logger.Fatal(message, argument1, argument2, argument3);
        }

        public bool IsTraceEnabled => _logger.IsTraceEnabled;

        public bool IsDebugEnabled => _logger.IsDebugEnabled;

        public bool IsInfoEnabled => _logger.IsInfoEnabled;

        public bool IsWarnEnabled => _logger.IsWarnEnabled;

        public bool IsErrorEnabled => _logger.IsErrorEnabled;

        public bool IsFatalEnabled => _logger.IsFatalEnabled;
    }
}