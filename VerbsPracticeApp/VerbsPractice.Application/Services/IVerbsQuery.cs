namespace VerbsPractice.Application.Services
{
    public interface IVerbsQuery
    {
        string QueryKey { get; }

        int Count();

        Verb GetVerbByIndex(int index);
    }
}