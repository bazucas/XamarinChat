using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinChat.Model;
using XamarinChat.Model.Dto;
using XamarinChat.Utils;

namespace XamarinChat.Service
{
    public class ChatService
    {
        private const string BaseAddress = "http://ws.spacedu.com.br/xf2018/rest/api";

        public static async Task<User> GetUser(User user)
        {
            const string url = BaseAddress + "/usuario";
            /*
             * QueryString: ?q=Footbal&tipo=imagem
             * StringContent param = new StringContent(string.Format("?name={0}&password={1}", user.name, user.password));
             */
            var param = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("nome", user.Name),
                new KeyValuePair<string, string>("password", user.Password)
            });
            var req = new HttpClient();
            var resp = await req.PostAsync(url, param);
            if (resp.StatusCode != HttpStatusCode.OK) return null;
            var content = await resp.Content.ReadAsStringAsync();
            var userDto = JsonConvert.DeserializeObject<UserDto>(content);

            var mapper = Mappings.Config.CreateMapper();
            return mapper.Map<UserDto, User>(userDto);
        }

        public static async Task<List<Chat>> GetChats()
        {
            const string url = BaseAddress + "/chats";
            var req = new HttpClient();
            var resp = await req.GetAsync(url);
            if (resp.StatusCode != HttpStatusCode.OK) throw new Exception("HTTP response code: " + resp.StatusCode);
            var content = await resp.Content.ReadAsStringAsync();
            if (content.Length <= 2) return null;
            var list = JsonConvert.DeserializeObject<List<ChatDto>>(content);

            var mapper = Mappings.Config.CreateMapper();
            return mapper.Map<List<ChatDto>, List<Chat>>(list);
        }

        public static async Task<bool> InsertChat(Chat chat)
        {
            const string url = BaseAddress + "/chat";
            var param = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("nome", chat.Name)
            });
            var req = new HttpClient();
            var resp = await req.PostAsync(url, param);
            return resp.StatusCode == HttpStatusCode.OK;
        }

        public static bool RenameChat(Chat chat)
        {
            var url = BaseAddress + "/chat/" + chat.Id;
            var param = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("nome", chat.Name)
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

        public static async Task<List<Message>> GetChatMessages(Chat chat)
        {
            var url = BaseAddress + "/chat/" + chat.Id + "/msg";
            var req = new HttpClient();
            var resp = await req.GetAsync(url);
            if (resp.StatusCode != HttpStatusCode.OK) return null;
            var content = await resp.Content.ReadAsStringAsync();
            if (content == null) return null;
            if (content.Length <= 2) return null;
            var list = JsonConvert.DeserializeObject<List<MessageDto>>(content);

            var mapper = Mappings.Config.CreateMapper();
            return mapper.Map<List<MessageDto>, List<Message>>(list);
        }

        public static bool InsertMessage(Message message)
        {
            var url = BaseAddress + "/chat/" + message.IdChat + "/msg";
            var param = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("mensagem", message.Msg),
                new KeyValuePair<string, string>("id_usuario", message.IdUser.ToString())
            });
            var req = new HttpClient();
            var resp = req.PostAsync(url, param).GetAwaiter().GetResult();
            return resp.StatusCode == HttpStatusCode.OK;
        }

        public static bool DeleteMessage(Message message)
        {
            var url = BaseAddress + "/chat/" + message.IdChat + "/delete/" + message.Id;
            var req = new HttpClient();
            var resp = req.DeleteAsync(url).GetAwaiter().GetResult();
            return resp.StatusCode == HttpStatusCode.OK;
        }
    }
}