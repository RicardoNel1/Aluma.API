using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Helpers
{
    public class UtilityHelper
    {
        private Dictionary<string, int> BanksDictionary = new Dictionary<string, int>()
                {
                    {"FNB",250655},
                    {"NEDBANK",198765},
                    {"STANDARDBANK",051001},
                    {"CAPITEC",470010},
                    {"AFRICANBANK",430000},
                    {"ABSA",632005},
                    {"INVESTEC",580105},
                    {"BIDVESTBANK",462005},
                    {"SASFINBANK",683000},
                    {"DISCOVERYBANK",679000},
                    {"GRINDRODBANK",584000},
                    {"TYMEBANK",678910},
                };

        Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
                {
                    {"00","Unknown"},
                    {"01","Current / Cheque Account"},
                    {"02","Savings Account"},
                    {"03","Transmission Account"},
                    {"04","Bond Account"},
                    {"06","Subscription Share"}
                };

        public string Initials(string str)
        {
            var newStr = string.Empty;

            str.Split(' ').ToList().ForEach(e => newStr += e[0]);
            return newStr;
        }
    }
}