using nonMetaSerializer.concreteAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nonMetaSerializer.actionContainers
{
    internal class Deserialize
    {
        private readonly List<byte> representBytes;
        private int indexNextRead;

        public Deserialize(byte[] representBytes)
        {
            this.representBytes = representBytes.ToList();
            indexNextRead = 0;
        }

        internal object RecordObject(Type type)
        {
            IConcreteAction action = ActionFactory.MakeAction(type);
            return action.Deserialize(StreamExtractor);
        }

        private List<byte> StreamExtractor(int length)
        {
            List<byte> tmp = representBytes.GetRange(indexNextRead, length);
            indexNextRead += length;
            return tmp;
        }
    }
}
