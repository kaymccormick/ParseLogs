using System;
using System.Collections.Generic;

namespace ParseLogsLib
    {
    public interface IFinderSubject
    {
        IEnumerable<string> ExtensionsOfInterest { get; }
        Predicate<IItem> selectionDelegate { get; }
        Action<IItem> ProcessAction { get; }
    }
}