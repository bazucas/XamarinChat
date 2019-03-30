using CommonServiceLocator;
using Unity;
using Unity.ServiceLocation;
using Xamarin.Forms;
using XamarinChat.View;

namespace XamarinChat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var uContainer = new UnityContainer();
            uContainer.RegisterType<Splash>();
            uContainer.RegisterType<RegisterChat>();
            uContainer.RegisterType<Message>();
            uContainer.RegisterType<Chats>();
            
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(uContainer)); 

            MainPage = new Splash();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}