namespace BusinessComponents
{
    public  class cliente //: IeditablePersona 
    {



        //public  List<personaData> GetAll(bool onlyEnable, bool Local = true)
        //{

        //    List<personaData> ps = clienteDataMapper.getAll(Local);
        //    ps.Sort(delegate(personaData x, personaData y)
        //    {
        //        return x.razonSocial.CompareTo(y.razonSocial);
        //    });

        //    if (onlyEnable)
        //    {
        //        ps = ps.FindAll(delegate(personaData p)
        //        {
        //            return p.enable;
        //        });
        //    }

        //    return ps;

        //}
        //public List<personaData> GetAll()
        //{
        //    return GetAll(true);
        //}

        //public bool Enable(Guid idp)
        //{
        //    personaData p = GetByID(idp);
        //    p.enable = true;
        //    return Update(p);
        //}

        //public bool Insert(personaData p, bool connlocal = true)
        //{
            
        //    if (p.ID == null || p.ID == new Guid())
        //    {
        //        p.ID = Guid.NewGuid();
        //    }

        //    return clienteDataMapper.insert(p, connlocal);
        //}

        //public  bool Delete(Guid idp)
        //{
        //    return clienteDataMapper.disable(idp);
        //}


        //public bool Update(personaData pLocal, bool connlocal = true)
        //{
            
        //    return clienteDataMapper.update(pLocal, connlocal);
            

        //}


        //public  personaData GetByID(Guid idp, bool connLocal = true)
        // {
        //    return clienteDataMapper.getByID(idp, connLocal);
        // }
    }
}
