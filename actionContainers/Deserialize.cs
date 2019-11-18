using nonMetaSerializer.concreteAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nonMetaSerializer.actionContainers
{
    internal class Deserialize
    {
        private readonly byte[] representBytes;
        private int indexNextRead;

        public Deserialize(byte[] representBytes)
        {
            this.representBytes = representBytes;
            indexNextRead = 0;
        }

        internal object ObjectRecord(Type type)
        {
            IConcreteAction action = ActionFactory.MakeAction(type);
            return action.Deserialize(StreamExtractor);
        }

        private byte[] StreamExtractor(int length)
        {
            byte[] tmp = new byte[length];
            for (int i = 0; i < length; i++)
            {
                tmp[i] = representBytes[indexNextRead++];
            }
            return tmp;
        }
    }
}
