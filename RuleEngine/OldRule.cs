namespace RuleEngine
{
    public class RuleOld
    {
        public int Id { get; set; }
        public int RuleSetId { get; set; }
        public int RuleType { get; set; }
        public string LeftPropertyName { get; set; }
        public string ComparisonOperator { get; set; }
        public string RightPropertyName { get; set; }
        public string ExpectedValue { get; set; }
        public int BooleanConnector { get; set; }
    }
}
