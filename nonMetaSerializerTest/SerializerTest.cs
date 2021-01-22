using Microsoft.VisualStudio.TestTools.UnitTesting;
using nonMetaSerializer.util;
using nonMetaSerializerTest.testDataObject;
using System.Linq;

namespace nonMetaSerializer
{
    [TestClass]
    public class SerializerTest
    {
        [TestMethod]
        public void Primitive()
        {
            int primitive = 129;
            byte[] data = BytesStream.Serialize(primitive);
            int decod = BytesStream.Deserialize<int>(data);
            Assert.AreEqual(primitive, decod);
        }

        [TestMethod]
        public void OneDimensionalArray()
        {
            int[] oneArray = new int[] { 15, 48, 99 };
            byte[] data = BytesStream.Serialize(oneArray);
            int[] decod = BytesStream.Deserialize<int[]>(data);
            Assert.IsTrue(Enumerable.SequenceEqual(oneArray, decod));
        }

        [TestMethod]
        public void MultiDimensionalArray()
        {
            int[,,] multiArray = new int[2, 3, 4];
            multiArray[1, 2, 3] = 7698;
            multiArray[0, 1, 1] = 278;
            multiArray[0, 0, 0] = 325;
            byte[] data = BytesStream.Serialize(multiArray);
            int[,,] decod = BytesStream.Deserialize<int[,,]>(data);

            Assert.AreEqual(multiArray.Length, decod.Length);
            var arrayIndex = new ArrayIterator(new int[] { 2, 3, 4 });
            for (int i = 0; i < multiArray.Length; i++)
            {
                int[] indices = arrayIndex.GetNext();
                Assert.AreEqual(multiArray.GetValue(indices), decod.GetValue(indices));
            }
        }

        [TestMethod]
        public void ArrayArray()
        {
            int[][] arrayArray = new int[2][];
            arrayArray[0] = new int[] { 55, 66 };
            arrayArray[1] = new int[] { 28, 63 };
            byte[] data = BytesStream.Serialize(arrayArray);
            int[][] decod = BytesStream.Deserialize<int[][]>(data);

            Assert.AreEqual(arrayArray.Length, arrayArray.Length);
            for (int i = 0; i < arrayArray.Length; i++)
            {
                Assert.AreEqual(arrayArray[i].Length, decod[i].Length);
                for (int j = 0; j < arrayArray[i].Length; j++)
                {
                    Assert.AreEqual(arrayArray[i][j], decod[i][j]);
                }
            }
            //Assert.IsTrue(Enumerable.SequenceEqual(arrayArray, decod));
            //for (int i = 0; i < arrayArray.Length; i++)
            //{
            //    Assert.IsTrue(Enumerable.SequenceEqual(arrayArray[i], decod[i]));
            //}
        }

        [TestMethod]
        public void PrimitiveInClass()
        {
            var inClass = new PrimitiveFieldsInClass()
            {
                f1 = true,
                f2 = 4,
                f3 = 'c',
                f4 = 3.44d,
                f5 = 1.02f,
                f6 = 9,
                f7 = 64655L,
                f8 = 14,
                f9 = 5u,
                f10 = 96uL,
                f11 = 10
            };
            byte[] data = BytesStream.Serialize(inClass);
            PrimitiveFieldsInClass decod = BytesStream.Deserialize<PrimitiveFieldsInClass>(data);
            Assert.IsTrue(inClass.Equals(decod));
        }

        [TestMethod]
        public void ClassInStructure()
        {
            var inStruct = new ClassInStructure();
            inStruct.field = new ArrayInClass() { field = new int[] { 6, 60 } };
            byte[] data = BytesStream.Serialize(inStruct);
            ClassInStructure decod = BytesStream.Deserialize<ClassInStructure>(data);
            Assert.IsTrue(inStruct.Equals(decod));
        }

        [TestMethod]
        public void ClassInStructureArray()
        {
            var inStructArr = new ClassInStructure[1] { new ClassInStructure() };
            inStructArr[0].field = new ArrayInClass() { field = new int[] { 6, 60 } };
            byte[] data = BytesStream.Serialize(inStructArr);
            ClassInStructure[] decod = BytesStream.Deserialize<ClassInStructure[]>(data);
            Assert.IsTrue(inStructArr[0].Equals(decod[0]));
        }

        [TestMethod]
        public void String()
        {
            string str = "yteeehk";
            byte[] data = BytesStream.Serialize(str);
            string decod = BytesStream.Deserialize<string>(data);
            Assert.AreEqual(str, decod);
        }
    }
}
