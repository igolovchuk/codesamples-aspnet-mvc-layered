using AutoMapper;
using LayeredWebDemo.BLL.DTO;
using LayeredWebDemo.DAL.Entities;
using LayeredWebDemo.BLL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredWebDemo.BLL.Config
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //----------------from-------------to----------//
            //frrom ApplicationUsr to UserDTO
            Mapper.CreateMap<ApplicationUser, UserDTO>();
           
            //from UserDTO to ApplicationUser
            Mapper.CreateMap<UserDTO, ApplicationUser>()
                .ForMember(dest => dest.UserName,
                           opts => opts.MapFrom(src => src.Email))
                           .IgnoreAllNonExisting();

            //from RegisterViewModel to ApplicationUser
            Mapper.CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName,
                           opts => opts.MapFrom(src => src.Email))
                         .ForMember(dest => dest.EmailConfirmed,
                           opts => opts.MapFrom(src => true))
                              .ForMember(dest => dest.DateOfBirth,
                           opts => opts.MapFrom(src => src.DateOfBitrh));

            //from ExternalLoginConfirmationViewModel to ApplicationUser
            Mapper.CreateMap<ExternalLoginConfirmationViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName,
                           opts => opts.MapFrom(src => src.Email))
                         .ForMember(dest => dest.EmailConfirmed,
                           opts => opts.MapFrom(src => true))
                              .ForMember(dest => dest.DateOfBirth,
                           opts => opts.MapFrom(src => DateTime.Now));


        }
    }
}
