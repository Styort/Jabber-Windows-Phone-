using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Jabber
{
    class Messages : ONPC
    {
        private Brush _senderNickColor; //Изменение цвета ника в зависимости от того отправлено сообщение или получено.
        public Brush SenderNickColor { get { return _senderNickColor; } set { _senderNickColor = value; OnPC("SenderNickColor"); } }
        private string _messageSender; //Ник отправителя/получателя
        public string MessageSender { get { return _messageSender; } set { _messageSender = value; OnPC("MessageSender"); } }
        private string _message; //Полученное/отправленное сообщение.
        public string Message { get { return _message; } set { _message = value; OnPC("Message"); } }
    }
}
