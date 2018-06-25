namespace RuleEngine.RepositoryInterfaces
{
    public interface IRuleSetRepository : IRepository<RuleSet>
    {
        RuleSet GetRuleSet(int id);
    }
}