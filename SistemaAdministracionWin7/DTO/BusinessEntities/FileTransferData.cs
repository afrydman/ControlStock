using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.BusinessEntities
{
    public class FileTransferData : GenericObject
    {
        public DateTime Date { get; set; }
        public string Error { get; set; }
        public bool Completed { get; set; }
        public string ToS3CompleteDir { get; set; }
        public string LocalFileName { get; set; }
        public string FromLocalID { get; set; }
        public string ToLocalID{ get; set; }
        public Guid remitoID { get; set; }

    }
}
