using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nonMetaSerializer.researchAlgorithm;

namespace nonMetaSerializer.implPrimitive
{
    class UintPrimitive : IPrimitive
    {
        private readonly int length = 4;

        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var uintValue = (uint)valueField;
            return BitConverter.GetBytes(uintValue);
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            byte[] bytes = streamExtractor(length).ToArray();
            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}
