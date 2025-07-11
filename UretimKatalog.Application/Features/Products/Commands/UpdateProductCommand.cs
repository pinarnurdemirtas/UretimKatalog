using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Domain.Interfaces;

namespace UretimKatalog.Application.Features.Products.Commands
{
    public record UpdateProductCommand(UpdateProductDto Dto) : IRequest<Unit>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Models.Product>(request.Dto);
            _uow.Products.Update(entity);
            await _uow.CommitAsync();
            return Unit.Value;
        }
    }
}