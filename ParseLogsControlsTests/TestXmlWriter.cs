using System.Xml;

namespace ParseLogsControls.Test
{
    public class TestXmlWriter : XmlWriter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private XmlWriter xmlWriterImplementation;

        public TestXmlWriter(XmlWriter xmlWriterImplementation)
        {
            this.xmlWriterImplementation = xmlWriterImplementation;
        }

        public override void WriteStartDocument()
        {
            Logger.Trace(nameof(WriteStartDocument));
            xmlWriterImplementation.WriteStartDocument();
        }

        public override void WriteStartDocument(bool standalone)
        {
            Logger.Trace(nameof(WriteStartDocument));
            xmlWriterImplementation.WriteStartDocument(standalone);
        }

        public override void WriteEndDocument()
        {
            xmlWriterImplementation.WriteEndDocument();
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            xmlWriterImplementation.WriteDocType(name, pubid, sysid, subset);
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            Logger.Trace(nameof(WriteStartElement) + $" {prefix} {localName} {ns}");
            xmlWriterImplementation.WriteStartElement(prefix, localName, ns);
        }

        public override void WriteEndElement()
        {
            xmlWriterImplementation.WriteEndElement();
        }

        public override void WriteFullEndElement()
        {
            xmlWriterImplementation.WriteFullEndElement();
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            xmlWriterImplementation.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteEndAttribute()
        {
            xmlWriterImplementation.WriteEndAttribute();
        }

        public override void WriteCData(string text)
        {
            xmlWriterImplementation.WriteCData(text);
        }

        public override void WriteComment(string text)
        {
            xmlWriterImplementation.WriteComment(text);
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
            xmlWriterImplementation.WriteProcessingInstruction(name, text);
        }

        public override void WriteEntityRef(string name)
        {
            xmlWriterImplementation.WriteEntityRef(name);
        }

        public override void WriteCharEntity(char ch)
        {
            xmlWriterImplementation.WriteCharEntity(ch);
        }

        public override void WriteWhitespace(string ws)
        {
            xmlWriterImplementation.WriteWhitespace(ws);
        }

        public override void WriteString(string text)
        {
            xmlWriterImplementation.WriteString(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            xmlWriterImplementation.WriteSurrogateCharEntity(lowChar, highChar);
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            xmlWriterImplementation.WriteChars(buffer, index, count);
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
            xmlWriterImplementation.WriteRaw(buffer, index, count);
        }

        public override void WriteRaw(string data)
        {
            xmlWriterImplementation.WriteRaw(data);
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            xmlWriterImplementation.WriteBase64(buffer, index, count);
        }

        public override void Flush()
        {
            xmlWriterImplementation.Flush();
        }

        public override string LookupPrefix(string ns)
        {
            return xmlWriterImplementation.LookupPrefix(ns);
        }

        public override WriteState WriteState => xmlWriterImplementation.WriteState;
    }
}