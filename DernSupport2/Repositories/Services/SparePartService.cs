using DernSupport2.Data;
using DernSupport2.Models.DTOs;
using DernSupport2.Models;
using DernSupport2.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DernSupport2.Repositories.Services
{
    public class SparePartService : ISparePart
    {
        private readonly ApplicationDbContext _context;

        public SparePartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool SparePartExists(int id)
        {
            return (_context.SpareParts?.Any(e => e.SparePartId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DeleteSparePart(int id)
        {
            var sparePart = await _context.SpareParts.FindAsync(id);
            if (sparePart == null)
            {
                return new NotFoundResult();
            }
            _context.SpareParts.Remove(sparePart);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Deleted");
        }

        public async Task<ActionResult<SparePartDTO>> GetSparePart(int id)
        {
            var sparePart = await _context.SpareParts.FindAsync(id);
            if (sparePart == null)
            {
                return new NotFoundResult();
            }
            var sparePartDto = new SparePartDTO
            {
                SparePartId = sparePart.SparePartId,
                PartName = sparePart.PartName,
                PartNumber = sparePart.PartNumber,
                Cost = sparePart.Cost,
                QuantityInStock = sparePart.QuantityInStock
            };
            return new OkObjectResult(sparePartDto);
        }

        public async Task<ActionResult<IEnumerable<SparePartDTO>>> GetSpareParts()
        {
            var spareParts = await _context.SpareParts
                .Select(sp => new SparePartDTO
                {
                    SparePartId = sp.SparePartId,
                    PartName = sp.PartName,
                    PartNumber = sp.PartNumber,
                    Cost = sp.Cost,
                    QuantityInStock = sp.QuantityInStock
                }).ToListAsync();
            return new OkObjectResult(spareParts);
        }

        public async Task<ActionResult<SparePartDTO>> PostSparePart(SparePartDTO sparePartDto)
        {
            var sparePart = new SparePart
            {
                PartName = sparePartDto.PartName,
                PartNumber = sparePartDto.PartNumber,
                Cost = sparePartDto.Cost,
                QuantityInStock = sparePartDto.QuantityInStock
            };

            await _context.SpareParts.AddAsync(sparePart);
            await _context.SaveChangesAsync();

            sparePartDto.SparePartId = sparePart.SparePartId;
            return new OkObjectResult(sparePartDto);
        }

        public async Task<IActionResult> PutSparePart(int id, SparePartDTO sparePartDto)
        {
            var existingSparePart = await _context.SpareParts.FindAsync(id);
            if (existingSparePart == null)
            {
                return new NotFoundResult();
            }

            existingSparePart.PartName = sparePartDto.PartName;
            existingSparePart.PartNumber = sparePartDto.PartNumber;
            existingSparePart.Cost = sparePartDto.Cost;
            existingSparePart.QuantityInStock = sparePartDto.QuantityInStock;

            _context.SpareParts.Update(existingSparePart);
            await _context.SaveChangesAsync();

            return new OkObjectResult(sparePartDto);
        }
    }
}
