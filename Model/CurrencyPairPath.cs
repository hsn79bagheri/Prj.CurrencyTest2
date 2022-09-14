using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyApp.Model
{
    public class CurrencyPairPath
    {
        public  IList<Tuple<string, string, double>> lstCurrencyPair { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
