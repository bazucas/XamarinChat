using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinChat.ViewModel;

namespace XamarinChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Splash : ContentPage
    {
        public Splash()
        {
            InitializeComponent();

            BindingContext = new SplashViewModel();
        }
    }
}