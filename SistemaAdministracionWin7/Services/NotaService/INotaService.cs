using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.BusinessEntities;

namespace Services.NotaService
{
    public interface INotaService
    {
        bool Insert(NotaData theObject);

        bool Disable(NotaData theObject);

        NotaData GetLast(Guid idLocal, int first);

        List<NotaData> GetByTercero(Guid idCliente, bool completo, bool onlyEnable = true);
    }
}
