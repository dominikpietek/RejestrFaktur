using HtmlAgilityPack;
using RejestrFaktur.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // dodaj try catach
            var htmlWeb = new HtmlWeb();
            var webPage = htmlWeb.Load($"https://aleo.com/int/companies?phrase={_nip}");
            var companyData = webPage.QuerySelector("body").Descendants().First(f => f.HasClass("catalog-row-top")).Descendants();
            string companyName = companyData.First(c => c.HasClass("catalog-row-first-line__company-name")).InnerText.ToString();
            string companyAddress = companyData.First(c => c.HasClass("catalog-row-company-info__address")).InnerText.ToString();
            return new CompanyDataOwnerModel() { Name = companyName, Address = companyAddress};
        }
    }
}
