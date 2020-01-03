using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLogsLib
{
    public interface IMetadataManager
    {
        IEnumerable<IMetadataItem> MetadataItems { get;  }
    }
}
