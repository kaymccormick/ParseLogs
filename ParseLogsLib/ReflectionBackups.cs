using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLogsLib
{
public class ReflectionBackups  : IFinderSubject
    {
        public IEnumerable<string> ExtensionsOfInterest { get; }
        public Predicate<IItem> selectionDelegate { get; }
        public Action<IItem> ProcessAction { get; }
    }
}
