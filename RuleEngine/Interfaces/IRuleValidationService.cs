namespace RuleEngine.Interfaces
{
    public interface IRuleValidationService
    {
        bool IsSatisfied(RuleSet ruleSet);
    }
}