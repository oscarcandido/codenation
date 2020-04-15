using Newtonsoft.Json.Linq;
using System;


namespace codenation
{
    class Program
    {
        static void Main(string[] args)
        {
            Data Data = new Data();
            Console.WriteLine("RECUPERANDO DADOS DO SERVIDOR.....");
            JObject JsonData = Data.ReadDataFromFile();
            string Encriptado = JsonData["cifrado"].ToString();
            int NumCasas = Convert.ToInt32(JsonData["numero_casas"]);
            Console.WriteLine("DECIFRANDO FRASE .....");
            JsonData["decifrado"] = new Cryptography().DecryptText(Encriptado, NumCasas);
            JsonData["resumo_criptografico"] = new Cryptography().Resumo(JsonData["decifrado"].ToString());
            new Answer().SaveAnswer(JsonData);
            Console.WriteLine("ENVIANDO RESPOSTA .....");
            Console.WriteLine(JsonData.ToString());
            var Envio = new Answer().SubmitAnswerAsync();
            Envio.Wait();
            Console.WriteLine("RESULTADO DO ENVIO.....");
            Console.WriteLine(Envio.Result);
        }
    }
}
