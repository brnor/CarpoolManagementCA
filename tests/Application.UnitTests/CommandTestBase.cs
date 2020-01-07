using Infrastructure.Persistence;
using System;

namespace Application.UnitTests
{
    public class CommandTestBase : IDisposable
    {
        public CarpoolDbContext Context { get; }

        public CommandTestBase()
        {
            Context = ApplicationDbContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Context);
        }
    }
}
