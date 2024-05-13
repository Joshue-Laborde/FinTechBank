using AutoMapper;
using FinTechBank.BusinessLogic.Extensions;
using FinTechBank.DataAccess.Models;
using FinTechBank.Domain.Dtos;
using FinTechBank.Domain.Enums;
using FinTechBank.Domain.RequestModel;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FinTechBank.BusinessLogic.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Usuario, UsuarioDto>().ForAllMembersIfNotEmpty();
            CreateMap<Usuario, UsuarioRequest>().ForAllMembersIfNotEmpty();

            CreateMap<Cliente, ClienteDto>()
                .ForMember(x => x.TipoCliente, opts => opts.MapFrom(x => (TipoClienteEnum)x.TipoCliente))
                .ForAllMembersIfNotEmpty();
            CreateMap<Cliente, ClienteRequest>()
                .ForMember(x => x.TipoCliente, opts => opts.MapFrom(x => (TipoClienteEnum)x.TipoCliente))
                .ForAllMembersIfNotEmpty();
            CreateMap<ClienteDto, ClienteRequest>().ForAllMembersIfNotEmpty();
        }
    }

    public static class MappingProfileExtensions
    {
        public static void ConfigureMappingProfile(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
