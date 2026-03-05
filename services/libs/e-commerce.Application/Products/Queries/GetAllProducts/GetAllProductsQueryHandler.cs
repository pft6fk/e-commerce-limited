using e_commerce.Application.Interfaces;
using MediatR;

namespace e_commerce.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);

        return products.Select(p => new ProductResponse(
            p.Id.Value,
            p.Name,
            p.Description,
            p.Price.Amount,
            p.Price.Currency,
            p.IsActive)).ToList();
    }
}
