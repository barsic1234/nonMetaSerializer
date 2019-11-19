using nonMetaSerializer.implPrimitive;
using nonMetaSerializer.util;
using System;
using System.Collections.Generic;

namespace nonMetaSerializer.concreteAction
{
    internal class ArrayAction : IConcreteAction
    {
        private readonly Type type;
        private Array array;
        private List<byte> resultStream;
        public ArrayAction(Type type)
        {
            this.type = type;
        }
        object IConcreteAction.Deserialize(StreamExtractorHandler streamExtractor)
        {
            int[] lengths = RecoveryDimensional(streamExtractor);
            array = ActivateInstance(lengths);
            return ArraySetElementValue(lengths, streamExtractor);
        }

        private int[] RecoveryDimensional(StreamExtractorHandler streamExtractor)
        {
            byte rank = RankRecovery(streamExtractor);
            int[] lengths = new int[rank];
            IPrimitive lengthPrimitive = PrimitiveFactory.MakePrimitive(typeof(ushort));
            for (int dimension = 0; dimension < rank; dimension++)
            {
                lengths[dimension] = (ushort)lengthPrimitive.GetValueField(streamExtractor);
            }
            return lengths;
        }

        private byte RankRecovery(StreamExtractorHandler streamExtractor)
        {
            IPrimitive rankPrimitive = PrimitiveFactory.MakePrimitive(typeof(byte));
            return (byte)rankPrimitive.GetValueField(streamExtractor);
        }

        private Array ActivateInstance(int[] lengths)
        {
            object[] activationParam = MakeParamsForActivator(lengths);
            return (Array)Activator.CreateInstance(type, activationParam);
        }
        private object[] MakeParamsForActivator(int[] lengths)
        {
            var objectArray = new object[lengths.Length];
            for (int i = 0; i < lengths.Length; i++)
            {
                objectArray[i] = lengths[i];
            }
            return objectArray;
        }

        private Array ArraySetElementValue(int[] lengths, StreamExtractorHandler streamExtractor)
        {
            IConcreteAction action = MakeActionForElementType();
            var arrayIndex = new ArrayIterator(lengths);
            for (int i = 0; i < array.Length; i++)
            {
                object elementValue = action.Deserialize(streamExtractor);
                int[] indices = arrayIndex.GetNext();
                array.SetValue(elementValue, indices);
            }
            return array;
        }

        private IConcreteAction MakeActionForElementType()
        {
            Type typeElement = type.GetElementType();
            return ActionFactory.MakeAction(typeElement);
        }

        List<byte> IConcreteAction.Serialize(object dataObject)
        {
            resultStream = new List<byte>();
            array = (Array)dataObject;

            int[] lengths = RecordAndExtractDimensional();
            DeserealizeElement(lengths);

            return resultStream;
        }

        private int[] RecordAndExtractDimensional()
        {
            IPrimitive rankPrimitive = PrimitiveFactory.MakePrimitive(typeof(byte));
            byte[] rank = rankPrimitive.GetByteStream((byte)array.Rank);
            resultStream.AddRange(rank);

            IPrimitive lengthPrimitive = PrimitiveFactory.MakePrimitive(typeof(ushort));
            int[] lengths = new int[array.Rank];
            for (int dimension = 0; dimension < array.Rank; dimension++)
            {
                lengths[dimension] = array.GetLength(dimension);

                byte[] bytesToWrite = lengthPrimitive.GetByteStream((ushort)lengths[dimension]);
                resultStream.AddRange(bytesToWrite);
            }
            return lengths;
        }

        private void DeserealizeElement(int[] lengths)
        {
            IConcreteAction action = MakeActionForElementType();
            var arrayIndexator = new ArrayIterator(lengths);
            for (int i = 0; i < array.Length; i++)
            {
                int[] index = arrayIndexator.GetNext();
                object arrayItem = array.GetValue(index);

                List<byte> data = action.Serialize(arrayItem);
                resultStream.AddRange(data);
            }
        }
    }
}
