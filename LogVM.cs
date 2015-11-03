using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix;

namespace Jabber
{
        class ConnectionError : LogModel
        {
            public ConnectionError()
            {
                LogMessage = "Ошибка подключения" + " " + DateTime.Now.ToString("h:mm:ss");
            }
        }
        class ConnectionSucces : LogModel
        {
            public ConnectionSucces()
            {
                LogMessage = "Авторизация прошла успешно." + " " + DateTime.Now.ToString("h:mm:ss");
            }
        }
        class ClientIsClose : LogModel
        {
            public ClientIsClose()
            {
                LogMessage = "Клиент закрыт." + " " + DateTime.Now.ToString("h:mm:ss");
            }
        }
        class RosterStart : LogModel
        {
            public RosterStart()
            {
                LogMessage = "Начало считывания контакт листа." + " " + DateTime.Now.ToString("h:mm:ss");
            }
        }
        class OnPresence : LogModel
        {
            public OnPresence(PresenceEventArgs e)
            {
                LogMessage = e.Presence.From + " => вошел в онлайн " + DateTime.Now.ToString("h:mm:ss");
            }
        }
        class RosterItem : LogModel
        {
            public RosterItem(Matrix.Xmpp.Roster.RosterEventArgs e)
            {
                LogMessage = e.RosterItem.Jid + e.RosterItem.Name + " => контакт считан " + DateTime.Now.ToString("h:mm:ss");
            }
        }

        class RosterEnd : LogModel
        {
            public RosterEnd()
            {
                LogMessage = "Контакт лист считан." + " " + DateTime.Now.ToString("h:mm:ss");
            }
        }
        class AuthError : LogModel
        {
            public AuthError()
            {
                LogMessage = "Ошибка авторизации!" + " " + DateTime.Now.ToString("h:mm:ss");
            }
        }
        class IncommingMessageError : LogModel
        {
            public IncommingMessageError()
            {
                LogMessage = "Ошибка получения сообщения!" + " " + DateTime.Now.ToString("h:mm:ss");
            }
        }
}
