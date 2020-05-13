using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace codenation
{
    public class Cryptography
    {
        private string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        public Cryptography()
        {
   
        }

        /// <summary>
        /// Criptografa um caracter de acordo com o número de casas informado
        /// </summary>
        /// <param name="_char"></param>
        /// <param name="numero_casas"></param>
        /// <returns></returns>
        public char EncryptChar(char _char, int numero_casas)
        {
            _char = char.ToLower(_char);
            int index = 0;
            if (Alphabet.Contains(_char))
            {
                int position = Alphabet.IndexOf(_char) + 1;
                if (position + numero_casas > 26)
                {
                    index = (position + numero_casas) - 26;
                }
                else
                {
                    index = position + numero_casas;
                }

                return Alphabet[index - 1];
            }
            else
            {
                return _char;
            }
        }

        /// <summary>
        /// Criptografa uma texto de acordo com o número de casas informado
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="numero_casas"></param>
        /// <returns></returns>
        public string EncryptText(string Text, int numero_casas)
        {
            string Result = null;
            foreach(char _char in Text)
            {
                Result += EncryptChar(_char, numero_casas);
            }
            return Result;
        }

        /// <summary>
        /// Descriptografa um caracter de acordo com o número de casas informado
        /// </summary>
        /// <param name="_char"></param>
        /// <param name="numero_casas"></param>
        /// <returns></returns>
        public char DecryptChar(char _char,int numero_casas)
        {
            _char = char.ToLower(_char);
            int index = 0;
            if (Alphabet.Contains(_char))
            {
                int position = Alphabet.IndexOf(_char) + 1;
                if (position - numero_casas < 1)
                {
                    index = (position - numero_casas) + 26;
                }
                else
                {
                    index = position - numero_casas;
                }

                return Alphabet[index - 1];
            }
            else
            {
                return _char;
            }
        }


        /// <summary>
        /// Descriptografa um texto de acordo com o número de casas informado
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="numero_casas"></param>
        /// <returns></returns>
        public string DecryptText(string Text, int numero_casas)
        {
            string Result = null;
            foreach (char _char in Text)
            {
                Result += DecryptChar(_char, numero_casas);
            }
            return Result;
        }


        /// <summary>
        /// Gera o hash SHA1 do texto informado
        /// </summary>
        /// <param name="Texto"></param>
        /// <returns></returns>
        public string Resumo(string Texto)
        {
            string StringResult = null;
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] Result = sha.ComputeHash(Encoding.UTF8.GetBytes(Texto));
            foreach(byte _byte in Result)
            {
                StringResult += _byte.ToString("X2");
            }
            return StringResult.ToLower();
        }
    }
}
