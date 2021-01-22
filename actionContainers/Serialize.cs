using nonMetaSerializer.concreteAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nonMetaSerializer.actionContainers
{
    internal class Serialize //класс для сериализации объекта
    {
        public Serialize()
        {

        }

        internal byte[] StreamRecord(object dataObject)
        {
            Type type = dataObject.GetType();

            IConcreteAction action = ActionFactory.MakeAction(type);
            List<byte> resultStream = action.Serialize(dataObject);

            return resultStream.ToArray();
        }
    }
}
