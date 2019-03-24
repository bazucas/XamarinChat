using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinChat.Model;

namespace XamarinChat.Utils
{
    public class UserPersistence
    {
        public static void SetLoggedUser(User usuario)
        {
            Application.Current.Properties["LOGIN"] = JsonConvert.SerializeObject(usuario);
        }

        public static User GetLoggedUser()
        {
            return Application.Current.Properties.ContainsKey("LOGIN")
                ? JsonConvert.DeserializeObject<User>((string) Application.Current.Properties["LOGIN"])
                : null;
        }
    }
}