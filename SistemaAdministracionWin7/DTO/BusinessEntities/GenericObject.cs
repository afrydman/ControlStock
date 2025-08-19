using System;

namespace DTO.BusinessEntities
{
    public abstract class GenericObject
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public bool Enable { get; set; }

        public GenericObject()
        {
            ID = Guid.Empty;
            Enable = false;//para probar tests...
        }
        public GenericObject(Guid _id,string _des, bool _en)
        {
            ID = _id;
            Description = _des;
            Enable = _en;
        }

        
    }

   
}
