using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jabber.Model;
using System.Collections.ObjectModel;
using System.Windows.Media;
namespace Jabber
{
    class ContactListReadingModel:ONPC
    {
        private ObservableCollection<Contacts> _contact = new ObservableCollection<Contacts>();
        public ObservableCollection<Contacts> Contact
        {
            get
            {
                return _contact;
            }
            set
            {
                _contact = value;
                OnPC("Contact");
            }
        }
        private string _contactListPivotHeader="Контакты";
        public string ContactListPivotHeader { get { return _contactListPivotHeader; } set { _contactListPivotHeader = value; OnPC("ContactListPivotHeader"); } }
       
    }
}
