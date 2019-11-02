using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nonMetaSerializerTest.testDataObject
{
    public class ArrayInClass
    {
        public int[] field;

        public bool Equals(ArrayInClass arrayInClass)
        {
            return Enumerable.SequenceEqual(field, arrayInClass.field);
        }
    }
}
