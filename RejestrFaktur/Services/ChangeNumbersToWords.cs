using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Services
{
    public class ChangeNumbersToWords
    {
        private readonly double _number;
        private readonly Dictionary<int, string> _liczbySlownie = new Dictionary<int, string>()
        {
            { 5_000_000, "milionów"}, // 22 - miliony; 23- miliony; 24 - miliony 
            { 1_000_000, "milion"},
            { 5_000, "tysięcy"},
            { 2_000, "tysiące"},
            { 1_000, "tysiąc"},
            { 200, "dwieście" }, // 3 + sta // 5 + set
            { 100, "sto" }, 
            { 10, "dziesięć"}, // 2 + dzieścia // 5 + dziesiąt
            { 9, "dziewięć"}, // 1 + naście
            { 8, "osiem"},
            { 7, "siedem"},
            { 6, "sześć"},
            { 5, "pięć"},
            { 4, "cztery"},
            { 3, "trzy"},
            { 2, "dwa"},
            { 1, "jeden"},
        };
        public ChangeNumbersToWords(double number)
        {
            _number = number;
        }

        public string ToWords()
        {
            string output = "";
            string sufix = "";

            int approximateFloorNumber = (int)Math.Floor(_number);

            if (_number - approximateFloorNumber > 0)
            {
                sufix = $"PLN {(int)(Math.Round(_number - approximateFloorNumber, 2) * 100)}/100";
            }

            while (approximateFloorNumber > 1)
            {
                approximateFloorNumber = approximateFloorNumber / 10;

            }

            output = $"{output} {sufix}";
            
            return output;
        }
    }
}
