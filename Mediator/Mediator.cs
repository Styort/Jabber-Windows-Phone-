using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Jabber.Mediator
{
    abstract class Mediator
    {
        public abstract void Send(MessageMediator message, Colleague colleague);
    }
}
