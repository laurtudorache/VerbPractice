namespace VerbsPracticeApp.Controllers
{
    public interface IVerbRandomizer
    {
        int GetNextIndex(int maximumIndex, int[] usedIndexes);
    }
}