using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1
{
    interface IState
    {
        IState getNextState(Char character);
    }
}
