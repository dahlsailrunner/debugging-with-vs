﻿using CarvedRock.Core;
using CarvedRock.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CarvedRock.Api.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AsyncProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductLogic _productLogic;


    public AsyncProductController(ILogger<ProductController> logger, IProductLogic productLogic)
    {
        _logger = logger;
        _productLogic = productLogic;
    }

    [HttpGet]
    [OutputCache]
    public async Task<IEnumerable<ProductModel>> Get(string category = "all")
    {
        using (_logger.BeginScope("ScopeCat: {ScopeCat}", category))
        {
            _logger.LogInformation("Getting products in API.");
            return await _productLogic.GetProductListForCategoryAsync(category);
        }
    }
}
