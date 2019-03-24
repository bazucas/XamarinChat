using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinChat.Model;
using XamarinChat.Service;
using XamarinChat.View;

namespace XamarinChat.ViewModel
{
    public class RegisterChatViewModel : INotifyPropertyChanged
    {
        private bool _errorMessage;
        private bool _loading;

        private string _message;

        public RegisterChatViewModel()
        {
            RegisterCommand = new Command(RegisterButton);
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

        public bool ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
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

        public Command RegisterCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RegisterButton()
        {
            var res = Task.Run(Register).GetAwaiter().GetResult();

            if (res != true) return;
            var currPage = (NavigationPage) Application.Current.MainPage;
            currPage.PopAsync();
        }

        private async Task<bool> Register()
        {
            Loading = true;
            ErrorMessage = false;
            try
            {
                var chat = new Chat {Name = Name};
                var ok = await ChatService.InsertChat(chat);
                if (ok)
                {
                    var currPage = (NavigationPage) Application.Current.MainPage;

                    var chats = (Chats) currPage.RootPage;
                    var vm = (ChatsViewModel) chats.BindingContext;
                    if (vm.UpdateChatCommand.CanExecute(null)) vm.UpdateChatCommand.Execute(null);
                    return true;
                }

                Message = "Registration Error!";
                Loading = false;
                return false;
            }
            catch (Exception e)
            {
                ErrorMessage = true;
                Message = e.Message;
                Loading = false;
                return false;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}