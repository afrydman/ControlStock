using System;

namespace DTO.BusinessEntities
{
    public class ColorData : GenericObject
    {

        public ColorData()
        {
        }

        public ColorData (Guid _id)
        {
            ID = _id;
        }


        public string Codigo { get; set; }
    

    }
}
