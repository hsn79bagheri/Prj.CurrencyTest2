using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyApp.interfaces
{
    interface ICurrencyConverter
    {
        void ClearConfiguration();
        void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> ConversionRates);
        double Convert(string fromCurrency, string toCurrency, double amount);
    }
}
