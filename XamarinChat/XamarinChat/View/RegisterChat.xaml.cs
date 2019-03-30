using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinChat.ViewModel;

namespace XamarinChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterChat : ContentPage
    {
        public RegisterChat()
        {
            InitializeComponent();

            BindingContext = ViewModelLocator.RegisterChatViewModel;
        }
    }
}