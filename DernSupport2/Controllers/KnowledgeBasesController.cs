using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DernSupport2.Repositories.Interfaces;
using DernSupport2.Models.DTOs;

namespace DernSupport2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeBasesController : ControllerBase
    {
        private readonly IKnowledgeBase _knowledgeBaseService;

        public KnowledgeBasesController(IKnowledgeBase knowledgeBaseService)
        {
            _knowledgeBaseService = knowledgeBaseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KnowledgeBaseDTO>>> GetKnowledgeBases()
        {
            return await _knowledgeBaseService.GetKnowledgeBases();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KnowledgeBaseDTO>> GetKnowledgeBase(int id)
        {
            return await _knowledgeBaseService.GetKnowledgeBase(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKnowledgeBase(int id, KnowledgeBaseDTO knowledgeBaseDto)
        {
            return await _knowledgeBaseService.PutKnowledgeBase(id, knowledgeBaseDto);
        }

        [HttpPost]
        public async Task<ActionResult<KnowledgeBaseDTO>> PostKnowledgeBase(KnowledgeBaseDTO knowledgeBaseDto)
        {
            return await _knowledgeBaseService.PostKnowledgeBase(knowledgeBaseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnowledgeBase(int id)
        {
            return await _knowledgeBaseService.DeleteKnowledgeBase(id);
        }
    }
}
