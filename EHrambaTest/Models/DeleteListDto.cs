using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHrambaTest.Models
{
    public class DeleteListDto
    {
        public IEnumerable<DeleteListItem> deleteList { get; set; }
    }

    public class DeleteListItem
    {
        public int docId { get; set; }
        public string reason { get; set; }
    } 
}
