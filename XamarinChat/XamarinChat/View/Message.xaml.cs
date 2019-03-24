using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinChat.Model;
using XamarinChat.ViewModel;

namespace XamarinChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Message : ContentPage
    {
        public Message(Chat chat)
        {
            InitializeComponent();

            BindingContext = new MessageViewModel(chat, SlMessageContainer);
        }
    }
}