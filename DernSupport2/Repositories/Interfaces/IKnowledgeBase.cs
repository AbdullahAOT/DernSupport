using DernSupport2.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DernSupport2.Repositories.Interfaces
{
    public interface IKnowledgeBase
    {
        Task<ActionResult<IEnumerable<KnowledgeBaseDTO>>> GetKnowledgeBases();
        Task<ActionResult<KnowledgeBaseDTO>> GetKnowledgeBase(int id);
        Task<IActionResult> PutKnowledgeBase(int id, KnowledgeBaseDTO knowledgeBaseDto);
        Task<ActionResult<KnowledgeBaseDTO>> PostKnowledgeBase(KnowledgeBaseDTO knowledgeBaseDto);
        Task<IActionResult> DeleteKnowledgeBase(int id);
        bool KnowledgeBaseExists(int id);
    }
}
