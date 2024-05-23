using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RejestrFaktur.Services
{
    public static class ConvertPdf
    {
        public static void FromByteArray(byte[] byteArray, string fileName)
        {
            File.WriteAllBytes($@"{ConfigurationManager.AppSettings["AbsolutePath"]}Invoices\{fileName}.pdf", byteArray);
        }
    }
}
