using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nonMetaSerializer.util
{
    public class ArrayIterator //вспомогательный класс, служащий для последовательного перебора элементов в массиве
    {
        private readonly int[] lengths;
        private readonly int[] indices;

        public ArrayIterator(int[] lengths)
        {
            this.lengths = lengths;
            indices = new int[lengths.Length];

            int lastDimension = lengths.Length - 1;
            indices[lastDimension]--;
        }

        public int[] GetNext()
        {
            int lastDimension = lengths.Length - 1;
            IncrementDimension(lastDimension);

            return indices;
        }

        private void IncrementDimension(int dimension)
        {
            if (dimension < 0)
            {
                return;
            }
            else if (indices[dimension] == lengths[dimension] - 1)
            {
                indices[dimension] = 0;
                IncrementDimension(dimension - 1);
            }
            else
            {
                indices[dimension]++;
            }
        }
    }
}
