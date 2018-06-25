using System;

namespace RulesRepository.UnitOfWorkInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
