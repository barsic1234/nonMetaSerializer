using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nonMetaSerializer.researchAlgorithm;
using nonMetaSerializer.util;
using System.Reflection;

namespace nonMetaSerializer.implementationAction
{
    internal class DeserializeAction : AbstractAction
    {
        private class Stream
        {
            public readonly List<byte> representBytes;
            public int indexNextRead;

            public Stream(byte[] representBytes)
            {
                this.representBytes = representBytes.ToList();
                indexNextRead = 0;
            }
        }

        private readonly Stream stream;
        private Type objectType = null;
        private object recordObject = null;

        public DeserializeAction(IPrimitiveFactory primitiveFactory,
            byte[] representBytes)
            : base(primitiveFactory)
        {
            stream = new Stream(representBytes);
        }

        internal object ObjectRecord(Type objectType)
        {
            this.objectType = objectType;

            SplitType(objectType);

            return recordObject;
        }

        internal override void ForArray()
        {
            IPrimitive rankPrimitive = primitiveFactory.MakePrimitive(typeof(byte));
            byte rank = (byte)rankPrimitive.GetValueField(StreamExtractor);
            
            int[] lengths = new int[rank];
            IPrimitive lengthPrimitive = primitiveFactory.MakePrimitive(typeof(ushort));

            object[] objLen = new object[rank];

            for (int dimension = 0; dimension < rank; dimension++)
            {
                objLen[dimension] = lengthPrimitive.GetValueField(StreamExtractor);
                lengths[dimension] = (ushort)objLen[dimension];
            }

            recordObject = Activator.CreateInstance(objectType, objLen);

            var array = (Array)recordObject;

            Type typeArray = array.GetType();
            Type typeElement = typeArray.GetElementType();

            var arrayIndex = new ArrayIterator(lengths);
            for (int i = 0; i < array.Length; i++)
            {
                object elementValue = CreateNewRecordInstance(typeElement);

                int[] indices = arrayIndex.GetNext();
                array.SetValue(elementValue, indices);
            }
        }

        private byte[] StreamExtractor(int lenght)
        {
            List<byte> tmp = stream.representBytes.GetRange(stream.indexNextRead, lenght);
            stream.indexNextRead += lenght;
            return tmp.ToArray();
        }

        private object CreateNewRecordInstance(Type type)
        {
            var instance = new DeserializeAction(primitiveFactory, stream);
            return instance.ObjectRecord(type);
        }
        private DeserializeAction(IPrimitiveFactory primitiveFactory,
            Stream stream)
            : base(primitiveFactory)
        {
            this.stream = stream;
        }

        internal override void ForPrimitive()
        {
            IPrimitive primitive = primitiveFactory.MakePrimitive(objectType);
            recordObject = primitive.GetValueField(StreamExtractor);
        }

        internal override void ForEasyDataObject()
        {
            recordObject = Activator.CreateInstance(objectType);

            FieldInfo[] fieldInfos = objectType.GetFields();
            foreach (FieldInfo field in fieldInfos)
            {
                object fieldValue = CreateNewRecordInstance(field.FieldType);
                field.SetValue(recordObject, fieldValue);
            }
        }

        internal override void ForString()
        {
            IPrimitive lenStrPrimitive = primitiveFactory.MakePrimitive(typeof(ushort));
            ushort lenStr = (ushort)lenStrPrimitive.GetValueField(StreamExtractor);
            lenStr *= 2;
            recordObject = Encoding.Unicode.GetString(stream.representBytes.GetRange(stream.indexNextRead, lenStr).ToArray());
            stream.indexNextRead += lenStr;
        }
    }
}
