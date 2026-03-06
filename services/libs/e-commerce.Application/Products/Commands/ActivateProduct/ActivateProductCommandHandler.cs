using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;
using e_commerce.Domain.ValueObjects;
using MediatR;

namespace e_commerce.Application.Products.Commands.ActivateProduct;

public class ActivateProductCommandHandler : IRequestHandler<ActivateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ActivateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken)
            ?? throw new DomainException($"Product {request.ProductId} not found.");

        product.Activate();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
