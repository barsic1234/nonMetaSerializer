using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nonMetaSerializer.implPrimitive;

namespace nonMetaSerializer.concreteAction
{
    internal class PrimitiveAction : IConcreteAction
    {
        private readonly Type type;

        public PrimitiveAction(Type type)
        {
            this.type = type;
        }

        object IConcreteAction.Deserialize(StreamExtractorHandler streamExtractor)
        {
            IPrimitive primitive = PrimitiveFactory.MakePrimitive(type);
            return primitive.GetValueField(streamExtractor);
        }

        List<byte> IConcreteAction.Serialize(object dataObject)
        {
            IPrimitive primitive = PrimitiveFactory.MakePrimitive(type);
            byte[] representBytes = primitive.GetByteStream(dataObject);
            return representBytes.ToList();
        }
    }
}
