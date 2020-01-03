using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ParseLogsLib.Annotations;

namespace ParseLogsLib
{
    class LogItemListCollectionView : CollectionView
    {
        public LogItemListCollectionView([NotNull] IEnumerable collection) : base(collection)
        {
        }
    }
}
