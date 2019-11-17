using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nonMetaSerializer.implPrimitive;

namespace nonMetaSerializer.concreteAction
{
    internal class StringAction : IConcreteAction
    {
        private readonly Type type;

        public StringAction(Type type)
        {
            this.type = type;
        }

        object IConcreteAction.Deserialize(StreamExtractorHandler streamExtractor)
        {
            IPrimitive lenStrPrimitive = PrimitiveFactory.MakePrimitive(typeof(ushort));
            ushort lenStr = (ushort)lenStrPrimitive.GetValueField(streamExtractor);
            lenStr *= 2;
            List<byte> bytesRepresentString = streamExtractor(lenStr);
            object recordObject = Encoding.Unicode.GetString(bytesRepresentString.ToArray());

            return recordObject;
        }

        List<byte> IConcreteAction.Serialize(object dataObject)
        {
            var resultStream = new List<byte>();

            string str = (string)dataObject;
            IPrimitive rankPrimitive = PrimitiveFactory.MakePrimitive(typeof(ushort));
            byte[] rank = rankPrimitive.GetByteStream((ushort)str.Length);
            resultStream.AddRange(rank);

            byte[] data = Encoding.Unicode.GetBytes(str);
            resultStream.AddRange(data);

            return resultStream;
        }
    }
}
