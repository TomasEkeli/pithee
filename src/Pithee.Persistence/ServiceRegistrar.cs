using Microsoft.Extensions.DependencyInjection;
using Pithee.Persistence.Postgres;
using Pithee.Persistence.Users;
using static Pithee.Persistence.Postgres.PostgresAdminDataContext;
using static Pithee.Persistence.Postgres.PostgresDataContext;

namespace Pithee.Persistence;

public static class ServiceRegistrar
{
    static readonly DbConfig DefaultConnection =
        new(
            Host: "localhost",
            Port: 5432,
            Database: "pithee",
            Username: "postgres",
            Password: "postgres"
        );

    static readonly AdminDbConfig DefaultAdmin =
        new(
            Host: "localhost",
            Port: 5432,
            Username: "postgres",
            Password: "postgres"
        );

    public static IServiceCollection RegisterPersistenceServices(
        this IServiceCollection services,
        Options? options = null
    )
    {
        services
            .AddSingleton(options?.AdminConnectionString ?? DefaultAdmin)
            .AddSingleton(options?.ConnectionString ?? DefaultConnection)
            .AddSingleton<IAdminDataContext, PostgresAdminDataContext>()
            .AddSingleton<IDatabaseCreator, PostgresDatabaseCreator>()
            .AddSingleton<ISchemaCreator, SchemaCreator>()
            .AddSingleton<IPersistenceInitializer, PostgresInitializer>()
            .AddTransient<IDefineSchema, UserSchema>()
            .AddTransient<IDataContext, PostgresDataContext>()
            .AddTransient<IUsersRepository, UsersRepository>();

        return services;
    }

    public class Options
    {
        public DbConfig? ConnectionString { get; set; }
        public AdminDbConfig? AdminConnectionString { get; set; }
    }
}

