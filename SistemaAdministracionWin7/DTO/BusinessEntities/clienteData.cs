using System;

namespace DTO.BusinessEntities
{
    public class ClienteData:PersonaData
    {
        public ClienteData() : base()
        {
            
        }
        public ClienteData(Guid id) : base(id) { }
    }
}
