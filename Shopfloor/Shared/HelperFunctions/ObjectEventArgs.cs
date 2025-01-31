using System;

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