using System;
using System.Collections.Generic;
using System.Text;
using nonMetaSerializer.researchAlgorithm;
using nonMetaSerializer.util;
using System.Reflection;

namespace nonMetaSerializer.implementationAction
{
    internal class SerializeAction : AbstractAction
    {
        private List<byte> resultStream = new List<byte>();

        private Type objectType = null;
        private object recordObject = null;

        internal SerializeAction(IPrimitiveFactory primitiveFactory)
            : base(primitiveFactory) { }

        internal byte[] StreamRecord(object dataObject)
        {
            recordObject = dataObject;
            objectType = dataObject.GetType();

            SplitType(objectType);

            return resultStream.ToArray();
        }

        internal override void ForArray()
        {
            var array = (Array)recordObject;

            IPrimitive rankPrimitive = primitiveFactory.MakePrimitive(typeof(byte));
            byte[] rank = rankPrimitive.GetByteStream((byte)array.Rank);
            resultStream.AddRange(rank);

            int[] lengths = new int[array.Rank];

            for (int dimension = 0; dimension < array.Rank; dimension++)
            {
                lengths[dimension] = array.GetLength(dimension);

                IPrimitive lengthPrimitive = primitiveFactory.MakePrimitive(typeof(ushort));
                byte[] bytesToWrite = lengthPrimitive.GetByteStream((ushort)lengths[dimension]);
                resultStream.AddRange(bytesToWrite);
            }

            var arrayIndexator = new ArrayIterator(lengths);
            for (int i = 0; i < array.Length; i++)
            {
                int[] index = arrayIndexator.GetNext();
                object arrayItem = array.GetValue(index);

                byte[] data = CreateNewSerializeInstance(arrayItem);
                resultStream.AddRange(data);
            }
        }

        private byte[] CreateNewSerializeInstance(object obj)
        {
            var instance = new SerializeAction(primitiveFactory);
            return instance.StreamRecord(obj);
        }

        internal override void ForPrimitive()
        {
            Type type = recordObject.GetType();
            IPrimitive primitive = primitiveFactory.MakePrimitive(type);
            byte[] bytesToWrite = primitive.GetByteStream(recordObject);
            resultStream.AddRange(bytesToWrite);
        }

        internal override void ForEasyDataObject()
        {
            FieldInfo[] fieldInfos = objectType.GetFields();
            foreach (FieldInfo field in fieldInfos)
            {
                byte[] data = CreateNewSerializeInstance(field.GetValue(recordObject));
                resultStream.AddRange(data);
            }
        }

        internal override void ForString()
        {
            string str = (string)recordObject;
            IPrimitive rankPrimitive = primitiveFactory.MakePrimitive(typeof(ushort));
            byte[] rank = rankPrimitive.GetByteStream((ushort)str.Length);
            resultStream.AddRange(rank);

            byte[] data = Encoding.Unicode.GetBytes(str);
            resultStream.AddRange(data);
        }
    }
}
