using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix.Xmpp.Client;
using System.Collections.ObjectModel;
using Jabber.Model;

namespace Jabber
{
    class ConnectionModel :Singleton<ConnectionModel>
    {

        private ConnectionModel() { }
        public XmppClient myXmppClient = new XmppClient();
        private string _password = "02241995";
        public string Password { get { return _password; } set { _password = value; OnPC("Password"); } }
        private string _login ="styort02";
        public string Login { get { return _login; } set { _login = value; OnPC("Login"); } }
        private string _server="jabber.mipt.ru";
        public string Server { get { return _server; } set { _server = value; OnPC("Server"); } }
        private bool _isEnabledAuthButton = true; //активность кнопки авторизация
        public bool AuthButtonIsEnabled { get { return _isEnabledAuthButton; } set { _isEnabledAuthButton = value; OnPC("AuthButtonIsEnabled"); } }
        private bool _isEnabledDisconnectButton = false; //активность кнопки разлогиниться
        public bool DissconnectButtonIsEnabled { get { return _isEnabledDisconnectButton; } set { _isEnabledDisconnectButton = value; OnPC("DissconnectButtonIsEnabled"); } }
        
        private ObservableCollection<LogModel> _eventList = new ObservableCollection<LogModel>(); //Логи
        public ObservableCollection<LogModel> EventList
        {
            get { return _eventList; }
            set { _eventList = value; }
        }
    }
}
