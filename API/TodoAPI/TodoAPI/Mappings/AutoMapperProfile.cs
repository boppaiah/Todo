using AutoMapper;
using TodoAPI.DTOs;
using TodoAPI.Todos.CreateTodo;
using TodoAPI.Todos.DeleteTodo;
using TodoAPI.Todos.UpdateTodo;


namespace TodoAPI.Mappings
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateTodoRequest, CreateTodoCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => src.IsSelected))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));


            CreateMap<UpdateTodoRequest, UpdateTodoCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => src.IsSelected))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));


            CreateMap<DeleteTodoRequest, DeleteTodoCommand>()
            .ForMember(dest => dest.Id,
                 opt => opt.MapFrom(src => src.Id));
        }


    }
}
