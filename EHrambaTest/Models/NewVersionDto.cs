using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHrambaTest.Models
{
    public class NewVersionDto
    {
        public int internalID { get; set; }
        public string? verDescription { get; set; }
        public ArchiveDataDto? archiveData { get; set; }
        
    }
}
