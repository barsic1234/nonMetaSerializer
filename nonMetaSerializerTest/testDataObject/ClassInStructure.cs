using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nonMetaSerializerTest.testDataObject
{
    public struct ClassInStructure
    {
        public ArrayInClass field;

        public bool Equals(ClassInStructure structure)
        {
            return field.Equals(structure.field);
        }
    }
}
