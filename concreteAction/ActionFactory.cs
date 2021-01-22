using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nonMetaSerializer.concreteAction
{
    internal static class ActionFactory //фабрика, возвращающая набор действий дл конкретного типаа объекта
    {
        internal static IConcreteAction MakeAction(Type type)
        {
            if (type.IsArray)
            {
                return new ArrayAction(type);
            }
            else if (type.IsPrimitive)
            {
                return new PrimitiveAction(type);
            }
            else if (type == typeof(string))
            {
                return new StringAction(type);
            }
            else
            {
                return new EasyDataObject(type);
            }
        }
    }
}
