using System;

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
            byte[] bytes = streamExtractor(length);
            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}
