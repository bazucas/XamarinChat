using AutoMapper;
using XamarinChat.Model;
using XamarinChat.Model.Dto;

namespace XamarinChat.Utils
{
    public static class Mappings
    {
        public static MapperConfiguration Config => Map();

        public static MapperConfiguration Map()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>().ForMember(dst => dst.Name, src => src.MapFrom(e => e.Nome));
                cfg.CreateMap<ChatDto, Chat>().ForMember(dst => dst.Name, src => src.MapFrom(e => e.Nome));
                cfg.CreateMap<MessageDto, Message>()
                    .ForMember(dst => dst.IdChat, src => src.MapFrom(e => e.Id_chat))
                    .ForMember(dst => dst.IdUser, src => src.MapFrom(e => e.Id_usuario))
                    .ForMember(dst => dst.Msg, src => src.MapFrom(e => e.Mensagem))
                    .ForMember(dst => dst.User, src => src.MapFrom(e => e.Usuario));
                cfg.CreateMap<User, UserDto>().ForMember(dst => dst.Nome, src => src.MapFrom(e => e.Name));
                cfg.CreateMap<Chat, ChatDto>().ForMember(dst => dst.Nome, src => src.MapFrom(e => e.Name));
                cfg.CreateMap<Message, MessageDto>()
                    .ForMember(dst => dst.Id_chat, src => src.MapFrom(e => e.IdChat))
                    .ForMember(dst => dst.Id_usuario, src => src.MapFrom(e => e.IdUser))
                    .ForMember(dst => dst.Mensagem, src => src.MapFrom(e => e.Msg))
                    .ForMember(dst => dst.Usuario, src => src.MapFrom(e => e.User));
            });
        }
    }
}