using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using eCommerce.BusinessLogicLayer.DTOs;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entitie;
using eCommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.Services;

internal class ProductsService : IProductsService
{
    // NOTE: for fluent validation if we want to validate the object automaticly we will write this:
    //  builder.Services.AddFluentValidationAutoValidation();

    // BUT in minimal api we can't do that, so we are injecting some Ivalidators in the services 
    // to do the validation in here.

    // NOTE: in both cases having this line:
    // builder.Services.AddValidatorsFromAssemblyContaining<ValidationClass> is mandatory.

    private readonly IValidator<ProductAddRequest> _addRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _updateRequestValidator;
    private readonly IProductsRepository _productRepository;
    private readonly IMapper _mapper;
    public ProductsService(IProductsRepository productRepository, IMapper mapper, 
        IValidator<ProductUpdateRequest> updateRequestValidator, IValidator<ProductAddRequest> addRequestValidator)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _updateRequestValidator = updateRequestValidator;
        _addRequestValidator = addRequestValidator;
    }

    public async Task<ProductRersponse?> AddProduct(ProductAddRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var validationResult = await _addRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ArgumentException(
                string.Join(';', validationResult.Errors.Select(e => e.ErrorMessage)));

        var response = await _productRepository.AddProduct(
            _mapper.Map<Product>(request));

        return _mapper.Map<ProductRersponse>(response);
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException(nameof(productId));

        if (!await _productRepository.DoesProductExist(productId))
            return false;

        return await _productRepository.DeleteProduct(productId);
    }

    public async Task<ProductRersponse?> GetProductByCondition(Expression<Func<ProductRersponse, bool>> condition)
    {
        if (condition is null)
            throw new ArgumentNullException(nameof(condition));

        var mappedCondition = _mapper.MapExpression<Expression<Func<Product, bool>>>(condition);
        var res = await _productRepository.GetProductByCondition(mappedCondition);

        return _mapper.Map<ProductRersponse>(res);
    }

    public async Task<List<ProductRersponse>?> GetProducts()
        => _mapper.Map<List<ProductRersponse>>(await _productRepository.GetProducts());

    public async Task<List<ProductRersponse>?> GetProductsByCondition(Expression<Func<ProductRersponse, bool>> condition)
    {
        if (condition is null)
            throw new ArgumentNullException(nameof(condition));

        var mappedExpression = _mapper.MapExpression<Expression<Func<Product, bool>>>(condition);
        var res = await _productRepository.GetProductsByCondition(mappedExpression);

        return _mapper.Map<List<ProductRersponse>>(res);
    }

    public async Task<ProductRersponse?> UpdateProduct(ProductUpdateRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var validationResult = await _updateRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(
                string.Join(';', validationResult.Errors.Select(e => e.ErrorMessage)));

        if(!await _productRepository.DoesProductExist(request.productId))
            throw new ArgumentException(nameof(request.productId));

        var response = await _productRepository
            .UpdateProduct(_mapper.Map<Product>(request));

        return _mapper.Map<ProductRersponse>(response);
    }
}

