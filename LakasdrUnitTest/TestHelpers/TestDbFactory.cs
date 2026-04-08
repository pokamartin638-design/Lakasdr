using Lakasdr.Data;
using Microsoft.EntityFrameworkCore;

namespace Lakasdr.Tests.TestHelpers;

public static class TestDbFactory
{
    public static WorkDbContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<WorkDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new WorkDbContext(options);
    }
}
    