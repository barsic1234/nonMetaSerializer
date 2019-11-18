using nonMetaSerializer.implPrimitive;
using nonMetaSerializer.util;
using System;
using System.Collections.Generic;

namespace nonMetaSerializer.concreteAction
{
    internal class ArrayAction : IConcreteAction
    {
        private readonly Type type;
        public ArrayAction(Type type)
        {
            this.type = type;
        }
        object IConcreteAction.Deserialize(StreamExtractorHandler streamExtractor)
        {
            IPrimitive rankPrimitive = PrimitiveFactory.MakePrimitive(typeof(byte));
            byte rank = (byte)rankPrimitive.GetValueField(streamExtractor);

            int[] lengths = new int[rank];
            IPrimitive lengthPrimitive = PrimitiveFactory.MakePrimitive(typeof(ushort));

            object[] objLen = new object[rank];

            for (int dimension = 0; dimension < rank; dimension++)
            {
                objLen[dimension] = lengthPrimitive.GetValueField(streamExtractor);
                lengths[dimension] = (ushort)objLen[dimension];
            }

            object recordObject = Activator.CreateInstance(type, objLen);

            var array = (Array)recordObject;

            Type typeElement = type.GetElementType();

            var arrayIndex = new ArrayIterator(lengths);
            for (int i = 0; i < array.Length; i++)
            {
                IConcreteAction action = ActionFactory.MakeAction(typeElement);
                object elementValue = action.Deserialize(streamExtractor);

                int[] indices = arrayIndex.GetNext();
                array.SetValue(elementValue, indices);
            }

            return recordObject;
        }

        List<byte> IConcreteAction.Serialize(object dataObject)
        {
            List<byte> resultStream = new List<byte>();

            var array = (Array)dataObject;

            IPrimitive rankPrimitive = PrimitiveFactory.MakePrimitive(typeof(byte));
            byte[] rank = rankPrimitive.GetByteStream((byte)array.Rank);
            resultStream.AddRange(rank);

            int[] lengths = new int[array.Rank];

            for (int dimension = 0; dimension < array.Rank; dimension++)
            {
                lengths[dimension] = array.GetLength(dimension);

                IPrimitive lengthPrimitive = PrimitiveFactory.MakePrimitive(typeof(ushort));
                byte[] bytesToWrite = lengthPrimitive.GetByteStream((ushort)lengths[dimension]);
                resultStream.AddRange(bytesToWrite);
            }

            IConcreteAction action = ActionFactory.MakeAction(type.GetElementType()); //!!!

            var arrayIndexator = new ArrayIterator(lengths);
            for (int i = 0; i < array.Length; i++)
            {
                int[] index = arrayIndexator.GetNext();
                object arrayItem = array.GetValue(index);

                List<byte> data = action.Serialize(arrayItem);
                resultStream.AddRange(data);
            }

            return resultStream;
        }
    }
}
