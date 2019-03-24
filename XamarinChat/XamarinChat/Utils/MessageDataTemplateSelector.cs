using Xamarin.Forms;
using XamarinChat.Model;

namespace XamarinChat.Utils
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MyMessagesTemplate { get; set; }
        public DataTemplate OtherPeopleMessagesTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var user = UserPersistence.GetLoggedUser();

            return ((Message) item).IdUser == user.Id ? MyMessagesTemplate : OtherPeopleMessagesTemplate;
        }
    }
}