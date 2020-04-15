using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace codenation
{
    public class Cryptography
    {
        private Dictionary<char, int> Alphabet  = new Dictionary<char, int>()
        {
            { 'a', 1},
            { 'b', 2},
            { 'c', 3},
            { 'd', 4},
            { 'e', 5},
            { 'f', 6},
            { 'g', 7},
            { 'h', 8},
            { 'i', 9},
            { 'j', 10},
            { 'k', 11},
            { 'l', 12},
            { 'm', 13},
            { 'n', 14},
            { 'o', 15},
            { 'p', 16},
            { 'q', 17},
            { 'r', 18},
            { 's', 19},
            { 't', 20},
            { 'u', 21},
            { 'v', 22},
            { 'w', 23},
            { 'x', 24},
            { 'y', 25},
            { 'z', 26}
        };

        public Cryptography()
        {
        }

        public char EncryptChar(char _char, int numero_casas)
        {
            _char = char.ToLower(_char);
            int index = 0;
            if (Alphabet.ContainsKey(_char))
            {
                int position = Alphabet[_char];
                if (position + numero_casas > 26)
                {
                    index = (position + numero_casas) - 26;
                }
                else
                {
                    index = position + numero_casas;
                }

                return Alphabet.ElementAt(index - 1).Key;
            }
            else
            {
                return _char;
            }
        }

        public string EncryptText(string Text, int numero_casas)
        {
            string Result = null;
            foreach(char _char in Text)
            {
                Result += EncryptChar(_char, numero_casas);
            }
            return Result;
        }

        public char DecryptChar(char _char,int numero_casas)
        {
            _char = char.ToLower(_char);
            int index = 0;
            if (Alphabet.ContainsKey(_char))
            {
                int position = Alphabet[_char];
                if (position - numero_casas < 1)
                {
                    index = (position - numero_casas) + 26;
                }
                else
                {
                    index = position - numero_casas;
                }

                return Alphabet.ElementAt(index - 1).Key;
            }
            else
            {
                return _char;
            }
        }

        public string DecryptText(string Text, int numero_casas)
        {
            string Result = null;
            foreach (char _char in Text)
            {
                Result += DecryptChar(_char, numero_casas);
            }
            return Result;
        }

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
