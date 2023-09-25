using AutoMapper;
using Contracts;
using Entities.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinesApi.Models;
using MinesApi.Models.ViewModels;

namespace MinesApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MinesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;


        public MinesController(IRepositoryWrapper repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
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
            var mines = await _repository.MineRepo.GetFilteredDetailedMines(filters);
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(nameof(PostMine))]
        public async Task<ActionResult<Mine>> PostMine(MineViewModel mine)
        {

            Mine mineData = _mapper.Map<Mine>(mine);

            await _repository.MineRepo.Add(mineData);

            return CreatedAtAction("GetMine", new { id = mineData.Id }, mineData);
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
