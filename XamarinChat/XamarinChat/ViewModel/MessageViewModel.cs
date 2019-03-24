using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinChat.Model;
using XamarinChat.Service;
using XamarinChat.Utils;

namespace XamarinChat.ViewModel
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        private bool _loading;
        private readonly Chat _chat;
        private List<Message> _messages;
        private bool _errorMessage;

        public bool ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        public bool Loading
        {
            get => _loading;
            set
            {
                _loading = value;
                OnPropertyChanged("Loading");
            }
        }

        public List<Message> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged("Messages");
            }
        }

        private string _txtMessage;
        public string TxtMessage
        {
            get => _txtMessage;
            set
            {
                _txtMessage = value;
                OnPropertyChanged("TxtMessage");
            }
        }

        public Command BtnSendCommand { get; set; }
        public Command UpdateMessageAsyncCommand { get; set; }

        public MessageViewModel(Chat chat)
        {
            _chat = chat;
            Task.Run(UpdateMessageAsync);
            BtnSendCommand = new Command(BtnSend);
            UpdateMessageAsyncCommand = new Command(UpdateMessage);

            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Task.Run(UpdateWithoutScreenLoading);
                return true;
            });
        }

        private void UpdateMessage()
        {
            Task.Run(UpdateMessageAsync);
        }

        private async Task UpdateMessageAsync()
        {
            try
            {
                ErrorMessage = false;
                Loading = true;
                Messages = await ChatService.GetChatMessages(_chat);
                Loading = false;
            }
            catch (Exception)
            {
                Loading = false;
                ErrorMessage = true;
            }
        }

        private async Task UpdateWithoutScreenLoading()
        {
            Messages = await ChatService.GetChatMessages(_chat);
        }

        private void BtnSend()
        {
            var msg = new Message()
            {
                IdUser = UserPersistence.GetLoggedUser().Id,
                Msg = TxtMessage,
                IdChat = _chat.Id
            };
            ChatService.InsertMessage(msg);
            Task.Run(UpdateMessageAsync);
            TxtMessage = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}