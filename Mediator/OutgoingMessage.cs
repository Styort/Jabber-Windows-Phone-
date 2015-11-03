using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix.Xmpp.Client;
using Matrix.Xmpp;
using System.Windows.Media;
using System.IO.IsolatedStorage;
using System.IO;
using Jabber.Memento;
namespace Jabber.Mediator
{
    class OutgoingMessage:Colleague
    {
        Caretaker caretaker = new Caretaker();
        MessageMediator message = new MessageMediator();
        public OutgoingMessage(Mediator mediator)
            :base (mediator)
        {
        }
        /// <summary>
        /// Отправка данных посреднику о исходящем сообщении.
        /// </summary>
        public void SendMessageMediator()
        {
            message.senderColorNickMediator = new SolidColorBrush(Colors.Blue);
            message.messageSenderMediator = ConnectionModel.Instance.Login + " (" + DateTime.Now.ToString("h:mm:ss dd/MM/yyyy") + ")";
            message.messageMediator = MessageProcessingModel.Instance.BodyMessage;
            mediator.Send(message, this);
        }
        /// <summary>
        /// Получены данные от посредника, отправляем их собеседнику и добавляем сообщение в диалоговое окно.
        /// </summary>
        /// <param name="message">данные о исходящем сообщении(Цвет ника,ник,сообщение)</param>
        public void SendMessage(MessageMediator message)
        {
            var msg = new Message
            {
                Type = MessageType.chat,
                To = MessageProcessingModel.Instance.MessageTo,
                Body = MessageProcessingModel.Instance.BodyMessage
            };
            ConnectionModel.Instance.myXmppClient.Send(msg);

            caretaker.SaveMementoOutgoingHistory();

            MessageProcessingModel.Instance.ChatMessage.Add(new Messages
            {
                SenderNickColor = message.senderColorNickMediator,
                MessageSender = message.messageSenderMediator,
                Message = message.messageMediator
            });

            MessageProcessingModel.Instance.BodyMessage = ""; //обнуляем текст введенного сообщения после отправки.
        }
    }
}
