using EHrambaTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EHrambaTest.Controllers
{
    public class LoginController
    {
        private string _userName = "";
        private string _password = "";

        public LoginController(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public async Task<string> loginUser()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync($"https://demo.zzi.si/EHrambaRest/EHramba/login?userName={_userName}&password={_password}", null);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                string errorMessage = Task.FromResult(response.Content.ReadFromJsonAsync<ErrorDto>()).Result.Result.message;
                throw new Exception(errorMessage);
            }
        }

        public async Task<string> GetAccountName(string userGuid)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://demo.zzi.si/EHrambaRest/EHramba/accounts?guid={userGuid}");

                if (response.IsSuccessStatusCode)
                {
                    return Task.FromResult(response.Content.ReadFromJsonAsync<AccountsDto>()).Result.Result.account.First().name;
                }
                string errorMessage = Task.FromResult(response.Content.ReadFromJsonAsync<ErrorDto>()).Result.Result.message;
                throw new Exception(errorMessage);
            }
        }
    }
}
