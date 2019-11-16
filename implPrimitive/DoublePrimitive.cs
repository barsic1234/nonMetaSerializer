using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nonMetaSerializer.researchAlgorithm;

namespace nonMetaSerializer.implPrimitive
{
    class DoublePrimitive : IPrimitive
    {
        private readonly int length = 8;

        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var DoubleValue = (double)valueField;
            return BitConverter.GetBytes(DoubleValue);
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            byte[] bytes = streamExtractor(length).ToArray();
            return BitConverter.ToDouble(bytes, 0);
        }
    }
}
