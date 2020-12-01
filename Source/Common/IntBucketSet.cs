using System.Collections.Generic;

namespace Common
{
    public class IntBucketSet
    {
        private readonly byte[] hashTable;
        private readonly int minOffset;

        public IntBucketSet(IList<int> data)
        {
            var max = 0;
            var min = 0;
            for(int i = 0; i < data.Count; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                }

                if (data[i] < min)
                {
                    min = data[i];
                }
            }

            this.hashTable = new byte[(max - min) + 1];
            this.minOffset = -min;

            for (int i = 0; i < data.Count; i++)
            {
                hashTable[data[i]] = 1;
            }
        }

        public bool Contains(int value)
        {
            if (value < minOffset || value > minOffset + value)
            {
                return false;
            }

            return hashTable[minOffset + value] > 0;
        }
    }
}
