using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.IO;
using System.IO.IsolatedStorage;

namespace Jabber
{
    class MessageProcessingModel : Singleton<MessageProcessingModel>
    {
        MessageProcessingModel() { }
        private ObservableCollection<Messages> _chatMessage = new ObservableCollection<Messages>();
        public ObservableCollection<Messages> ChatMessage
        {
            get
            {
                return _chatMessage;
            }
            set
            {
                _chatMessage = value;
                OnPC("ChatMessage");
            }
        }
        private string _chatWith = "Диалоги"; //Название диалогового окна.
        public string ChatWith { get { return _chatWith; } set { _chatWith = value; OnPC("ChatWith"); } }
        private string _bodyMessage; //Введенный текст для сообщения.
        public string BodyMessage { get { return _bodyMessage; } set { _bodyMessage = value; OnPC("BodyMessage"); } }
        public string MessageTo; //кому отправлять

        private string _tbIncoming;

        public string TbIncoming
        {
            get { return _tbIncoming; }
            set { _tbIncoming = value; OnPC("TbIncoming"); }
        }
        private string _tbOutgoing;

        public string TbOutgoing
        {
            get { return _tbOutgoing; }
            set { _tbOutgoing = value; OnPC("TbOutgoing"); }
        }

        public string senderUserName;
        public string incomingBodyMessage;
        public IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication();
        private string _incomingMessageHistory;
        public string IncomingMessageHistory { get { return _incomingMessageHistory; } set { _incomingMessageHistory = value; OnPC("IncomingMessageHistory"); } }
        private string _outgoingMessageHistory;
        public string OutgoingMessageHistory { get { return _outgoingMessageHistory; } set { _outgoingMessageHistory = value; OnPC("OutgoingMessageHistory"); } }
        public string saveHistoryIncoming;
        public string saveHistoryOutgoing;
    }
}
