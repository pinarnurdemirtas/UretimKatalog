using MediatR;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Categories.Result;
using UretimKatalog.Application.Features.Categories.Requests.Commands;

public class CategoryCommandHandler :
    IRequestHandler<CreateCategoryCommand, CreateCategoryResult>,
    IRequestHandler<UpdateCategoryCommand, Unit>,
    IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryService _svc;
    public CategoryCommandHandler(ICategoryService svc) => _svc = svc;

    public async Task<CreateCategoryResult> Handle(CreateCategoryCommand cmd, CancellationToken ct)
{
    return await _svc.CreateAsync(cmd.Dto);
}

    public async Task<Unit> Handle(UpdateCategoryCommand cmd, CancellationToken ct)
    {
        await _svc.UpdateAsync(cmd.Dto);
        return Unit.Value;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand cmd, CancellationToken ct)
    {
        await _svc.DeleteAsync(cmd.Id);
        return Unit.Value;
    }
}

