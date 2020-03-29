using VerbsPractice.Application.Services;

namespace VerbsPractice.Persistence
{
    public class IregularBasicVerbsQuery : IVerbsQuery
    {
        private readonly IVerbsRepository verbsRepository;

        public IregularBasicVerbsQuery(IVerbsRepository verbsRepository)
        {
            this.verbsRepository = verbsRepository;
        }

        public string QueryKey => QueryKeys.IrregularBasicVerbs;

        public int Count()
        {
            return verbsRepository.CountIrregularBasic();
        }

        public Verb GetVerbByIndex(int index)
        {
            return verbsRepository.GetIrregularBasicVerb(index);
        }
    }
}