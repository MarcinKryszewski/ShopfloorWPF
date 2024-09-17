using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Shared.HelperFunctions
{
    internal class ObjectEventArgs : EventArgs
    {
        public ObjectEventArgs(object args)
        {
            Args = args;
        }
        public object Args { get; init; }
    }
}