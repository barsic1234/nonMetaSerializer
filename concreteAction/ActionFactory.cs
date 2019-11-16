using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nonMetaSerializer.concreteAction
{
    internal static class ActionFactory
    {
        internal static IConcreteAction MakeAction(Type type)
        {
            if (type.IsArray)
            {
                ForArray();
            }
            else if (type.IsPrimitive)
            {
                ForPrimitive();
            }
            else if (type == typeof(string))
            {
                ForString();
            }
            else
            {
                ForEasyDataObject();
            }
        }
    }
}
