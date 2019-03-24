using System;
using System.ComponentModel;
using Xamarin.Forms;
using XamarinChat.Model;
using XamarinChat.Service;
using XamarinChat.Utils;
using XamarinChat.View;

namespace XamarinChat.ViewModel
{
    public class SplashViewModel : INotifyPropertyChanged
    {
        private bool _errorMessage;
        private bool _loading;
        private string _message;

        private string _name;
        private string _password;

        public SplashViewModel()
        {
            AccessCommand = new Command(AccessApp);
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

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        public Command AccessCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void AccessApp()
        {
            try
            {
                ErrorMessage = false;
                Loading = true;
                var user = new User {Name = Name, Password = Password};
                var usuarioLogado = await ChatService.GetUser(user);
                if (usuarioLogado == null)
                {
                    Message = "Password incorreta.";
                    Loading = false;
                }
                else
                {
                    UserPersistence.SetLoggedUser(usuarioLogado);
                    Application.Current.MainPage = new NavigationPage(new Chats())
                        {BarBackgroundColor = Color.FromHex("#5ED055"), BarTextColor = Color.White};
                }
            }
            catch (Exception)
            {
                ErrorMessage = true;
            }
            finally
            {
                Loading = false;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}