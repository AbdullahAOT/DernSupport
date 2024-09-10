using DernSupport2.Data;
using DernSupport2.Models.DTOs;
using DernSupport2.Models;
using DernSupport2.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DernSupport2.Repositories.Services
{
    public class KnowledgeBaseService : IKnowledgeBase
    {
        private readonly ApplicationDbContext _context;

        public KnowledgeBaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool KnowledgeBaseExists(int id)
        {
            return (_context.KnowledgeBases?.Any(e => e.KnowledgeBaseId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DeleteKnowledgeBase(int id)
        {
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(id);
            if (knowledgeBase == null)
            {
                return new NotFoundResult();
            }
            _context.KnowledgeBases.Remove(knowledgeBase);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Deleted");
        }

        public async Task<ActionResult<KnowledgeBaseDTO>> GetKnowledgeBase(int id)
        {
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(id);
            if (knowledgeBase == null)
            {
                return new NotFoundResult();
            }
            var knowledgeBaseDto = new KnowledgeBaseDTO
            {
                KnowledgeBaseId = knowledgeBase.KnowledgeBaseId,
                Issue = knowledgeBase.Issue,
                Diagnosis = knowledgeBase.Diagnosis,
                Solution = knowledgeBase.Solution,
                KnowledgeType = knowledgeBase.KnowledgeType
            };
            return new OkObjectResult(knowledgeBaseDto);
        }

        public async Task<ActionResult<IEnumerable<KnowledgeBaseDTO>>> GetKnowledgeBases()
        {
            var knowledgeBases = await _context.KnowledgeBases
                .Select(kb => new KnowledgeBaseDTO
                {
                    KnowledgeBaseId = kb.KnowledgeBaseId,
                    Issue = kb.Issue,
                    Diagnosis = kb.Diagnosis,
                    Solution = kb.Solution,
                    KnowledgeType = kb.KnowledgeType
                }).ToListAsync();
            return new OkObjectResult(knowledgeBases);
        }

        public async Task<ActionResult<KnowledgeBaseDTO>> PostKnowledgeBase(KnowledgeBaseDTO knowledgeBaseDto)
        {
            var knowledgeBase = new KnowledgeBase
            {
                Issue = knowledgeBaseDto.Issue,
                Diagnosis = knowledgeBaseDto.Diagnosis,
                Solution = knowledgeBaseDto.Solution,
                KnowledgeType = knowledgeBaseDto.KnowledgeType
            };

            await _context.KnowledgeBases.AddAsync(knowledgeBase);
            await _context.SaveChangesAsync();

            knowledgeBaseDto.KnowledgeBaseId = knowledgeBase.KnowledgeBaseId;
            return new OkObjectResult(knowledgeBaseDto);
        }

        public async Task<IActionResult> PutKnowledgeBase(int id, KnowledgeBaseDTO knowledgeBaseDto)
        {
            var existingKnowledgeBase = await _context.KnowledgeBases.FindAsync(id);
            if (existingKnowledgeBase == null)
            {
                return new NotFoundResult();
            }

            existingKnowledgeBase.Issue = knowledgeBaseDto.Issue;
            existingKnowledgeBase.Diagnosis = knowledgeBaseDto.Diagnosis;
            existingKnowledgeBase.Solution = knowledgeBaseDto.Solution;
            existingKnowledgeBase.KnowledgeType = knowledgeBaseDto.KnowledgeType;

            _context.KnowledgeBases.Update(existingKnowledgeBase);
            await _context.SaveChangesAsync();

            return new OkObjectResult(knowledgeBaseDto);
        }
    }
}
