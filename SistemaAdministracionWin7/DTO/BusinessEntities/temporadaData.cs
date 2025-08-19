using System;

namespace DTO.BusinessEntities
{
    public class TemporadaData : GenericObject
    {
        
        public TemporadaData()
        {
            Description = "";
        }

        public TemporadaData(Guid _id, string _desc)
        {
            ID = _id;
            Description = _desc;
        }

    }
}
