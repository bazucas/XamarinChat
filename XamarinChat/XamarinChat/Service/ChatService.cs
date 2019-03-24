using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using XamarinChat.Model;

namespace XamarinChat.Service
{
    public class ChatService
    {
        private const string BaseAddress = "http://ws.spacedu.com.br/xf2018/rest/api";

        public static User GetUser(User user)
        {
            const string url = BaseAddress + "/User";
            /*
             * QueryString: ?q=Footbal&tipo=imagem
             * StringContent param = new StringContent(string.Format("?nome={0}&password={1}", User.nome, User.password));
             */
            var param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("nome", user.Name),
                new KeyValuePair<string,string>("password", user.Password)
            });
            var req = new HttpClient();
            var resp = req.PostAsync(url, param).GetAwaiter().GetResult();
            if (resp.StatusCode != HttpStatusCode.OK) return null;
            var content = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<User>(content);
        }

        public static List<Chat> GetChats()
        {
            const string url = BaseAddress + "/chats";
            var req = new HttpClient();
            var resp = req.GetAsync(url).GetAwaiter().GetResult();
            if (resp.StatusCode != HttpStatusCode.OK) return null;
            var content = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (content.Length <= 2) return null;
            var list = JsonConvert.DeserializeObject<List<Chat>>(content);
            return list;
        }
        public static bool InsertChat(Chat chat)
        {
            const string url = BaseAddress + "/chat";
            var param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("nome", chat.Name)
            });
            var req = new HttpClient();
            var resp = req.PostAsync(url, param).GetAwaiter().GetResult();

            return resp.StatusCode == HttpStatusCode.OK;
        }

        public static bool RenameChat(Chat chat)
        {
            var url = BaseAddress + "/chat/" + chat.Id;
            var param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("nome", chat.Name)
            });
            var req = new HttpClient();
            var resp = req.PutAsync(url, param).GetAwaiter().GetResult();
            return resp.StatusCode == HttpStatusCode.OK;
        }

        public static bool DeleteChat(Chat chat)
        {
            var url = BaseAddress + "/chat/delete/" + chat.Id;
            var req = new HttpClient();
            var resp = req.DeleteAsync(url).GetAwaiter().GetResult();
            return resp.StatusCode == HttpStatusCode.OK;
        }

        public static List<Message> GetMessagesFromChat(Chat chat)
        {
            var url = BaseAddress + "/chat/" + chat.Id + "/msg";
            var req = new HttpClient();
            var resp = req.GetAsync(url).GetAwaiter().GetResult();
            if (resp.StatusCode != HttpStatusCode.OK) return null;
            var content = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (content.Length <= 2) return null;
            var list = JsonConvert.DeserializeObject<List<Message>>(content);
            return list;

        }

        public static bool InsertMessage(Message mensagem)
        {
            var url = BaseAddress + "/chat/" + mensagem.IdChat + "/msg";
            var param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("mensagem", mensagem.Msg),
                new KeyValuePair<string,string>("id_User", mensagem.IdUser.ToString())
            });
            var req = new HttpClient();
            var resp = req.PostAsync(url, param).GetAwaiter().GetResult();
            return resp.StatusCode == HttpStatusCode.OK;
        }

        public static bool DeleteMessage(Message mensagem)
        {
            var url = BaseAddress + "/chat/" + mensagem.IdChat + "/delete/" + mensagem.Id;
            var req = new HttpClient();
            var resp = req.DeleteAsync(url).GetAwaiter().GetResult();
            return resp.StatusCode == HttpStatusCode.OK;
        }
    }
}
