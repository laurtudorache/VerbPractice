using VerbsPractice.Application.Services;

namespace VerbsPractice.Persistence
{
    public class IregularAllVerbsQuery : IVerbsQuery
    {
        private readonly IVerbsRepository verbsRepository;

        public IregularAllVerbsQuery(IVerbsRepository verbsRepository)
        {
            this.verbsRepository = verbsRepository;
        }

        public string QueryKey => QueryKeys.IrregularAllVerbs;

        public int Count()
        {
            return verbsRepository.CountAllIrregular();
        }

        public Verb GetVerbByIndex(int index)
        {
            return verbsRepository.GetIrregularAllVerb(index);
        }
    }
}