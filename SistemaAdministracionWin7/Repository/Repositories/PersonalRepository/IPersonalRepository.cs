using System;
using System.Collections.Generic;
using DTO;

namespace Repository.Repositories.PersonalRepository
{
    public interface IPersonalRepository : IGenericRepository<PersonalData>
    {
        List<PersonalData> GetPersonalbyLocal(Guid idLocal);
    }
}
