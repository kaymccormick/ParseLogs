// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using NUnit.Framework;
using ParseLogs;

namespace ParseLogsUI.Test
{

    [TestFixture]
    public class TestClass
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        enum TestResult
        {
            None = 0,
            Success,
            Failure
        }
        [Test]
        public void TestMethod()
        {
            bool isRunning = true;
            int numYields = 0;
            
            ConcurrentQueue<string> messages = new ConcurrentQueue<string>();

            // First task: SpinWait until someBoolean is set to true
            Task t1 = Task.Factory.StartNew(() =>
            {
                SpinWait sw = new SpinWait();
                while (isRunning)
                {
                    // The NextSpinWillYield property returns true if
                    // calling sw.SpinOnce() will result in yielding the
                    // processor instead of simply spinning.
                    if (sw.NextSpinWillYield) numYields++;
/*                    while (!messages.IsEmpty)
                    {
                        string message;
                        if(messages.TryDequeue(out message))
                        {
                            Logger.Trace(message);
                        }
                    }*/
                    sw.SpinOnce();
                }

                // As of .NET Framework 4: After some initial spinning, SpinWait.SpinOnce() will yield every time.
                Logger.Trace("SpinWait called {0} times, yielded {1} times", sw.Count, numYields);
            });

            Action<object> mainAction = w =>
            {
                Window window = (Window) w;
                AutomationElement a1 = AutomationElement.RootElement.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.ClassNameProperty, "MainWindow"));
                var button = a1.FindFirst(TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "_searchButton"));
                button.GetClickablePoint();
                foreach (var pattern in button.GetSupportedPatterns())
                {
                    Logger.Trace(pattern.ProgrammaticName);
                }
                window.Close();
            };

            object resultLock = new object();
            TestResult result = TestResult.None;

            EventManager.RegisterClassHandler(typeof(MainWindow), Window.LoadedEvent, new RoutedEventHandler(
                (sender, args) =>
                {
                    Task.Factory.StartNew(mainAction, sender);
                }));

            Task.Factory.StartNew(() =>
            {
                Logger.Trace("constructing app");
                App app = new App();
                app.Exit += new ExitEventHandler((object sender, ExitEventArgs e) =>
                {
                    Logger.Trace("Exiting");
                    lock (resultLock)
                    {
                        result = TestResult.Success;
                    }

                    isRunning = false;
                });


                Logger.Trace("initializing application");
                app.InitializeComponent();
                Logger.Trace("running application");
                app.Run();
            });

            var condition = t1.Wait(10000);
            Logger.Trace($"{condition}");
            Assert.True(
                condition);
            Assert.Fail();
        }
    }
}
