using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

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
        /// <summary>
        /// Salva o arquivo com o objeto da resposta no caminho informado na configuração
        /// </summary>
        /// <param name="_Answer"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Envia a resposta salva em arquivo para o servidor
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<JObject> SubmitAnswerAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var form = new MultipartFormDataContent())
                {
                    using (var fs = File.OpenRead(FilePath))
                    {
                        using (var streamContent = new StreamContent(fs))
                        {
                            using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                            {
                                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                                // "file" parameter name should be the same as the server side input parameter name
                                form.Add(fileContent, "answer", Path.GetFileName(FilePath));
                                HttpResponseMessage response = await httpClient.PostAsync(UrlAPI+ "submit-solution?token=" +Token, form);
                                var content = response.Content.ReadAsStringAsync();
                                content.Wait();
                                JObject Result = JObject.Parse(content.Result);
                                return Result ;
                            }
                        }
                    }
                }
            }
        }

    }
}
