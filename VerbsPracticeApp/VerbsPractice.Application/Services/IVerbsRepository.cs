namespace VerbsPractice.Application.Services
{
    public interface IVerbsRepository
    {
        void Initialize(string rootFolder);

        int CountIrregularBasic();

        int CountAllIrregular();

        int CountAllBasic();

        int CountAll();

        Verb GetIrregularBasicVerb(int index);

        Verb GetIrregularAllVerb(int index);

        Verb GetBasicVerb(int index);

        Verb GetVerb(int index);
    }
}