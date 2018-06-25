using System.Collections.Generic;

namespace RuleEngine
{
    /// <summary>
    /// Container for rules that have the same boolean relationship between them.
    /// </summary>
    public class RuleSetOld
    {
        public int RuleStatementId { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Statement { get; set; }
        public int Operation { get; set; }
        public List<Rule> Rules { get; set; }
    }
}
