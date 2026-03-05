using MediatR;

namespace e_commerce.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<List<ProductResponse>>;
