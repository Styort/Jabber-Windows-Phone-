using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.IO;

namespace Jabber.Memento
{
    class Caretaker
    {
        private Memento _memento = new Memento("", "", "", "");
        public Memento Memento { get { return _memento; } set { _memento = value; } }

        /// <summary>
        /// Загружаем историю с файла
        /// </summary>
        public void LoadMementoHistory()
        {
            try
            {
                //открываем файл исходящих сообщений в режиме чтения 
                IsolatedStorageFileStream FSom = MessageProcessingModel.Instance.ISF.OpenFile("OutgoingMessageHistory.txt", FileMode.Open, FileAccess.Read);

                using (StreamReader SR = new StreamReader(FSom)) // считывает исходящие сообщения
                {
                    Memento.OutgoingHistory = SR.ReadToEnd();
                }

            }
            catch(System.IO.IsolatedStorage.IsolatedStorageException)
            {
                Memento.OutgoingHistory = "Исходящих сообщений нет!";
            }

            //открываем файл входящих сообщений в режиме чтения
            try
            {

                IsolatedStorageFileStream FSim = MessageProcessingModel.Instance.ISF.OpenFile("IncomingMessageHistory.txt", FileMode.Open, FileAccess.Read);
                
                using (StreamReader SR = new StreamReader(FSim)) // считывает входящие сообщения
                {
                    Memento.IncomingHistory = SR.ReadToEnd();
                }

            }
            catch(System.IO.IsolatedStorage.IsolatedStorageException)
            {
                Memento.IncomingHistory = "Входящих сообщений нет!";
            }
            SetMemento();
        }

        /// <summary>
        /// Сохраняем входящие сообщения в историю
        /// </summary>
        public void SaveMementoIncomingHistory()
        {
            using (StreamWriter SW = new StreamWriter(new IsolatedStorageFileStream("IncomingMessageHistory.txt", FileMode.Create, FileAccess.Write, MessageProcessingModel.Instance.ISF)))
            {
                Memento.saveHistoryIncoming += "Сообщение получено от пользователя " + MessageProcessingModel.Instance.senderUserName + " в " + DateTime.Now.ToString("h:mm:ss dd/MM/yyyy") + "\n" + MessageProcessingModel.Instance.incomingBodyMessage + "\n";
                SW.WriteLine(Memento.saveHistoryIncoming);
                SW.Close();
            }
        }
        /// <summary>
        /// Сохраняем исходящие сообщения в историю
        /// </summary>
        public void SaveMementoOutgoingHistory()
        {
                //создаем файл исходящих сообщений
                using (StreamWriter SW = new StreamWriter(new IsolatedStorageFileStream("OutgoingMessageHistory.txt", FileMode.Create, FileAccess.Write, MessageProcessingModel.Instance.ISF)))
                {
                    //добавляем исходящие сообщение в историю
                    Memento.saveHistoryOutgoing += "Сообщение отправлено пользователю " + MessageProcessingModel.Instance.MessageTo + " в " + DateTime.Now.ToString("h:mm:ss dd/MM/yyyy") + "\n" + MessageProcessingModel.Instance.BodyMessage + "\n";
                    SW.WriteLine(Memento.saveHistoryOutgoing);
                    SW.Close();
                }
        }
        /// <summary>
        /// Передает значения из хранителя в основные переменные.
        /// </summary>
        public void SetMemento()
        {
            MessageProcessingModel.Instance.saveHistoryIncoming = _memento.saveHistoryIncoming;
            MessageProcessingModel.Instance.saveHistoryOutgoing = _memento.saveHistoryOutgoing;
            MessageProcessingModel.Instance.OutgoingMessageHistory = _memento.OutgoingHistory;
            MessageProcessingModel.Instance.IncomingMessageHistory = _memento.IncomingHistory;
        }
    }
}
