using nonMetaSerializer.implPrimitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nonMetaSerializer.concreteAction
{
    interface IConcreteAction
    {
        List<byte> Serialize(object dataObject);
        object Deserialize(StreamExtractorHandler streamExtractor);
    }
}
