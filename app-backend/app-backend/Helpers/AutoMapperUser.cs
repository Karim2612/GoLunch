using app_backend.Models;
using AutoMapper;

namespace app_backend.Helpers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegister, User>();
    }
}

