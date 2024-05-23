using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Services
{
    public static class SearchInvoicesLocally
    {
        public static IEnumerable<string> GetInvoicesName()
        {
            foreach (var file in Directory.GetFiles(@$"{ConfigurationManager.AppSettings["AbsolutePath"]}\Invoices\"))
            {
                yield return Path.GetFileNameWithoutExtension(file).ToString();
            }
        }
    }
}
