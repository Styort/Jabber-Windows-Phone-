using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Jabber.Observer
{
    class Observer:Singleton<Observer>
    {
        Observer() { }
        public void Log()
        {
            ConnectionVM.Instance.ClientAuthError += Instance_ClientAuthError;
        }
        void Instance_ClientAuthError()
        {
            MessageBox.Show("Ошибка авторизации!");
        }
    }
}
