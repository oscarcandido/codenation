using Newtonsoft.Json.Linq;
using System;


namespace codenation
{
    class Program
    {
        static void Main(string[] args)
        {
            Data Data = new Data();
            JObject JsonData = Data.ReadDataFromFile();
            string Encriptado = JsonData["cifrado"].ToString();
            int NumCasas = Convert.ToInt32(JsonData["numero_casas"]);
            JsonData["decifrado"] = new Cryptography().DecryptText(Encriptado, NumCasas);
            JsonData["resumo_criptografico"] = new Cryptography().Resumo(JsonData["decifrado"].ToString());
            new Answer().SaveAnswer(JsonData);
            Console.WriteLine(JsonData.ToString());
        }
    }
}
