namespace DernSupport2.Models.DTOs
{
    public class KnowledgeBaseDTO
    {
        public int KnowledgeBaseId { get; set; }
        public string Issue { get; set; }
        public string Diagnosis { get; set; }
        public string Solution { get; set; }
        public KnowledgeType KnowledgeType { get; set; }
    }
}
