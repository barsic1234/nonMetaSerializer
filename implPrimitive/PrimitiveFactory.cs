using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nonMetaSerializer.researchAlgorithm;
using nonMetaSerializer.errors;

namespace nonMetaSerializer.implPrimitive
{
    internal static class PrimitiveFactory
    {
        private static BoolPrimitive boolPrimitive = new BoolPrimitive();
        private static BytePrimitive bytePrimitive = new BytePrimitive();
        private static CharPrimitive charPrimitive = new CharPrimitive();
        private static DoublePrimitive doublePrimitive = new DoublePrimitive();
        private static FloatPrimitive floatPrimitive = new FloatPrimitive();
        private static IntegerPrimitive integerPrimitive = new IntegerPrimitive();
        private static LongPrimitive longPrimitive = new LongPrimitive();
        private static ShortPrimitive shortPrimitive = new ShortPrimitive();
        private static UintPrimitive uintPrimitive = new UintPrimitive();
        private static UlongPrimitive ulongPrimitive = new UlongPrimitive();
        private static UshortPrimitive ushortPrimitive = new UshortPrimitive();

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
