using System;
using System.Collections.Generic;
using System.Data;
using DTO.BusinessEntities;

namespace Repository.Repositories.StockRepository
{
   public interface IStockRepository
    {

       StockData GetStock(Guid idProducto, Guid idColor, int talle, Guid idLocal);

        bool UpdateStock(Guid idProducto, Guid idColor, int talle, Guid idLocal, decimal newStock);

        List<StockData> GetAllbyLocalAndProducto(Guid idlocal, Guid idProducto);

        List<DTO.BusinessEntities.StockData> GetAllbyProducto(Guid idproducto);

        bool InsertStock(Guid idProducto, Guid idColor, int talle, Guid idLocal, decimal newStock);


        List<detalleStockData> GetDetalleStock(string codigo, Guid idlocal);

        List<StockData> GetAll(Guid idlocal);//todo! ver esto si no es lo menos performante del mundo. spoiler alert=> si!

        IEnumerable<StockData> OperatorGiveMeData(IDbConnection con, string sql, object parameters);

    }
}
