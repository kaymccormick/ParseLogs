using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ParseLogsLib.Annotations;

namespace ParseLogsLib
{
    [TypeConverter(typeof(LogItemTypeConverter))]
    public class LogItem : INotifyPropertyChanged
    {
        private XDocument _document;
        public FileInfo File { get; set; }
        public Exception Ex { get; set; }

        public String FullName => (File != null ? File.FullName : AltFullName);

        public string AltFullName { get; set; }

        public 
            LogItem(FileInfo file)
        {
            File = file;
        }

        public LogItem(FileInfo file, Exception ex)
        {
            File = file;
            Ex = ex;
        }

        public LogItem(string dirFullName, Exception ioException)
        {
            AltFullName = dirFullName;
            Ex = ioException;
        }

        public LogItem(string altFullName)
        {
            AltFullName = altFullName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public XDocument Document
        {
            get
            {
                if (_document == null)
                {
                    _document = new XDocument();
                    WebPermission myWebPermission = new WebPermission(PermissionState.None);
                    myWebPermission.AddPermission(NetworkAccess.Connect, @"c:\");
                    PermissionSet set = new PermissionSet(PermissionState.Unrestricted);
                    var resolver = new XmlSecureResolver(new XmlUrlResolver(), set);
#pragma warning restore 612, 618
                    //XmlReaderSettings settings = new XmlReaderSettings(resolver);
                    var b = new UriBuilder();
                    b.Scheme = "file";
                    b.Host = "";
                    b.Path = String.Join("/", FullName.Split(Path.PathSeparator));
                    var t = new NameTable();
                    XmlParserContext context =
                        new XmlParserContext(t, new XmlNamespaceManager(t), "en", XmlSpace.Default);


                    var text = System.IO.File.ReadAllText(FullName);

                    try
                    {
                        _document = XDocument.Parse(text);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            text = "<?xml version=\"1.0\"?>\n<doc>" + text + "</doc>";
                            _document = XDocument.Parse(text);
                        }
                        catch (Exception exception)
                        {
                            text = "<?xml version=\"1.0\"?>\n<doc></doc>";
                            _document = XDocument.Parse(text);
                        }
                    }

                }

                return _document;
            }
            set { _document = value; }
        }
    }
}
