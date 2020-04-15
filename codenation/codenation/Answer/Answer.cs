using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;

namespace codenation
{
    class Answer
    {
        public int Numero_casas { get; set; }
        public string Token
        {
            get => ConfigurationManager.AppSettings["token"];
            set => ConfigurationManager.AppSettings["token"] = value;
        }
        public string Cifrado { get; set; }
        public string Decifrado { get; set; }
        public string Resumo_criptografico { get; set; }

        public static string UrlAPI;
        public static string FilePath;

        public Answer()
        {
            UrlAPI = ConfigurationManager.AppSettings["urlAPI"];
            FilePath = ConfigurationManager.AppSettings["filePath"];
        }

        public string SaveAnswer(JObject _Answer)
        {
            try
            {
                File.WriteAllText(FilePath, _Answer.ToString());
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public JObject SubmitAnswer()
        {
            MultipartContent Dados = new MultipartContent();
            return new JObject();
        }
    }
}
