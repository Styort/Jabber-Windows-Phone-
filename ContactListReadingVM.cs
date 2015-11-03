using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using Jabber.Model;
using System.Windows.Media;

namespace Jabber
{
    class ContactListReadingVM:ContactListReadingModel
    {
        //События считывания контакт листа
        public void SetupXmppContactListHandler()
        {
            ConnectionModel.Instance.myXmppClient.OnPresence += MyXmppClient_OnPresence;
            ConnectionModel.Instance.myXmppClient.OnRosterItem += MyXmppClient_OnRosterItem;
            ConnectionModel.Instance.myXmppClient.OnRosterStart += MyXmppClient_OnRosterStart;
            ConnectionModel.Instance.myXmppClient.OnRosterEnd += MyXmppClient_OnRosterEnd;
        }

        //конец считывание контакт листа
        private void MyXmppClient_OnRosterEnd(object sender, Matrix.EventArgs e)
        {
            ConnectionModel.Instance.EventList.Add(new RosterEnd());
        }
        //начало считывания контакт листа
        private void MyXmppClient_OnRosterStart(object sender, Matrix.EventArgs e)
        {
            ConnectionModel.Instance.EventList.Add(new RosterStart());
        }
        //вход в онлайн юзера
        private void MyXmppClient_OnPresence(object sender, PresenceEventArgs e)
        {
            ConnectionModel.Instance.EventList.Add(new OnPresence(e));
            string jid = e.Presence.From.Bare;
            var contact = Contact.FirstOrDefault(c => c.Jid == jid);
            if (contact != null)
                contact.SetOnlineStateFromPresence(e.Presence); //проверяем статус человека вошедшего в сеть.
        }
        //считывание конкретного юзера в лист
        private void MyXmppClient_OnRosterItem(object sender, Matrix.Xmpp.Roster.RosterEventArgs e)
        {
            ConnectionModel.Instance.EventList.Add(new RosterItem(e));
            if (e.RosterItem.Subscription != Matrix.Xmpp.Roster.Subscription.remove) //считываем контакт лист
                Contact.Add(
                    new Contacts
                    {
                        HoverOnContact = new SolidColorBrush(Colors.Black),
                        Name = e.RosterItem.Name ?? e.RosterItem.Jid,
                        Jid = e.RosterItem.Jid
                    });
            else
            {
                var contact = Contact.FirstOrDefault(c => c.Jid == e.RosterItem.Jid);
                if (contact != null)
                    Contact.Remove(contact);
            }
        }
    }
}
