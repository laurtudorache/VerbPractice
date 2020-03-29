using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace VerbsPractice.Application.Services
{
    public class NameServiceResolver : INameServiceResolver
    {
        private readonly IServiceProvider container;

        public NameServiceResolver(IServiceProvider container)
        {
            this.container = container;
        }

        public T GetByName<T>(string name)
            where T : IVerbsQuery
        {
            return container.GetServices<T>().Where(s => s.QueryKey == name).Single();
        }
    }
}