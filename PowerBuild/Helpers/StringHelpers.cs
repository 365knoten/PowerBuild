using PowerBuild;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Helpers
{
    class StringHelpers
    {


        static readonly Regex re = new Regex(@"<##([^{##>}]+)##>", RegexOptions.Compiled);


        /**
        * Extension method for Replacing String Placeholders with Variables from the given Context
        */
        public static string replaceTokens(string str, Variables variables)
        {
            if (variables == null)
                return str;

            return re.Replace(str, delegate (Match match)
            {
                string key = match.Groups[1].Value;
                string data = "";
                variables.TryGetValue(key, out data);
                return data;
            });

        }







        public static string RemoveWhiteSpaceFromStylesheets(string body)
        {
            body = Regex.Replace(body, @"[a-zA-Z]+#", "#");
            body = Regex.Replace(body, @"[\n\r]+\s*", string.Empty);
            body = Regex.Replace(body, @"\s+", " ");
            body = Regex.Replace(body, @"\s?([:,;{}])\s?", "$1");
            body = body.Replace(";}", "}");
            body = Regex.Replace(body, @"([\s:]0)(px|pt|%|em)", "$1");
            // Remove comments from CSS
            body = Regex.Replace(body, @"/\*[\d\D]*?\*/", string.Empty);
            return body;
        }

    }
}
