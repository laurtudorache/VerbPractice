using System.Collections.Generic;

namespace VerbsPractice.Persistence
{
    public interface IVerbsFileReader
    {
        List<VerbData> LoadVerbs(string rootFolder);
    }
}