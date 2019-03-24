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
        private string _message;
        private string _name;
        private string _password;

        public SplashViewModel()
        {
            AccessCommand = new Command(Acessar);
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Message"));
            }
        }

        public Command AccessCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Acessar()
        {
            var user = new User {Name = Name, Password = Password};
            var usuarioLogado = ChatService.GetUser(user);
            if (usuarioLogado == null)
            {
                Message = "Wrong Password.";
            }
            else
            {
                UserPersistence.SetLoggedUser(usuarioLogado);
                Application.Current.MainPage = new NavigationPage(new Chats())
                    {BarBackgroundColor = Color.FromHex("#5ED055"), BarTextColor = Color.White};
            }
        }
    }
}