using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.ComponentModel;

namespace ParseLogs.Actions
{
    class ListConvertersAction
    {
        public ListConvertersAction()
        {
            var c = UniversalTypeConverter.Conversions;
            foreach (var conversion in c)
            {
            }
        }
    }
}
