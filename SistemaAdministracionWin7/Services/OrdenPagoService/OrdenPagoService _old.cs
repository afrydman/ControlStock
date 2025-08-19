namespace Services.OrdenPagoService
{
    public class OrdenPagoService_old
    {


        //protected readonly IOrdenPagoRepository _repo;
        //protected readonly IOrdenPagoDetalleRepository _repoDetalle;
        //     public OrdenPagoService_old(IOrdenPagoRepository opRepository,IOrdenPagoDetalleRepository repoD)
        //{
        //    _repo = opRepository;
        //         _repoDetalle = repoD;
        //}

        //     public OrdenPagoService_old(bool Local = true)
        // {
        //     _repo = new OrdenPagoRepository(Local);
        //         _repoDetalle = new OrdenPagoDetalleRepository(Local);
        // }

        //public string GetNextNumberAvailable(bool completo, Guid idLocal, int first)
        //{
        //    reciboData ultimo = GetLast(idLocal, first);

        //    if (ultimo.ID == Guid.Empty)
        //    {
        //        ultimo.Numero = 1;
        //        ultimo.Prefix = 1;
        //    }
        //    ultimo.Numero++;

        //    return completo ? ultimo.numeroCompleto : ultimo.Numero.ToString();
        //}

        //public bool Insert(reciboData theObject)
        //{
        //    var opts = new TransactionOptions
        //    {
        //        IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //    };
        //    Guid idPadre;
        //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //    {
        //        try
        //        {
        //            //conexion.closeConecction();//!todo! //Para que se vuelva a abrir dentro de la trans! //todo! que cierre la conexion correspondiente!

        //            idPadre = theObject.ID;
        //            if (idPadre == new Guid())
        //            {
        //                idPadre = Guid.NewGuid();
        //                theObject.ID = idPadre;
        //            }

        //            foreach (var det in theObject.Children)
        //            {
        //                det.FatherID = idPadre;
        //                _repoDetalle.InsertDetalle(det);
        //            }

        //            _repo.Insert(theObject);
        //            trans.Complete();
                    
        //        }
        //        catch (Exception e)
        //        {
        //            //todo! agregar log
        //            return false;
        //        }

        //    }

        //    return true;
        //}

        //public List<reciboData> GetbyCliente(Guid idCliente)
        //{
        //    List<reciboData> aux = GetAll(true).FindAll(r => r.tercero.ID == idCliente);


        //    return NormalizeList(aux);
        //}

        //public List<reciboData> NormalizeList(List<reciboData> aux, bool onlyEnable = true)
        //{
        //    if (onlyEnable)
        //        aux = aux.FindAll(n => n.Enable);

        //    aux.ForEach(n => n = getPropertiesInfo(n));

        //    aux.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

        //    return aux;

        //}

        //private reciboData getPropertiesInfo(reciboData re)
        //{
        //    re.Children = _repoDetalle.GetDetalles(re.ID);
        //    return re;

        //}

        //public List<reciboData> GetAll(bool conDetalles=true)
        //{
        //    List<reciboData> aux = _repo.GetAll();
      
        //    return NormalizeList(aux);
        //}

        //public reciboData GetLast(Guid idLocal, int first)
        //{
        //    return _repo.GetLast(idLocal, first);
        //}

        //public List<reciboData> GetOlderThan(bool conDetalles, Guid idLocal, int first)
        //{
        //    return NormalizeList(_repo.GetOlderThan(GetLast(idLocal, first).Date, idLocal, first));
        //}

        //public bool Anular(reciboData opago)
        //{
        //    return _repo.Disable(opago);
        //}

        //public reciboData GetById(Guid opago, bool completo)
        //{
        //    reciboData aux = _repo.GetByID(opago);
            

        //    return getPropertiesInfo(aux);
        //}

        //public reciboData GetOrdenQueEntregoCheque(Guid idcheque, bool getCompleto)
        //{
        //    reciboData aux = _repo.getOrdenByCheque(idcheque);
   
        
        //    return getPropertiesInfo(aux);
        //}

        //public List<reciboData> GetByFecha(DateTime fecha, bool completo, Guid idlocal)
        //{
        //    List<reciboData> aux = _repo.GetByRangoFecha(fecha.Date.AddDays(-1), fecha.Date.AddHours(23), idlocal);
     
           


        //    return NormalizeList(aux);
        //}
    }
}
