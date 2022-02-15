using AutoMapper;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Entities;

namespace FRIO.MAR.APPLICATION.CORE.Extensions
{
    public static class AppExtensions
    {
        internal static LoginInternoAppResultDto MapToLoginInternoAppDto(this LoginInternoQueryDto obj)
        {
            //var configuration = new MapperConfiguration(cfg => cfg.CreateMap<LoginInternoQueryDto, LoginInternoAppResultDto>());

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LoginInternoQueryDto, LoginInternoAppResultDto>()
                .ForMember(dest => dest.Correo, m => m.MapFrom(src => src.CorreoElectronico))
                .ForMember(dest => dest.Usuario, m => m.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.Bloqueado, m => m.MapFrom(src => src.Bloqueado))
                .ForMember(dest => dest.Autorizado, m => m.MapFrom(src => src.Autorizado))
                .ForMember(dest => dest.Nombre, m => m.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.ForzarCambioClave, m => m.MapFrom(src => src.ForzarCambioClave));
            });

            var mapper = configuration.CreateMapper();
            return mapper.Map<LoginInternoAppResultDto>(obj);
        }


        internal static RolAppResultDto MapToRolesAppResultDto(this RolesQueryDto obj)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<RolesQueryDto, RolAppResultDto>());
            var mapper = configuration.CreateMapper();
            return mapper.Map<RolAppResultDto>(obj);
        }


    }
}
