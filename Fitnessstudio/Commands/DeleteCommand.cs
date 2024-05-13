using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio.Commands
{
    public class DeleteCommand : CommandBase
    {
        public DeleteCommand()
        {
        }


        public override void Execute(object? parameter)
        {
            Debug.WriteLine("Delete Command");
        }


    }
}
