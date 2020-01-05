    using System;
using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

    namespace ParseLogs
{
    /// <summary>
    /// Interaction logic for ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        public ConsoleWindow()
        {
            Writer writer = new Writer(ConsoleControl);
            Console.SetError(writer);
            writer.WriteLine("EAt dirt");
            InitializeComponent();
            
        }
    }

    public class Writer : TextWriter
    {
        private ConsoleControl.WPF.ConsoleControl control;

        public Writer(ConsoleControl.WPF.ConsoleControl control)
        {
            this.control = control;
        }

        public override Encoding Encoding { get; } = Encoding.UTF8;
        public override void Close()
        {
            throw new NotImplementedException();   
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override void Flush()
        {
            base.Flush();
        }

        public override void Write(char[] buffer)
        {
            base.Write(buffer);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            base.Write(buffer, index, count);
        }

        public override void Write(bool value)
        {
            base.Write(value);
        }

        public override void Write(int value)
        {
            base.Write(value);
        }

        public override void Write(uint value)
        {
            base.Write(value);
        }

        public override void Write(long value)
        {
            base.Write(value);
        }

        public override void Write(ulong value)
        {
            base.Write(value);
        }

        public override void Write(float value)
        {
            base.Write(value);
        }

        public override void Write(double value)
        {
            base.Write(value);
        }

        public override void Write(decimal value)
        {
            base.Write(value);
        }

        public override void Write(object value)
        {
            base.Write(value);
        }

        public override void WriteLine(char value)
        {
            base.WriteLine(value);
        }

        public override void WriteLine(char[] buffer)
        {
            base.WriteLine(buffer);
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            base.WriteLine(buffer, index, count);
        }

        public override void WriteLine(bool value)
        {
            base.WriteLine(value);
        }

        public override void WriteLine(int value)
        {
            base.WriteLine(value);
        }

        public override void WriteLine(uint value)
        {
            base.WriteLine(value);
        }

        public override void WriteLine(long value)
        {
            base.WriteLine(value);
        }

        public override void WriteLine(ulong value)
        {
            base.WriteLine(value);
        }

        public override void WriteLine(float value)
        {
            base.WriteLine(value);
        }

        public override void WriteLine(double value)
        {
            base.WriteLine(value);
        }

        public override void WriteLine(decimal value)
        {
            base.WriteLine(value);
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(value);
        }

        public override void WriteLine(object value)
        {
            base.WriteLine(value);
        }

        public override void Write(string value)
        {
            control?.WriteOutput(value, Color.FromRgb(255, 255, 255));
        }

        public override void Write(char value)
        {
            control?.WriteOutput(value.ToString(), Color.FromRgb(255, 255, 255));
        }
    }
}
