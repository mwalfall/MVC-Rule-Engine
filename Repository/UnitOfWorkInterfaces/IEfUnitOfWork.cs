using RulesRepository.UnitOfWorkInterfaces;

namespace RulesRepository.UnitOfWorkInterfaces
{
    public interface IEfUnitOfWork<T> : IUnitOfWork
    {
        T GetContext { get; }
    }
}

