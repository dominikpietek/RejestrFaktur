using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Commands
{
    public class AddAnotherItemCommand : CommandBase
    {
        private readonly Action _AddItem;

        public AddAnotherItemCommand(Action AddItem)
        {
            _AddItem = AddItem;   
        }

        public override void Execute(object parameter)
        {
            _AddItem.Invoke();
        }
    }
}
