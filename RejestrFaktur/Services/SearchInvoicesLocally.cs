using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Services
{
    public static class SearchInvoicesLocally
    {
        private const string REALTIVEPATH = @"C:\Programs\RejestrFaktur\RejestrFaktur\Invoices\";

        public static IEnumerable<string> GetInvoicesName()
        {
            foreach (var file in Directory.GetFiles(REALTIVEPATH))
            {
                yield return Path.GetFileNameWithoutExtension(file).ToString();
            }
        }
    }
}
