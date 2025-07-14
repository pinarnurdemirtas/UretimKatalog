using AutoMapper;
using UretimKatalog.Application.Features.Auth.Requests.Commands;
using UretimKatalog.Application.Features.Auth.Result;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Application.Features.Auth.Mapping
{
    public partial class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterCommand, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, RegisterResult>();
            CreateMap<User, LoginResult>();
        }
    }
}
