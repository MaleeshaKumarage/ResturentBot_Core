using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace SinhalaTokenizationLibrary
{
    public class TokenizationLibrary
    {
        List<string> tokenList;
        public TokenizationLibrary()
        {
            tokenList = new List<string>();
        }

        public List<string> Tokenize(object utterence)
        {
            if (utterence.GetType().ToString() == "System.String")
            {
                tokenList = utterence.ToString().Split(null).ToList();
            }
            else
            {
                foreach (var item in (List<string>)utterence)
                {
                    foreach (var word in item.Split(null).ToList())
                    {
                        tokenList.Add(word);
                    }
                }

            }

            RemoveSimilarWords();
            return tokenList;
        }



        private void RemoveSimilarWords()
        {

            tokenList = tokenList.Distinct().ToList();

        }

        public string GetNumericalValues(string uterence)
        {
            //List<string> normalWordList = new List<string>() { "එක", "දෙක", "තුන", "හතර", "පහ", "හය", "හත", "අට", "නමය", "දහය", "එකොලහ", "දොලහ", "දහතුන", "දාහතර", "පහලොව", "දහසය", "දාහත", "දහඅට", "දහනවය" };
            uterence = uterence.Replace("එක", "1");
            uterence = uterence.Replace("දෙක", "2");
            uterence = uterence.Replace("තුන", "3");
            uterence = uterence.Replace("හතර", "4");
            uterence = uterence.Replace("පහ", "5");
            uterence = uterence.Replace("හය", "6");
            uterence = uterence.Replace("හත", "7");
            uterence = uterence.Replace("අට", "8");
            uterence = uterence.Replace("නමය", "9");
            uterence = uterence.Replace("දහය", "10");
            uterence = uterence.Replace("එකොලහ", "11");
            uterence = uterence.Replace("දොලහ", "12");
            uterence = uterence.Replace("දහතුන", "13");
            uterence = uterence.Replace("දාහතර", "14");
            uterence = uterence.Replace("පහලොව", "15");
            uterence = uterence.Replace("දහසය", "16");
            uterence = uterence.Replace("දාහත", "17");
            uterence = uterence.Replace("දහඅට", "18");
            uterence = uterence.Replace("දහනවය", "19");
            uterence = uterence.Replace("විසි", "2");
            uterence = uterence.Replace("විසි", "2");
            uterence = uterence.Replace("තිස්", "3");
            uterence = uterence.Replace("හතලිස්", "4");
            uterence = uterence.Replace("පනස්", "5");
            uterence = uterence.Replace(" හැට", "6");

            return Regex.Replace(uterence, "[^0-9]+", string.Empty);

        }





    }
}
