using System.Collections.Generic;

namespace nonMetaSerializer.implPrimitive
{
    interface IPrimitive
    {
        byte[] GetByteStream(object valueField);
        object GetValueField(StreamExtractorHandler streamExtractor);
    }
}
