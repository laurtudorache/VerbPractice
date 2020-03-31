namespace VerbsPractice.Application.Services
{
    public interface IVerbRandomizer
    {
        int GetNextIndex(int maximumIndex, int[] usedIndexes);
    }
}