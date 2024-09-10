using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DernSupport2.Repositories.Interfaces;
using DernSupport2.Models.DTOs;

namespace DernSupport2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SparePartsController : ControllerBase
    {
        private readonly ISparePart _sparePartService;

        public SparePartsController(ISparePart sparePartService)
        {
            _sparePartService = sparePartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SparePartDTO>>> GetSpareParts()
        {
            return await _sparePartService.GetSpareParts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SparePartDTO>> GetSparePart(int id)
        {
            return await _sparePartService.GetSparePart(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSparePart(int id, SparePartDTO sparePartDto)
        {
            return await _sparePartService.PutSparePart(id, sparePartDto);
        }

        [HttpPost]
        public async Task<ActionResult<SparePartDTO>> PostSparePart(SparePartDTO sparePartDto)
        {
            return await _sparePartService.PostSparePart(sparePartDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSparePart(int id)
        {
            return await _sparePartService.DeleteSparePart(id);
        }
    }
}
