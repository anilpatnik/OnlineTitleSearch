using System.Collections.Generic;
using OTS.Models;

namespace OTS.Repositories.Contexts
{
    public static class MockDatabase
    {
        public static IEnumerable<Lookup> SearchEngines => new List<Lookup>
        {
            new Lookup {Id = 1, Name = "Google"},
            new Lookup {Id = 2, Name = "Bing"}
        };
    }
}
