using EHrambaTest.Controllers;
using EHrambaTest.Models;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace EHrambaTest
{
    public class Program
    {
        private string pdfPath = "C:\\Users\\spolj\\OneDrive\\Radna površina\\DraftTest.pdf";
        private PdfFileController _pdfFileController;
        private LoginController _loginController;
        private DocumentController _documentController;
        private string userName = ""; // userName i password sam uklonio jer su to pravi autentifikacijski podaci firme
        private string password = "";
        private bool archiveCompressed = false;
        private string userGUID = null;
        private byte[] pdfFile = null;

        public Program()
        {
            _pdfFileController = new PdfFileController(pdfPath);
            _loginController = new LoginController(userName, password);
            _documentController = new DocumentController(_pdfFileController, _loginController);
        }
        public async Task Main()
        {
            await LoginUser();

            pdfFile = _pdfFileController.LoadPdf();

            //await ArchivePdfFile();

            //await _documentController.GetArchivedPdfFile(userGUID, archiveCompressed, 6174242);

           /*await _documentController.DeletePdfFromArchive(userGUID, "test delete", "test delete desc", new DeleteListDto()
            {
                deleteList = new List<DeleteListItem>()
                {
                    new DeleteListItem()
                    {
                        docId = 6174242,
                        reason = "test"
                    }
                }
            });*/

            int ddcId = await InsertPdfDraft();
            await ArchivePdfFileFromDraft(ddcId);

            //await InsertNewVersionOfPdfFile();
        }

        public async Task LoginUser()
        {
            string loginGUID = await _loginController.loginUser();
            if (loginGUID != null)
            {
                userGUID = loginGUID;
                Console.WriteLine(userGUID);
            }
        }

        public async Task<int> InsertPdfDraft()
        {
            DraftDto draft = new DraftDto()
            {
                draftID = 0,
                archiveData = new ArchiveDataDto()
                {
                    dataIdentification = new DataIdentification()
                    {
                        externalID = "Test draft 25.10."
                    },
                    data = new Data()
                    {
                        rawData = _pdfFileController.GetBase64Pdf(pdfFile),
                        messageImprint = new List<MessageImprint>()
                        {
                            new MessageImprint()
                            {
                                digestAlgorithm = "SHA1",
                                digestValue = _documentController.getDigestValue(pdfFile)
                            }
                        }
                    },
                    metaData = new MetaData()
                    {
                        title = "Test draft 25.10.",
                        creationTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                        creationLocation = "Dubrovnik",
                        fileName = _pdfFileController.GetFileName(),
                        classificationName = "00299",
                        mimetype = "application/pdf",
                    }
                }
            };

            string response = await _documentController.InsertPdfFileDraft(userGUID, archiveCompressed, draft);
            if (response != null)
            {
                Console.WriteLine(response);
            }
            return int.Parse(response);
        }

        public async Task ArchivePdfFileFromDraft(int ddcId)
        {
            string response = await _documentController.ArchivePdfFileFromDraft(userGUID, ddcId);
            if (response != null)
            {
                Console.WriteLine(response);
            }
        }

        public async Task ArchivePdfFile()
        {
            Int64 timeNow = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            ArchiveDataDto pdfDoc = new ArchiveDataDto()
            {
                dataIdentification = new DataIdentification()
                {
                    externalID = "Test Arhiviranja 5"
                },
                data = new Data()
                {
                    rawData = _pdfFileController.GetBase64Pdf(pdfFile),
                    messageImprint = new List<MessageImprint>()
                        {
                            new MessageImprint()
                            {
                                digestAlgorithm = "SHA1",
                                digestValue = _documentController.getDigestValue(pdfFile)
                            }
                        }
                },
                metaData = new MetaData()
                {
                    title = "Test arhiviranja 5",
                    creationTime = timeNow,
                    creationLocation = "Dubrovnik",
                    fileName = _pdfFileController.GetFileName(),
                    classificationName = "00299",
                    mimetype = "application/pdf",
                },
                documents = new Documents()
                {
                    document = new List<Document>()
                        {
                            new Document()
                            {
                                id = 0,
                                externalid = "Test Arhiviranja 5",
                                title = _pdfFileController.GetTitleFromFileName(".pdf"),
                                filename = _pdfFileController.GetFileName(),
                                mimetype = "application/pdf",
                                classificationname = "00299",
                                docid = 0,
                                verdescription = "Test 3. verzije"
                            }
                        }
                },
                documentversions = new DocumentVersions()
                {
                    documentversion = new List<DocumentVersion>() {
                        new DocumentVersion()
                        {
                            version = 3,
                            description = "Test 3. verzije",
                            time = timeNow
                        }
                    }
                },
                documentversion = new DocumentVersion()
                {
                    version = 3,
                    description = "Test 3. verzije",
                    time = timeNow
                }
            };

            string response = await _documentController.ArchivePdfFile(userGUID, archiveCompressed, pdfDoc);
            if (response != null)
            {
                Console.WriteLine(response);
            }
        }

        public async Task InsertNewVersionOfPdfFile()
        {
            Int64 timeNow = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            NewVersionDto pdfDoc = new NewVersionDto()
            {
                internalID = 6174243,
                verDescription = "Test verzije 3",
                archiveData = new ArchiveDataDto()
                {
                    dataIdentification = new DataIdentification()
                    {
                        externalID = "Test Arhiviranja 5"
                    },
                    data = new Data()
                    {
                        rawData = _pdfFileController.GetBase64Pdf(pdfFile),
                        messageImprint = new List<MessageImprint>()
                        {
                            new MessageImprint()
                            {
                                digestAlgorithm = "SHA1",
                                digestValue = _documentController.getDigestValue(pdfFile)
                            }
                        }
                    },
                    metaData = new MetaData()
                    {
                        title = "Test arhiviranja 5",
                        creationTime = timeNow,
                        creationLocation = "Dubrovnik",
                        fileName = _pdfFileController.GetFileName(),
                        classificationName = "00299",
                        mimetype = "application/pdf",
                    },
                    documents = new Documents()
                    {
                        document = new List<Document>()
                        {
                            new Document()
                            {
                                externalid = "Test Arhiviranja 5",
                                title = _pdfFileController.GetTitleFromFileName(".pdf"),
                                filename = _pdfFileController.GetFileName(),
                                mimetype = "application/pdf",
                                classificationname = "00299",
                                docid = 0,
                                verdescription = "Test 1. verzije"
                            },
                            new Document()
                            {
                                externalid = "Test Arhiviranja 5",
                                title = _pdfFileController.GetTitleFromFileName(".pdf"),
                                filename = _pdfFileController.GetFileName(),
                                mimetype = "application/pdf",
                                classificationname = "00299",
                                docid = 0,
                                verdescription = "Test 1. verzije 2"
                            }
                        }
                    },
                    documentversions = new DocumentVersions()
                    {
                        documentversion = new List<DocumentVersion>() {
                        new DocumentVersion()
                        {
                            version = 3,
                            description = "Test 3. verzije",
                            time = timeNow
                        },
                        new DocumentVersion()
                        {
                            version = 4,
                            description = "Test 4. verzije",
                            time = timeNow
                        }
                    }
                    },
                    documentversion = new DocumentVersion()
                    {
                        version = 0,
                        description = "Test 4. verzije",
                        time = timeNow
                    }
                }
            };

            await _documentController.InsertNewPdfVersion(userGUID, archiveCompressed, pdfDoc);
        }
    }
}
