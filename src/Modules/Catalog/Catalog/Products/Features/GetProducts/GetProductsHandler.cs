﻿namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest)
  : IQuery<GetProductResult>;
public record GetProductResult(PaginatedResult<ProductDto> Products);

public class GetProductsHandler(CatalogDbContext dbContext)
  : IQueryHandler<GetProductsQuery, GetProductResult>
{
  public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
  {
    var pageIndex = query.PaginationRequest.PageIndex;
    var pageSize = query.PaginationRequest.PageSize;
    var totalCount = await dbContext.Products.LongCountAsync(cancellationToken);

    var products = await dbContext.Products
                        .AsNoTracking()
                        .OrderBy(p => p.Name)
                        .Skip(pageSize * pageIndex)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken);

    var productDtos = products.Adapt<List<ProductDto>>();

    var paginatedResult = new PaginatedResult<ProductDto>(
                             pageIndex,
                             pageSize,
                             totalCount,
                             productDtos);
                             
    return new GetProductResult(paginatedResult);
  }

}
