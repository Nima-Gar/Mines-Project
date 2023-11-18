using AutoMapper;
using Contracts;
using Entities.Models.ViewModels;
using MediatR;
using MinesApi.Models;

namespace Features.Mines.Commands;

public class CreateMineRequest : IRequest<Mine>
{
    public MineViewModel MineDto { get; set; }
}

internal class CreateMineRequestHandler : IRequestHandler<CreateMineRequest, Mine>
{
    private readonly IRepositoryWrapper _repository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public CreateMineRequestHandler(IRepositoryWrapper repository, ICacheService cacheService, IMapper mapper)
    {
        _repository = repository;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<Mine> Handle(CreateMineRequest request, CancellationToken cancellationToken)
    {
        if (request.MineDto != null)
        {
            Mine mine = _mapper.Map<Mine>(request.MineDto);
            await _repository.MineRepo.Add(mine);
            var epiaryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<Mine>($"mine{mine.Id}", mine, epiaryTime);

            return mine;
        }

        return default;
    }
}
