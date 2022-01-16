namespace Ordering.IntegrationTests;

using Ordering.Databases;
using Ordering.IntegrationTests.TestUtilities;
using Ordering;
using Ordering.Resources;
using Ordering.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Npgsql;
using NUnit.Framework;
using Respawn;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

[SetUpFixture]
public class TestFixture
{
    private static IConfigurationRoot _configuration;
    private static IWebHostEnvironment _env;
    private static IServiceScopeFactory _scopeFactory;
    private static Checkpoint _checkpoint;

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        var dockerDbPort = await DockerDatabaseUtilities.EnsureDockerStartedAndGetPortPortAsync();
        var dockerConnectionString = DockerDatabaseUtilities.GetSqlConnectionString(dockerDbPort.ToString());
        Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", dockerConnectionString);

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddInMemoryCollection(new Dictionary<string, string> { })
            .AddEnvironmentVariables();

        _configuration = builder.Build();
        _env = Mock.Of<IWebHostEnvironment>(e => e.EnvironmentName == LocalConfig.IntegrationTestingEnvName);

        var startup = new Startup(_configuration, _env);

        var services = new ServiceCollection();

        services.AddLogging();

        startup.ConfigureServices(services);

        // add any mock services here
        var httpContextAccessorService = services.FirstOrDefault(d =>
            d.ServiceType == typeof(IHttpContextAccessor));
        services.Remove(httpContextAccessorService);
        services.AddScoped(_ => Mock.Of<IHttpContextAccessor>());

        _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

        _checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { "__EFMigrationsHistory" },
            SchemasToExclude = new[] { "information_schema", "pg_subscription", "pg_catalog", "pg_toast" },
            DbAdapter = DbAdapter.Postgres
        };

        EnsureDatabase();

        // MassTransit Setup -- Do Not Delete Comment
    }

    private static void EnsureDatabase()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<OrdersDbContext>();

        context.Database.Migrate();
    }

    public static TScopedService GetService<TScopedService>()
    {
        var scope = _scopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetService<TScopedService>();
        return service;
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task ResetState()
    {
        using var conn = new NpgsqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
        await conn.OpenAsync();
        await _checkpoint.Reset(conn);
    }

    public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<OrdersDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<OrdersDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();

        try
        {
            //await dbContext.BeginTransactionAsync();

            await action(scope.ServiceProvider);

            //await dbContext.CommitTransactionAsync();
        }
        catch (Exception)
        {
            //dbContext.RollbackTransaction();
            throw;
        }
    }

    public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();

        try
        {
            //await dbContext.BeginTransactionAsync();

            var result = await action(scope.ServiceProvider);

            //await dbContext.CommitTransactionAsync();

            return result;
        }
        catch (Exception)
        {
            //dbContext.RollbackTransaction();
            throw;
        }
    }

    public static Task ExecuteDbContextAsync(Func<OrdersDbContext, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<OrdersDbContext>()));

    public static Task ExecuteDbContextAsync(Func<OrdersDbContext, ValueTask> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<OrdersDbContext>()).AsTask());

    public static Task ExecuteDbContextAsync(Func<OrdersDbContext, IMediator, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<OrdersDbContext>(), sp.GetService<IMediator>()));

    public static Task<T> ExecuteDbContextAsync<T>(Func<OrdersDbContext, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<OrdersDbContext>()));

    public static Task<T> ExecuteDbContextAsync<T>(Func<OrdersDbContext, ValueTask<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<OrdersDbContext>()).AsTask());

    public static Task<T> ExecuteDbContextAsync<T>(Func<OrdersDbContext, IMediator, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<OrdersDbContext>(), sp.GetService<IMediator>()));

    public static Task<int> InsertAsync<T>(params T[] entities) where T : class
    {
        return ExecuteDbContextAsync(db =>
        {
            foreach (var entity in entities)
            {
                db.Set<T>().Add(entity);
            }
            return db.SaveChangesAsync();
        });
    }

    // MassTransit Methods -- Do Not Delete Comment

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        // MassTransit Teardown -- Do Not Delete Comment
    }
}