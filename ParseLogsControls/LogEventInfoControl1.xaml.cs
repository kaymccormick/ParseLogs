using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NLog;

namespace ParseLogsControls
{
    /// <summary>
    /// Interaction logic for LogEventInfoControl1.xaml
    /// </summary>
    public partial class LogEventInfoControl1 : UserControl
    {
        public WpfLogEventInfo Instance { get; set; }
        public LogEventInfoControl1()
        {
            InitializeComponent();
        }
    }
}
