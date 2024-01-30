using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHrambaTest.Models
{
    public class DraftDto
    {
        public int? draftID { get; set; }
        public string? userCreated { get; set; }
        public string? userCreatedDesc { get; set; }
        public string? userFor { get; set; }
        public string? userForDesc { get; set; }
        public string? groupFor { get; set; }
        public string? groupForDesc { get; set; }
        public string? folderName { get; set; }
        public string? folderDesc { get; set; }
        public int? docSize { get; set; }
        public int? userIdCreated { get; set; }
        public int? userIdFor { get; set; }
        public int? groupIdFor { get; set; }
        public int? fldId { get; set; }
        public string? fsId { get; set; }
        public string? accName { get; set; }
        public int? creationTime { get; set; }
        public string? externalId { get; set; }
        public string? title { get; set; }
        public string? filename { get; set; }
        public string? mimetype { get; set; }
        public string? type { get; set; }
        public int? size { get; set; }
        public ArchiveDataDto archiveData { get; set; }


    }

}
