using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHrambaTest.Models
{
	public class SearchFilterDto
	{
		public IEnumerable<Filter> filters { get; set; }
	}

	public class Filter
	{
		public string name { get; set; }
		public string value { get; set; }
		public string @operator { get; set; }
		public string value2 { get; set; }
	}
}