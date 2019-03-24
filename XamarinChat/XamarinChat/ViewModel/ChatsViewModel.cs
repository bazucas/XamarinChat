using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using XamarinChat.Model;
using XamarinChat.Service;
using XamarinChat.View;
using Message = XamarinChat.View.Message;

namespace XamarinChat.ViewModel
{
    public class ChatsViewModel : INotifyPropertyChanged
    {
        private List<Chat> _chats;
        private Chat _selectedItemChat;

        public ChatsViewModel()
        {
            Chats = ChatService.GetChats();
            AddChatCommand = new Command(AddChat);
            OrderChatCommand = new Command(OrderChat);
            UpdateChatCommand = new Command(UpdateChat);
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void GoToMessagePage(Chat chat)
        {
            if (chat == null) return;
            SelectedItemChat = null;
            ((NavigationPage) Application.Current.MainPage).Navigation.PushAsync(new Message(chat));
        }

        private static void AddChat()
        {
            ((NavigationPage) Application.Current.MainPage).Navigation.PushAsync(new RegisterChat());
        }

        private void OrderChat()
        {
            Chats = Chats.OrderBy(a => a.Name).ToList();
        }

        private void UpdateChat()
        {
            Chats = ChatService.GetChats();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}