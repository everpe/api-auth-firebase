using AutoMapper;
using Firebase.Api.Vms;
using NetFirebase.Api.Models.Domain;

namespace Firebase.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
       CreateMap<Producto, ProductoVm>();
    }
}