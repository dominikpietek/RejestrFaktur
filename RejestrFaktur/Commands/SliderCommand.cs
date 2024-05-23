using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Commands
{
    public class SliderCommand : CommandBase
    {
        private readonly Action<int> _ChangeTaxRateValue;
        private readonly int _number;

        public SliderCommand(Action<int> ChangeTaxRateValue, int number)
        {
            _ChangeTaxRateValue = ChangeTaxRateValue;
            _number = number;
        }

        public override void Execute(object parameter)
        {
            _ChangeTaxRateValue.Invoke(_number);
        }
    }
}
