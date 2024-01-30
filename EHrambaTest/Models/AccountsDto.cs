using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHrambaTest.Models
{
    public class AccountsDto
    {
        public IEnumerable<Account> account { get; set; }
    
    }
    public class Account
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string frmname { get; set; }
        public string frmtin { get; set; }

    }
}
