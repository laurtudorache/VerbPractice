namespace VerbsPractice.Application.Services
{
    public interface INameServiceResolver
    {
        T GetByName<T>(string name) where T : IVerbsQuery;
    }
}