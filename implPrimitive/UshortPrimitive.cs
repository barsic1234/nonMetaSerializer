using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nonMetaSerializer.researchAlgorithm;

namespace nonMetaSerializer.implPrimitive
{
    class UshortPrimitive : IPrimitive
    {
        private readonly int length = 2;

        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var ushortValue = (ushort)valueField;
            return BitConverter.GetBytes(ushortValue);
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            byte[] bytes = streamExtractor(length);
            return BitConverter.ToUInt16(bytes, 0);
        }
    }
}
