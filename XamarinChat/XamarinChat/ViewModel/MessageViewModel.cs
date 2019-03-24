using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using XamarinChat.Model;
using XamarinChat.Service;
using XamarinChat.Utils;

namespace XamarinChat.ViewModel
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        private readonly Chat _chat;
        private readonly StackLayout _sl;
        private List<Message> _messages;

        private string _txtMessage;

        public MessageViewModel(Chat chat, StackLayout slMensagemContainer)
        {
            _chat = chat;
            _sl = slMensagemContainer;
            RefreshMessages();
            BtnSendCommand = new Command(BtnSend);
            UpdateMessagesCommand = new Command(RefreshMessages);

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                RefreshMessages();
                return true;
            });
        }

        public List<Message> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged("Messages");
                if (value != null) ShowOnScreen();
            }
        }

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
        public Command UpdateMessagesCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RefreshMessages()
        {
            Messages = ChatService.GetMessagesFromChat(_chat);
        }

        private void BtnSend()
        {
            var msg = new Message
            {
                IdUser = UserPersistence.GetLoggedUser().Id,
                Msg = TxtMessage,
                IdChat = _chat.Id
            };
            ChatService.InsertMessage(msg);
            RefreshMessages();
            TxtMessage = string.Empty;
        }

        private void ShowOnScreen()
        {
            var usuario = UserPersistence.GetLoggedUser();
            _sl.Children.Clear();
            foreach (var msg in Messages)
                _sl.Children.Add(msg.User.Id == usuario.Id
                    ? CreateOwnMessage(msg)
                    : CreateMessagesOtherUsers(msg));
        }

        private static Xamarin.Forms.View CreateOwnMessage(Message mensagem)
        {
            var layout = new StackLayout
                {Padding = 5, BackgroundColor = Color.FromHex("#5ED055"), HorizontalOptions = LayoutOptions.End};
            var label = new Label {TextColor = Color.White, Text = mensagem.Msg};

            layout.Children.Add(label);
            return layout;
        }

        private static Xamarin.Forms.View CreateMessagesOtherUsers(Message mensagem)
        {
            var labelNome = new Label {Text = mensagem.User.Name, FontSize = 10, TextColor = Color.FromHex("#5ED055")};
            var labelMensagem = new Label {Text = mensagem.Msg, TextColor = Color.FromHex("#5ED055")};

            var sl = new StackLayout();
            sl.Children.Add(labelNome);
            sl.Children.Add(labelMensagem);
            var frame = new Frame
            {
                BorderColor = Color.FromHex("#5ED055"),
                CornerRadius = 0,
                HorizontalOptions = LayoutOptions.Start,
                Content = sl
            };

            return frame;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}