using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHrambaTest.Models
{
    public class ArchiveDataDto
    {
        public DataIdentification? dataIdentification { get; set; }
        public Data? data { get; set; }
        public MetaData? metaData { get; set; }
        public DocumentInfo? documentinfo { get; set; }
        public Documents? documents { get; set; }
        public Folders? folders { get; set; }
        public DocumentVersions? documentversions { get; set; }
        public DocumentVersion? documentversion { get; set; }
        public DocumentFunctions? documentfunctions { get; set; }
        public ZziInformation? zziinformation { get; set; }

    }


    public class DataIdentification
    {
        public string externalID { get; set; }

    }

    public class Data
    {
        public string? rawData { get; set; }
        public IEnumerable<MessageImprint>? messageImprint { get; set; }
        public string? dataReference { get; set; }

    }
    public class MessageImprint
    {
        public string digestAlgorithm { get; set; }
        public string digestValue { get; set; }

    }
    public class MetaData
    {
        public string? title { get; set; }
        public Int64? creationTime { get; set; }
        public string? creationLocation { get; set; }
        public string? fileName { get; set; }
        public string? organization { get; set; }
        public MetaDataParameters? parameters { get; set; }
        public string? classificationName { get; set; }
        public string? type { get; set; }
        public string? groupName { get; set; }
        public string? mimetype { get; set; }

    }

    public class MetaDataParameters
    {
        public IEnumerable<MetaDataParams>? param { get; set; }

    }
    public class MetaDataParams
    {
        public string? parameterName { get; set; }
        public string? parameterValue { get; set; }
        public string? staticParam { get; set; }

    }

    public class DocumentInfo
    {
        public int? insertdate { get; set; }
        public int status { get; set; }
        public int? size { get; set; }

    }

    public class Documents
    {
        public IEnumerable<Document> document { get; set; }
    }

    public class Document
    {
        public int? id { get; set; }
        public string? externalid { get; set; }
        public string? title { get; set; }
        public Int64? creationtime { get; set; }
        public string? creationlocation { get; set; }
        public string? filename { get; set; }
        public string? mimetype { get; set; }
        public string? organization { get; set; }
        public int? insertdate { get; set; }
        public string? classificationname { get; set; }
        public string? accountname { get; set; }
        public string? usrid { get; set; }
        public string? usrusername { get; set; }
        public string? status { get; set; }
        public string? usriduporabnik { get; set; }
        public int? size { get; set; }
        public string? type { get; set; }
        public int? accid { get; set; }
        public string? verdescription { get; set; }
        public int? docid { get; set; }
        public string? datareference { get; set; }

    }

    public class Folders
    {
        public IEnumerable<Folder> folders { get; set; }
    }

    public class Folder
    {
        public int? id { get; set; }
        public string? externalid { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? fstatus { get; set; }
        public string? gruname { get; set; }
        public string? organization { get; set; }
        public int? createdate { get; set; }
        public int? primary { get; set; }
        public FolderParams? folderparams { get; set; }
    }
    public class FolderParams
    {
        public IEnumerable<FolderParam> folderparam { get; set; }
    }
    public class FolderParam
    {
        public int id { get; set; }
        public int? fldid { get; set; }
        public string? name { get; set; }
        public string? value { get; set; }
        public string? docdata { get; set; }
        public int accid { get; set; }
        public string? @static { get; set; }
    }
    public class DocumentVersions
    {
        public IEnumerable<DocumentVersion> documentversion { get; set; }
    }
    public class DocumentVersion
    {
        public int version { get; set; }
        public string? description { get; set; }
        public Int64? time { get; set; }
    }

    public class DocumentFunctions
    {
        public int? insert { get; set; }
        public int? edit { get; set; }
        public int? delete { get; set; }
        public int? view { get; set; }
        public int? verify { get; set; }
        public int? status { get; set; }
        public int? export { get; set; }
    }
    public class ZziInformation
    {
        public int? internalID { get; set; }
        public string? account { get; set; }
        public int? userID { get; set; }
        public string? folderName { get; set; }
        public int? accID { get; set; }
        public int? claID { get; set; }
        public string? polName { get; set; }
        public string? polEkeeper { get; set; }
        public string? polArchiveDoc { get; set; }
        public int? gruID { get; set; }
        public string? version { get; set; }
    }
}
