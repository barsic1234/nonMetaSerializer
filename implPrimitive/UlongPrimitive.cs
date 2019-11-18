using System;

namespace nonMetaSerializer.implPrimitive
{
    class UlongPrimitive : IPrimitive
    {
        private readonly int length = 8;

        byte[] IPrimitive.GetByteStream(object valueField)
        {
            var ulongValue = (ulong)valueField;
            return BitConverter.GetBytes(ulongValue);
        }

        object IPrimitive.GetValueField(StreamExtractorHandler streamExtractor)
        {
            byte[] bytes = streamExtractor(length);
            return BitConverter.ToUInt64(bytes, 0);
        }
    }
}
