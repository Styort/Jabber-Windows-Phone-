using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Matrix.Xmpp.Client;
using System.Windows.Media;

namespace Jabber.Model
{
    public class Contacts : ONPC
    {
        public Contacts()
        {
            ImageStatus = @"ContactListSettings/StatusImage/offline.png";
        }
        private string _name;
        public string Name { get { return _name; } set { _name = value; OnPC("Name"); } }
        private string _jid;
        public string Jid { get { return _jid; } set { _jid = value; OnPC("Jid"); } }
        private string _imageStatus; //Картинка статуса
        public string ImageStatus
        {
            get
            {
                return _imageStatus;
            }
            set
            {

                _imageStatus = value; OnPC("ImageStatus");
            }
        }
        private Brush _hoverOnContact;
        public Brush HoverOnContact { get { return _hoverOnContact; } set { _hoverOnContact = value; OnPC("HoverOnContact"); } }
        public StatusOnline StatusOnline { get; set; } 
        /// <summary>
        /// Определение статуса юзера.
        /// </summary>
        /// <param name="pres"></param>
        public void SetOnlineStateFromPresence(Presence pres)
        {
            if (pres.Type == Matrix.Xmpp.PresenceType.unavailable)
                StatusOnline = Model.StatusOnline.Offline;

            if (pres.Type == Matrix.Xmpp.PresenceType.available)
            {
                if (pres.Show == Matrix.Xmpp.Show.chat)
                    StatusOnline = Model.StatusOnline.Chat;
                else if (pres.Show == Matrix.Xmpp.Show.away)
                    StatusOnline = Model.StatusOnline.Away;
                else if (pres.Show == Matrix.Xmpp.Show.xa)
                    StatusOnline = Model.StatusOnline.ExtendedAway;
                else if (pres.Show == Matrix.Xmpp.Show.dnd)
                    StatusOnline = Model.StatusOnline.DoNotDisturb;
                else if (pres.Show == Matrix.Xmpp.Show.NONE)
                    StatusOnline = Model.StatusOnline.Online;
                else
                    StatusOnline = Model.StatusOnline.Online;
            }

            //определяем в зависимости от статуса, какую иконку статуса отображать
            switch (StatusOnline)
            {
                case StatusOnline.Online:
                    ImageStatus = @"ContactListSettings/StatusImage/online.png";
                    break;
                case StatusOnline.Offline:
                    ImageStatus = @"ContactListSettings/StatusImage/offline.png";
                    break;
                case StatusOnline.Away:
                    ImageStatus = @"ContactListSettings/StatusImage/away.png";
                    break;
                case StatusOnline.ExtendedAway:
                    ImageStatus = @"ContactListSettings/StatusImage/extendedAway.png";
                    break;
                case StatusOnline.DoNotDisturb:
                    ImageStatus = @"ContactListSettings/StatusImage/doNotDisturb.png";
                    break;
                case StatusOnline.Chat:
                    ImageStatus = @"ContactListSettings/StatusImage/chat.png";
                    break;
            }
        }
    }
}
