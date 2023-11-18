using Contracts;
using MediatR;
using MinesApi.Models;

namespace Features.Mines.Queries;

public class GetMinesRequest : IRequest<IEnumerable<Mine>>
{
}

internal class GetMinesRequestHandler : IRequestHandler<GetMinesRequest, IEnumerable<Mine>>
{
    private readonly IRepositoryWrapper _repository;
    private readonly ICacheService _cacheService;

    public GetMinesRequestHandler(IRepositoryWrapper repository, ICacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<Mine>> Handle(GetMinesRequest request, CancellationToken cancellationToken)
    {
        var mines = _cacheService.GetData<IEnumerable<Mine>>("mines");
        if (mines == null || !mines.Any())
        {
            mines = await _repository.MineRepo.GetAll();

            var expiaryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<IEnumerable<Mine>>("mines", mines, expiaryTime);
        }

        return mines;
    }
}
