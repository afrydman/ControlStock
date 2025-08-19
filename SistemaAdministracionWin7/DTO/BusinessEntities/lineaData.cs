using System;

namespace DTO.BusinessEntities
{
    public class LineaData : GenericObject
    {

        public LineaData()
        {
            Description = "";
        }

        public LineaData(Guid _id, string _d) { 
            ID=_id;
            Description = _d;
        }

   
       
    }
}
