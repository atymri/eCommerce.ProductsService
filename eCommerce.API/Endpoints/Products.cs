using eCommerce.BusinessLogicLayer.DTOs;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entitie;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
namespace eCommerce.API.Endpoints;

public static class Products
{
    public static IEndpointRouteBuilder AddProductEndpoints(this IEndpointRouteBuilder app)
    {
        // GET: /api/products
        app.MapGet("/api/products", async (IProductsService service) =>
            Results.Ok(await service.GetProducts()));

        // GET: /api/products/search/{product-id}
        app.MapGet("/api/products/search/product-id/{productID:guid}", async (IProductsService service, Guid productId) =>
            Results.Ok(await service.GetProductByCondition(p => p.productID == productId)));

        app.MapPost("/api/products/validate", async (IProductsService service, [FromBody] List<Guid> productIds) =>
        {
            var res = await service.ValidateProducts(productIds);
            return res.IsSuccess ? Results.Ok(res) : Results.BadRequest(res);
        });

        app.MapGet("/api/products/validate/{productId:guid}", async (IProductsService service, Guid productId) =>
        {
            if (productId == null || productId == Guid.Empty)
                return Results.BadRequest();

            var result = await service.ValidateProduct(productId);
            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        });

        // GET: /api/products/search/{keyword}
        app.MapGet("/api/products/search/{keyword:alpha}", async (IProductsService service, string keyword) =>
            {
                var searchByname = await service
                .GetProductsByCondition(p => p.productName != null
                && p.productName.Contains(keyword, StringComparison.OrdinalIgnoreCase));

                var searchByCategory = await service
                .GetProductsByCondition(p => p.category != null
                && p.category.Contains(keyword, StringComparison.OrdinalIgnoreCase));

                return Results.Ok(searchByname.Union(searchByCategory));
            });

        // GET: /api/products/category/{category}
        app.MapGet("/api/products/category/{category:alpha}", async (IProductsService service, string category) =>
            Results.Ok(await service.GetProductsByCondition(p => p.category != null
                && p.category.Equals(category, StringComparison.OrdinalIgnoreCase))));

        // DELETE: /api/products/{product-id}
        app.MapDelete("/api/products/{productID:guid}", async (IProductsService service, Guid productId) =>
        {
            var res = await service.DeleteProduct(productId);
            return res == true
            ? Results.Ok(res)
            : Results.Problem("error deleting the product");
        });

        // POST: /api/products
        app.MapPost("/api/products", async (IProductsService service,
            IValidator<ProductAddRequest> validator, ProductAddRequest req) =>
        {
            var validationRes = await validator.ValidateAsync(req);
            if (!validationRes.IsValid)
            {
                Dictionary<string, string[]> errors = validationRes.Errors.GroupBy(res => res.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(g => g.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var addRes = await service.AddProduct(req);
            if (addRes is null)
                return Results.Problem("error adding product");

            return Results.Created($"/api/products/{addRes.productID}", addRes);
        });

        // PUT: /api/products
        app.MapPut("/api/products", async (IProductsService service,
            IValidator<ProductUpdateRequest> validator, ProductUpdateRequest req) =>
        {
            var validationRes = await validator.ValidateAsync(req);
            if (!validationRes.IsValid)
            {
                Dictionary<string, string[]> errors = validationRes.Errors.GroupBy(res => res.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(g => g.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var updateRes = await service.UpdateProduct(req);
            if (updateRes is null)
                return Results.Problem("error updating product");

            return Results.Ok(updateRes);
        });


        return app;
    }
}

