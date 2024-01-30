using EHrambaTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EHrambaTest.Controllers
{
    public class DocumentController
    {
        private PdfFileController _pdfFileController;
        private LoginController _loginController;


        public DocumentController(PdfFileController pdfFileController, LoginController loginController)
        {
            _pdfFileController = pdfFileController;
            _loginController = loginController;
        }

        #region Unos dokumenta
        public async Task<string> InsertPdfFileDraft(string userGUID, bool archiveCompressed, DraftDto draft)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"https://demo.zzi.si/EHrambaRest/EHramba/documents/draft?guid={userGUID}&accountName={await _loginController.GetAccountName(userGUID)}&compressed={archiveCompressed}", draft);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                string errorMessage = Task.FromResult(response.Content.ReadFromJsonAsync<ErrorDto>()).Result.Result.message;
                throw new Exception(errorMessage);
            }
        }
        #endregion

        #region Arhiviranje unesenog dokumenta iz predloska
        public async Task<string> ArchivePdfFileFromDraft(string userGUID, int ddcId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync($"https://demo.zzi.si/EHrambaRest/EHramba/documents/draft/fromDraft?guid={userGUID}&accountName={await _loginController.GetAccountName(userGUID)}&ddcID={ddcId}", null);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                string errorMessage = Task.FromResult(response.Content.ReadFromJsonAsync<ErrorDto>()).Result.Result.message;
                throw new Exception(errorMessage);
            }
        }
        #endregion

        #region Arhiviranje
        public async Task<string> ArchivePdfFile(string userGUID, bool archiveCompressed, ArchiveDataDto pdfDoc)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync($"https://demo.zzi.si/EHrambaRest/EHramba/documents?guid={userGUID}&accountName={await _loginController.GetAccountName(userGUID)}&compressed={archiveCompressed}", pdfDoc);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                string errorMessage = Task.FromResult(response.Content.ReadFromJsonAsync<ErrorDto>()).Result.Result.message;
                throw new Exception(errorMessage);
            }
        }
        #endregion

        #region Dohvat arhiviranog pdf-a
        public async Task GetArchivedPdfFile(string userGUID, bool archiveCompressed, int docId)
        {

            ArchiveDataDto document = await GetDocument(userGUID, archiveCompressed, docId);
            if (document != null)
            {
                SavePdfFile(document.metaData.fileName, _pdfFileController.FromBase64Pdf(document.data.rawData));
            }
        }

        public async Task<ArchiveDataDto> GetDocument(string userGUID, bool archiveCompressed, int docId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://demo.zzi.si/EHrambaRest/EHramba/documents/{docId}?guid={userGUID}&compressed={archiveCompressed}");

                if (response.IsSuccessStatusCode)
                {
                    var result = Task.FromResult(response.Content.ReadFromJsonAsync<ArchiveDataDto>()).Result;
                    return result.Result;
                }
                string errorMessage = Task.FromResult(response.Content.ReadFromJsonAsync<ErrorDto>()).Result.Result.message;
                throw new Exception(errorMessage);
            }
        }
        #endregion

        #region Brisanje dokumenta iz arhive
        public async Task DeletePdfFromArchive(string userGUID, string title, string desc, DeleteListDto deleteList)
        {
            int deleteListId = await CreateDeleteList(userGUID, title, desc);
            foreach(DeleteListItem deleteListItem in deleteList.deleteList)
            {
                await AddToDeleteList(userGUID, deleteListId, deleteListItem);
            }
            await RunDeletePdfFromArchive(userGUID, deleteListId);
        }

        #region Kreiranje liste za brisanje
        public async Task<int> CreateDeleteList(string userGUID, string title, string desc)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync($"https://demo.zzi.si/EHrambaRest/EHramba/documents/delete?guid={userGUID}&accountName={await _loginController.GetAccountName(userGUID)}&title={title}&description={desc}", null);

                if (response.IsSuccessStatusCode)
                {
                    return int.Parse(await response.Content.ReadAsStringAsync());
                }
                string errorMessage = Task.FromResult(response.Content.ReadFromJsonAsync<ErrorDto>()).Result.Result.message;
                throw new Exception(errorMessage);
            }
        }
        #endregion

        #region Dodavanje u listu brisanja
        public async Task AddToDeleteList(string userGUID, int deleteListId, DeleteListItem deleteListItem)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync($"https://demo.zzi.si/EHrambaRest/EHramba/documents/delete/{deleteListId}/add?guid={userGUID}&docId={deleteListItem.docId}&reason={deleteListItem.reason}", null);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = Task.FromResult(response.Content.ReadFromJsonAsync<ErrorDto>()).Result.Result.message;
                    throw new Exception(errorMessage);
                }
            }
        }
        #endregion

        #region Brisanje liste za brisanje
        public async Task RunDeletePdfFromArchive(string userGUID, int deleteListId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync($"https://demo.zzi.si/EHrambaRest/EHramba/documents/delete/{deleteListId}/run?guid={userGUID}", null);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = Task.FromResult(response.Content.ReadFromJsonAsync<ErrorDto>()).Result.Result.message;
                    throw new Exception(errorMessage);
                }
            }
        }
        #endregion

        #endregion

        #region Unos nove verzije dokumenta
            
        public async Task InsertNewPdfVersion(string userGUID, bool archiveCompressed, NewVersionDto newVersion)
        {
            using (HttpClient client = new HttpClient()) {

                HttpResponseMessage response = await client.PostAsJsonAsync($"https://demo.zzi.si/EHrambaRest/EHramba/documents/version?guid={userGUID}&accountName={await _loginController.GetAccountName(userGUID)}&compressed={archiveCompressed}", newVersion);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = Task.FromResult(response.Content.ReadFromJsonAsync<ErrorDto>()).Result.Result.message;
                    throw new Exception(errorMessage);
                }
            }
        }
        #endregion
        public string getDigestValue(byte[] pdfFile)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] computedPdf = sha1.ComputeHash(pdfFile);
            return Convert.ToBase64String(computedPdf);
        }
        public void SavePdfFile(string fileName, byte[] pdfFile)
        {
            File.WriteAllBytes($"C:\\\\Users\\\\spolj\\\\OneDrive\\\\Radna površina\\\\{fileName}", pdfFile);
        }

        

    }
}
