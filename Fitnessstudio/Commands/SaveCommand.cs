using Fitnessstudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio.Commands
{
    public class SaveCommand : CommandBase
    {
        private KundenVerwaltungViewModel viewModel;
        public SaveCommand(KundenVerwaltungViewModel viewModel) 
        { 
            this.viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            Debug.WriteLine("Execute Save");
        }
    }
}
