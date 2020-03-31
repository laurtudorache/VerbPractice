using System;
using System.Linq;

namespace VerbsPractice.Application.Services
{
    public class VerbRandomizer : IVerbRandomizer
    {
        public int GetNextIndex(int totalCount, int[] usedIndexes)
        {
            var indexes = usedIndexes.ToList();
            Random rand = new Random((int)DateTime.Now.Ticks);

            if (indexes.Count > totalCount)
            {
                return rand.Next(0, totalCount);
            }

            if (indexes.Count + 10 > totalCount)
            {
                //pick the next available index
                for (var i = 0; i < totalCount; i++)
                {
                    if (!indexes.Contains(i))
                    {
                        return i;
                    }
                }
            }

            int nextIndex;
            do
            {
                nextIndex = rand.Next(0, totalCount);
            } while (usedIndexes.Contains(nextIndex));
            return nextIndex;
        }
    }
}