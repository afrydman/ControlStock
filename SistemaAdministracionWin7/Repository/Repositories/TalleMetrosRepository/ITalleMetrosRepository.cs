using System;
using System.Collections.Generic;

namespace Repository.Repositories.TalleMetrosRepository
{
    public interface ITalleMetrosRepository
    {
        string GetTalle(Guid idProducto, Guid idColor, decimal metros);
        bool InsertTalleMetros(Guid idProducto, Guid idColor, decimal metros, string talle, int talledec);

        int ObtengoUltimoTalle(Guid idProducto, Guid idColor);

        decimal GetMetros(Guid idProducto, Guid idColor, int talle);

        Dictionary<string, decimal> ObtenerTodoByProductoColor(Guid idProducto, Guid idColor);

        Dictionary<decimal, string> ObtenerTodoByProducto(Guid idProducto);
    }
}
