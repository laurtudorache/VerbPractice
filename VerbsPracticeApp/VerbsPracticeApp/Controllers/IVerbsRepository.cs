using Microsoft.AspNetCore.Hosting;

namespace VerbsPracticeApp.Controllers
{
    public interface IVerbsRepository
    {
        void Initialize(IWebHostEnvironment environment);

        int Count(bool isRegular, bool isBasic);

        VerbData GetVerbByIndex(int index);
    }
}