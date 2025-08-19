using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository;

namespace Services.AbstractService
{
    public abstract class ObjectService<T, X> : IGenericService<T> 
        where X : IGenericRepository<T>
        where T : GenericObject
    
    
    {
        internal X _repo;
        public ObjectService(X repo)
        {
            _repo = repo;
        }
        public ObjectService() { }


        public Type getDataType()
        {
            return default(T).GetType();
        }

        public virtual bool IsEmpty(T objectToCheck)
        {
            if (objectToCheck == null)
                return true;

            if (objectToCheck.ID == Guid.Empty)
                return true;

            return false;
        }

        public virtual List<T> GetAll(bool onlyEnable = true)
        {
            List<T> aux = null;

            try
            {
                aux = _repo.GetAll();
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);
                
            }
            return NormalizeList(aux, onlyEnable);
        }

        public virtual bool Enable(T theObject)
        {
            return Enable(theObject.ID);
        }

        public virtual bool Disable(T theObject)
        {
            return Disable(theObject.ID);
        }

        public virtual bool Disable(Guid id)
        {

            try
            {
                return _repo.Disable(id);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(id, "dynamic_Disable"), true, true);
            }
            return false;
        }
        
        public virtual bool Enable(Guid id)
        {


            try
            {
                return _repo.Enable(id);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(id, "dynamic_Enable"), true, true);
            }
            return false;
        }

        public virtual bool Insert(T theObject)
        {
            if (theObject.ID == null || theObject.ID == new Guid())
            {
                theObject.ID = Guid.NewGuid();
            }
            bool task = false;
            try
            {
                task = _repo.Insert(theObject);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(theObject, "dynamic_insert"), true, true);
            }
            return task;
        }

        public virtual bool Update(T theObject)
        {

            bool task = false;
            try
            {
                task = _repo.Update(theObject);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(theObject, "dynamic_Update"), true, true);
            }
            return task;
        }

        public virtual T GetByID(Guid idp)
        {

            try
            {
                return getPropertiesInfo(_repo.GetByID(idp));
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(idp, "dynamic_GetBYID"), true, true);
            }
            return null;

        }

        public virtual T GetLast(Guid idLocal, int first)
        {
            try
            {
                return getPropertiesInfo(_repo.GetLast(idLocal, first));
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(idLocal, "dynamic_GetLast") + ObjectDumperExtensions.DumpToString(first, "dynamic_GetLast"), true, true);
            }
            return null;
        }

        public abstract List<T> NormalizeList(List<T> list, bool onlyEnable = true);

        public abstract T getPropertiesInfo(T theObject);

        public Type GetTypeRepo()
        {
            return _repo.GetType();
        }
    }
}
