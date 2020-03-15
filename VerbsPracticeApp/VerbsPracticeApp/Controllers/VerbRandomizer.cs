using System;
using System.Linq;

namespace VerbsPracticeApp.Controllers
{
    public class VerbRandomizer : IVerbRandomizer
    {
        public int GetNextIndex(int totalCount, int[] usedIndexes)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            if (usedIndexes.Length > totalCount)
            {
                return rand.Next(0, totalCount);
            }

            if (usedIndexes.Length + 10 > totalCount)
            {
                //pick the next available index
                for (var i = 0; i < totalCount; i++)
                {
                    if (!usedIndexes.Contains(i))
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