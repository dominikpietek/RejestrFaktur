using RejestrFaktur.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Commands
{
    public class AddAndSaveCommand : CommandBase
    {
        private readonly Func<Task> _Save;
        private readonly Action _CloseWindow;

        public AddAndSaveCommand(Func<Task> Save, Action CloseWindow)
        {
            _Save = Save;
            _CloseWindow = CloseWindow;
        }

        public async override void Execute(object parameter)
        {
            await _Save.Invoke();
            _CloseWindow.Invoke();   
        }
    }
}
