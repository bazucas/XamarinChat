using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinChat.Model;
using XamarinChat.Service;
using Message = XamarinChat.View.Message;

namespace XamarinChat.ViewModel
{
    public class ChatsViewModel : INotifyPropertyChanged
    {
        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set
            {
                _loading = value;
                OnPropertyChanged("Loading");
            }
        }

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

        private Chat _selectedItemChat;
        public Chat SelectedItemChat
        {
            get => _selectedItemChat;
            set
            {
                _selectedItemChat = value;
                OnPropertyChanged("SelectedItemChat");
                GoToMessagePage(value);
            }
        }

        private List<Chat> _chats;
        public List<Chat> Chats
        {
            get => _chats;
            set
            {
                _chats = value;
                OnPropertyChanged("Chats");
            }
        }

        public Command AddChatCommand { get; set; }
        public Command OrderChatCommand { get; set; }
        public Command UpdateChatCommand { get; set; }

        public ChatsViewModel()
        {
            Task.Run(LoadChats);
            AddChatCommand = new Command(AddChat);
            OrderChatCommand = new Command(OrderChat);
            UpdateChatCommand = new Command(UpdateChat);
        }

        private void GoToMessagePage(Chat chat)
        {
            if (chat == null) return;
            SelectedItemChat = null;
            ((NavigationPage)Application.Current.MainPage).Navigation.PushAsync(new Message(chat));
        }

        private async Task LoadChats()
        {
            try
            {
                ErrorMessage = false;
                Loading = true;
                Chats = await ChatService.GetChats();
                Loading = false;
            }
            catch (Exception)
            {
                Loading = false;
                ErrorMessage = true;
            }
        }

        private static void AddChat()
        {
            ((NavigationPage)Application.Current.MainPage).Navigation.PushAsync(new View.RegisterChat());
        }

        private void OrderChat()
        {
            Chats = Chats.OrderBy(a => a.Name).ToList();
        }

        private void UpdateChat()
        {
            Task.Run(LoadChats);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}