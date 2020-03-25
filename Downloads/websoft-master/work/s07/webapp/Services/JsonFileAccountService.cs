using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using webapp.Models;

namespace webapp.Services
{
    public class JsonFileAccountService
    {
        public JsonFileAccountService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "../", "data", "account.json"); }
        }

        public IEnumerable<Account> GetAccounts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Account[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public Account GetAccount()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                var accounts = JsonSerializer.Deserialize<Account[]>(jsonFileReader.ReadToEnd());

                foreach(var account in accounts){
                    if(account.Number == 222222){
                        return account;
                        }
                        else if(account.Number == 111111){
                            return account;
                        }
                        else if(account.Number == 333333){
                            return account;
                        }
                        else if(account.Number == 42){
                            return account;
                        }
                }

                return null;
            }
        }
    }
}
