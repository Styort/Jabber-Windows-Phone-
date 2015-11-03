using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Jabber.Memento
{
    class Memento
    {
        public string IncomingHistory = MessageProcessingModel.Instance.IncomingMessageHistory;
        public string OutgoingHistory = MessageProcessingModel.Instance.OutgoingMessageHistory;
        public string saveHistoryIncoming;
        public string saveHistoryOutgoing;
        public Memento(string saveHistoryIncoming, string saveHistoryOutgoing, string IncomingHistory, string OutgoingHistory)
        {
            this.IncomingHistory = IncomingHistory;
            this.OutgoingHistory = OutgoingHistory;
            this.saveHistoryIncoming = saveHistoryIncoming;
            this.saveHistoryOutgoing = saveHistoryOutgoing;
        }

    }
}
