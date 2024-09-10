using DernSupport2.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DernSupport2.Repositories.Interfaces
{
    public interface ISparePart
    {
        Task<ActionResult<IEnumerable<SparePartDTO>>> GetSpareParts();
        Task<ActionResult<SparePartDTO>> GetSparePart(int id);
        Task<IActionResult> PutSparePart(int id, SparePartDTO sparePartDto);
        Task<ActionResult<SparePartDTO>> PostSparePart(SparePartDTO sparePartDto);
        Task<IActionResult> DeleteSparePart(int id);
        bool SparePartExists(int id);
    }
}
