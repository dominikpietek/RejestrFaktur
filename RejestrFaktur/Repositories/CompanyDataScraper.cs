using HtmlAgilityPack;
using Microsoft.IdentityModel.Tokens;
using RejestrFaktur.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RejestrFaktur.Repositories
{
    public class CompanyDataScraper
    {
        private readonly string _nip;
        public CompanyDataScraper(string nip)
        {
            _nip = nip;
        }
        public CompanyDataOwnerModel Search()
        {
            var htmlWeb = new HtmlWeb();
            string companyName = "";
            string companyAddress = "";
            try
            {
                var webPage = htmlWeb.Load($"https://aleo.com/int/companies?phrase={_nip}");
                var companyData = webPage.QuerySelector("body").Descendants().First(f => f.HasClass("catalog-row-top")).Descendants();
                companyName = companyData.First(c => c.HasClass("catalog-row-first-line__company-name")).InnerText.ToString();
                companyAddress = companyData.First(c => c.HasClass("catalog-row-company-info__address")).InnerText.ToString();
            }
            catch(Exception e)
            {
                MessageBox.Show(
                    messageBoxText:"Nie istniejący numer NIP!", 
                    caption:"", 
                    button: MessageBoxButton.OK, 
                    icon: MessageBoxImage.Error);
            }
            return new CompanyDataOwnerModel() { Name = companyName, Address = companyAddress};
        }
    }
}
