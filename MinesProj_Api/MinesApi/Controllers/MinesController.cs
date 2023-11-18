using AutoMapper;
using Contracts;
using Entities.Models.ViewModels;
using Features.Mines.Commands;
using Features.Mines.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinesApi.Models;
using static NuGet.Packaging.PackagingConstants;

namespace MinesApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MinesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IRepositoryWrapper _repository;


        public MinesController(IRepositoryWrapper repository, IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _repository = repository;
        }

        // GET: api/Mines/GetMines
        [HttpGet(nameof(GetMines))]
        public async Task<ActionResult<IEnumerable<Mine>>> GetMines()
        {
            var mines = await _mediator.Send(new GetMinesRequest() { });

            return mines == null ? NotFound() : Ok(mines);
        }

        // GET: api/Mines/GetMine?id=5
        [HttpGet(nameof(GetMine))]
        public async Task<ActionResult<Mine>> GetMine(int id)
        {
            var mine = await _repository.MineRepo.Get(id);

            if (mine == null)
            {
                return NotFound();
            }

            return Ok(mine);
        }

        // GET: api/Mines/GetDetailedMines
        [HttpGet(nameof(GetDetailedMines))]
        public async Task<ActionResult<IEnumerable<MineViewModel>>> GetDetailedMines()
        {
            var mines = await _repository.MineRepo.GetDetailedMines();
            if (mines == null)
            {
                return NotFound();
            }

            return Ok(mines);
        }

        // GET: api/Mines/GetDetailedMine?id=5
        [HttpGet(nameof(GetDetailedMine))]
        public async Task<ActionResult<MineViewModel>> GetDetailedMine(int id)
        {
            var mines = await _repository.MineRepo.GetDetailedMines();
            var mine = mines.FirstOrDefault(mine => mine.Id == id);

            if (mine == null)
            {
                return NotFound();
            }

            return Ok(mine);
        }


        //POST: api/Mines/GetFilteredDetailedMines
        [HttpPost(nameof(GetFilteredDetailedMines))]
        public async Task<ActionResult<IEnumerable<MineViewModel>>> GetFilteredDetailedMines([FromBody] Filters filters)
        {
            var mines = await _mediator.Send(new GetFilteredDetailedMinesRequest() { Filters = filters });
            if (mines == null)
            {
                return NotFound();
            }

            return Ok(mines);
        }


        // PUT: api/Mines/PutMine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut(nameof(PutMine))]
        public async Task<IActionResult> PutMine(MineViewModel mine)
        {
            Mine mineData = _mapper.Map<Mine>(mine);

            Mine? updatedMine = await _repository.MineRepo.Update(mineData);
            if (updatedMine == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Mines/PostMine
        [HttpPost(nameof(PostMine))]
        public async Task<ActionResult<Mine>> PostMine(MineViewModel mineDto)
        {
            Mine createdMine = await _mediator.Send(new CreateMineRequest() { MineDto = mineDto });

            if (createdMine != null)
                return Ok(createdMine);

            return Conflict("the parent resource does not exist for you to post to!");
        }

        // DELETE: api/Mines/DeleteMine
        [HttpDelete(nameof(DeleteMine))]
        public async Task<IActionResult> DeleteMine(int id)
        {
            Mine? mine = await _repository.MineRepo.Get(id);
            if (mine == null)
            {
                return NotFound();
            }

            await _repository.MineRepo.Delete(mine.Id);

            return NoContent();
        }

    }
}
