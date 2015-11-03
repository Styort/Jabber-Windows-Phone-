using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix.Xmpp.Client;
using Matrix.Xmpp;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.IO.IsolatedStorage;
using Jabber.Memento;

namespace Jabber.Mediator
{
    class IncomingMessage:Colleague
    {
        MessageMediator message = new MessageMediator();
        Caretaker caretaker = new Caretaker();
        public IncomingMessage(Mediator mediator)
            :base (mediator)
        {
        }
        /// <summary>
        /// События для получения сообщения
        /// </summary>
        public void SetupXmppMessageHandler()
        {
            ConnectionModel.Instance.myXmppClient.OnMessage += MyXmppClient_OnMessage;
        }
        //Получение сообщения и отправка данных посреднику
        private void MyXmppClient_OnMessage(object sender, Matrix.Xmpp.Client.MessageEventArgs e)
        {
            if (e.Message.Type == Matrix.Xmpp.MessageType.error)
            {
                ConnectionModel.Instance.EventList.Add(new IncommingMessageError());
            }
            if (e.Message.Body != null)
            {
                message.senderColorNickMediator = new SolidColorBrush(Colors.Red);
                message.messageSenderMediator = e.Message.From.ToString().Substring(0, e.Message.From.ToString().LastIndexOf("/")) + " (" + DateTime.Now.ToString("h:mm:ss dd/MM/yyyy") + ")";
                message.messageMediator = e.Message.Body;
                mediator.Send(message, this);

                MessageProcessingModel.Instance.incomingBodyMessage = e.Message.Body;
                MessageProcessingModel.Instance.senderUserName = e.Message.From.ToString().Substring(0, e.Message.From.ToString().LastIndexOf("/"));

                caretaker.SaveMementoIncomingHistory();
            }
        }


        /// <summary>
        /// Получены данные от посредника и добавляем их в коллекцию. (Отображаем сообщение в диалоге).
        /// </summary>
        /// <param name="message">данные о входящем сообщении(Цвет ника,ник,сообщение)</param>
        public void AcceptIncomingMessage(MessageMediator message)
        {
            MessageProcessingModel.Instance.ChatMessage.Add(new Messages
                {
                    SenderNickColor = message.senderColorNickMediator,
                    MessageSender = message.messageSenderMediator,
                    Message = message.messageMediator
                });
        }
    }
}
