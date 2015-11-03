using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix;
using System.Windows.Controls;
using Jabber.Model;
using Matrix.Xmpp.Client;
using System.Collections.ObjectModel;
using System.Windows;

namespace Jabber
{
    class ConnectionVM:Singleton<ConnectionVM>
    {
        ConnectionVM() { }

        public delegate void AuthErrorEventHandler();
        public event AuthErrorEventHandler ClientAuthError;

        /// <summary>
        /// Лицензионный ключ к библиотеке(Пробный на 30 дней с 26.11.2014)
        /// </summary>
        void SetLicense()
        {
            const string LIC = @"eJxkkF1T6jAQhv8K4+2ZY1qqlTprRqRVO1YoVAR6F5sUA0lTkxQsv17OET9G
                                b3b23Wc/3llIeMEqwzqvUlTm4ogs/xpV2i3R7Fy8oyMMqVa0KWxMcWYbyhWg
                                rwqMG1JZblvsAvrMYdAYqyTTGIZEMhxtiGiIVRrQfw0DJWtStR+Aq6pzsALo
                                g0EkCRfYEMHM5Tdnx3Tf9M72zZ+HpjUllkWvNdcs3Ge467inbtdzAP1CEJuQ
                                SYWtbva7DgL+xR/zjtsNAP0AkPFlRWyjGb5ur7zH+UlAs3xEh706Xdhw8/Tc
                                Z7ts5UQBSpbes/cnsKflHXf89aRbL8wsLtaTEHkvbrlNHnzHn97Sko2bE0fl
                                uXB203EqN/PH0ZOoVtxvxXaji5v+YrcW7WpQZhPTmvX98Hp+dl8GXqz7M3cV
                                pbnPVPFQIJnM6p4fj73RKKUyC1yT617YJl7vAtCXb0CHd+M3AQ== ";
            Matrix.License.LicenseManager.SetLicense(LIC);
        }
        /// <summary>
        /// Подключаемся
        /// </summary>
        public void Connect()
        { 
            SetLicense();
            ConnectionModel.Instance.myXmppClient.Username = ConnectionModel.Instance.Login;
            ConnectionModel.Instance.myXmppClient.Password = ConnectionModel.Instance.Password;
            ConnectionModel.Instance.myXmppClient.XmppDomain = ConnectionModel.Instance.Server;

            if (ConnectionModel.Instance.myXmppClient.Username != null & ConnectionModel.Instance.myXmppClient.Username!= "" &
                ConnectionModel.Instance.myXmppClient.Password != null & ConnectionModel.Instance.myXmppClient.Password!= "" &
                ConnectionModel.Instance.myXmppClient.XmppDomain != null & ConnectionModel.Instance.myXmppClient.XmppDomain!= "")
            {
                ConnectionModel.Instance.myXmppClient.Show = Matrix.Xmpp.Show.chat;
                ConnectionModel.Instance.myXmppClient.StartTls = false;
                ConnectionModel.Instance.myXmppClient.Open();
            }
            else
            {
                MessageBox.Show("Поля для авторизации не должны быть путыми!");
            }
        }
        //Обьявление событий подключения.
        public void SetupXmppConnectionHandler()
        {
            ConnectionModel.Instance.myXmppClient.OnAuthError += MyXmppClient_OnAuthError;
            ConnectionModel.Instance.myXmppClient.OnLogin += myXmppClient_OnLogin;
            ConnectionModel.Instance.myXmppClient.OnError += myXmppClient_OnError;
            ConnectionModel.Instance.myXmppClient.OnClose += myXmppClient_OnClose;
        }

        //ошибка авторизации
        private void MyXmppClient_OnAuthError(object sender, Matrix.Xmpp.Sasl.SaslEventArgs e)
        {
            ConnectionModel.Instance.EventList.Add(new AuthError());
        }


        //Событие при успешной авторизации
        private void myXmppClient_OnLogin(object sender, Matrix.EventArgs e)
        {
            ConnectionModel.Instance.DissconnectButtonIsEnabled = true;
            ConnectionModel.Instance.AuthButtonIsEnabled = false;
            ConnectionModel.Instance.EventList.Add(new ConnectionSucces());
        }

        //Проверка на ошибку подключения.
        private void myXmppClient_OnError(object sender, ExceptionEventArgs e)
        {
            ConnectionModel.Instance.EventList.Add(new ConnectionError());
            Observer.Observer.Instance.Log();
            ClientAuthError();
        }

        //Событие при дисконнекте
        void myXmppClient_OnClose(object sender, Matrix.EventArgs e)
        {
            ConnectionModel.Instance.DissconnectButtonIsEnabled = false;
            ConnectionModel.Instance.EventList.Add(new ClientIsClose());
        }
    }
}
