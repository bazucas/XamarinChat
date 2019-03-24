using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinChat.ViewModel;

namespace XamarinChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chats : ContentPage
    {
        public Chats()
        {
            InitializeComponent();

            BindingContext = new ChatsViewModel();
        }
    }
}