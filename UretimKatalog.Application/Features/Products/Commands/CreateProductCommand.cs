using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Domain.Interfaces;

namespace UretimKatalog.Application.Features.Products.Commands
{
    public record CreateProductCommand(CreateProductDto Dto) : IRequest<ProductDto>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Models.Product>(request.Dto);
            await _uow.Products.AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<ProductDto>(entity);
        }
    }
}