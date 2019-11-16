using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nonMetaSerializer.concreteAction
{
    internal delegate List<byte> StreamExtractorHandler(int streamLength);
    interface IConcreteAction
    {
        List<byte> Serialize(object dataObject);
        object Deserialize(StreamExtractorHandler streamExtractor);
    }
}
