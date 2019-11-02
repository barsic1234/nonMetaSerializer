using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace nonMetaSerializerTest.testDataObject
{
    public class PrimitiveFieldsInClass
    {
        public bool f1;
        public byte f2;
        public char f3;
        public double f4;
        public float f5;
        public int f6;
        public long f7;
        public short f8;
        public uint f9;
        public ulong f10;
        public ushort f11;

        public bool Equals(PrimitiveFieldsInClass obj)
        {
            if (f1.Equals(obj.f1) &&
                f2.Equals(obj.f2) &&
                f3.Equals(obj.f3) &&
                f4.Equals(obj.f4) &&
                f5.Equals(obj.f5) &&
                f6.Equals(obj.f6) &&
                f7.Equals(obj.f7) &&
                f8.Equals(obj.f8) &&
                f9.Equals(obj.f9) &&
                f10.Equals(obj.f10) &&
                f11.Equals(obj.f11))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
