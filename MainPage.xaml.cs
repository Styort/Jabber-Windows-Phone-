using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Jabber.Resources;
using Jabber.Model;
using System.Collections;
using System.Collections.ObjectModel;
using Jabber.Mediator;
using System.IO;
using System.IO.IsolatedStorage;
using Jabber.Memento;

namespace Jabber
{
    public partial class MainPage : PhoneApplicationPage
    {
        Caretaker c = new Caretaker();
        ContactListReadingVM clrVM = new ContactListReadingVM();
        ConcreteMediator mediator = new ConcreteMediator();
        
        // Конструктор
        public MainPage()
        {
            InitializeComponent();

            var incomingMessage = new IncomingMessage(mediator);
            var outgoingMessage = new OutgoingMessage(mediator);

            mediator.IncomingMessage = incomingMessage;
            mediator.OutgoingMessage = outgoingMessage;

            Observer.Observer.Instance.Log();
            DataContext = ConnectionModel.Instance;
            contactPivot.DataContext = clrVM;
            chatPivot.DataContext = MessageProcessingModel.Instance;
            ConnectionVM.Instance.SetupXmppConnectionHandler();
            clrVM.SetupXmppContactListHandler();
            incomingMessage.SetupXmppMessageHandler();
            historyPivot.DataContext = MessageProcessingModel.Instance;
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectionVM.Instance.Connect(); //коннектимся

            //проверяем есть ли у нас история сообщений.
            if((MessageProcessingModel.Instance.IncomingMessageHistory==null||MessageProcessingModel.Instance.IncomingMessageHistory=="")&
                (MessageProcessingModel.Instance.OutgoingMessageHistory==null||MessageProcessingModel.Instance.OutgoingMessageHistory==""))
            {
                MessageProcessingModel.Instance.TbOutgoing = "История сообщений пуста!";
            }
            else
            {
                MessageProcessingModel.Instance.TbIncoming = "Входящие сообщения:";
                MessageProcessingModel.Instance.TbOutgoing = "Исходящие сообщения:";
            }

        }
        private void Disconnect_Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectionModel.Instance.myXmppClient.Close(); //дисконнектимся
            ConnectionModel.Instance.AuthButtonIsEnabled = true; //кнопка авторизации становится активной при дисконнекте.
            clrVM.Contact.Clear(); //очищаем коллекцию контактов при дисконнекте
        }


        private void MessageSendButtonClick(object sender, RoutedEventArgs e)
        {
            //пустое сообщение не пройдет! 
            if(MessageProcessingModel.Instance.BodyMessage!=null & MessageProcessingModel.Instance.BodyMessage != "")
            {
                //проверяем, выбрали ли мы кому отправлять сообщение или нет.
                if(MessageProcessingModel.Instance.MessageTo!=null & MessageProcessingModel.Instance.MessageTo!= "")
                {
                    mediator.OutgoingMessage.SendMessageMediator();
                    scrollDialog.ScrollToVerticalOffset(double.MaxValue);
                }
                else
                {
                    MessageBox.Show("Выберите собеседника для общения!");
                }
            }
            else
            {
                MessageBox.Show("Введите текст сообщения!");
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock  f = sender as TextBlock;
            if (f != null)
            {
                if(f.Text != MessageProcessingModel.Instance.MessageTo)
                {
                    MessageProcessingModel.Instance.ChatMessage.Clear();
                }
                MessageProcessingModel.Instance.MessageTo = f.Text; //запоминаем ник того, кому будем отправлять сообщение.
                MyPivot.SelectedIndex = 2; //переходим на окно диалогов при клике на контакт
            }
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            c.LoadMementoHistory();
            if ((MessageProcessingModel.Instance.IncomingMessageHistory == null || MessageProcessingModel.Instance.IncomingMessageHistory == "") &
                (MessageProcessingModel.Instance.OutgoingMessageHistory == null || MessageProcessingModel.Instance.OutgoingMessageHistory == ""))
            {
                MessageProcessingModel.Instance.TbOutgoing = "История сообщений пуста!";
            }
            else
            {
                MessageProcessingModel.Instance.TbIncoming = "Входящие сообщения:";
                MessageProcessingModel.Instance.TbOutgoing = "Исходящие сообщения:";
            }
            MyPivot.SelectedIndex = 4;
        }
    }
}