using System;
using nonMetaSerializer.errors;

namespace nonMetaSerializer.implPrimitive
{
    internal static class PrimitiveFactory
    {
        private static readonly BoolPrimitive boolPrimitive = new BoolPrimitive();
        private static readonly BytePrimitive bytePrimitive = new BytePrimitive();
        private static readonly CharPrimitive charPrimitive = new CharPrimitive();
        private static readonly DoublePrimitive doublePrimitive = new DoublePrimitive();
        private static readonly FloatPrimitive floatPrimitive = new FloatPrimitive();
        private static readonly IntegerPrimitive integerPrimitive = new IntegerPrimitive();
        private static readonly LongPrimitive longPrimitive = new LongPrimitive();
        private static readonly ShortPrimitive shortPrimitive = new ShortPrimitive();
        private static readonly UintPrimitive uintPrimitive = new UintPrimitive();
        private static readonly UlongPrimitive ulongPrimitive = new UlongPrimitive();
        private static readonly UshortPrimitive ushortPrimitive = new UshortPrimitive();

        internal static IPrimitive MakePrimitive(Type type)
        {
            var typeCode = Type.GetTypeCode(type);

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return boolPrimitive;
                case TypeCode.Byte:
                    return bytePrimitive;
                case TypeCode.Char:
                    return charPrimitive;
                case TypeCode.Double:
                    return doublePrimitive;
                case TypeCode.Single:
                    return floatPrimitive;
                case TypeCode.Int32:
                    return integerPrimitive;
                case TypeCode.Int64:
                    return longPrimitive;
                case TypeCode.Int16:
                    return shortPrimitive;
                case TypeCode.UInt32:
                    return uintPrimitive;
                case TypeCode.UInt64:
                    return ulongPrimitive;
                case TypeCode.UInt16:
                    return ushortPrimitive;
                default:
                    throw new NonMetaSerializerException(ErrorCode.UNASSIGNED_PRIMIRIVE, type.ToString());
            }
        }
    }
}
