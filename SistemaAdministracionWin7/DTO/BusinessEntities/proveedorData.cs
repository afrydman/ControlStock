using System;

namespace DTO.BusinessEntities
{
    public class ProveedorData : PersonaData
    {
        public ProveedorData(Guid id):base(id)
        {
            
        }
        public ProveedorData():base()
        {
            
        }
    }
}
