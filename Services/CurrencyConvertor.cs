using CurrencyApp.interfaces;
using CurrencyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyApp.Services
{
    public class CurrencyConvertor : ICurrencyConverter

    {
        IList<Tuple<string, string, double>> lstCurrencyPair;
        public CurrencyConvertor()
        {
            lstCurrencyPair = new List<Tuple<string, string, double>>();

        }
        public void ClearConfiguration()
        {
            lstCurrencyPair.Clear();
        }

        public double Convert(string fromCurrency, string toCurrency, double amount)
        {
            try
            {
                double returnvalue = 0;
                IList<Tuple<string, string, double>> PathScaned = new List<Tuple<string, string, double>>();
                var IsItFinded = false;
                find(fromCurrency, toCurrency,ref PathScaned, ref IsItFinded);
                if(IsItFinded)
                {
                    foreach(var aCurencyPair in PathScaned)
                    {
                        if (returnvalue == 0)
                            returnvalue = amount * aCurencyPair.Item3;
                        else
                            returnvalue= returnvalue * aCurencyPair.Item3;
                    }
                }
                return returnvalue;
            }
            catch
            {
                return -1;

            }
         
        }
        public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> ConversionRates)
        {
            //برای اینکه گراف زوج ارزها کامل باشد ضریب تبدیل هردو ارز نسبت به هم ثبت شده است
            // (USD => EUR) 0.86 => (EUR=>USD) 1/0.86=1.162

            foreach (var t in ConversionRates)
            {
                lstCurrencyPair.Add(t);
                //ثبت ضریب تبدیل برعکس 
                lstCurrencyPair.Add(new Tuple<string, string, double>(t.Item2,t.Item1,1/t.Item3));
                
            }
            
        }
        //تابع به صورت بازگشتی تعریف شده است و شرط پایان جستجو 
        //IsItFinded==true
        private void find(string fromCurrency, string toCurrency,ref IList<Tuple<string, string, double>> PathScaned ,ref bool IsItFinded)
        {                       
            if (!IsItFinded) {

                if (lstCurrencyPair.Any(cp => cp.Item1 == fromCurrency && cp.Item2 == toCurrency))
                {
                    PathScaned.Add(lstCurrencyPair.Single(cp => cp.Item1 == fromCurrency && cp.Item2 == toCurrency));
                    IsItFinded = true;
                }
                else
                {
                    //lstCurrency
                    //جهت بررسی نودهای اسکن شده تا رسیدن به نود مورد نظر ایجاد شده است 
                    //تا از ایجاد لوپ در اسکن نودها جلوگیری شود
                    List<string> lstCurrency = PathScaned.Select(ps => ps.Item1).Union(PathScaned.Select(ps => ps.Item2)).ToList();
                    var lstRemainCurrencyPair = lstCurrencyPair.Where(cp => cp.Item1 == fromCurrency && !lstCurrency.Contains(cp.Item2));                    foreach (var aCurrencyPair in lstRemainCurrencyPair)
                    {
                        find(aCurrencyPair.Item2,toCurrency,ref PathScaned, ref IsItFinded);
                    }
                }
            }
            
        }

        
    }
}
