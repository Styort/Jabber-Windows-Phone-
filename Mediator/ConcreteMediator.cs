using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Jabber.Mediator
{
    class ConcreteMediator : Mediator
    {
        public IncomingMessage IncomingMessage { get; set; }
        public OutgoingMessage OutgoingMessage { get; set; }

        public override void Send(MessageMediator message, Colleague colleague)
        {
            if(colleague == IncomingMessage)
            {
                IncomingMessage.AcceptIncomingMessage(message);
            }
            else if(colleague == OutgoingMessage)
            {
                OutgoingMessage.SendMessage(message);
            }
        }
    }
}
