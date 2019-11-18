using System;

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
            byte[] bytes = streamExtractor(length).ToArray();
            return BitConverter.ToUInt16(bytes, 0);
        }
    }
}
