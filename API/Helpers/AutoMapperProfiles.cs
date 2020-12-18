using API.DTOs;
using API.Entities;
using API.Entities.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<AppUser, MemberDto>()
            //    .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            //    .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            //CreateMap<Photo, PhotoDto>();
            //CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<LoginDto, AppUser>();

            CreateMap<AppUser, LoggedInUserDto>();
            CreateMap<AppUser, UserWithRoleDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(aur => aur.Role.Name)));

            CreateMap<Budget, BudgetDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserBudgets));
            CreateMap<CreateBudgetDto, Budget>();

            CreateMap<AppUserBudget, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
            CreateMap<UserDto, AppUserBudget>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id)); ;

            CreateMap<BudgetType, BudgetTypeDto>();
            CreateMap<CreateBudgetTypeDto, BudgetType>();

            CreateMap<ItemType, ItemTypeDto>();
            CreateMap<CreateItemTypeDto, ItemType>();
        }
    }
}
