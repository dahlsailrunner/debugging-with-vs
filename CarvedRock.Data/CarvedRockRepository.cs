using System.Diagnostics;
using CarvedRock.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarvedRock.Data;

public class CarvedRockRepository : ICarvedRockRepository
{
    private readonly LocalContext _ctx;
    private readonly ILogger<CarvedRockRepository> _logger;
    private readonly ILogger _factoryLogger;

    public CarvedRockRepository(LocalContext ctx, ILogger<CarvedRockRepository> logger,
        ILoggerFactory loggerFactory)
    {
        _ctx = ctx;
        _logger = logger;
        _factoryLogger = loggerFactory.CreateLogger("DataAccessLayer");
    }

    public async Task<List<Product>> GetProductsAsync(CancellationToken cancelToken, string category)
    {
        _logger.LogInformation("Getting products in repository for {category}", category);

        try
        {
            var timer = new Stopwatch();
            timer.Start();

            await Task.Delay(500, cancelToken); // simulates heavy query
            var products = await _ctx.Products
                .Where(p => p.Category == category || category == "all")
                .Include(p => p.Rating).ToListAsync(cancelToken);

            timer.Stop();
            _logger.LogInformation("Database took {ElapsedMs} milliseconds",
                timer.ElapsedMilliseconds);
            return products;
        }
        catch (Exception ex)
        {
            var newEx = new ApplicationException("Something bad happened in database", ex);
            newEx.Data.Add("Category", category);
            throw newEx;
        }
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _ctx.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetProductListAsync(string category)
    {
        return await _ctx.Products.Where(p => p.Category == category || category == "all")
            .ToListAsync();
    }

    public List<Product> GetProductList(string category)
    {
        return _ctx.Products.Where(p => p.Category == category || category == "all")
            .ToList();
    }

    public Product? GetProductById(int id)
    {
        var timer = new Stopwatch();
        timer.Start();

        var product = _ctx.Products.Find(id);
        timer.Stop();

        _logger.LogDebug("Querying products for {id} finished in {milliseconds} milliseconds",
            id, timer.ElapsedMilliseconds);

        _factoryLogger.LogInformation("(F) Querying products for {id} finished in {ticks} ticks",
            id, timer.ElapsedTicks);

        return product;
    }

    public async Task<Product> AddNewProductAsync(Product product, bool invalidateCache)
    {
        _ctx.Products.Add(product);
        await _ctx.SaveChangesAsync();

        return product;
    }
}
