namespace Catalog.Products.Features.GetProductByCategory;

// public record GetProductRequest(string category);
public record GetProductsByCategoryResponse(IEnumerable<ProductDto> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
    {
      var result = await sender.Send(new GetProductByCategoryQuery(category));

      var response = result.Adapt<GetProductsByCategoryResponse>();

      return Results.Ok(response);
    })
    .WithName("GetProductByCategory")
    .Produces<GetProductsByCategoryResponse>(StatusCodes.Status201Created)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Get Product By Category")
    .WithDescription("Get Product By Category");
  }
}
