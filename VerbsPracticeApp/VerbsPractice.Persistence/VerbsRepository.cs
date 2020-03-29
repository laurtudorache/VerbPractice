using System;
using System.Collections.Generic;
using System.Linq;
using VerbsPractice.Application.Services;

namespace VerbsPractice.Persistence
{
    public class VerbsRepository : IVerbsRepository
    {
        public VerbsRepository(IVerbsFileReader verbsFileReader)
        {
            this.verbsFileReader = verbsFileReader;
        }

        private List<VerbData> verbs;
        private readonly IVerbsFileReader verbsFileReader;

        public int CountAll()
        {
            return verbs.Count;
        }

        public int CountAllBasic()
        {
            return verbs.Count(v => v.IsBasic);
        }

        public int CountAllIrregular()
        {
            return verbs.Count(v => !v.IsRegular);
        }

        public int CountIrregularBasic()
        {
            return verbs.Count(v => !v.IsRegular && v.IsBasic);
        }

        public Verb GetBasicVerb(int index)
        {
            return GetVerb(index, v => v.IsBasic);
        }

        public Verb GetIrregularAllVerb(int index)
        {
            return GetVerb(index, v => !v.IsRegular);
        }

        public Verb GetIrregularBasicVerb(int index)
        {
            return GetVerb(index, v => !v.IsRegular && v.IsBasic);
        }

        public Verb GetVerb(int index)
        {
            return verbs[index].ToVerb();
        }

        private Verb GetVerb(int index, Func<VerbData, bool> condition)
        {
            index = index - 1;
            if (index < 0)
            {
                index = 0;
            }
            return verbs.Where(v => condition(v)).Skip(index).Take(1).Single().ToVerb();
        }

        public void Initialize(string rootFolder)
        {
            verbs = verbsFileReader.LoadVerbs(rootFolder);
        }
    }

    internal static class Helper
    {
        public static Verb ToVerb(this VerbData verbData)
        {
            return new Verb
            {
                English = verbData.English,
                ImperfectumPl = verbData.ImperfectumPl,
                ImperfectumSg = verbData.ImperfectumSg,
                Infinitief = verbData.Infinitief,
                Perfectum = verbData.Perfectum
            };
        }
    }
}