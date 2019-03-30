using CommonServiceLocator;

namespace XamarinChat.ViewModel
{
    public static class ViewModelLocator
    {
        public static MessageViewModel MessageViewModel => ServiceLocator.Current.GetInstance<MessageViewModel>();
        public static ChatsViewModel ChatsViewModel => ServiceLocator.Current.GetInstance<ChatsViewModel>();
        public static RegisterChatViewModel RegisterChatViewModel => ServiceLocator.Current.GetInstance<RegisterChatViewModel>();
        public static SplashViewModel SplashViewModel => ServiceLocator.Current.GetInstance<SplashViewModel>();
    }
}
