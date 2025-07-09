using System;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;  // ← Bu şart!
using Microsoft.EntityFrameworkCore.Design;
using UretimKatalog.Infrastructure.Data;


namespace UretimKatalog.Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
{
    var builder = new DbContextOptionsBuilder<AppDbContext>();

    builder.UseMySql(
        "Server=localhost;Port=3306;Database=UretimKatalogDb;User=root;Password=Pinarbm18_;",
        new MySqlServerVersion(new Version(8, 0, 33))
    );

    return new AppDbContext(builder.Options);
}

    }
}
