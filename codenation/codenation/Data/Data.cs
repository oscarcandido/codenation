using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json.Linq;

namespace codenation
{
    public class Data
    {
        public int Numero_casas { get; set; }
        public string Token { 
            get=> ConfigurationManager.AppSettings["token"]; 
            set=> ConfigurationManager.AppSettings["token"] = value; 
        }
        public string Cifrado { get; set; }
        public string Decifrado { get; set; }
        public string Resumo_criptografico { get; set; }

        public static string UrlAPI;
        public static string FilePath;

        public Data()
        {
            UrlAPI = ConfigurationManager.AppSettings["urlAPI"];
            FilePath = ConfigurationManager.AppSettings["filePath"];
        }

        public string GetDataFromAPI()
        {
            HttpClient client = new HttpClient();
            var Result = client.GetAsync(UrlAPI + "generate-data?token=" + Token);
            Result.Wait();
            var Content = Result.Result.Content.ReadAsStringAsync();
            Content.Wait();
            return Content.Result.ToString();
        }

        public string SaveDataToFile()
        {
            try
            {
                
                string Data = GetDataFromAPI();
                File.WriteAllText(FilePath, Data);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public JObject ReadDataFromFile()
        {
            if (File.Exists(FilePath))
            {
                string Data = File.ReadAllText(FilePath);
                JObject JO = JObject.Parse(Data);
                return JO;
            }
            else
            {
                GetDataFromAPI();
                return ReadDataFromFile();
            }
        }
    }
}
