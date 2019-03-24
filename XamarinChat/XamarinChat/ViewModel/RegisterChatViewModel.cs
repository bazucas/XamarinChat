using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinChat.Model;
using XamarinChat.Service;

namespace XamarinChat.ViewModel
{
    public class RegisterChatViewModel : INotifyPropertyChanged
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

        public string Name { get; set; }

        private string _message;
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

        public RegisterChatViewModel()
        {
            RegisterCommand = new Command(RegisterButton);
        }
        private void RegisterButton()
        {
            var res = Task.Run(Register).GetAwaiter().GetResult();

            if (res != true) return;
            var currPage = ((NavigationPage)Application.Current.MainPage);
            currPage.PopAsync();

        }
        private async Task<bool> Register()
        {
            Loading = true;
            ErrorMessage = false;
            try
            {
                var chat = new Chat { Name = Name };
                var ok = await ChatService.InsertChat(chat);
                if (ok)
                {
                    var currPage = ((NavigationPage)Application.Current.MainPage);

                    var chats = (View.Chats)currPage.RootPage;
                    var vm = (ChatsViewModel)chats.BindingContext;
                    if (vm.UpdateChatCommand.CanExecute(null))
                    {
                        vm.UpdateChatCommand.Execute(null);
                    }
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}