namespace DernSupport2.Models
{
    public class KnowledgeBase
    {
        public int KnowledgeBaseId { get; set; }
        public string Issue { get; set; }
        public string Diagnosis { get; set; }
        public string Solution { get; set; }
        public KnowledgeType KnowledgeType { get; set; } // Enum for Hardware, Software
    }

    public enum KnowledgeType
    {
        Hardware,
        Software
    }
}
