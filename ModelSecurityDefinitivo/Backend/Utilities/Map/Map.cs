using AutoMapper;
using Entity.DTOs.Read;
using Entity.DTOs.Write;
using Entity.Model;

namespace Utilities.Map;

public class Map : Profile
{
    public Map()
    {

        //Map User
        CreateMap<User, UserDTO>()
        .ForMember(
            dest => dest.PersonName, 
            opt => opt.MapFrom(src => src.Person != null ? src.Person.Name: null) 
        );
        CreateMap<UserDTO, User>();
        CreateMap<User, UserWriteDTO>();
        CreateMap<UserWriteDTO, User>()
            .ForMember(dest => dest.Person, opt => opt.Ignore())
            .ForMember(dest => dest.RolUsers, opt => opt.Ignore());

        CreateMap<User, LoginDTO>();

        //Map Person
        CreateMap<Person, PersonDTO>();
        CreateMap<PersonDTO, Person>();

        //Map Form
        CreateMap<Form, FormDTO>();
        CreateMap<FormDTO, Form>();

        //Map Rol
        CreateMap<Rol, RolDTO>();
        CreateMap<RolDTO, Rol>();

        //Map Module
        CreateMap<Module, ModuleDTO>();
        CreateMap<ModuleDTO, Module>();

        //Map Permission
        CreateMap<Permission, PermissionDTO>();
        CreateMap<PermissionDTO, Permission>();

        //Map RolUser
        CreateMap<RolUser, RolUserDTO>()
        .ForMember(
            dest => dest.RolName,
            opt => opt.MapFrom(src => src.Rol != null ? src.Rol.Name: null) 
        )
        .ForMember(
            dest => dest.UserEmail,
            opt => opt.MapFrom(src => src.User != null ? src.User.Email: null) 
        );
        CreateMap<RolUserDTO, RolUser>();
        CreateMap<RolUser, RolUserWriteDTO>();
        CreateMap<RolUserWriteDTO, RolUser>();

        //Map FormModule
        CreateMap<FormModule, FormModuleDTO>()
        .ForMember(
            dest => dest.FormName,
            opt => opt.MapFrom(src => src.Form != null ? src.Form.Name: null) 
        )
        .ForMember(
            dest => dest.ModuleName,
            opt => opt.MapFrom(src => src.Module != null ? src.Module.Name: null) 
        );
        CreateMap<FormModuleDTO, FormModule>();
        CreateMap<FormModule, FormModuleWriteDTO>();
        CreateMap<FormModuleWriteDTO, FormModule>();

        //Map RolFormPermission
        CreateMap<RolFormPermission, RolFormPermissionDTO>()
        .ForMember(
            dest => dest.RolName,
            opt => opt.MapFrom(src => src.Rol != null ? src.Rol.Name: null) 
        )
        .ForMember(
            dest => dest.FormName,
            opt => opt.MapFrom(src => src.Form != null ? src.Form.Name: null) 
        )   
        .ForMember(
            dest => dest.PermissionName,
            opt => opt.MapFrom(src => src.Permission != null ? src.Permission.Name: null) 
        );
        CreateMap<RolFormPermissionDTO, RolFormPermission>();
        CreateMap<RolFormPermission, RolFormPermissionWriteDTO>();
        CreateMap<RolFormPermissionWriteDTO, RolFormPermission>();

    }
}
