using System.ComponentModel;
using Xamarin.Forms;
using XamarinChat.Model;
using XamarinChat.Service;
using XamarinChat.View;

namespace XamarinChat.ViewModel
{
    public class RegisterChatViewModel : INotifyPropertyChanged
    {
        private string _message;

        public RegisterChatViewModel()
        {
            CadastrarCommand = new Command(Cadastrar);
        }

        public string Name { get; set; }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged("message");
            }
        }

        public Command CadastrarCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Cadastrar()
        {
            var chat = new Chat {Name = Name};
            var ok = ChatService.InsertChat(chat);
            if (ok)
            {
                ((NavigationPage) Application.Current.MainPage).Navigation.PopAsync();
                var nav = (NavigationPage) Application.Current.MainPage;
                var chats = (Chats) nav.CurrentPage;
                var viewModel = (ChatsViewModel) chats.BindingContext;
                if (viewModel.UpdateChatCommand.CanExecute(null)) viewModel.UpdateChatCommand.Execute(null);
            }
            else
            {
                Message = "Registration Error!";
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}